using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Metra.ManutenzioneDatiAssemblaggio
{
    public class Lotto
    {
        public int ID;
        public DateTime TsStart;
        public DateTime TsStop;

        DbBridge DB;

        public DataSet DS;
        public DataTable Lotti;
        public DataTable Attrezzaggi;
        public DataTable Controlli;
        public DataTable ControlliAstine;
        public DataTable Fermi;
        public DataTable Operatori;
        public DataTable Ordini;
        public DataTable Produzioni;
        public DataTable Scarti;
        public DataTable TempiCalcolati;
        public DataTable Timeline;

        public string DiagMsg;
        public Boolean DiagOk;

        public string LotNumber
        {
            get
            {
                if (Lotti.Rows.Count > 0) return Lotti.Rows[0]["NumeroLotto"].ToString();
                else return string.Empty;
            }
        }

        public Lotto()
        {
            DS = new DataSet();
            DB = new DbBridge(DS);
            Lotti = new DataTable();
            Attrezzaggi = new DataTable();
            Controlli = new DataTable();
            ControlliAstine = new DataTable();
            Fermi = new DataTable();
            Operatori = new DataTable();
            Ordini = new DataTable();
            Produzioni = new DataTable();
            Scarti = new DataTable();
            TempiCalcolati = new DataTable();
            Timeline = new DataTable();
        }

        public bool Load(int id)
        {
            Boolean ok;

            ID = id;
            DB.Command.Parameters.AddWithValue("idLotto", id);
            DB.Command.Parameters.AddWithValue("idLottoControllo", 0);
            DB.Command.CommandText = "SELECT * FROM dbo.Lotti WHERE ID=@idLotto";
            DB.ReadDataTable(Lotti);
            ok = Lotti.Rows.Count > 0;
            if (ok)
            {
                if (Lotti.Rows[0]["TsInizio"] != DBNull.Value) TsStart = Convert.ToDateTime(Lotti.Rows[0]["TsInizio"]);
                if (Lotti.Rows[0]["TsFine"] != DBNull.Value) TsStop = Convert.ToDateTime(Lotti.Rows[0]["TsFine"]);

                //attrezzaggi
                DB.Command.CommandText = @"SELECT [ID]
                        ,[IDLotto]
                        ,[Inizio_Ts] 
                        ,[Fine_Ts] 
                        --,CAST( 1.0 * DATEDIFF(SECOND, Inizio_Ts, Fine_Ts) / 60.0 AS DECIMAL(9,2)) AS DurataMinuti
                        ,[Note] 
                    FROM dbo.LottiAttrezzaggi WHERE IDLotto=@idLotto
                    ORDER BY Inizio_Ts";
                DB.ReadDataTable(Attrezzaggi);
                //DataRelation drLottiAttrezzaggi = new DataRelation("LottiAttrezzaggi",
                //   DS.Tables[Lotti.TableName].Columns["ID"],
                //   DS.Tables[Attrezzaggi.TableName].Columns["IDLotto"]);

                //fermi
                DB.Command.CommandText = @"SELECT [ID]
                        ,[IDLotto]
                        ,[IDCausaleFermo]
                        ,[Tipo]
                        ,[Inizio_Ts] 
                        ,[Fine_Ts]
                        --,CAST( 1.0 * DATEDIFF(SECOND, Inizio_Ts, Fine_Ts) / 60.0 AS DECIMAL(9,2)) AS DurataMinuti
                        ,[Note] 
                    FROM dbo.LottiFermi WHERE IDLotto=@idLotto
                    ORDER BY Inizio_Ts";
                DB.ReadDataTable(Fermi);
                //DataRelation drLottiFermi = new DataRelation("LottiLottiFermi",
                //    DS.Tables[Lotti.TableName].Columns["ID"],
                //    DS.Tables[Fermi.TableName].Columns["IDLotto"]);

                //operatori
                DB.Command.CommandText = "SELECT * FROM dbo.LottiOperatori WHERE IDLotto=@idLotto ORDER BY Inizio_Ts, IDOperatore";
                DB.ReadDataTable(Operatori);
                //DataRelation drLottiOperatori = new DataRelation("LottiLottiOperatori",
                //    DS.Tables[Lotti.TableName].Columns["ID"],
                //    DS.Tables[Operatori.TableName].Columns["IDLotto"]);

                //ordini
                DB.Command.CommandText = "SELECT * FROM dbo.LottiOrdini WHERE IDLotto=@idLotto ORDER BY OrdineProduzione";
                DB.ReadDataTable(Ordini);
                //DataRelation drLottiOrdini = new DataRelation("LottiLottiOrdini",
                //    DS.Tables[Lotti.TableName].Columns["ID"],
                //    DS.Tables[Ordini.TableName].Columns["IDLotto"]);

                //produzioni
                DB.Command.CommandText = @"SELECT [ID]
                                                ,[IDLotto]
                                                ,[Inizio_Ts] AS Inizio
                                                ,[Fine_Ts] AS Fine
                                                ,CAST( 1.0 * DATEDIFF(SECOND, Inizio_Ts, Fine_Ts) / 60.0 AS DECIMAL(9,2)) AS DurataMinuti
                                                ,[Qta]
                                                ,[NumeroPassate]
                                                ,[Note] 
                                                ,[ProdRidotta]
                                                ,[ProdRidotta_IDCausale]    
                                            FROM dbo.LottiProduzioni WHERE IDLotto=@idLotto ORDER BY Inizio_Ts";
                DB.ReadDataTable(Produzioni);
                //DataRelation drLottiProduzioni = new DataRelation("LottiLottiProduzioni",
                //    DS.Tables[Lotti.TableName].Columns["ID"],
                //    DS.Tables[Produzioni.TableName].Columns["IDLotto"]);

                //scarti
                DB.Command.CommandText = "SELECT * FROM dbo.LottiScarti WHERE IDLotto=@idLotto ORDER BY Ts";
                DB.ReadDataTable(Scarti);
                //DataRelation drLottiScarti = new DataRelation("LottiLottiScarti",
                //    DS.Tables[Lotti.TableName].Columns["ID"],
                //    DS.Tables[Scarti.TableName].Columns["IDLotto"]);

                //timeline
                DB.Command.CommandText = @"SELECT 
                        VLTL.Tipo ,
                        VLTL.DataOraInizio AS Inizio,
                        VLTL.DataOraFine AS Fine,
                        VLTL.Descrizione ,
                        VLTL.Qta ,
                        --VLTL.DurataSecondi ,
                        --VLTL.NumeroAttrezzaggi,
                        --VLTL.NumeroProduzioni ,
                        --VLTL.NumeroFermi ,
                        --VLTL.DurataEffettivaAttrezzaggioSecondi ,
                        --VLTL.DurataEffettivaProduzioneSecondi ,
                        VLTL.DurataMinuti AS Minuti ,
                        VLTL.DurataEffettivaAttrezzaggioMinuti AS [Minuti effettivi attrezzaggio],
                        VLTL.DurataEffettivaProduzioneMinuti AS [Minuti effettivi produzione]
                     FROM 
                        dbo.vwLottiTimeLine VLTL
                     WHERE 
                        VLTL.IDLotto=@idLotto 
                     ORDER BY 
                        VLTL.DataOraInizio,
                        VLTL.DurataSecondi";
                DB.ReadDataTable(Timeline);

                //tempi calcolati
                DB.Command.CommandText = @"WITH LTL AS (
                    SELECT 
	                    LTL.IDLotto,
	                    SUM(CASE WHEN LTL.IDTipo='A' AND LTL.DurataMinuti>0 THEN LTL.DurataMinuti ELSE 0 END) AS MinutiAttrezzaggio,
	                    SUM(CASE WHEN LTL.IDTipo='A' THEN LTL.DurataMinuti ELSE 0 END) AS MinutiAttrezzaggio_Calendario,
	                    SUM(CASE WHEN LTL.IDTipo='P' AND LTL.DurataMinuti>0 THEN LTL.DurataMinuti ELSE 0 END) AS MinutiProduzione,
	                    SUM(CASE WHEN LTL.IDTipo='P' THEN LTL.DurataMinuti ELSE 0 END) AS MinutiProduzione_Calendario,
	                    SUM(CASE WHEN LTL.IDTipo='F' AND LTL.DurataMinuti>0 THEN LTL.DurataMinuti ELSE 0 END) AS MinutiFermo,
	                    SUM(CASE WHEN LTL.IDTipo='F' THEN LTL.DurataMinuti ELSE 0 END) AS MinutiFermo_Calendario
                    FROM 
	                    dbo.vwLottiTimeLine LTL
                    GROUP BY
	                    LTL.IDLotto),
                    LO AS (SELECT LO.IDLotto, SUM(LO.Qta) AS QtaOrdine FROM dbo.LottiOrdini LO GROUP BY LO.IDLotto),
                    LP AS (SELECT LP.IDLotto, SUM(LP.Qta) AS QtaProdotta FROM dbo.LottiProduzioni LP GROUP BY LP.IDLotto)

                    SELECT
	                    LTL.IDLotto ,
                        LTL.MinutiAttrezzaggio ,
                        LTL.MinutiAttrezzaggio_Calendario ,
                        LTL.MinutiProduzione ,
                        LTL.MinutiProduzione_Calendario ,
                        LTL.MinutiFermo ,
                        LTL.MinutiFermo_Calendario,
	                    COALESCE(LTL.MinutiAttrezzaggio, 0)
	                    + COALESCE(LTL.MinutiProduzione, 0)
	                    + COALESCE(LTL.MinutiFermo, 0) AS MinutiDichiarazioni,
	                    COALESCE(LTL.MinutiAttrezzaggio_Calendario, 0)
	                    + COALESCE(LTL.MinutiProduzione_Calendario, 0)
	                    + COALESCE(LTL.MinutiFermo_Calendario, 0) AS MinutiDichiarazioni_Calendario,
	                    COALESCE(LO.QtaOrdine, 0) AS QtaOrdine,
	                    COALESCE(LP.QtaProdotta, 0) AS QtaProdotta	 
                    FROM LTL
	                    INNER JOIN LO ON LO.IDLotto = LTL.IDLotto
	                    INNER JOIN LP ON LP.IDLotto = LTL.IDLotto
                    WHERE
                        LTL.IDLotto = @IDLotto";
                DB.ReadDataTable(TempiCalcolati);
                //DataRelation drLottiTempiCalcolati = new DataRelation("LottiLottiTempiCalcolati",
                //    DS.Tables[Lotti.TableName].Columns["ID"],
                //    DS.Tables[TempiCalcolati.TableName].Columns["IDLotto"]);

                //controlli / astine
                DB.Command.CommandText = @"SELECT * FROM dbo.LottiControlli WHERE IDLotto=@idLotto ORDER BY NumeroBarra, Ts";
                DB.ReadDataTable(Controlli);
                Controlli.Columns["ID"].AutoIncrement = true;
                Controlli.Columns["ID"].AutoIncrementSeed = 1;
                Controlli.Columns["ID"].AutoIncrementStep = 1;
                //DataRelation drLotti = new DataRelation("Lotti",
                //   DS.Tables[Lotti.TableName].Columns["ID"],
                //   DS.Tables[Controlli.TableName].Columns["IDLotto"]);

                DB.Command.CommandText = @"SELECT * FROM dbo.LottiControlliAstine WHERE IDLottoControllo=-1";
                DB.ReadDataTable(ControlliAstine);
                foreach (DataRow c in Controlli.Rows)
                {
                    string s = @"SELECT * FROM dbo.LottiControlliAstine WHERE IDLottoControllo=@idLottoControllo";
                    DB.Command.Parameters.Clear();
                    DB.Command.Parameters.AddWithValue("idLottoControllo", c["ID"]);
                    DB.ReadDataTable(ControlliAstine, s, true, true);
                }
                DataRelation drControlliControlliAstine = new DataRelation("LottiControlli_LottiControlliAstine",
                        DS.Tables[Controlli.TableName].Columns["ID"],
                        DS.Tables[ControlliAstine.TableName].Columns["IDLottoControllo"]);
                DS.Relations.Add(drControlliControlliAstine);

                //Controlli.RowDeleting += Controlli_RowDeleting;
            }

            return ok;
        }

        //private void Controlli_RowDeleting(object sender, DataRowChangeEventArgs e)
        //{
        //    foreach (DataRow r in ControlliAstine.Select("IDLottoControllo=" + e.Row["ID"].ToString()))
        //    {
        //        r.Delete();
        //    }
        //}

        public DataTable Astine
        {
            get
            {
                DB.Command.Parameters.Clear();
                DB.Command.Parameters.AddWithValue("idLotto", ID);
                DB.Command.Parameters.AddWithValue("dataAreaId", Properties.Settings.Default.DataAreaID);
                DB.Command.CommandText = @"SELECT DISTINCT 
	                           COALESCE(IT.NPOPBAREFITEMID, PB.ITEMID) AS ItemId 
                        FROM   dbo.LottiOrdini LO
                               INNER JOIN [AXSQL\AX2009].AX2009_METRA_LIVE.dbo.PRODBOM PB ON PB.DATAAREAID = @dataAreaId
                                                                                             AND PB.PRODID = LO.OrdineProduzione
                               INNER JOIN [AXSQL\AX2009].AX2009_METRA_LIVE.dbo.INVENTTABLE IT ON IT.DATAAREAID = PB.DATAAREAID
                                                                                                 AND IT.ITEMID = PB.ITEMID
                                                                                                 AND IT.ITEMTYPE = 0
                        WHERE  LO.IDLotto = @idLotto;";
                DataTable dt = new DataTable();
                DB.ReadDataTable(dt);
                return dt;
            }
        }

        private Boolean CheckTs(DataTable dt, ref string msg)
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr.RowState != DataRowState.Deleted)
                {
                    if (dt.Columns.Contains("Inizio") && dr["Inizio"] == DBNull.Value &&
                        dt.Columns.Contains("Inizio_TsForzato") && dr["Inizio_TsForzato"] == DBNull.Value &&
                        dt.Columns.Contains("Inizio_TsOriginale") && dr["Inizio_TsOriginale"] == DBNull.Value &&
                        dt.Columns.Contains("Fine") && dr["Fine"] == DBNull.Value &&
                        dt.Columns.Contains("Fine_TsForzato") && dr["Fine_TsForzato"] == DBNull.Value &&
                        dt.Columns.Contains("Fine_TsOriginale") && dr["Fine_TsOriginale"] == DBNull.Value)
                    {
                        dr.Delete();
                    }
                    else
                    {
                        if (dt.Columns.Contains("Ts") && dr["Ts"] == DBNull.Value) msg += String.Format("\n{0}: compilare Ts", dt.TableName);
                        if (dt.Columns.Contains("Inizio") && dr["Inizio"] == DBNull.Value) msg += String.Format("\n{0}: compilare Inizio", dt.TableName);
                        if (dt.Columns.Contains("Fine") && dr["Fine"] == DBNull.Value) msg += String.Format("\n{0}: compilare Fine", dt.TableName);
                        if (dt.Columns.Contains("Inizio_TsForzato") && dr["Inizio_TsForzato"] == DBNull.Value) dr["Inizio_TsForzato"] = 0;
                        if (dt.Columns.Contains("Inizio_TsOriginale") && dr["Inizio_TsOriginale"] == DBNull.Value) dr["Inizio_TsOriginale"] = dr["Inizio_Ts"];
                        if (dt.Columns.Contains("Fine_TsForzato") && dr["Fine_TsForzato"] == DBNull.Value) dr["Fine_TsForzato"] = 0;
                        if (dt.Columns.Contains("Fine_TsOriginale") && dr["Fine_TsOriginale"] == DBNull.Value) dr["Fine_TsOriginale"] = dr["Fine_Ts"];
                        if (dt.Columns.Contains("Note") && dr["Note"] == DBNull.Value) dr["Note"] = "";
                    }
                }
            }
            return msg.Length == 0;
        }

        public Result Save()
        {
            Result res = new Result();

            res = DB.UpdateDataTable(Lotti);
            res.Ok &= CheckTs(Attrezzaggi, ref res.Descrizione);
            res.Ok &= CheckTs(Controlli, ref res.Descrizione);
            res.Ok &= CheckTs(Fermi, ref res.Descrizione);
            res.Ok &= CheckTs(Produzioni, ref res.Descrizione);
            if (res.Ok)
            {
                res.Ok &= DB.UpdateDataTable(Attrezzaggi).Ok;
                res.Ok &= DB.UpdateDataTable(Fermi).Ok;
                res.Ok &= DB.UpdateDataTable(Operatori).Ok;
                res.Ok &= DB.UpdateDataTable(Ordini).Ok;
                res.Ok &= DB.UpdateDataTable(Produzioni).Ok;
                res.Ok &= DB.UpdateDataTable(Scarti).Ok;
                res.Ok &= DB.UpdateDataTable(Controlli).Ok;
                DB.ShowErrors = false;
                DB.UpdateDataTable(ControlliAstine);
                DB.ShowErrors = true;
            }
            return res;
        }

        private void AddMsg(Dictionary<int, string> dict, int key, string msg)
        {
            if (!dict.ContainsKey(key))
            {
                dict.Add(key, "");
            }
            dict[key] += msg;
        }

        private bool IsBicolore()
        {
            foreach (DataRow dr in Ordini.Rows)
            {
                if (dr["Finitura"].ToString().Contains("+")) return true;
            }
            return false;
        }

        public bool Diagnostica(Boolean showMessage)
        {
            DbBridge db = new DbBridge();
            DiagMsg = "";
            string s;

            //stato
            if (TsStop > DateTime.MinValue && Lotti.Rows[0]["IDCausaleChiusura"] == DBNull.Value || Lotti.Rows[0]["IDCausaleChiusura"].ToString() == "0")
            {
                DiagMsg += "\nCausale chiusura mancante";
            }

            //tsfine
            if (TsStop == DateTime.MinValue)
            {
                DiagMsg += "\nLotto non chiuso";
            }

            //controllo calendario di lavoro

            //controllo date in base a dichiarazioni
            s = @"SELECT IDLotto,MIN(Inizio) AS Inizio,MAX(Fine) AS Fine
                FROM(
	                SELECT IDLotto,MIN(Inizio_Ts) AS Inizio,MAX(Fine_Ts) AS Fine 
	                FROM LottiAttrezzaggi WHERE IDLotto=@idLotto
	                GROUP BY IDLotto
	                UNION ALL
	                SELECT IDLotto,MIN(Inizio_Ts) AS Inizio,MAX(Fine_Ts) AS Fine 
	                FROM LottiFermi WHERE IDLotto=@idLotto
	                GROUP BY IDLotto
	                UNION ALL
	                SELECT IDLotto,MIN(Inizio_Ts) AS Inizio,MAX(Fine_Ts) AS Fine 
	                FROM LottiProduzioni WHERE IDLotto=@idLotto
	                GROUP BY IDLotto) L
                GROUP BY IDLotto";
            db.Command.Parameters.AddWithValue("idLotto", this.ID);
            DataTable dt = new DataTable();
            db.Command.CommandText = s;
            db.ReadDataTable(dt);
            {
                DateTime tsPrimaDichiarazione = DateTime.MinValue;
                if (dt.Rows.Count > 0)
                {
                    DateTime tsUltimaDichiarazione = DateTime.MinValue;
                    if (dt.Rows[0]["Inizio"] != DBNull.Value) tsPrimaDichiarazione = Convert.ToDateTime(dt.Rows[0]["Inizio"]);
                    if (dt.Rows[0]["Fine"] != DBNull.Value) tsUltimaDichiarazione = Convert.ToDateTime(dt.Rows[0]["Fine"]);
                    if (tsPrimaDichiarazione > DateTime.MinValue && tsPrimaDichiarazione < TsStart)
                    {
                        DiagMsg += string.Format("\nInizio lotto ({0}) successivo a inizio prima dichiarazione ({1})", TsStart, tsPrimaDichiarazione);
                    }
                    if (TsStop > DateTime.MinValue && tsUltimaDichiarazione > DateTime.MinValue && TsStop > DateTime.MinValue && tsUltimaDichiarazione > TsStop)
                    {
                        DiagMsg += string.Format("\nFine lotto ({0}) precedente a fine ultima dichiarazione ({1})", TsStop, tsUltimaDichiarazione);
                    }
                }
            }

            //controllo sovrapposizione dichiarazioni
            s = @"WITH 
                Dichiarazioni AS(
	                SELECT 'A'+CONVERT(varchar(10),LD.ID) AS PK, 'A' AS Tipo, LD.ID, L.IDPostazione, LD.IDLotto, L.NumeroLotto, LD.Inizio_Ts, LD.Fine_Ts FROM LottiAttrezzaggi LD INNER JOIN Lotti L ON L.ID=LD.IDLotto
	                UNION ALL 
	                SELECT 'P'+CONVERT(varchar(10),LD.ID) AS PK, 'P' AS Tipo, LD.ID, L.IDPostazione, LD.IDLotto, L.NumeroLotto, LD.Inizio_Ts, LD.Fine_Ts FROM LottiProduzioni LD INNER JOIN Lotti L ON L.ID=LD.IDLotto
                )
                SELECT  
	                TAB1.IDPostazione,
	                TAB1.NumeroLotto,
	                TAB1.Tipo,
	                TAB1.Inizio_Ts,
	                TAB1.Fine_Ts,
	                TAB2.IDPostazione   AS IDPostazione2,
	                TAB2.NumeroLotto    AS NumeroLotto2,
	                TAB2.Tipo		    AS Tipo2,
	                TAB2.Inizio_Ts		AS Inizio_Ts2,
	                TAB2.Fine_Ts		AS Fine_Ts2
                FROM 
	                Dichiarazioni TAB1
	                INNER JOIN Dichiarazioni TAB2 ON 
		                TAB2.PK!=TAB1.PK AND 
		                TAB1.IDPostazione=TAB2.IDPostazione AND 
		                TAB2.Inizio_Ts<TAB1.Fine_Ts AND 
		                TAB2.Fine_Ts>TAB1.Inizio_Ts
                WHERE 
	                TAB1.IDPostazione='TEST3'
                    AND TAB1.IDLotto=@idLotto";
            db.Command.CommandText = s;
            db.Command.Parameters.Clear();
            db.Command.Parameters.AddWithValue("idLotto", ID);
            db.Command.Parameters.AddWithValue("idPostazione", Lotti.Rows[0]["IDPostazione"]);
            dt = new DataTable();
            db.ReadDataTable(dt);
            if (dt.Rows.Count > 0)
            {
                DiagMsg += string.Format("\nInizio e fine dichiarazione sovrapposti:");
                foreach (DataRow dr in dt.Rows)
                {
                    DiagMsg += string.Format("\n    {0} {1} - {2}\t{3}: {4} {5} - {6}",
                            dr["Tipo"],
                            ((DateTime)dr["Inizio_Ts"]).ToString("dd/MM/yy HH:mm:ss"),
                            ((DateTime)dr["Fine_Ts"]).ToString("dd/MM/yy HH:mm:ss"),
                            dr["NumeroLotto2"],
                            dr["Tipo2"],
                            ((DateTime)dr["Inizio_Ts2"]).ToString("dd/MM/yy HH:mm:ss"),
                            ((DateTime)dr["Fine_Ts2"]).ToString("dd/MM/yy HH:mm:ss"));
                }
            }

            //controllo coerenza numero passate
            s = @"SELECT NumeroPassate,
                    COUNT(*)
                FROM dbo.LottiProduzioni
                WHERE IDLotto = @IDLotto
                    AND NumeroPassate>0
                GROUP BY NumeroPassate;";
            db.Command.CommandText = s;
            db.Command.Parameters.Clear();
            db.Command.Parameters.AddWithValue("IDLotto", ID);
            dt = new DataTable();
            db.ReadDataTable(dt);
            if (dt.Rows.Count > 1)
            {
                DiagMsg += string.Format("\nNumero di passate non coerente:");
                foreach (DataRow r in dt.Rows)
                {
                    DiagMsg += string.Format("\n    {0} passate: {1} dichiarazioni", r[0], r[1]);
                }
            }

            //controllo che numero di passate prima della dichiarazione coincida con numero di attrezzaggi
            s = @"WITH Dichiarazioni AS(
	                SELECT IDLotto,'A' AS TipoFase, Inizio_Ts, NULL AS NumeroPassate
	                FROM dbo.LottiAttrezzaggi WHERE IDLotto=@IDLotto
	                UNION ALL	
	                SELECT IDLotto,CASE WHEN NumeroPassate>0 THEN 'P' ELSE '0' END AS TipoFase, Inizio_Ts, CASE WHEN NumeroPassate>0 THEN NumeroPassate ELSE NULL END AS NumeroPassate
	                FROM dbo.LottiProduzioni WHERE IDLotto=@IDLotto
                )
                ,DichiarazioniOrdinate AS (SELECT D.*, 
	                ROW_NUMBER() OVER (PARTITION BY D.IDLotto ORDER BY D.Inizio_Ts) AS RN
	                FROM Dichiarazioni D)

                SELECT DO.*
                FROM DichiarazioniOrdinate DO
                    LEFT JOIN DichiarazioniOrdinate DO2
                        ON DO2.IDLotto = DO.IDLotto
                           AND DO2.RN = DO.RN - 1";
            db.Command.CommandText = s;
            db.Command.Parameters.Clear();
            db.Command.Parameters.AddWithValue("IDLotto", ID);
            dt = new DataTable();
            db.ReadDataTable(dt);
            int attrezzaggi = 0;
            int passate = 0;
            string tipoFasePrec = "";
            s = "";
            foreach (DataRow r in dt.Rows)
            {
                if (r["TipoFase"].ToString() == "A" && tipoFasePrec == "P") attrezzaggi = 0;
                switch (r["TipoFase"].ToString())
                {
                    case "A":
                        attrezzaggi++;
                        break;
                    case "P":
                        passate = (int)r["NumeroPassate"];
                        if (passate != attrezzaggi)
                        {
                            s += string.Format("\n    Prod. {0}: attr = {1}, passate = {2}", r["Inizio_Ts"], attrezzaggi, passate);
                        }
                        break;
                }
                tipoFasePrec = r["TipoFase"].ToString();
            }
            if (s.Length > 0) DiagMsg += "\nNumero passate non coincide con attrezzaggi:" + s;

            //controlli: barra 1
            Dictionary<int, string> BarMsg = new Dictionary<int, string>();
            if (Controlli.Select("NumeroBarra=1").Length == 0)
            {
                AddMsg(BarMsg, 1, "\nBarra 1: mancanza controllo");
            }
            else
            {
                if (Controlli.Select("NumeroBarra=1 AND ProvaT_Grezzo>0").Length == 0)
                {
                    AddMsg(BarMsg, 1, "\nBarra 1: mancanza controllo grezzo");
                }
                if (IsBicolore() == false &&
                    Controlli.Select("NumeroBarra=1 AND ProvaT_DopoForno>0").Length == 0)
                {
                    AddMsg(BarMsg, 1, "\nBarra 1: mancanza controllo dopo forno");
                }
            }

            //controlli: barra 100, 300..
            var v = Produzioni.Compute("SUM(Qta)", "");
            int barre = 0;
            if (v != DBNull.Value) barre = Convert.ToInt16(v);
            for (int barra = 100; barra < barre; barra += 200)
            {
                if (Controlli.Select(string.Format("NumeroBarra={0}", barra)).Length == 0)
                {
                    AddMsg(BarMsg, barra, string.Format("\nBarra {0}: mancanza controllo", barra));
                }
                else if (Controlli.Select(string.Format("NumeroBarra={0} AND ProvaT_Grezzo>0", barra)).Length == 0)
                {
                    AddMsg(BarMsg, barra, string.Format("\nBarra {0}: mancanza controllo grezzo", barra));
                }
            }

            //controlli: barra 200, 400..
            for (int barra = 200; barra < barre; barra += 200)
            {
                if (Controlli.Select(string.Format("NumeroBarra={0}", barra)).Length == 0)
                {
                    AddMsg(BarMsg, barra, string.Format("\nBarra {0}: mancanza controllo", barra));
                }
                else
                {
                    //controllo grezzo
                    if (Controlli.Select(string.Format("NumeroBarra={0} AND ProvaT_Grezzo>0", barra)).Length == 0)
                    {
                        AddMsg(BarMsg, barra, string.Format("\nBarra {0}: mancanza controllo grezzo", barra));
                    }

                    //controllo dopo forno se non è un bicolore
                    if (IsBicolore() == false &&
                        Controlli.Select(string.Format("NumeroBarra={0} AND ProvaT_DopoForno>0", barra)).Length == 0)
                    {
                        AddMsg(BarMsg, barra, string.Format("\nBarra {0}: mancanza controllo dopo forno", barra));
                    }
                }
            }

            //controllo di tenuta
            foreach (DataRow r in Controlli.Select("NumeroBarra>0 AND ProvaT_DopoForno>0 AND ProvaT_DopoForno<24"))
            {
                AddMsg(BarMsg, Convert.ToInt16(r["NumeroBarra"]), string.Format("\nBarra {0}: tenuta dopo forno {1} N/mm", r["NumeroBarra"], r["ProvaT_DopoForno"]));
            }

            //astine
            foreach (DataRow rc in Controlli.Rows)
            {
                if (ControlliAstine.Select(string.Format("IDLottoControllo='{0}'", rc["ID"])).Length == 0)
                {
                    AddMsg(BarMsg, Convert.ToInt16(rc["NumeroBarra"]), string.Format("\nBarra {0}: astine mancanti", rc["NumeroBarra"]));
                }
                else
                {
                    s = string.Format("IDLottoControllo='{0}' AND (LottoAstina IS NULL OR LottoAstina='')", rc["ID"]);
                    foreach (DataRow rca in ControlliAstine.Select(s))
                    {
                        AddMsg(BarMsg, Convert.ToInt16(rc["NumeroBarra"]), string.Format("\nBarra {0}: lotto mancante per astina {1}", rc["NumeroBarra"], rca["CodiceAstina"]));
                    }
                }
            }

            //riepilogo messaggi per controllo
            var list = BarMsg.Keys.ToList();
            list.Sort();
            foreach (int key in list)
            {
                DiagMsg += BarMsg[key];
            }

            //finito
            DiagOk = DiagMsg.Length == 0;
            DiagMsg = String.Format("Lotto {0}: {1}", Lotti.Rows[0]["NumeroLotto"], DiagOk ? "OK" : "ERR") + (DiagMsg.Length > 0 ? ("\n" + DiagMsg) : "");
            DiagMsg = DiagMsg.Replace("\n", "\r\n");

            Lotti.Rows[0]["Elab_Diagnostica"] = DiagOk ? "OK" : "ERR";
            Lotti.Rows[0]["Elab_DiagnosticaTs"] = DateTime.Now;
            Lotti.Rows[0]["Elab_DiagnosticaMsg"] = DiagMsg.Length < 1000 ? DiagMsg : DiagMsg.Substring(0, 1000);

            //messaggio
            if (showMessage)
            {
                if (DiagOk)
                {
                    System.Windows.Forms.MessageBox.Show("Nessun erorre riscontrato", "Diagnostica", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show(DiagMsg, "Diagnostica", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }
            }

            return DiagOk;
        }
    }
}
