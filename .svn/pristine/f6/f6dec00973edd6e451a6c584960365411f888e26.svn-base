using System;
using System.Data;
using System.Windows.Forms;

namespace Metra.ManutenzioneDatiAssemblaggio
{
    public partial class ucDowntime : UserControl
    {
        DbBridge DB = new DbBridge();
        DataTable dtFermi;
        DataTable dtLotti;
        //DsBridge.DsDataTable dbtFermi;

        public ucDowntime()
        {
            InitializeComponent();

            //colori
            Skins.SetGridColors(dgvElenco);

            //inizializzo filtro
            cmbPostazione.Items.Clear();
            cmbPostazione.Items.Add("TUTTE");
            foreach (DataRow dr in Globals.Postazioni.Rows)
            {
                cmbPostazione.Items.Add(dr["ID"].ToString());
            }
            cmbPostazione.SelectedIndex = 0;

            cmbTipo.Items.Add("TUTTI");
            cmbTipo.Items.Add("Attrezzaggio");
            cmbTipo.Items.Add("Produzione");
            cmbTipo.Items.Add("Disponibilità");
            cmbTipo.SelectedIndex = 0;

            cmbDiagnostica.SelectedIndex = 0;

            //periodo: default mese corrente
            Utils.SetPeriodo(dtpInizio, dtpFine);

            //eventi filtri
            cmbDiagnostica.SelectedIndexChanged += RefreshData;
            cmbPostazione.SelectedIndexChanged += RefreshData;
            cmbTipo.SelectedIndexChanged += RefreshData;
            dtpFine.CloseUp += RefreshData;
            dtpInizio.CloseUp += RefreshData;

            //primo caricamento
            LoadData();

            //dgvElenco.Columns.Clear();
            //dbtFermi = new DsBridge.DsDataTable(dgvElenco, dtFermi);
            //dbtFermi.Load();
            dgvElenco.AutoGenerateColumns = true;
            dgvElenco.DataSource = dtFermi;
            dgvElenco.AutoGenerateColumns = false;
            dgvElenco.Columns["ID"].Visible = false;
            foreach (DataGridViewColumn col in dgvElenco.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            dgvElenco.Columns["IDPostazione"].ReadOnly = true;
            dgvElenco.Columns["Note"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvElenco.Columns["DurataMinuti"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvElenco.Columns["IDCausaleFermo"].Visible = false;
            DataGridViewComboBoxColumn cmbIdCausale = new DataGridViewComboBoxColumn();
            cmbIdCausale.FlatStyle = FlatStyle.Flat;
            cmbIdCausale.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            cmbIdCausale.Name = "cmbIDCausaleFermo";
            cmbIdCausale.HeaderText = "Causale";
            cmbIdCausale.DataPropertyName = "IDCausaleFermo";
            cmbIdCausale.DataSource = Globals.CausaliFermo;
            cmbIdCausale.DisplayMember = "Descrizione";
            cmbIdCausale.ValueMember = "ID";
            dgvElenco.Columns.Insert(1, cmbIdCausale);

            dgvElenco.Columns["IDLotto"].Visible = false;
            DataGridViewComboBoxColumn cmbIdLotto = new DataGridViewComboBoxColumn();
            cmbIdLotto.FlatStyle = FlatStyle.Flat;
            cmbIdLotto.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            cmbIdLotto.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            cmbIdLotto.ReadOnly = true;
            cmbIdLotto.Name = "cmbIDLotto";
            cmbIdLotto.HeaderText = "Lotto";
            cmbIdLotto.DataPropertyName = "IDLotto";
            cmbIdLotto.DataSource = dtLotti;
            cmbIdLotto.DisplayMember = "NumeroLotto";
            cmbIdLotto.ValueMember = "ID";
            dgvElenco.Columns.Insert(0, cmbIdLotto);


        }

        private void LoadData()
        {
            //elenco fermi
            dtFermi = new DataTable();
            DB.Command.CommandText = @"SELECT
                     [IDPostazione]
                    ,[IDLotto]
                    ,[IDCausaleFermo]
                    ,[Tipo]
                    ,[Inizio_Ts] AS Inizio
                    --,[Inizio_TsForzato]
                    --,[Inizio_TsOriginale]
                    ,[Fine_Ts] AS Fine
                    --,[Fine_TsForzato]
                    --,[Fine_TsOriginale]
                    ,CAST( 1.0 * DATEDIFF(SECOND, Inizio_Ts, Fine_Ts) / 60.0 AS DECIMAL(9,2)) AS DurataMinuti
                    ,[Note], ID  
                FROM LottiFermi 
                WHERE Inizio_Ts BETWEEN @data1 AND @data2
                    AND (@tipo='T' OR Tipo=@tipo)
                    AND (@idPostazione='TUTTE' OR IDPostazione=@idPostazione)
                ORDER BY Inizio_Ts";
            DB.Command.Parameters.Clear();
            DB.Command.Parameters.AddWithValue("@data1", dtpInizio.Value);
            DB.Command.Parameters.AddWithValue("@data2", dtpFine.Value.AddDays(1));
            DB.Command.Parameters.AddWithValue("@tipo", cmbTipo.Text.Substring(0, 1));
            DB.Command.Parameters.AddWithValue("@idPostazione", cmbPostazione.Text);
            DB.ReadDataTable(dtFermi);

            if (dtLotti == null) dtLotti = new DataTable();
            DB.Command.CommandText = @"SELECT  L.ID ,
                        L.NumeroLotto ,
                        L.TsInizio
                FROM    dbo.Lotti L
                        INNER JOIN dbo.LottiFermi LF ON LF.IDLotto = L.ID
                WHERE   Inizio_Ts
                BETWEEN @data1 AND @data2
                UNION ALL
                SELECT -1 ,
                       '' ,
                       '19000101'
                ORDER BY TsInizio;";

            DB.Command.Parameters.Clear();
            DB.Command.Parameters.AddWithValue("@data1", dtpInizio.Value);
            DB.Command.Parameters.AddWithValue("@data2", dtpFine.Value.AddDays(1));
            DB.Command.Parameters.AddWithValue("@idPostazione", cmbPostazione.Text);
            DB.ReadDataTable(dtLotti);


        }

        private void RefreshData(object sender, EventArgs e)
        {
            LoadData();
            dgvElenco.DataSource = dtFermi;
        }

        private void btnAnnulla_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void btnDiagnostica_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DB.UpdateDataTable(dtFermi);
        }
    }
}
