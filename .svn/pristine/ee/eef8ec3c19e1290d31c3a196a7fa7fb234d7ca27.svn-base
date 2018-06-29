using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;

namespace Metra.ManutenzioneDatiAssemblaggio
{
    public class Lotto
    {
        public int ID;
        public DateTime TsStart;
        public DateTime TsStop;

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
        public string DiagMsg;
        public Boolean DiagOk;

        DbBridge db;

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
            db = new DbBridge();
        }

        public bool Load(int id)
        {
            Boolean ok;

            ID = id;
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

            db.Command.Parameters.AddWithValue("idLotto", id);
            db.Command.Parameters.AddWithValue("idLottoControllo", 0);
            db.ReadDataTable(Lotti, "SELECT * FROM dbo.Lotti WHERE ID=@idLotto");
            ok = Lotti.Rows.Count > 0;
            if (ok)
            {
                if (Lotti.Rows[0]["TsInizio"] != DBNull.Value) TsStart = Convert.ToDateTime(Lotti.Rows[0]["TsInizio"]);
                if (Lotti.Rows[0]["TsFine"] != DBNull.Value) TsStop = Convert.ToDateTime(Lotti.Rows[0]["TsFine"]);

                //attrezzaggi
                db.ReadDataTable(Attrezzaggi, @"SELECT [ID]
                                                    ,[IDLotto]
                                                    ,[Inizio_Ts] AS Inizio
                                                    ,[Fine_Ts] AS Fine
                                                    ,CAST( 1.0 * DATEDIFF(SECOND, Inizio_Ts, Fine_Ts) / 60.0 AS DECIMAL(9,2)) AS DurataMinuti
                                                    ,[Note] 
                                                FROM dbo.LottiAttrezzaggi WHERE IDLotto=@idLotto
                                                ORDER BY Inizio_Ts");

                //controlli
                db.ReadDataTable(Controlli, "SELECT * FROM dbo.LottiControlli WHERE IDLotto=@idLotto ORDER BY Ts", true);
                db.Command.Parameters["idLottoControllo"].Value = -1;
                db.ReadDataTable(ControlliAstine, "SELECT * FROM dbo.LottiControlliAstine WHERE IDLottoControllo=@idLottoControllo", true);
                foreach (DataRow r in Controlli.Rows)
                {
                    db.Command.Parameters["idLottoControllo"].Value = r["ID"];
                    db.ReadDataTable(ControlliAstine, "SELECT * FROM dbo.LottiControlliAstine WHERE IDLottoControllo=@idLottoControllo", false, true);
                }

                //fermi
                db.ReadDataTable(Fermi, @"SELECT [ID]
                                            ,[IDLotto]
                                            ,[IDCausaleFermo]
                                            ,[Tipo]
                                            ,[Inizio_Ts] AS Inizio
                                            ,[Fine_Ts] AS Fine
                                            ,CAST( 1.0 * DATEDIFF(SECOND, Inizio_Ts, Fine_Ts) / 60.0 AS DECIMAL(9,2)) AS DurataMinuti
                                            ,[Note] 
                                        FROM dbo.LottiFermi WHERE IDLotto=@idLotto
                                        ORDER BY Inizio_Ts");

                //operatori
                db.ReadDataTable(Operatori, "SELECT * FROM dbo.LottiOperatori WHERE IDLotto=@idLotto ORDER BY Inizio_Ts, IDOperatore");

                //ordini
                db.ReadDataTable(Ordini, "SELECT * FROM dbo.LottiOrdini WHERE IDLotto=@idLotto ORDER BY OrdineProduzione");

                //produzioni
                db.ReadDataTable(Produzioni, @"SELECT [ID]
                                                ,[IDLotto]
                                                ,[Inizio_Ts] AS Inizio
                                                ,[Fine_Ts] AS Fine
                                                ,CAST( 1.0 * DATEDIFF(SECOND, Inizio_Ts, Fine_Ts) / 60.0 AS DECIMAL(9,2)) AS DurataMinuti
                                                ,[Qta]
                                                ,[NumeroPassate]
                                                ,[Note] 
                                                ,[ProdRidotta]
                                                ,[ProdRidotta_IDCausale]    
                                            FROM dbo.LottiProduzioni WHERE IDLotto=@idLotto ORDER BY Inizio_Ts");

                //scarti
                db.ReadDataTable(Scarti, "SELECT * FROM dbo.LottiScarti WHERE IDLotto=@idLotto ORDER BY Ts");

                //tempi calcolati
                db.ReadDataTable(TempiCalcolati, "SELECT * FROM LottiTempiCalcolati WHERE IDLotto=@idLotto");

                //eventi
                Controlli.RowChanged += Controlli_RowChanged;
            }

            DS = new DataSet();
            DS.Tables.Add(Controlli);
            DS.Tables.Add(ControlliAstine);
            DS.Relations.Add(new DataRelation("Controlli_ControlliAstine",
                DS.Tables[0].Columns["ID"],
                DS.Tables[1].Columns["IDLottoControllo"]));
            return ok;
        }

        private void Controlli_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            if (e.Row.RowState == DataRowState.Added)
            {
                if (e.Row["Ts"] == DBNull.Value) e.Row["Ts"] = DateTime.Now;
                if (ControlliAstine.Select("IDLottoControllo = " + e.Row["ID"].ToString()).Length == 0)
                {
                    foreach (DataRow r in Astine.Rows)
                    {
                        DataRow rn = ControlliAstine.NewRow();
                        rn["IDLottoControllo"] = e.Row["ID"];
                        rn["CodiceAstina"] = r["ItemId"];
                        ControlliAstine.Rows.Add(rn);
                    }
                }
            }
        }

        public DataTable Astine
        {
            get
            {
                db.Command.Parameters.Clear();
                db.Command.Parameters.AddWithValue("idLotto", ID);
                db.Command.Parameters.AddWithValue("dataAreaId", Properties.Settings.Default.DataAreaID);
                db.Command.CommandText = @"SELECT DISTINCT 
	                           COALESCE(IT.NPOPBAREFITEMID, PB.ITEMID) AS ItemId 
                        FROM   dbo.LottiOrdini LO
                               INNER JOIN [AXSQL\AX2009].AX2009_METRA_LIVE.dbo.PRODBOM PB ON PB.DATAAREAID = @dataAreaId
                                                                                             AND PB.PRODID = LO.OrdineProduzione
                               INNER JOIN [AXSQL\AX2009].AX2009_METRA_LIVE.dbo.INVENTTABLE IT ON IT.DATAAREAID = PB.DATAAREAID
                                                                                                 AND IT.ITEMID = PB.ITEMID
                                                                                                 AND IT.ITEMTYPE = 0
                        WHERE  LO.IDLotto = @idLotto;";
                DataTable dt = new DataTable();
                db.ReadDataTable(dt);
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
            Result res = db.UpdateDataTable(Lotti);
            if (res.Ok)
            {
                res.Ok &= CheckTs(Attrezzaggi, ref res.Descrizione);
                res.Ok &= CheckTs(Controlli, ref res.Descrizione);
                res.Ok &= CheckTs(Fermi, ref res.Descrizione);
                res.Ok &= CheckTs(Produzioni, ref res.Descrizione);
                if (res.Ok)
                {
                    db.UpdateDataTable(Attrezzaggi);
                    db.UpdateDataTable(Controlli);
                    db.UpdateDataTable(ControlliAstine);
                    db.UpdateDataTable(Fermi);
                    db.UpdateDataTable(Operatori);
                    db.UpdateDataTable(Ordini);
                    db.UpdateDataTable(Produzioni);
                    db.UpdateDataTable(Scarti);
                }
            }
            return res;
        }

        public bool Diagnose(Boolean showMessage)
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
	                --UNION ALL
	                --SELECT 'F'+CONVERT(varchar(10),LD.ID) AS PK, 'F' AS Tipo, LD.ID, L.IDPostazione, LD.IDLotto, L.NumeroLotto, LD.Inizio_Ts, LD.Fine_Ts FROM LottiFermi LD INNER JOIN Lotti L ON L.ID=LD.IDLotto
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
                           AND DO2.RN = DO.RN - 1
                WHERE DO.TipoFase = 'P'
	                OR (DO.TipoFase='A' AND COALESCE(DO2.TipoFase,'') != DO.TipoFase)";
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
                            s += string.Format("\n    Prod. {0}: attrezzaggi {1}, passate dichiarate {2}", r["Inizio_Ts"], attrezzaggi, passate);
                        }
                        break;
                }
                tipoFasePrec = r["TipoFase"].ToString();
            }
            if (s.Length > 0) DiagMsg += "\nNumero passate non coincide con attrezzaggi:" + s;

            //finito
            DiagOk = DiagMsg.Length == 0;
            DiagMsg = String.Format("Lotto {0}: {1}", Lotti.Rows[0]["NumeroLotto"], DiagOk ? "OK" : "ERR") + (DiagMsg.Length > 0 ? ("\n" + DiagMsg) : "");
            DiagMsg = DiagMsg.Replace("\n", "\r\n");
            if (!DiagOk && showMessage) System.Windows.Forms.MessageBox.Show(DiagMsg, "Diagnostica", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

            Lotti.Rows[0]["Elab_Diagnostica"] = DiagOk ? "OK" : "ERR";
            Lotti.Rows[0]["Elab_DiagnosticaTs"] = DateTime.Now;
            Lotti.Rows[0]["Elab_DiagnosticaMsg"] = DiagMsg.Length < 1000 ? DiagMsg : DiagMsg.Substring(0, 1000);

            return DiagOk;
        }
    }
}
