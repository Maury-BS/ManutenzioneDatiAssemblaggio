using System;
using System.Drawing;
using System.Windows.Forms;

namespace Metra.ManutenzioneDatiAssemblaggio
{
    class Utils
    {
        public static System.Windows.Forms.Form GetContainerForm(System.Windows.Forms.Control ctrl  )
        {
            if (ctrl.Parent == null)
            {
                return null;
            }
            else if (ctrl.Parent.GetType().BaseType.ToString() == typeof(System.Windows.Forms.Form).ToString())
            {
                return (System.Windows.Forms.Form)ctrl.Parent;
            }
            else
            {
                return GetContainerForm((System.Windows.Forms.Control)ctrl.Parent);
            }
        }

        public static void SetPeriodo(System.Windows.Forms.DateTimePicker dtpInizio, System.Windows.Forms.DateTimePicker dtpFine)
        {
            //periodo: default ultimi 2 giorni
            DateTime d = DateTime.Today;
            dtpFine.Text = d.ToString();
            d = d.AddDays(-2);
            dtpInizio.Text = d.ToString();
        }
    }

    public static class DgvExport
    {
        public enum ExportMode { Selection, All };

        public static void ToClipboard(DataGridView dgv, ExportMode mode)
        {
            if (mode == ExportMode.All || (mode == ExportMode.Selection && dgv.SelectedCells.Count > 0))
            {
                //salvo attuale stato dgv
                int iCol = dgv.FirstDisplayedScrollingColumnIndex;
                int iRow = dgv.FirstDisplayedScrollingRowIndex;
                Point pCell = dgv.CurrentCellAddress;
                bool allowMultiselect = dgv.MultiSelect;
                DataGridViewClipboardCopyMode copyMode = dgv.ClipboardCopyMode;
                DataGridViewCell[] selectedCells = new DataGridViewCell[dgv.SelectedCells.Count];
                dgv.SelectedCells.CopyTo(selectedCells, 0);

                //seleziono e copio negli appunti
                if (mode == ExportMode.All)
                {
                    dgv.MultiSelect = true;
                    dgv.SelectAll();
                }
                dgv.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
                Clipboard.Clear();
                DataObject obj = dgv.GetClipboardContent();
                Clipboard.SetDataObject(obj);

                //ripristino stato dgv
                if (mode == ExportMode.All)
                {
                    dgv.ClearSelection();
                    if (pCell.Y >= dgv.Rows.Count) pCell.Y = dgv.Rows.Count - 1;
                    if (pCell.X >= 0 & pCell.Y >= 0) dgv.CurrentCell = dgv.Rows[pCell.Y].Cells[pCell.X];
                    if (dgv.Columns[iCol].Visible) dgv.FirstDisplayedScrollingColumnIndex = iCol;
                    if (iRow >= 0 && dgv.RowCount > 0 && dgv.RowCount >= iRow) dgv.FirstDisplayedScrollingRowIndex = iRow;
                    dgv.MultiSelect = allowMultiselect;
                    dgv.ClipboardCopyMode = copyMode;
                    foreach (DataGridViewCell cell in selectedCells)
                    {
                        dgv.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Selected = true;
                    }
                }
            }
        }

        public static bool Excel(DataGridView dgv, string fileName)
        {


            return true;
        }
    }
}
