using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Metra.ManutenzioneDatiAssemblaggio
{
    public partial class ucLots : UserControl
    {
        private bool Loading = true;
        private DbBridge db;
        DataTable Lotti = null;

        public ucLots()
        {
            InitializeComponent();
            db = new DbBridge();
        }

        private void Filtra(object sender, EventArgs e)
        {
            if (Loading) return;
            Requery();
        }

        private string GetSql()
        {
            return @"/*
                DECLARE @data1 DATETIME = DATEADD(DAY, -1, CURRENT_TIMESTAMP) ,
                        @data2 DATETIME = CURRENT_TIMESTAMP ,
                        @idPostazione VARCHAR(10) = 'TUTTE' ,
                        @riconciliare INT = 0 ,
                        @riconciliato INT = -1 ,
                        @diagnostica VARCHAR(10) = 'TUTTO' ,
                        @codiceArticolo VARCHAR(20) = '%' 
                --*/

                ;WITH 
                LO AS ( 
	                SELECT LO.IDLotto ,
		                SUM(LO.Qta) AS QtaOrdine
	                FROM    
		                dbo.LottiOrdini LO
	                GROUP BY 
		                LO.IDLotto ) ,
                LA AS ( 
	                SELECT 
		                LA.IDLotto ,
		                SUM(DATEDIFF(SECOND, LA.Inizio_Ts, LA.Fine_Ts))/60.0 AS MinutiAttrezzaggio
	                FROM dbo.LottiAttrezzaggi LA
	                GROUP BY
		                LA.IDLotto),
                LP AS ( 
	                SELECT LP.IDLotto ,
		                SUM(LP.Qta) AS QtaProdotta ,
		                SUM(DATEDIFF(SECOND, LP.Inizio_Ts, LP.Fine_Ts))/60.0 AS MinutiProduzione
	                FROM     
		                dbo.LottiProduzioni LP
	                GROUP BY 
		                LP.IDLotto),
                LF AS ( 
	                SELECT LF.IDLotto ,
		                SUM(DATEDIFF(SECOND, LF.Inizio_Ts, LF.Fine_Ts))/60.0 AS MinutiFermo
	                FROM     
		                dbo.LottiFermi LF
	                GROUP BY 
		                LF.IDLotto),
                LTL AS (
	                SELECT 
		                LTL.IDLotto,
		                SUM(CASE WHEN Tipo=N'A' THEN LTL.DurataMinuti ELSE 0 END) AS MinutiAttrezzaggio,
		                SUM(CASE WHEN Tipo=N'P' THEN LTL.DurataMinuti ELSE 0 END) AS MinutiProduzione,
		                SUM(CASE WHEN Tipo=N'F' THEN LTL.DurataMinuti ELSE 0 END) AS MinutiFermo
	                FROM
		                dbo.vwLottiTimeLine LTL
	                GROUP BY
		                LTL.IDLotto)

                SELECT	L.ID ,
		                L.NumeroLotto ,
		                L.TsInizio ,
		                L.TsFine ,
		                L.IDPostazione ,
		                L.IDTurno ,
		                L.CodiceArticolo ,
		                L.CodiceCra ,
		                L.IDCausaleChiusura ,
		                COALESCE(L.Campionatura, 0) AS Campionatura ,
		                CC.Descrizione AS CausaleChiusura ,
		                L.Elab_Diagnostica ,
		                COALESCE(L.Elab_Riconciliare, 0) AS Elab_Riconciliare ,
		                COALESCE(L.Elab_Riconciliato, 0) AS Elab_Riconciliato ,
		                COALESCE(LO.QtaOrdine, 0) AS Elab_QtaOrdine,
		                COALESCE(LP.QtaProdotta, 0) AS Elab_QtaProdotta ,
		                COALESCE(LA.MinutiAttrezzaggio, 0) AS Elab_MinutiAttrezzaggio ,
		                COALESCE(LTL.MinutiAttrezzaggio, 0) AS MinutiAttrezzaggio_Calendario ,
		                COALESCE(LP.MinutiProduzione, 0) AS Elab_MinutiProduzione ,
		                COALESCE(LTL.MinutiProduzione, 0) AS MinutiProduzione_Calendario ,
		                COALESCE(LF.MinutiFermo, 0) AS Elab_MinutiFermo ,
		                COALESCE(LTL.MinutiFermo, 0) AS MinutiFermo_Calendario ,
		                COALESCE(LA.MinutiAttrezzaggio, 0)  
		                + COALESCE(LP.MinutiProduzione, 0)  
		                + COALESCE(LF.MinutiFermo, 0) AS Elab_MinutiDichiarazioni ,
		                COALESCE(LTL.MinutiAttrezzaggio, 0) 
		                + COALESCE(LTL.MinutiProduzione, 0) 
		                + COALESCE(LTL.MinutiFermo, 0) AS Elab_MinutiDichiarazioni_Calendario ,
		                DATEDIFF(SECOND, L.TsInizio, L.TsFine)/60 AS Elab_MinutiLotto

                FROM   dbo.Lotti L 
	                LEFT JOIN LO ON LO.IDLotto = L.ID
	                LEFT JOIN LA ON LA.IDLotto = L.ID
	                LEFT JOIN LP ON LP.IDLotto = L.ID
	                LEFT JOIN LF ON LF.IDLotto = L.ID
	                LEFT JOIN LTL ON LTL.IDLotto = L.ID
	                LEFT JOIN Anagrafica.CausaliChiusura CC ON CC.ID = L.IDCausaleChiusura
	                LEFT JOIN Anagrafica.Postazioni P ON P.ID = L.IDPostazione

                WHERE	L.TsFine > @data1 AND L.TsInizio < @data2
		                AND ((@idPostazione = 'TUTTE' AND COALESCE(P.Test,0)=0) OR L.IDPostazione = @idPostazione)
		                AND (@riconciliare = -1 OR COALESCE(Elab_Riconciliare,0) = @riconciliare)
		                AND (@riconciliato = -1 OR COALESCE(Elab_Riconciliato,0) = @riconciliato)
		                AND (@diagnostica = 'TUTTO' OR L.Elab_Diagnostica = @diagnostica)
		                AND (@codiceArticolo = '%%' OR L.CodiceArticolo LIKE @codiceArticolo)
                ORDER BY L.ID;";


            //return @"/*
            //        DECLARE @data1 DATETIME = DATEADD(DAY, -1, CURRENT_TIMESTAMP) ,
            //                @data2 DATETIME = CURRENT_TIMESTAMP ,
            //                @idPostazione VARCHAR(10) = 'TUTTE' ,
            //                @riconciliare INT = 0 ,
            //                @riconciliato INT = -1 ,
            //                @diagnostica VARCHAR(10) = 'TUTTO' ,
            //                @codiceArticolo VARCHAR(20) = '%' 
            //        --*/

            //        SELECT	L.ID ,
		          //          L.NumeroLotto ,
		          //          L.TsInizio ,
		          //          L.TsFine ,
		          //          L.IDPostazione ,
		          //          L.IDTurno ,
		          //          L.CodiceArticolo ,
		          //          L.CodiceCra ,
		          //          L.IDCausaleChiusura ,
		          //          L.Campionatura ,
		          //          CC.Descrizione AS CausaleChiusura ,
		          //          L.Elab_Diagnostica ,
		          //          COALESCE(L.Elab_Riconciliare, 0) AS Elab_Riconciliare ,
		          //          COALESCE(L.Elab_Riconciliato, 0) AS Elab_Riconciliato ,
		          //          LTC.QtaOrdine AS Elab_QtaOrdine,
		          //          LTC.QtaProdotta AS Elab_QtaProdotta ,
		          //          LTC.MinutiAttrezzaggio AS Elab_MinutiAttrezzaggio ,
		          //          LTC.MinutiAttrezzaggio_Calendario ,
		          //          LTC.MinutiProduzione AS Elab_MinutiProduzione ,
		          //          LTC.MinutiProduzione_Calendario ,
		          //          LTC.MinutiFermo AS Elab_MinutiFermo ,
		          //          LTC.MinutiFermo_Calendario ,
		          //          LTC.MinutiDichiarazioni AS Elab_MinutiDichiarazioni ,
		          //          LTC.MinutiDichiarazioni_Calendario ,
		          //          LTC.MinutiLotto AS Elab_MinutiLotto
            //        FROM	dbo.Lotti L
		          //          LEFT JOIN Anagrafica.Postazioni P ON P.ID = L.IDPostazione
		          //          LEFT JOIN Anagrafica.CausaliChiusura CC ON CC.ID = L.IDCausaleChiusura
		          //          LEFT JOIN dbo.LottiTempiCalcolati LTC ON LTC.IDLotto = L.ID
            //        WHERE	L.TsFine > @data1 AND L.TsInizio < @data2
		          //          AND ((@idPostazione = 'TUTTE' AND COALESCE(P.Test,0)=0) OR L.IDPostazione = @idPostazione)
		          //          AND (@riconciliare = -1 OR COALESCE(Elab_Riconciliare,0) = @riconciliare)
		          //          AND (@riconciliato = -1 OR COALESCE(Elab_Riconciliato,0) = @riconciliato)
		          //          AND (@diagnostica = 'TUTTO' OR L.Elab_Diagnostica = @diagnostica)
		          //          AND (@codiceArticolo = '%%' OR L.CodiceArticolo LIKE @codiceArticolo)
            //        ORDER BY L.ID";
        }

        private string GetSql_Old()
        {
            return @"SELECT
	                    L.ID,
	                    L.NumeroLotto,
                        L.TsInizio,
                        L.TsFine,
	                    L.IDPostazione,
	                    L.IDTurno,
	                    L.CodiceArticolo,
	                    L.CodiceCRA,
	                    L.IDCausaleChiusura,
                        L.Campionatura,
	                    CC.Descrizione AS CausaleChiusura,
	                    L.Elab_Diagnostica,
	                    COALESCE(L.Elab_Riconciliare,0) AS Elab_Riconciliare,
	                    COALESCE(L.Elab_Riconciliato,0) AS Elab_Riconciliato,
						COALESCE(LO.Qta,0) AS Elab_QtaOrdine,
						COALESCE(LP.Qta,0) AS Elab_QtaProdotta,
	                    CAST(COALESCE(LA.Secondi,0) AS float)/60 AS Elab_MinutiAttrezzaggio,	                        
	                    CAST(COALESCE(LP.Secondi,0) AS float)/60 AS Elab_MinutiProduzione,	                        
	                    CAST(COALESCE(LF.Secondi,0) AS float)/60 AS Elab_MinutiFermo,	   
						CAST(COALESCE(LA.Secondi,0)+COALESCE(LP.Secondi,0) AS float)/60 AS Elab_MinutiDichiarazioni,                
	                    CAST(DATEDIFF(s,L.TsInizio,L.TsFine) AS float)/60 AS Elab_MinutiLotto
                    FROM 
	                    Lotti L
                        INNER JOIN Anagrafica.Postazioni P ON P.ID=L.IDPostazione
                        LEFT JOIN Anagrafica.CausaliChiusura CC ON CC.ID=L.IDCausaleChiusura
	                    LEFT JOIN (SELECT IDLotto, SUM(DATEDIFF(s,Inizio_Ts,Fine_Ts)) AS Secondi FROM LottiAttrezzaggi GROUP BY IDLotto) AS LA ON LA.IDLotto=L.ID
	                    LEFT JOIN (SELECT IDLotto, SUM(DATEDIFF(s,Inizio_Ts,Fine_Ts)) AS Secondi, SUM(Qta) AS Qta FROM LottiProduzioni GROUP BY IDLotto) AS LP ON LP.IDLotto=L.ID
	                    LEFT JOIN (SELECT IDLotto, SUM(DATEDIFF(s,Inizio_Ts,Fine_Ts)) AS Secondi FROM LottiFermi GROUP BY IDLotto) AS LF ON LF.IDLotto=L.ID
	                    LEFT JOIN (SELECT IDLotto, SUM(Qta) AS Qta FROM LottiOrdini GROUP BY IDLotto) AS LO ON LO.IDLotto=L.ID
                    WHERE 
                        (TsInizio BETWEEN @data1 AND @data2 OR TsFine BETWEEN @data1 AND @data2)
                        AND ((@idPostazione='TUTTE' AND COALESCE(P.Test,0)=0) OR IDPostazione=@idPostazione)
                        AND (@riconciliare=-1 OR COALESCE(Elab_Riconciliare,0)=@riconciliare)
                        AND (@riconciliato=-1 OR COALESCE(Elab_Riconciliato,0)=@riconciliato)
                        AND (@diagnostica='TUTTO' OR Elab_Diagnostica=@diagnostica)
                        AND (@codiceArticolo='%%' OR CodiceArticolo LIKE @codiceArticolo)";
        }

        public void Requery()
        {
            Cursor = Cursors.WaitCursor;

            //posizione corrente
            int iCol = dgvElenco.FirstDisplayedScrollingColumnIndex;
            int iRow = dgvElenco.FirstDisplayedScrollingRowIndex;
            Point pCell = dgvElenco.CurrentCellAddress;

            //carico i dati
            Lotti = new DataTable();
            db.Command.CommandText = GetSql();
            db.Command.Parameters.Clear();
            db.Command.Parameters.AddWithValue("data1", dtpInizio.Value);
            db.Command.Parameters.AddWithValue("data2", dtpFine.Value.AddDays(1));
            db.Command.Parameters.AddWithValue("idPostazione", cmbPostazione.Text);
            switch (cmbStato.SelectedIndex)
            {
                case 1: //da controllare
                    db.Command.Parameters.AddWithValue("riconciliare", 0);
                    db.Command.Parameters.AddWithValue("riconciliato", -1);
                    break;
                case 2: //controllati
                    db.Command.Parameters.AddWithValue("riconciliare", 1);
                    db.Command.Parameters.AddWithValue("riconciliato", -1);
                    break;
                //case 3: //riconciliati
                //    db.Command.Parameters.AddWithValue("riconciliare", 1);
                //    db.Command.Parameters.AddWithValue("riconciliato", 1);
                //    break;
                default: //tutti
                    db.Command.Parameters.AddWithValue("riconciliare", -1);
                    db.Command.Parameters.AddWithValue("riconciliato", -1);
                    break;
            }
            db.Command.Parameters.AddWithValue("diagnostica", cmbDiagnostica.Text);
            db.Command.Parameters.AddWithValue("codiceArticolo", "%" + txtArticolo.Text + "%");

            db.ReadDataTable(Lotti);
            dgvElenco.DataSource = Lotti;

            //ripristino posizione corrente
            if (pCell.Y >= dgvElenco.Rows.Count) pCell.Y = dgvElenco.Rows.Count - 1;
            if (pCell.X >= 0 & pCell.Y >= 0) dgvElenco.CurrentCell = dgvElenco.Rows[pCell.Y].Cells[pCell.X];
            if (iCol >= 0 && dgvElenco.Columns[iCol].Visible) dgvElenco.FirstDisplayedScrollingColumnIndex = iCol;
            if (iRow >= 0 && dgvElenco.RowCount > 0 && dgvElenco.RowCount >= iRow) dgvElenco.FirstDisplayedScrollingRowIndex = iRow;

            Cursor = Cursors.Default;
        }

        private void ucLotti_Load(object sender, EventArgs e)
        {
            //versione
            Text = "Metra - " + Application.ProductName + " " + Application.ProductVersion.ToString();

            //readonly
            if (Properties.Settings.Default.ReadOnly)
            {
                btnDiagnostica.Visible = false;
                btnDiagnostica.Width = 0;
                //btnStampa.Left = btnDiagnostica.Left;
            }

            //colori
            Skins.SetGridColors(dgvElenco);

            //periodo: default mese corrente
            Utils.SetPeriodo(dtpInizio, dtpFine);

            //permessi
            dgvElenco.AllowUserToAddRows = false;
            dgvElenco.AllowUserToDeleteRows = false;
            dgvElenco.ReadOnly = true;

            //layout
            dgvElenco.ContextMenuStrip = cmsElenco;

            //dgv: contrassegno colonne "PRESET"
            int iNumCol = dgvElenco.Columns.Count;
            for (int i = 0; i < dgvElenco.Columns.Count; i++)
            {
                dgvElenco.Columns[i].Tag = "PRESET_" + i.ToString();
                if (dgvElenco.Columns[i].DataPropertyName.Length == 0) dgvElenco.Columns[i].DataPropertyName = dgvElenco.Columns[i].Name;
            }

            //dgv: formati
            dgvElenco.AutoGenerateColumns = false;
            dgvElenco.Columns["Elab_MinutiAttrezzaggio"].DefaultCellStyle.Format = "#,##0.00";
            dgvElenco.Columns["Elab_MinutiProduzione"].DefaultCellStyle.Format = "#,##0.00";
            dgvElenco.Columns["Elab_MinutiFermo"].DefaultCellStyle.Format = "#,##0.00";
            dgvElenco.Columns["Elab_MinutiDichiarazioni"].DefaultCellStyle.Format = "#,##0.00";
            dgvElenco.Columns["Elab_MinutiLotto"].DefaultCellStyle.Format = "#,##0.00";

            dgvElenco.Columns["Elab_QtaOrdine"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvElenco.Columns["Elab_QtaProdotta"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvElenco.Columns["Elab_MinutiAttrezzaggio"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvElenco.Columns["Elab_MinutiProduzione"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvElenco.Columns["Elab_MinutiFermo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvElenco.Columns["Elab_MinutiDichiarazioni"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvElenco.Columns["Elab_MinutiLotto"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //inizializzo filtro
            cmbPostazione.Items.Clear();
            cmbPostazione.Items.Add("TUTTE");
            foreach (DataRow dr in Globals.Postazioni.Rows)
            {
                cmbPostazione.Items.Add(dr["ID"].ToString());
            }
            cmbPostazione.SelectedIndex = 0;
            cmbStato.SelectedIndex = 1;
            cmbDiagnostica.SelectedIndex = 0;

            //Refresh();
            Loading = false;
            Filtra(null, null);
        }

        private void dgvElenco_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvElenco.CurrentRow != null)
            {
                frmLot f = new frmLot((int)((DataRowView)dgvElenco.CurrentRow.DataBoundItem)["ID"]);
                f.ShowDialog();
                Requery();
            }
        }

        private void segnaDaElaborareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ContrassegnaSelezione(true);
        }

        private void ContrassegnaSelezione(bool DaElaborare)
        {
            //controllo selezione
            if (dgvElenco.CurrentRow != null)
            {
                if (dgvElenco.SelectedRows.Count == 0)
                    //nessuna selezione
                    MessageBox.Show("Nessuna riga selezionata!", "Metra", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                {
                    //selezione di una o più righe
                    int contrassegnate = 0;
                    int bloccate = 0;
                    int errate = 0;

                    int righe = dgvElenco.SelectedRows.Count;
                    string msg = "Contrassegnare le " + righe.ToString() + " righe selezionate?";
                    if (MessageBox.Show(msg, "Metra", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        //elenco lotti
                        Cursor = Cursors.WaitCursor;
                        db.Connection.Open();
                        foreach (DataGridViewRow r in dgvElenco.SelectedRows)
                        {
                            string lotto = r.Cells["NumeroLotto"].Value.ToString();
                            db.Command.CommandText = "UPDATE Lotti SET Elab_Riconciliare=" + (DaElaborare ? "1" : "0") + "WHERE NumeroLotto='" + lotto + "'";
                            db.Command.ExecuteNonQuery();
                        }
                        db.Connection.Close();
                        Cursor = Cursors.Default;

                        //aggiorno elenco
                        Requery();

                        //echo
                        msg = "Impostazione di " + righe.ToString() + " righe completata:";
                        msg += "\n\n - Contrassegnate: " + contrassegnate.ToString();
                        if (errate > 0) msg += "\n - Ignorate perché errate: " + errate.ToString();
                        if (bloccate > 0) msg += "\n - Ignorate perché bloccate: " + bloccate.ToString();
                        MessageBox.Show(msg, "Metra", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void annullaDaElaborareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ContrassegnaSelezione(false);
        }

        private void selezionaTuttoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dr in dgvElenco.Rows)
            {
                dr.Selected = true;
            }
        }

        private void deselezionaTuttoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dr in dgvElenco.Rows)
            {
                dr.Selected = false;
            }
        }

        private void cmsElenco_Opening(object sender, CancelEventArgs e)
        {
            RaiseMouseEvent(dgvElenco, new MouseEventArgs(MouseButtons.Left, 1, Cursor.Position.X, Cursor.Position.Y, 0));
        }

        private void btnAggiorna_Click(object sender, EventArgs e)
        {
            Filtra(sender, e);
        }

        private void copiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
            Clipboard.SetDataObject(dgvElenco.GetClipboardContent());
        }

        //private void btnS
        private void dgvElenco_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //se warning evidenzio in rosso
            DataGridViewRow r = dgvElenco.Rows[e.RowIndex];
            bool bRed = r.Cells["Elab_Diagnostica"].Value != DBNull.Value && r.Cells["Elab_Diagnostica"].Value.ToString() == "ERR";
            if (bRed)
            {
                foreach (DataGridViewCell c in r.Cells)
                {
                    c.Style.ForeColor = Color.Red;
                    c.Style.SelectionForeColor = Color.Red;
                }
            }
        }

        private void nuovoLottoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Per la creazione di nuovi lotti utilizzare l'applicazione di raccolta dati di campo", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void eliminaLottoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //eliminazione lotto
            if (dgvElenco.CurrentRow != null)
            {
                string numeroLotto = dgvElenco.CurrentRow.Cells["NumeroLotto"].Value.ToString();
                string s = "Eliminare il lotto " + numeroLotto + "?";
                s += "\n\nNB:\tL'eliminazione cancellerà completamente i dati raccolti" +
                     "\n\tche NON potranno essere ripristinati";

                if (MessageBox.Show(s, "Attenzione", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //elimino
                    db.Connection.Open();
                    db.Command.CommandText = "DELETE FROM Lotti WHERE NumeroLotto = @NumeroLotto";
                    db.Command.Parameters.Clear();
                    db.Command.Parameters.AddWithValue("NumeroLotto", numeroLotto);
                    db.Command.ExecuteNonQuery();
                   
                    db.Connection.Close();
                    Filtra(null, null);

                    db.Log(DbBridge.LogType.DELETE, numeroLotto, "ELIMINATO LOTTO: " + numeroLotto);
                }
            }
        }

        private void btnDiagnostica_Click(object sender, EventArgs e)
        {
            //controllo selezione
            if (dgvElenco.CurrentRow != null)
            {
                if (dgvElenco.SelectedRows.Count == 0)
                    //nessuna selezione
                    MessageBox.Show("Nessuna riga selezionata!", "Metra", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                {
                    //selezione di una o più righe
                    Lotto lot;
                    int lotId;
                    int lotCount = 0;
                    int lotCountOk = 0;
                    int lotCountErr = 0;

                    int rowCount = dgvElenco.SelectedRows.Count;

                    //elenco lotti
                    Cursor = Cursors.WaitCursor;
                    foreach (DataGridViewRow r in dgvElenco.SelectedRows)
                    {
                        lotId = (int)((DataRowView)r.DataBoundItem).Row["ID"];
                        lot = new Lotto();
                        lot.Load(lotId);
                        lotCount++;
                        if (lot.Diagnostica(false)) lotCountOk++;
                        else lotCountErr++;
                        lot.Save();
                    }
                    Cursor = Cursors.Default;

                    //aggiorno elenco
                    Requery();

                    //echo
                    string msg = "Diagnostica completata:" +
                                "\n\n - Lotti esaminati:\t" + lotCount.ToString() +
                                "\n - Errori:\t\t" + lotCountErr.ToString();
                    MessageBox.Show(msg, "Metra", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void modificaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dgvElenco_CellDoubleClick(sender, null);
        }

        private void btnAnnulla_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
