/* -----------------------------------------------------------------------------------------------------------------------------------

		Riconciliazione orari fermi

		I fermi macchina causalizzati non devono esistere fuori dagli orari di calendario
		Se il calendario viene modificato ex-post è necessario verificare i fermi

		- Riduzione orario: esistono fermi "fuori calendario" completamente o parzialmente. Eliminare o aggiornare/splittare
			- Analizzo e splitto i fermi per isolare le porzioni non a calendario
			- Elimino le porzioni non a calendario
		- Aumento orario: ci saranno slot per cui non esistono nè fermi nè dichiarazioni. Creare fermi, unire con eventuali fermi adiacenti 
			- Calcolo impegno come unione di fermi e dichiarazioni 
			- Creo fermi per gli impegni fuori calendario
		- Unione fermi adiacenti

   ----------------------------------------------------------------------------------------------------------------------------------- */

/*
DELETE FROM dbo.__Cal
INSERT INTO dbo.__Cal VALUES (1001, 'ASM1', '2017-09-10', '2017-09-10 08:00:00', '2017-09-10 16:30:00')
INSERT INTO dbo.__Cal VALUES (1002, 'ASM1', '2017-09-11', '2017-09-11 08:00:00', '2017-09-11 16:30:00')
INSERT INTO dbo.__Cal VALUES (1003, 'ASM1', '2017-09-12', '2017-09-12 08:00:00', '2017-09-12 16:30:00')
INSERT INTO dbo.__Cal VALUES (1004, 'ASM1', '2017-09-13', '2017-09-13 08:00:00', '2017-09-13 16:30:00')
INSERT INTO dbo.__Cal VALUES (1005, 'ASM1', '2017-09-14', '2017-09-14 08:00:00', '2017-09-14 16:30:00')
INSERT INTO dbo.__Cal VALUES (1006, 'ASM1', '2017-09-15', '2017-09-15 08:00:00', '2017-09-15 16:30:00')
INSERT INTO dbo.__Cal VALUES (1007, 'ASM1', '2017-09-16', '2017-09-16 08:00:00', '2017-09-16 16:30:00')

DELETE FROM dbo.__LottiFermi
INSERT INTO dbo.__LottiFermi VALUES (1, -1, 'ASM1', -1, '2017-09-14 06:00:00', '2017-09-14 07:00:00', NULL, 'A')
INSERT INTO dbo.__LottiFermi VALUES (2, -1, 'ASM1', -1, '2017-09-14 06:00:00', '2017-09-14 09:00:00', NULL, 'A')
INSERT INTO dbo.__LottiFermi VALUES (3, -1, 'ASM1', -1, '2017-09-14 09:30:00', '2017-09-14 12:00:00', NULL, 'A')
INSERT INTO dbo.__LottiFermi VALUES (4, -1, 'ASM1', -1, '2017-09-14 12:00:00', '2017-09-14 18:00:00', NULL, 'A')
INSERT INTO dbo.__LottiFermi VALUES (5, -1, 'ASM1', -1, '2017-09-14 19:00:00', '2017-09-15 07:00:00', NULL, 'A')
INSERT INTO dbo.__LottiFermi VALUES (6, -1, 'ASM1', -1, '2017-09-14 12:00:00', '2017-09-15 10:00:00', NULL, 'A')
INSERT INTO dbo.__LottiFermi VALUES (7, -1, 'ASM1', -1, '2017-09-14 22:00:00', '2017-09-15 10:00:00', NULL, 'A')
INSERT INTO dbo.__LottiFermi VALUES (8, -1, 'ASM1', -1, '2017-09-15 10:00:00', '2017-09-15 14:00:00', NULL, 'A')
INSERT INTO dbo.__LottiFermi VALUES (9, -1, 'ASM1', -1, '2017-09-14 12:00:00', '2017-09-16 12:00:00', NULL, 'A')

SELECT * FROM dbo.__Cal
SELECT * FROM dbo.__LottiFermi
*/

WITH 
Split AS (SELECT 2 AS RTot, 1 AS RNum UNION ALL SELECT 2,2 UNION ALL SELECT 1,1)
,Fermi AS (
SELECT LF.*, 
	CI.ID AS CI_ID,
	CI.TsStart AS CI_TsStart, 
	CI.TsStop  AS CI_TsStop,
	CF.ID AS CF_ID,
	CF.TsStart AS CF_TsStart, 
	CF.TsStop  AS CF_TsStop,
	CASE WHEN CI.ID IS NULL AND CF.ID IS NULL THEN 'DELETE'
		 WHEN CI.ID IS NULL AND CF.ID IS NOT NULL THEN 'LF.Inizio_Ts = CF_TsStart'
		 WHEN CI.ID IS NOT NULL AND CF.ID IS NULL THEN 'LF.Fine_Ts = CI_TsStop'
 		 WHEN CI.ID IS NOT NULL AND CF.ID IS NOT NULL THEN 'SPLIT' END AS Operazione

FROM   dbo.__LottiFermi LF
	LEFT JOIN __Cal C ON C.IDPostazione = LF.IDPostazione
					AND LF.Inizio_Ts BETWEEN C.TsStart AND C.TsStop
					AND LF.Fine_Ts   BETWEEN C.TsStart AND C.TsStop
	LEFT JOIN dbo.__Cal CI ON CI.IDPostazione = LF.IDPostazione
					AND LF.Inizio_Ts BETWEEN CI.TsStart AND CI.TsStop
	LEFT JOIN dbo.__Cal CF ON CF.IDPostazione = LF.IDPostazione
					AND LF.Fine_Ts BETWEEN CF.TsStart AND CF.TsStop
WHERE  C.IDPostazione IS NULL)
SELECT
	  
	CASE WHEN S.RNum=1 THEN F.ID ELSE -1 END AS ID,
	F.IDLotto,
	F.IDCausaleFermo,
	CASE
		WHEN F.CI_ID IS NULL AND F.CF_ID IS NULL THEN NULL
		WHEN F.CI_ID IS NULL AND F.CF_ID IS NOT NULL THEN F.CF_TsStart
		WHEN F.CI_ID IS NOT NULL AND F.CF_ID IS NULL THEN F.Inizio_Ts
		WHEN F.CI_ID IS NOT NULL AND F.CF_ID IS NOT NULL THEN 
			CASE WHEN S.RNum=1 THEN F.Inizio_Ts ELSE F.CF_TsStart END
	END AS Inizio_Ts,
	CASE
		WHEN F.CI_ID IS NULL AND F.CF_ID IS NULL THEN NULL
		WHEN F.CI_ID IS NULL AND F.CF_ID IS NOT NULL THEN F.Fine_Ts
		WHEN F.CI_ID IS NOT NULL AND F.CF_ID IS NULL THEN F.CI_TsStop
		WHEN F.CI_ID IS NOT NULL AND F.CF_ID IS NOT NULL THEN 
			CASE WHEN S.RNum=1 THEN F.CI_TsStop ELSE F.Fine_Ts END
	END AS Fine_Ts,
	F.Note, 
	F.Tipo,
	*
FROM Fermi F
INNER JOIN Split S ON S.RTot = CASE WHEN F.CI_ID IS NOT NULL AND F.CF_ID IS NOT NULL THEN 2 ELSE 1 END 
