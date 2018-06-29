using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Metra.ManutenzioneDatiAssemblaggio
{
    public partial class ucParametri : UserControl
    {
        DbBridge _db = new DbBridge();
        DataTable _dt = null;
        DsBridge.DsDataTable _dbt;
        string _sQuery;
        TagParameters _tag;

        public ucParametri()
        {
            InitializeComponent();
            lblTitolo.Text = "Anagrafiche";

            //colori
            Skins.SetColors(this, Skins.ColorSet.StandardList);

            //parametri "tabella"
            TreeNode tnTab = new TreeNode("Anagrafiche");
            TreeNode tn;

            tn = new TreeNode("Aziende");
            tn.Tag = new TagParameters("SHOWTABLE", "SELECT * FROM Anagrafica.Aziende");
            tnTab.Nodes.Add(tn);

            tn = new TreeNode("Causali chiusura");
            tn.Tag = new TagParameters("SHOWTABLE", "SELECT ID, Descrizione FROM Anagrafica.CausaliChiusura");
            tnTab.Nodes.Add(tn);

            tn = new TreeNode("Causali fermo");
            tn.Tag = new TagParameters("SHOWTABLE", "SELECT * FROM Anagrafica.CausaliFermo");
            tnTab.Nodes.Add(tn);

            tn = new TreeNode("Causali produttivitą ridotta");
            tn.Tag = new TagParameters("SHOWTABLE", "SELECT * FROM Anagrafica.CausaliProdRidotta");
            tnTab.Nodes.Add(tn);

            //tn = new TreeNode("Causali scarto");
            //tn.Tag = new TagParameters("SHOWTABLE", "SELECT * FROM Anagrafica.CausaliScarto");
            //tnTab.Nodes.Add(tn);

            tn = new TreeNode("Operatori");
            tn.Tag = new TagParameters("SHOWTABLE", "SELECT * FROM Anagrafica.Operatori");
            tnTab.Nodes.Add(tn);

            tn = new TreeNode("Postazioni");
            tn.Tag = new TagParameters("SHOWTABLE", "SELECT * FROM Anagrafica.Postazioni");
            tnTab.Nodes.Add(tn);

            //tn = new TreeNode("Turni");
            //tn.Tag = new TagParameters("SHOWTABLE", "SELECT * FROM Anagrafica.Turni");
            //tnTab.Nodes.Add(tn);

            //popolo treeview
            tvScelta.Nodes.Clear();
            tvScelta.Nodes.Add(tnTab);
            tvScelta.Sort();
            tvScelta.ExpandAll();
            tvScelta.SelectedNode = tvScelta.Nodes[0];
        }

        private void tvScelta_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (tvScelta.SelectedNode.Tag != null)
            {
                //selezionata tabella
                lblTitolo.Text = "Anagrafiche: " + tvScelta.SelectedNode.Text;

                dgvElenco.Visible = true;
                _tag = (TagParameters)tvScelta.SelectedNode.Tag;

                Filtra();

                //permessi
                btnSalva.Enabled = true;
                //if (_tag.Query.Contains("FROM Presse")&& Authentication.UserCanChangePressNotes)
                //{
                //    btnSalva.Enabled = true;
                //    dgvElenco.AllowUserToAddRows = false;
                //    dgvElenco.AllowUserToDeleteRows = false;
                //    dgvElenco.ReadOnly = false;
                //    foreach (DataGridViewColumn c in dgvElenco.Columns)
                //    {
                //        c.ReadOnly = !(c.DataPropertyName.StartsWith("Note_"));
                //    }
                //}
                //else
                //{
                //    btnSalva.Enabled = false;
                //    dgvElenco.AllowUserToAddRows = !_tag.ReadOnly && btnSalva.Enabled;
                //    dgvElenco.AllowUserToDeleteRows = !_tag.ReadOnly && btnSalva.Enabled;
                //    dgvElenco.ReadOnly = _tag.ReadOnly || !btnSalva.Enabled;
                //}
            }
            else
            {
                lblTitolo.Text = "Anagrafiche";
                _dt = null;
            }
        }

        private void Filtra()
        {
            
            //carico tabella da db
            _dt = new DataTable();
            _sQuery = _tag.Query;
            //if (optElt.Visible)
            //{
            //    if (optElt.Checked)
            //    {
            //        _sQuery += " WHERE Reparto='ELT'";
            //        _tag.FieldToSet = "Reparto";
            //        _tag.FieldToSet_Value = "ELT";
            //    }
            //    else
            //    {
            //        _sQuery += " WHERE Reparto='MEC'";
            //        _tag.FieldToSet = "Reparto";
            //        _tag.FieldToSet_Value = "MEC";
            //    }
            //}
            _db.ReadDataTable(_dt, _sQuery);

            //collego datatable
            if (_dbt != null) _dbt.Clear();
            _dbt = new DsBridge.DsDataTable(dgvElenco, _dt);
            _dbt.Load(true);

            //nascondo colonna id?
            if (_tag.ShowID == false)
            {
                dgvElenco.Columns[_tag.IDFieldName].Visible = false;
                try
                {
                    dgvElenco.Columns["Controllo"].Visible = false;
                }
                catch (Exception)
                {
                }
                try
                {
                    dgvElenco.Columns["Campo"].Visible = false;
                }
                catch (Exception)
                {
                }
            }

            //larghezza colonne
            dgvElenco.AllowUserToResizeColumns = true;
            for (int i = 0; i < dgvElenco.ColumnCount - 1; i++)
            {
                dgvElenco.Columns[i].Resizable = DataGridViewTriState.True;
                dgvElenco.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            if (dgvElenco.ColumnCount > 0)
            {
                dgvElenco.Columns[dgvElenco.ColumnCount - 1].Resizable = DataGridViewTriState.True;
                dgvElenco.Columns[dgvElenco.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void btnSalva_Click(object sender, EventArgs e)
        {
            //salvo parametri o tabella selezionata
            if (tvScelta.SelectedNode.Tag != null)
            {
                if (tvScelta.SelectedNode.Tag.ToString() == "SHOWPARAMS")
                {
                    //parametri
                    //ucProprieta.SaveParameters();

                    //colori di questo oggetto
                    Skins.SetColors(this, Skins.ColorSet.ColorList);
                    //ucProprieta.BackColor = Skins.ListBackColor(Skins.ColorSet.ColorList);
                    //ucProprieta.ForeColor = Color.White;

                    //colori menu
                    frmMain f = (frmMain)this.Parent.Parent.Parent.Parent.Parent;
                    foreach (Control c in f.splitMain.Panel1.Controls)
                    {
                        c.Refresh();
                    }
                }
                else
                {
                    //campo da settare con valore fisso?
                    //TagParameters par = (TagParameters)tvScelta.SelectedNode.Tag;
                    if (_tag.FieldToSet.Length > 0 && _tag.FieldToSet_Value.Length > 0)
                    {
                        foreach (DataRow dr in _dt.Rows)
                        {
                            if (dr[_tag.FieldToSet] == null || dr[_tag.FieldToSet].ToString() == "")
                            {
                                dr[_tag.FieldToSet] = _tag.FieldToSet_Value;
                            }
                        }
                    }

                    //salvo modifiche
                    _db.UpdateDataTable(_dt);
                    Globals.Load();
                }
            }
        }

        private void btnAnnulla_Click(object sender, EventArgs e)
        {
            //Properties.Settings.Default.Save();

            //ricarico tabelle globali
            //Globals.Caricatabelle();

            Dispose();
        }

        private void ControllaDt()
        {
            if (_dt != null)
            {
                if (_dt.GetChanges() != null)
                {
                    DialogResult dr = MessageBox.Show("Salvare le modifiche alla tabella '" + tvScelta.SelectedNode.Text + "'?", "Attenzione", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        _db.UpdateDataTable(_dt);
                    }
                }
            }
        }

        private void dgTabella_DoubleClick(object sender, EventArgs e)
        {
            TagParameters Tag;
            Tag = (TagParameters)tvScelta.SelectedNode.Tag;
            if (Tag.DetailForm.Length > 0)
            {
                object[] Args = new object[1];
                Args[0] = dgvElenco.CurrentRow.Cells[Tag.IDFieldName].Value;
                string sAppName = Application.ProductName + ".";
                Form f = (Form)Type.GetType(sAppName + Tag.DetailForm).InvokeMember(Tag.DetailForm, System.Reflection.BindingFlags.CreateInstance, null, null, Args);
                f.ShowDialog();
                Requery();
            }
        }

        private void ctlComandi_Click_MoveFirst(object sender, EventArgs e)
        {
            if (_dbt != null) dgvElenco.CurrentCell = dgvElenco.Rows[0].Cells[0];
        }

        private void ctlComandi_Click_MoveLast(object sender, EventArgs e)
        {
            if (_dbt != null)
                if (dgvElenco.AllowUserToAddRows) dgvElenco.CurrentCell = dgvElenco.Rows[dgvElenco.Rows.Count - 2].Cells[0];
                else dgvElenco.CurrentCell = dgvElenco.Rows[dgvElenco.Rows.Count - 1].Cells[0];
        }

        private void ctlComandi_Click_MovePrev(object sender, EventArgs e)
        {
            if (_dbt != null)
            {
                Point p = dgvElenco.CurrentCellAddress;
                if (p.Y > 0)
                {
                    p.Y--;
                    dgvElenco.CurrentCell = dgvElenco.Rows[p.Y].Cells[p.X];
                }
            }
        }

        private void ctlComandi_Click_MoveNext(object sender, EventArgs e)
        {
            if (_dbt != null)
            {
                Point p = dgvElenco.CurrentCellAddress;
                if (p.Y < dgvElenco.Rows.Count - 1)
                {
                    p.Y++;
                    dgvElenco.CurrentCell = dgvElenco.Rows[p.Y].Cells[p.X];
                }
            }
        }

        private void ctlComandi_Click_Delete(object sender, EventArgs e)
        {
            if (_dbt != null)
            {
                if (MessageBox.Show("Eliminare la riga?", "Attenzione", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    dgvElenco.Rows.Remove(dgvElenco.CurrentRow);
            }
        }

        private void ctlComandi_Click_Edit(object sender, EventArgs e)
        {
            if (_dbt != null)
            {
                dgTabella_DoubleClick(sender, e);
            }
        }

        private void ctlComandi_Click_Add(object sender, EventArgs e)
        {
            if (_dbt != null)
            {
                TagParameters Tag;
                Tag = (TagParameters)tvScelta.SelectedNode.Tag;
                if (Tag.DetailForm.Length > 0)
                {
                    object[] Args = new object[1];
                    Args[0] = null;
                    string sAppName = Application.ProductName + ".";
                    Form f = (Form)Type.GetType(sAppName + Tag.DetailForm).InvokeMember(Tag.DetailForm, System.Reflection.BindingFlags.CreateInstance, null, null, Args);
                    f.ShowDialog();
                    Requery();
                }
                else if (dgvElenco.AllowUserToAddRows)
                {
                    dgvElenco.CurrentCell = dgvElenco.Rows[dgvElenco.Rows.Count - 1].Cells[0];
                }
            }
        }

        public void Requery()
        {
            int iCol = dgvElenco.FirstDisplayedScrollingColumnIndex;
            int iRow = dgvElenco.FirstDisplayedScrollingRowIndex;
            Point pCell = dgvElenco.CurrentCellAddress;

            _db.ReadDataTable(_dt, _sQuery);
            _dbt.Load();

            dgvElenco.CurrentCell = dgvElenco.Rows[pCell.Y].Cells[pCell.X];
            dgvElenco.FirstDisplayedScrollingColumnIndex = iCol;
            dgvElenco.FirstDisplayedScrollingRowIndex = iRow;
        }

        private void ctlParametri_Load(object sender, EventArgs e)
        {
            btnSalva.Enabled = false;
        }
    }

    public class TagParameters
    {
        //classe per i parametri da inserire nel tag della treeview

        public string Command = "";
        public string Query = "";
        public bool ReadOnly = false;
        public string DetailForm = "";
        public string IDFieldName = "ID";
        public bool ShowID = true;
        public string FieldToSet;
        public string FieldToSet_Value;

        public TagParameters(string Command, string Query)
        {
            this.Command = Command;
            this.Query = Query;
            this.ReadOnly = false;
            this.DetailForm = "";
            this.IDFieldName = "ID";
            this.FieldToSet = "";
            this.FieldToSet_Value = "";
        }

        public TagParameters(string Command, string Query, bool ReadOnly, string DetailForm, string IDFieldName)
        {
            this.Command = Command;
            this.Query = Query;
            this.ReadOnly = ReadOnly;
            this.DetailForm = DetailForm;
            this.IDFieldName = IDFieldName;
            this.FieldToSet = "";
            this.FieldToSet_Value = "";
        }

        public TagParameters(string Command, string Query, bool ShowID)
        {
            this.Command = Command;
            this.Query = Query;
            this.ReadOnly = false;
            this.DetailForm = "";
            this.IDFieldName = "ID";
            this.ShowID = ShowID;
            this.FieldToSet = "";
            this.FieldToSet_Value = "";
        }
    }
}
