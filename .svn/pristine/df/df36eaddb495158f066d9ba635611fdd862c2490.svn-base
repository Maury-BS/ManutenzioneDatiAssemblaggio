using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Metra.ManutenzioneDatiAssemblaggio
{
    public class DsBridge
    {
        static bool CONF_COLONNE = false;

        public class DsDataRow
        {
            #region Variabili pubbliche

            public System.Windows.Forms.ErrorProvider ErrorProvider;

            #endregion

            #region Variabili private

            private DataTable _dt;
            private DataRow _dr;
            private List<DsDataRowField> _aFields;

            #endregion

            #region Costruttore

            public DsDataRow(DataTable dt, DataRow dr)
            {
                _dt = dt;
                _dr = dr;
                _aFields = new List<DsDataRowField>();
            }

            #endregion

            #region Proprietà

            public DataTable DataTable
            {
                get { return _dt; }
                set { _dt = value; }
            }

            public DataRow DataRow
            {
                get { return _dr; }
                set { _dr = value; }
            }

            #endregion

            #region Metodi pubblici

            /// <summary>
            /// Oggetto DsDataRowField associato all'ultimo controllo accodato
            /// </summary>
            /// <param name="c">Controllo</param>
            /// <returns></returns>
            public DsDataRowField LastField()
            {
                if (_aFields.Count > 0) return _aFields[_aFields.Count - 1];
                else return null;
            }

            /// <summary>
            /// Oggetto DsDataRowField associato al controllo
            /// </summary>
            /// <param name="c">Controllo</param>
            /// <returns></returns>
            public DsDataRowField Fields(Control c)
            {
                int i = 0;
                for (i = 0; i < _aFields.Count; i++)
                {
                    if (_aFields[i].Control == c) break;
                }
                if (i < _aFields.Count) return _aFields[i];
                else return null;
            }

            /// <summary>
            /// Oggetto DsDataRowField associato al campo
            /// </summary>
            /// <param name="FieldName">Nome del campo</param>
            /// <returns></returns>
            public DsDataRowField Fields(string FieldName)
            {
                int i = 0;
                for (i = 0; i < _aFields.Count; i++)
                {
                    if (_aFields[i].Field == FieldName) break;
                }
                if (i < _aFields.Count) return _aFields[i];
                else return null;
            }

            /// <summary>
            /// Aggiunta associazione campo-controllo. Il campo viene desunto dal nome del controllo, escluse le
            /// prime 3 lettere (usate per qualificare il controllo stesso)
            /// </summary>
            /// <param name="c">Controllo da associare</param>
            /// <returns></returns>
            public bool AddField(Control c)
            {
                return AddField(c, "", false, false, true);
            }

            /// <summary>
            /// Aggiunta associazione campo-controllo. Il campo viene desunto dal nome del controllo, escluse le
            /// prime 3 lettere (usate per qualificare il controllo stesso)
            /// </summary>
            /// <param name="c">Controllo da associare</param>
            /// <param name="Required">Impostare a true se il campo è necessario in fase di validazione</param>
            /// <returns></returns>
            public bool AddField(Control c, bool Required)
            {
                return AddField(c, "", Required, false, true);
            }

            /// <summary>
            /// Aggiunta associazione campo-controllo. Il campo viene desunto dal nome del controllo, escluse le
            /// prime 3 lettere (usate per qualificare il controllo stesso)
            /// </summary>
            /// <param name="c">Controllo da associare</param>
            /// <param name="Required">Impostare a true se il campo è necessario in fase di validazione</param>
            /// <param name="Locked">Impostare a true se il campo non è modificabile</param>
            /// <returns></returns>
            public bool AddField(Control c, bool Required, bool Locked)
            {
                return AddField(c, "", Required, Locked, true);
            }

            /// <summary>
            /// Aggiunta associazione campo-controllo
            /// </summary>
            /// <param name="c">Controllo da associare</param>
            /// <param name="Field">Nome del campo da associare</param>
            /// <returns></returns>
            public bool AddField(Control c, string Field)
            {
                return AddField(c, Field, false, false, true);
            }

            /// <summary>
            /// Aggiunta associazione campo-controllo
            /// </summary>
            /// <param name="c">Controllo da associare</param>
            /// <param name="Field">Nome del campo da associare</param>
            /// <param name="Required">Impostare a true se il campo è necessario in fase di validazione</param>
            /// <returns></returns>
            public bool AddField(Control c, string Field, bool Required)
            {
                return AddField(c, Field, Required, false, true);
            }

            /// <summary>
            /// Aggiunta associazione campo-controllo
            /// </summary>
            /// <param name="c">Controllo da associare</param>
            /// <param name="Field">Nome del campo da associare</param>
            /// <param name="Required">Impostare a true se il campo è necessario in fase di validazione</param>
            /// <param name="Locked">Impostare a true se il campo non è modificabile</param>
            /// <returns></returns>
            public bool AddField(Control c, string Field, bool Required, bool Locked)
            {
                return AddField(c, Field, Required, Locked, true);
            }

            /// <summary>
            /// Aggiunta associazione campo-controllo
            /// </summary>
            /// <param name="c">Controllo da associare</param>
            /// <param name="Field">Nome del campo da associare</param>
            /// <param name="Required">Impostare a true se il campo è necessario in fase di validazione</param>
            /// <param name="Locked">Impostare a true se il campo non è modificabile</param>
            /// <param name="AllowZeroLength">Impostare a true se è consentito testo a lunghezza zero</param>
            /// <returns></returns>
            public bool AddField(Control c, string FieldName, bool Required, bool Locked, bool AllowZeroLength)
            {
                //controllo esistenza campo con lo stesso nome del controllo (senza il prefisso) 
                string sFieldName;
                if (FieldName.Length == 0)
                {
                    sFieldName = c.Name.Substring(3);
                }
                else
                {
                    sFieldName = FieldName;
                }

                int indice = GetFieldIndex(sFieldName);

                if (sFieldName.Length > 0 & indice >= 0)
                {
                    //campo esistente o tabella non identificata
                    DsDataRowField Field = new DsDataRowField(c, _dr, indice);
                    Field.Required = Required;
                    Field.Locked = Locked;
                    Field.AllowZeroLength = AllowZeroLength;
                    Field.ErrorProvider = this.ErrorProvider;
                    _aFields.Add(Field);
                    return true;
                }
                else
                {
                    //campo non esistente
                    MessageBox.Show("Campo '" + sFieldName + "' non esistente!");
                    return false;
                }
            }

            /// <summary>
            /// Aggiunta associazione campo-radiobutton
            /// </summary>
            /// <param name="opt">RadioButton da associare</param>
            /// <param name="FieldName">Nome del campo da associare</param>
            /// <param name="RelatedValue">Valore intero correlato all'opzione</param>
            /// <returns></returns>
            public bool AddField(RadioButton opt, string FieldName, int RelatedValue)
            {
                //radio button con valore collegato di tipo INTEGER 
                int iIndice = GetFieldIndex(FieldName);
                if (FieldName.Length > 0 & iIndice >= 0)
                {
                    Control c = (Control)opt;
                    DsDataRowField Field = new DsDataRowField(c, _dr, iIndice);
                    Field.RelatedValueInteger = RelatedValue;
                    _aFields.Add(Field);
                    return true;
                }
                else
                {
                    return false;
                }
            }

            /// <summary>
            /// Aggiunta associazione campo-radiobutton
            /// </summary>
            /// <param name="opt">RadioButton da associare</param>
            /// <param name="FieldName">Nome del campo da associare</param>
            /// <param name="RelatedValue">Valore stringa correlato all'opzione</param>
            /// <returns></returns>
            public bool AddField(RadioButton opt, string FieldName, string RelatedValue)
            {
                //radio button con valore collegato di tipo STRING 
                int iIndice = GetFieldIndex(FieldName);
                if (FieldName.Length > 0 & iIndice >= 0)
                {
                    Control c = (Control)opt;
                    DsDataRowField Field = new DsDataRowField(c, _dr, iIndice);
                    Field.RelatedValueString = RelatedValue;
                    _aFields.Add(Field);
                    return true;
                }
                else
                {
                    return false;
                }
            }

            /// <summary>
            /// Pulizia dei controlli associati
            /// </summary>
            public void Clear()
            {
                foreach (DsDataRowField Field in _aFields)
                {
                    Field.Clear();
                }
            }

            /// <summary>
            /// Caricamento dati da datatable a controlli
            /// </summary>
            /// <returns>Esito dell'operazione</returns>
            public Result Load()
            {
                Result err = new Result();

                //se è nuova non faccio Accept altrimenti poi non riesco a salvare
                if (_dr.RowState != DataRowState.Added & _dr.RowState != DataRowState.Detached) _dr.AcceptChanges();

                //carico i controlli 
                foreach (DsDataRowField Field in _aFields)
                {
                    bool b = Field.Load();
                    if (!b)
                    {
                        err.Ok = false;
                        err.Descrizione += Field.Field + ";";
                    }
                }

                return err;
            }

            /// <summary>
            /// Salvataggio dati dai controlli a datatable
            /// </summary>
            /// <returns>Esito dell'operazione</returns>
            public Result Save()
            {
                Result ret = new Result();
                ret.Descrizione = "";
                ret.Function = "SaveDataRow";

                //controllo validità dati (in base al tipo) 
                foreach (DsDataRowField Field in _aFields)
                {
                    string s = Field.Check();
                    if (s.Length > 0)
                        ret.Descrizione += "\n - " + Field.FieldDescription + ": " + s;
                }
                if (ret.Descrizione.Length > 0)
                {
                    ret.Descrizione = "Attenzione:\n" + ret.Descrizione;
                }

                //se ok salvo i controlli 
                if (ret.Descrizione.Length == 0)
                {
                    bool ok;
                    foreach (DsDataRowField Field in _aFields)
                    {
                        ok = Field.Save();
                    }
                }

                //se ok controllo se deve essere aggiunta
                if (ret.Descrizione.Length == 0)
                {
                    if (_dr.RowState == DataRowState.Detached)
                    {
                        _dt.Rows.Add(_dr);
                    }
                }

                //ritorno eventuale errore 
                ret.Ok = ret.Descrizione.Length == 0;
                return ret;
            }

            /// <summary>
            /// Verifica della validità dei dati
            /// </summary>
            /// <returns>Restituisce false se tutti i dati sono validi</returns>
            public bool Check()
            {
                bool bOk = true;

                foreach (DsDataRowField c in this._aFields)
                {
                    bOk = bOk & c.Check() == "";
                }
                return bOk;
            }

            /// <summary>
            /// Lista dei campi modificati rispetto alla datatable
            /// </summary>
            /// <returns>Elenco dei campi variati</returns>
            public string ModifiedFields()
            {
                string sMod = "";

                //analizzo campi 
                foreach (DsDataRowField Field in _aFields)
                {
                    if (Field.IsModified())
                        sMod = sMod + ";" + Field.Field;
                }
                if (sMod.Length > 0) sMod = sMod.Substring(1);
                return sMod;
            }

            /// <summary>
            /// Controllo se ci sono modifiche nei controlli rispetto alla datatable
            /// </summary>
            /// <returns>True se ci sono modifiche, usare ModifiedFields per lista campi</returns>
            public bool IsModified()
            {
                //controllo campi 
                bool bFields = ModifiedFields().Length > 0;// _dr.RowState != DataRowState.Unchanged;

                //controllo tabelle child 
                //bool bTables = ModifiedChildTables() != "";

                return bFields;//| bTables;
            }

            #endregion

            #region Metodi privati

            private int GetFieldIndex(string sFieldName)
            {
                int iIndex = -1;
                for (int i = 0; i <= _dt.Columns.Count - 1; i++)
                {
                    if (_dt.Columns[i].ColumnName == sFieldName)
                    {
                        iIndex = i;
                        break;
                    }
                }
                //for (int i = 0; i <= _dr.Table.Columns.Count - 1; i++)
                //{
                //    if (_dr.Table.Columns[i].ColumnName == sFieldName)
                //    {
                //        iIndex = i;
                //        break;
                //    }
                //}
                return iIndex;
            }

            #endregion
        }

        public class DsDataRowField
        {
            public System.Windows.Forms.ErrorProvider ErrorProvider;
            DataRow _dr;
            Control _cControl;
            Color _cControlForeColor;
            Color _cControlBackColor;
            //Color _cErrorBackColor = Color.Orange;
            Color _cErrorBackColor = Color.FromArgb(255, 255, 128);

            int _iColIndex;
            bool _bRequired = false;
            bool _bLocked = false;
            bool _bAllowZeroLength = true;
            bool _bComboCheck = false;
            string _sFieldDescription;
            string _sDataSourceField = "";
            int _iRelatedType = 0; //1=string,2=integer 
            string _sRelatedValue = "";
            int _iRelatedValue = 0;
            string _sFormat = "";

            //---------------- 
            //inizializzazione 
            //---------------- 

            public DsDataRowField(Control Controllo, DataRow dr, int ColIndex)
            {
                //parametri 
                _cControl = Controllo;
                _cControlForeColor = Controllo.ForeColor;
                _cControlBackColor = Controllo.BackColor;
                _dr = dr;
                _iColIndex = ColIndex;
                _sFieldDescription = Field;

                //per textbox e combobox lunghezza max 
                if (_dr.Table.Columns[_iColIndex].MaxLength > 0)
                {
                    string sCtrlType = Control.GetType().ToString();
                    if (sCtrlType == "System.Windows.Forms.TextBox")
                    {
                        TextBox c = Controllo as TextBox;
                        if ((c != null))
                            c.MaxLength = _dr.Table.Columns[_iColIndex].MaxLength;
                    }
                    if (sCtrlType == "System.Windows.Forms.ComboBox")
                    {
                        ComboBox c = Controllo as ComboBox;
                        if ((c != null))
                            c.MaxLength = _dr.Table.Columns[_iColIndex].MaxLength;
                    }
                }

                //eventi
                _cControl.Validating += Control_Validating;
                _cControl.KeyPress += Control_KeyPress;
            }

            //--------- 
            //proprietà 
            //--------- 

            public Control Control
            {
                get { return _cControl; }
            }

            public Color ControlForeColor
            {
                get { return _cControlForeColor; }
            }

            public Color ControlBackColor
            {
                get { return _cControlBackColor; }
            }

            public string Field
            {
                get
                {
                    return _dr.Table.Columns[_iColIndex].ColumnName;
                }
            }

            public string FieldDescription
            {
                get { return _sFieldDescription; }
                set { _sFieldDescription = value; }
            }

            public object DataSource
            {
                get
                {
                    string sCtrlType = _cControl.GetType().ToString();
                    if (sCtrlType == "System.Windows.Forms.ComboBox")
                    {
                        return ((ComboBox)_cControl).DataSource;
                    }
                    else
                    {
                        return null;
                    }
                }
                set
                {
                    string sCtrlType = _cControl.GetType().ToString();
                    if (sCtrlType == "System.Windows.Forms.ComboBox")
                    {
                        ((ComboBox)_cControl).DataSource = value;
                    }
                }
            }

            public string DataSourceField
            {
                get { return _sDataSourceField; }
                set { _sDataSourceField = value; }
            }

            public string DisplayMember
            {
                get
                {
                    string sCtrlType = _cControl.GetType().ToString();
                    if (sCtrlType == "System.Windows.Forms.ComboBox")
                    {
                        return ((ComboBox)_cControl).DisplayMember;
                    }
                    else
                    {
                        return null;
                    }
                }
                set
                {
                    string sCtrlType = _cControl.GetType().ToString();
                    if (sCtrlType == "System.Windows.Forms.ComboBox")
                    {
                        ((ComboBox)_cControl).DisplayMember = value;
                    }
                }
            }

            public int Index
            {
                get { return _iColIndex; }
            }

            public bool Required
            {
                get { return _bRequired; }
                set { _bRequired = value; }
            }

            public bool Locked
            {
                get { return _bLocked; }
                set
                {
                    _bLocked = value;
                    _cControl.TabStop = !_bLocked;
                }
            }

            public bool AllowZeroLength
            {
                get { return _bAllowZeroLength; }
                set { _bAllowZeroLength = value; }
            }

            public int RelatedValueInteger
            {
                get { return _iRelatedValue; }
                set
                {
                    _iRelatedValue = value;
                    _iRelatedType = 2;
                }
            }

            public string RelatedValueString
            {
                get { return _sRelatedValue; }
                set
                {
                    _sRelatedValue = value;
                    _iRelatedType = 1;
                }
            }

            public bool RelatedIsString
            {
                get { return _iRelatedType == 1; }
            }

            public bool RelatedIsInteger
            {
                get { return _iRelatedType == 2; }
            }

            public string Format
            {
                get { return _sFormat; }
                set { _sFormat = value; }
            }

            public bool ComboCheck
            {
                get { return _bComboCheck; }
                set { _bComboCheck = value; }
            }

            //--------------- 
            //gestione eventi 
            //--------------- 

            private void Control_Validating(object sender, System.ComponentModel.CancelEventArgs e)
            {
                //eseguo su validating del controllo 
                Control c = sender as Control;
                if ((c != null))
                {

                    string sErr = this.Check();
                    if (ErrorProvider == null)
                    {
                        if (sErr.Length == 0)
                        {
                            //salvo in datarow
                            this.Save();
                            c.BackColor = this.ControlBackColor;
                        }
                        else
                        {
                            c.BackColor = _cErrorBackColor;
                        }
                    }
                    else
                    {
                        ErrorProvider.SetError(c, sErr);
                    }
                }
            }

            private void Control_KeyPress(object sender, KeyPressEventArgs e)
            {
                if (_bLocked) e.KeyChar = (char)0;
            }

            //------------------- 
            //controllo contenuti 
            //------------------- 

            public string Check()
            {
                //variabile per errore
                string sErr = "";

                //controllo per combobox
                string sCtrlType = _cControl.GetType().ToString();
                if (sCtrlType == "System.Windows.Forms.ComboBox" & _bComboCheck)
                {
                    ComboBox cmb = (ComboBox)_cControl;
                    if (cmb.DataSource != null & cmb.Text.Length > 0)
                    {
                        string s = cmb.DisplayMember + "='" + cmb.Text + "'";
                        try
                        {
                            DataTable dt = (DataTable)cmb.DataSource;
                            bool b = dt.Select(s).Length > 0;
                            if (!b) sErr = "Valore non ammesso";
                        }
                        catch (Exception)
                        { }
                    }
                    cmb.SelectionStart = 0;
                    cmb.SelectionLength = 0;
                }

                //controllo validità dato
                if (sErr.Length == 0)
                {
                    if (sCtrlType == "System.Windows.Forms.ComboBox" & DisplayMember != DataSourceField)
                    {
                        sErr = "";
                    }
                    else
                    {
                        sErr = DsFormUtils.CheckField(this.Control, _dr.Table.Columns[_iColIndex], _sFormat);
                    }
                }

                //controllo se dato è richiesto
                if (sErr.Length == 0 & this._bRequired & this._cControl.Text.ToString() == "")
                {
                    sErr = "Richiesto dato";
                    this.Control.BackColor = _cErrorBackColor;
                }

                //colore sfondo
                if (sErr.Length == 0)
                {
                    this.Control.BackColor = this._cControlBackColor;
                }
                else
                {
                    this.Control.BackColor = _cErrorBackColor;
                }

                //esito
                return sErr;
            }

            public void Clear()
            {
                //azzero il controllo 
                string sCtrlType = this.Control.GetType().ToString();
                switch (sCtrlType)
                {
                    case "System.Windows.Forms.CheckBox":
                        CheckBox chk = (CheckBox)this.Control;
                        chk.Checked = false;
                        break;
                    case "System.Windows.Forms.RadioButton":
                        RadioButton opt = (RadioButton)this.Control;
                        opt.Checked = false;
                        break;
                    default:
                        this.Control.Text = "";
                        break;
                }
            }

            public bool IsModified()
            {
                //controllo se il valore del controllo è cambiato 
                string sCtrl = _cControl.GetType().ToString();
                bool bMod = false;
                switch (sCtrl)
                {
                    case "System.Windows.Forms.TextBox":
                    case "System.Windows.Forms.MaskedTextBox":
                    case "System.Windows.Forms.NumericUpDown":
                    case "System.Windows.Forms.ComboBox":
                    case "System.Windows.Forms.DateTimePicker":

                        string sText = _cControl.Text;
                        if (sCtrl == "System.Windows.Forms.ComboBox") sText = GetComboRelatedText().ToString();

                        switch (_dr.Table.Columns[_iColIndex].DataType.ToString())
                        {
                            case "System.DateTime":
                                System.DateTime d1 = DateTime.MinValue;
                                System.DateTime d2 = DateTime.MinValue;

                                if (_dr[_iColIndex].ToString() != "")
                                {
                                    d1 = (System.DateTime)_dr[_iColIndex];
                                }

                                try
                                {
                                    d2 = Convert.ToDateTime(sText);
                                }
                                catch (Exception)
                                {
                                }

                                //arrotondo i millisecondi
                                d1 = d1.AddMilliseconds(-d1.Millisecond);
                                d2 = d2.AddMilliseconds(-d2.Millisecond);

                                if (d1 != d2) bMod = true;

                                break;
                            case "System.Int16":
                            case "System.Int32":
                            case "System.Int64":
                                Int64 i = 0;
                                if (_dr[_iColIndex] != DBNull.Value)
                                {
                                    i = Convert.ToInt64(_dr[_iColIndex]);
                                    if (_sFormat.Length > 0)
                                    {
                                        if (i.ToString(_sFormat) != sText) bMod = true;
                                    }
                                    else
                                    {
                                        if (i.ToString() != sText) bMod = true;
                                    }
                                }
                                else
                                {
                                    if (sText != "") bMod = true;
                                }
                                break;
                            case "System.Single":
                            case "System.Double":
                            case "System.Decimal":
                                double d = 0;
                                if (_dr[_iColIndex] != DBNull.Value)
                                {
                                    d = Convert.ToDouble(_dr[_iColIndex]);
                                    if (_sFormat.Length > 0)
                                    {
                                        if (d.ToString(_sFormat) != sText) bMod = true;
                                    }
                                    else
                                    {
                                        if (d.ToString() != sText) bMod = true;
                                    }
                                }
                                else
                                {
                                    if (sText != "") bMod = true;
                                }
                                break;
                            default:
                                if (_dr[_iColIndex].ToString() != sText) bMod = true;
                                break;
                        }
                        break;
                    case "System.Windows.Forms.CheckBox":
                        System.Windows.Forms.CheckBox chk;
                        chk = (CheckBox)_cControl;
                        bool b = false;
                        if (_dr[_iColIndex] != DBNull.Value) b = Convert.ToBoolean(_dr[_iColIndex]);
                        if (b != chk.Checked) bMod = true;

                        break;
                    case "System.Windows.Forms.RadioButton":
                        RadioButton opt;
                        opt = (RadioButton)_cControl;
                        if (_iRelatedType == 1)
                        {
                            if (opt.Checked)
                            {
                                if ((string)_dr[_iColIndex] != _sRelatedValue) bMod = true;
                            }
                        }
                        else if (_iRelatedType == 2)
                        {
                            if (opt.Checked)
                            {
                                if ((int)_dr[_iColIndex] != _iRelatedValue) bMod = true;
                            }
                        }

                        break;
                }
                return bMod;
            }

            public bool Empty()
            {
                bool bEmpty = false;
                string sCtrlType = _cControl.GetType().ToString();
                switch (sCtrlType)
                {
                    case "System.Windows.Forms.TextBox":
                    case "System.Windows.Forms.MaskedTextBox":
                    case "System.Windows.Forms.NumericUpDown":
                    case "System.Windows.Forms.ComboBox":
                        if (_cControl.Text == "")
                            bEmpty = true;

                        break;
                    case "System.Windows.Forms.CheckBox":
                        break;
                    case "System.Windows.Forms.RadioButton":
                        break;
                }
                return bEmpty;
            }

            //---- 
            //dati 
            //---- 

            public bool Save()
            {
                Type t = _cControl.GetType();
                string sCtrlType = _cControl.GetType().ToString();
                string sDataType = _dr[_iColIndex].GetType().ToString();
                bool Salva;

                switch (sCtrlType)
                {
                    case "System.Windows.Forms.TextBox":
                    case "System.Windows.Forms.MaskedTextBox":
                    case "System.Windows.Forms.NumericUpDown":
                        Salva = false;
                        switch (sDataType)
                        {
                            case "System.Int16":
                            case "System.Int32":
                            case "System.Int64":
                                if (_cControl.Text == "") _cControl.Text = "0";
                                Salva = Convert.ToInt64(_dr[_iColIndex]) != Convert.ToInt64(_cControl.Text);
                                break;
                            case "System.Decimal":
                                if (_cControl.Text.Length == 0) _cControl.Text = "0";
                                Salva = Convert.ToDouble(_dr[_iColIndex]) != Convert.ToDouble(_cControl.Text);
                                break;
                            case "System.String":
                            case "System.Guid":
                            default:
                                Salva = _dr[_iColIndex].ToString() != _cControl.Text;
                                break;
                        }
                        if (Salva)
                        {
                            if (sDataType == "System.Guid" && _cControl.Text == "")
                            {
                                _dr[_iColIndex] = DBNull.Value;
                            }
                            else if (sDataType == "System.DateTime")
                            {
                                try
                                {
                                    _dr[_iColIndex] = Convert.ToDateTime(_cControl.Text);
                                }
                                catch
                                {
                                    _dr[_iColIndex] = DBNull.Value;
                                }
                            }
                            else
                            {
                                _dr[_iColIndex] = _cControl.Text;
                            }
                        }
                        break;
                    case "System.Windows.Forms.DateTimePicker":
                        Salva = false;
                        if (_dr[_iColIndex] != DBNull.Value)
                        {
                            DateTime Old = Convert.ToDateTime(_dr[_iColIndex]);
                            Salva = Old != ((DateTimePicker)_cControl).Value;
                        }
                        else
                        {
                            Salva = true;
                        }
                        if (Salva)
                        {
                            _dr[_iColIndex] = ((DateTimePicker)_cControl).Value;
                        }
                        break;
                    case "System.Windows.Forms.ComboBox":
                        object Val = GetComboRelatedText();
                        Salva = false;
                        if (_dr[_iColIndex] != null)
                        {
                            Salva = _dr[_iColIndex].ToString() != Val.ToString();
                        }
                        if (Salva)
                        {
                            if (Val == null || Val.ToString() == "")
                            {
                                _dr[_iColIndex] = DBNull.Value;
                            }
                            else
                            {
                                _dr[_iColIndex] = Val;
                            }
                        }
                        break;
                    case "System.Windows.Forms.CheckBox":
                        CheckBox chk;
                        chk = (CheckBox)_cControl;
                        Salva = false;
                        if (_dr[_iColIndex] != null)
                        {
                            if (_dr[_iColIndex] != DBNull.Value)
                            {
                                Salva = Convert.ToBoolean(_dr[_iColIndex]) != chk.Checked; ;
                            }
                            else
                            {
                                Salva = true;
                            }
                        }
                        if (Salva)
                        {
                            _dr[_iColIndex] = chk.Checked;
                        }
                        break;
                    case "System.Windows.Forms.RadioButton":
                        RadioButton opt;
                        opt = (RadioButton)_cControl;
                        if (_iRelatedType == 1)
                        {
                            //stringa 
                            if (opt.Checked)
                            {
                                _dr[_iColIndex] = _sRelatedValue;
                            }
                        }
                        else if (_iRelatedType == 2)
                        {
                            //intero 
                            if (opt.Checked)
                            {
                                _dr[_iColIndex] = _iRelatedValue;
                            }
                        }
                        break;
                }
                return true;
            }

            public bool Load()
            {
                string sCtrlType = _cControl.GetType().ToString();
                switch (sCtrlType)
                {
                    case "System.Windows.Forms.TextBox":
                    case "System.Windows.Forms.MaskedTextBox":
                    case "System.Windows.Forms.NumericUpDown":
                    case "System.Windows.Forms.DateTimePicker":

                        _cControl.Text = _dr[_iColIndex].ToString();
                        DsFormUtils.CheckField(_cControl, _dr.Table.Columns[_iColIndex], _sFormat);
                        break;

                    case "System.Windows.Forms.ComboBox":

                        string s = GetComboText();
                        if (s.Length > 0)
                        {
                            _cControl.Text = s;

                        }
                        else
                        {
                            _cControl.Text = null;
                        }
                        DsFormUtils.CheckField(_cControl, _dr.Table.Columns[_iColIndex], _sFormat);

                        //valore eventuale textbox collegato per stato readonly
                        if (_cControl.Tag != null && _cControl.Tag.GetType().ToString() == typeof(TextBox).ToString())
                        {
                            ((TextBox)_cControl.Tag).Text = s;
                        }

                        break;

                    case "System.Windows.Forms.CheckBox":

                        CheckBox chk;
                        chk = (CheckBox)_cControl;
                        if (_dr[_iColIndex] == DBNull.Value) chk.Checked = false;
                        else chk.Checked = (bool)_dr[_iColIndex];
                        break;

                    case "System.Windows.Forms.RadioButton":

                        RadioButton opt;
                        opt = (RadioButton)_cControl;
                        if (_iRelatedType == 1)
                        {
                            opt.Checked = _dr[_iColIndex].ToString() == _sRelatedValue;
                        }
                        else if (_iRelatedType == 2)
                        {
                            opt.Checked = _dr[_iColIndex].ToString() == _iRelatedValue.ToString();
                        }
                        break;
                }
                return true;
            }

            public string GetComboText()
            {
                //recupero il testo da visualizzare 
                string Ret = _dr[_iColIndex].ToString();
                ComboBox cmb = (ComboBox)_cControl;
                if (cmb.DisplayMember.Length != 0 & DataSourceField.Length != 0 & cmb.DisplayMember != DataSourceField)
                {
                    if (cmb.DataSource != null)
                    {
                        try
                        {
                            DataTable dt = (DataTable)cmb.DataSource;
                            if (dt != null)
                            {
                                string s = DataSourceField + "=";
                                switch (dt.Columns[cmb.DisplayMember].DataType.ToString())
                                {
                                    case "System.String":
                                        s += "'" + Ret + "'";
                                        break;
                                    default:
                                        s += Ret;
                                        break;
                                }
                                DataRow dr = dt.Select(s)[0];
                                Ret = dr[DisplayMember].ToString();
                            }
                        }
                        catch (Exception)
                        {
                            //Ret = null;
                        }
                    }
                }
                return Ret;
            }

            public object GetComboRelatedText()
            {
                //recupero il testo da salvare
                object Ret = _cControl.Text;
                ComboBox cmb = (ComboBox)_cControl;
                if (cmb.DisplayMember.Length != 0 & DataSourceField.Length != 0 & cmb.DisplayMember != Field & cmb.DataSource != null)
                {
                    try
                    {
                        DataTable dt = (DataTable)cmb.DataSource;
                        string s = cmb.DisplayMember + "=";
                        switch (dt.Columns[cmb.DisplayMember].DataType.ToString())
                        {
                            case "System.String":
                                s += "'" + cmb.Text.Replace("'", "''") + "'";
                                break;
                            default:
                                s += cmb.Text;
                                break;
                        }
                        DataRow dr = dt.Select(s)[0];
                        Ret = dr[DataSourceField];
                    }
                    catch (Exception)
                    {
                    }
                }
                return Ret;
            }
        }

        public class DsDataTable
        {
            private DataTable _dt;
            private DataView _dv;
            private string _RowIndexField;
            private string _LinkField;
            private object _LinkValue;
            private string _Guid;
            private DataGridView _dgGrid;
            private bool _dgGrid_AllColumnsReadOnly;
            //private ToolStripDropDownMenu _Menu;
            private Dictionary<string, object> _DefaultValues;
            private BindingSource _bindingSource;
            private bool _saveColumns = true;

            #region COSTRUTTORE

            public DsDataTable(DataGridView Grid, DataTable Table)
            {
                Init(Grid, Table, null, null, null);
            }

            public DsDataTable(DataGridView Grid, DataTable Table, string RowIndexField)
            {
                Init(Grid, Table, RowIndexField, null, null);
            }

            public DsDataTable(DataGridView Grid, DataTable Table, string RowIndexField, string LinkField, Object LinkValue)
            {
                Init(Grid, Table, RowIndexField, LinkField, LinkValue);
            }
        
            public DsDataTable(DataGridView Grid, BindingSource Source, DataTable Table, string RowIndexField, string LinkField, Object LinkValue)
            {
                _bindingSource = Source;
                Init(Grid, Table, RowIndexField, LinkField, LinkValue);
            }

            private void Init(DataGridView Grid, DataTable Table, string RowIndexField, string LinkField, Object LinkValue)
            {
                //inizializzazione parametri
                _dgGrid = Grid;
                _dt = Table;
                _LinkField = LinkField;
                _LinkValue = LinkValue;
                _RowIndexField = RowIndexField;
                _DefaultValues = new Dictionary<string, object>();
                if (DsBridge.DsFormUtils.GetColumn(_dt, _RowIndexField) == null) _RowIndexField = null;
                _dgGrid_AllColumnsReadOnly = true;
                foreach (DataGridViewColumn c in _dgGrid.Columns)
                {
                    _dgGrid_AllColumnsReadOnly &= c.ReadOnly;
                }

                //controllo campo id per eventuale assegnazione del guid
                if (DsBridge.DsFormUtils.GetColumn(_dt, "ID") != null)
                    if (_dt.Columns["ID"].DataType.ToString() == "System.Guid")
                        _Guid = "ID";

                //menu per conf. griglia
                //_Menu = new ToolStripDropDownMenu();
                //_Menu.Items.Add("Configurazione colonne", inVoice.Properties.Resources.SaveFormDesignHS, new EventHandler(DataGrid_ColumnsConfig));
                //_Menu.Items.Add("Salva configurazione colonne", inVoice.Properties.Resources.SaveFormDesignHS, new EventHandler(DataGrid_ColumnsConfigSave));
                //_Menu.Items.Add("Copia tutto", inVoice.Properties.Resources.CopyHS, new EventHandler(DataGrid_CopyToClipboard));
                //_Menu.Items.Add("Copia selezione", inVoice.Properties.Resources.CopyHS, new EventHandler(DataGrid_CopySelectionToClipboard));
                //_Menu.Items.Add(new ToolStripSeparator());
                //_Menu.Items.Add("Annulla");

                //eventi griglia
                _dgGrid.CellValidating += new DataGridViewCellValidatingEventHandler(DataGrid_CellValidating);
                _dgGrid.DataError += new DataGridViewDataErrorEventHandler(DataGrid_DataError);
                //_dgGrid.MouseClick += new MouseEventHandler(DataGrid_MouseClick);

                //eventi datatable
                _dt.TableNewRow += new DataTableNewRowEventHandler(_dt_TableNewRow);
                _dt.RowDeleted += new DataRowChangeEventHandler(_dt_RowDeleted);
            }

            #endregion

            #region PROPRIETA

            public DataTable DataTable
            {
                get { return _dt; }
                set
                {
                    //datatable e eventi
                    _dt.TableNewRow -= new DataTableNewRowEventHandler(_dt_TableNewRow);
                    _dt.RowDeleted -= new DataRowChangeEventHandler(_dt_RowDeleted);
                    _dt = value;
                    _dt.TableNewRow += new DataTableNewRowEventHandler(_dt_TableNewRow);
                    _dt.RowDeleted += new DataRowChangeEventHandler(_dt_RowDeleted);
                }
            }

            //public ToolStripDropDownMenu DropDownMenu
            //{
            //    get { return _Menu; }
            //}

            public bool SaveColumns
            {
                get { return _saveColumns; }
                set { _saveColumns = value; }
            }

            #endregion

            #region METODI PUBBLICI

            public void Clear()
            {
                _dgGrid.CellValidating -= DataGrid_CellValidating;
                _dt.Clear();
                _dgGrid.DataSource = null;
                _dgGrid.Columns.Clear();
            }

            public Result Save()
            {
                //devo aggiornare i campi di collegamento/ordinamento 
                Result ris = new Result();
                ris.Function = "SaveDataTable";

                //salvo larghezza colonne
                if (_saveColumns)
                {
                    DsFormUtils.DataGridSaveColumnWidth(_dgGrid, _dgGrid.Tag);
                }

                //esito
                ris.Ok = true;
                return ris;
            }

            public void Load()
            {
                Load("", true);
            }

            public void Load(bool EnableSelect)
            {
                Load("", EnableSelect);
            }

            public void Load(string sCrit, bool EnableSelect)
            {
                //se non esistono colonne le autogenero
                _dgGrid.AutoGenerateColumns = _dgGrid.Columns.Count == 0;

                //layout (selezione, colori...)
                DsBridge.DsFormUtils.DataGridViewLayout(_dgGrid, _dgGrid_AllColumnsReadOnly, EnableSelect);

                //selezione righe
                if (_bindingSource != null)
                {
                    _dgGrid.DataSource = _bindingSource;
                }
                else
                if (sCrit.Length > 0)
                {
                    _dv = new DataView(_dt, sCrit, _RowIndexField, DataViewRowState.CurrentRows);
                    _dgGrid.DataSource = _dv;
                }
                else
                {
                    _dgGrid.DataSource = _dt;
                }

                //larghezza colonne
                if (_saveColumns)
                {
                    DsBridge.DsFormUtils.DataGridLoadColumnWidth(_dgGrid, _dgGrid.Tag);
                }

                //convalido le modifiche
                if (_dv != null)
                {
                    for (int i = 0; i <= _dv.Count - 1; i++)
                    {
                        if (_dv[i].Row.RowState != DataRowState.Added)
                            _dv[i].Row.AcceptChanges();
                    }
                }
                else
                {
                    for (int i = 0; i <= _dt.Rows.Count - 1; i++)
                    {
                        if (_dt.Rows[i].RowState != DataRowState.Added)
                            _dt.Rows[i].AcceptChanges();
                    }
                }
            }

            public bool IsModified()
            {
                //controllo se esistono righe modificate 
                bool bMod = false;

                if (_dv != null)
                {
                    foreach (DataRowView r in this._dv)
                    {
                        if (r.Row.RowState != DataRowState.Unchanged)
                        {
                            bMod = true;
                            break;
                        }
                    }
                }
                else
                {
                    foreach (DataRow r in _dt.Rows)
                    {
                        if (r.RowState != DataRowState.Unchanged)
                        {
                            bMod = true;
                            break;
                        }
                    }
                }
                return bMod;
            }

            public bool MoveLine(int Direction)
            {
                if (Direction > 0) Direction = 1;
                if (Direction < 0) Direction = -1;
                if (_RowIndexField.Length > 0 & Direction != 0)
                {
                    Point pCell = _dgGrid.CurrentCellAddress;
                    int iCurr = pCell.Y;
                    int iSwap = iCurr + Direction;

                    if (iSwap >= 0 & iSwap < _dgGrid.Rows.Count - 1)
                    {
                        //scambio riga con quella adiacente
                        //int i = (int)((DataRowView)_dgGrid.Rows[iCurr].DataBoundItem)[_RowIndexField];
                        //((DataRowView)_dgGrid.Rows[iCurr].DataBoundItem)[_RowIndexField] = ((DataRowView)_dgGrid.Rows[iSwap].DataBoundItem)[_RowIndexField];
                        //((DataRowView)_dgGrid.Rows[iSwap].DataBoundItem)[_RowIndexField] = i;

                        int i = (int)_dgGrid.Rows[iCurr].Cells[_RowIndexField].Value;
                        _dgGrid.Rows[iCurr].Cells[_RowIndexField].Value = _dgGrid.Rows[iSwap].Cells[_RowIndexField].Value;
                        _dgGrid.Rows[iSwap].Cells[_RowIndexField].Value = i;

                        //riordino
                        _dgGrid.Sort(_dgGrid.Columns[_RowIndexField], System.ComponentModel.ListSortDirection.Ascending);
                        _dgGrid.Refresh();

                        //ricalcolo ordinamento
                        for (int j = 0; j < _dgGrid.Rows.Count; j++)
                            _dgGrid.Rows[j].Cells[_RowIndexField].Value = j;

                        //seleziono riga
                        _dgGrid.CurrentCell = _dgGrid.Rows[pCell.Y + Direction].Cells[pCell.X];
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            public void ClearDefaultValues()
            {
                _DefaultValues.Clear();
            }

            public void AddDefaultValue(string fieldName, object defaultValue)
            {
                _DefaultValues.Add(fieldName, defaultValue);
            }

            public void UpdateDefaultValue(string fieldName, object defaultValue)
            {
                try
                {
                    _DefaultValues[fieldName] = defaultValue;
                }
                catch { }
            }

            #endregion

            #region METODI PRIVATI

            //void DataGrid_CopySelectionToClipboard(object sender, EventArgs e)
            //{
            //    Utils.DgvExport.ToClipboard(_dgGrid, Utils.DgvExport.ExportMode.Selection);
            //}

            //void DataGrid_CopyToClipboard(object sender, EventArgs e)
            //{
            //    Utils.DgvExport.ToClipboard(_dgGrid, Utils.DgvExport.ExportMode.All);
            //}

            //void DataGrid_ColumnsConfig(object sender, EventArgs e)
            //{
            //    Utils.DgvColumnConfiguration.Show(_dgGrid);
            //}

            //void DataGrid_ColumnsConfigSave(object sender, EventArgs e)
            //{
            //    DsFormUtils.DataGridSaveColumnWidth(_dgGrid, _dgGrid.Tag);
            //}

            //void DataGrid_MouseClick(object sender, MouseEventArgs e)
            //{
            //    if (e.Button == MouseButtons.Right && !_dgGrid.ReadOnly) // && e.X - _dgGrid.Left < _dgGrid.RowHeadersWidth - 5 && e.Y - _dgGrid.Top < _dgGrid.ColumnHeadersHeight - 5)
            //    {
            //        _Menu.Show(_dgGrid, e.Location);
            //    }
            //}

            void _dt_RowDeleted(object sender, DataRowChangeEventArgs e)
            {
                if (_RowIndexField != null)
                {
                    //ricalcolo posizioni
                    int i = 0;
                    foreach (DataRow dr in _dt.Rows)
                    {
                        if (dr.RowState != DataRowState.Deleted) dr[_RowIndexField] = i++;
                    }
                }
            }

            void _dt_TableNewRow(object sender, DataTableNewRowEventArgs e)
            {
                if (_Guid != null) e.Row["ID"] = Guid.NewGuid();
                if (_LinkField != null) e.Row[_LinkField] = _LinkValue;
                if (_RowIndexField != null) e.Row[_RowIndexField] = e.Row.Table.Rows.Count + 1;
                foreach (KeyValuePair<string, object> kvp in _DefaultValues)
                {
                    e.Row[kvp.Key] = kvp.Value;
                }
            }

            #endregion

            #region EVENTI DATAGRID

            private void DataGrid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
            {
                //se la cella è sola lettura non controllo
                if (_dgGrid.Columns[e.ColumnIndex].ReadOnly == true) return;

                //controllo valore proposto (eventualmente viene riformattato) 
                string sTxt = e.FormattedValue.ToString();
                Control ctrl;
                if (_dgGrid.Columns[e.ColumnIndex].CellType.ToString() == "System.Windows.Forms.DataGridViewComboBoxCell") 
                {
                    ctrl = new ComboBox();
                }
                else
                {
                    ctrl = new TextBox();
                }
                ctrl.Text = sTxt;

                string format = "";
                //if (_dt.Columns[_dgGrid.Columns[e.ColumnIndex].DataPropertyName].DataType.ToString()=="System.Decimal")
                //{
                //    format = "N3";
                //}
                string sErr = DsFormUtils.CheckField(ctrl, _dt.Columns[_dgGrid.Columns[e.ColumnIndex].DataPropertyName], format);
                string sTxtNew = ctrl.Text;

                //controllo eventuale errore 
                if (sErr.Length > 0)
                {
                    //valore errato: msg e annullo operzione validazione 
                    MessageBox.Show(sErr, null, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    e.Cancel = true;
                }
                else
                {
                    //valore ok, controllo se è stato riformattato 
                    //If dgGrid.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString <> txt.Text Then 
                    if (sTxt != sTxtNew)
                    {
                        //riporto valore riformattato (per numeri/date) 
                        if (_dgGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].IsInEditMode)
                        {
                            _dgGrid.CancelEdit();
                            _dgGrid.EndEdit();
                        }
                        _dgGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = sTxtNew;
                    }
                }
            }

            private void DataGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
            {
                DataGridView dg = (DataGridView)sender;
                string s = "Errore: " + (e.ColumnIndex >=0 ? dg.Columns[e.ColumnIndex].DataPropertyName : "");
                if (e.Exception != null)
                {
                    s += "\n" + e.Exception.Message;
                    MessageBox.Show(s, null, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                e.Cancel = true;
            }

            #endregion
        }

        public class DsFormUtils
        {
            public static DataColumn GetColumn(DataTable dt, string ColumnName)
            {
                try
                {
                    return dt.Columns[ColumnName];
                }
                catch (Exception)
                {
                    return null;
                }
            }

            public static void LockControl(Control Control)
            {
                Control.TabStop = false;
                Control.KeyPress += new KeyPressEventHandler(Control_KeyPress);
            }

            static void Control_KeyPress(object sender, KeyPressEventArgs e)
            {
                e.KeyChar = (char)0;
            }

            public static string CheckField(Control Control, DataColumn Column, string sFormat)
            {
                //controllo che il contenuto del controllo sia compatibile con la colonna (tipo di dati) 
                string sCtrlType;
                string sDataType;
                string sOut = "";

                if (Column == null)
                    return "";

                sCtrlType = Control.GetType().ToString();
                sDataType = Column.DataType.ToString();
                switch (sCtrlType)
                {
                    //case "System.Windows.Forms.ComboBox":
                    case "System.Windows.Forms.TextBox":
                    case "System.Windows.Forms.MaskedTextBox":
                    case "System.Windows.Forms.NumericUpDown":
                    case "System.Windows.Forms.DateTimePicker":
                        if (Control.Text.Length > 0)
                        {
                            //controllo la proprietà "text" 
                            switch (sDataType)
                            {
                                case "System.String":
                                    //stringa va sempre bene 
                                    if (sFormat.Length > 0)
                                        Control.Text = String.Format(Control.Text, sFormat);
                                    break;
                                case "System.Int16":
                                    //controllo tipo 
                                    try
                                    {
                                        //controllo la virgola 
                                        Control.Text = Control.Text.Replace(".", ",");
                                        Int16 i = Convert.ToInt16(Control.Text);
                                        if (Control.Text.Contains(","))
                                        {
                                            sOut = "Richiesto numero intero";
                                        }
                                        else
                                        {
                                            if (sFormat.Length > 0) Control.Text = i.ToString(sFormat);
                                            else Control.Text = i.ToString();
                                            //if (sFormat.Length > 0)
                                            //    Control.Text = String.Format(i.ToString(), sFormat);
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        sOut = "Richiesto numero";
                                    }

                                    break;
                                case "System.Int32":
                                    //controllo tipo 
                                    //if (Control.GetType().ToString())
                                    try
                                    {
                                        Control.Text = Control.Text.Replace(".", ",");
                                        Int32 i = Convert.ToInt32(Control.Text);
                                        if (Control.Text.Contains(","))
                                        {
                                            sOut = "Richiesto numero intero";
                                        }
                                        else
                                        {
                                            if (sFormat.Length > 0) Control.Text = i.ToString(sFormat);
                                            else Control.Text = i.ToString();
                                            //if (sFormat.Length > 0)
                                            //    Control.Text = String.Format(i.ToString(), sFormat);
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        sOut = "Richiesto numero";
                                    }

                                    break;
                                case "System.Int64":
                                    //controllo tipo 
                                    try
                                    {
                                        Control.Text = Control.Text.Replace(".", ",");
                                        Int64 i = Convert.ToInt64(Control.Text);
                                        if (Control.Text.Contains(","))
                                        {
                                            sOut = "Richiesto numero intero";
                                        }
                                        else
                                        {
                                            if (sFormat.Length > 0) Control.Text = i.ToString(sFormat);
                                            else Control.Text = i.ToString();
                                            //if (sFormat.Length > 0)
                                            //    Control.Text = String.Format(i.ToString(), sFormat);
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        sOut = "Richiesto numero";
                                    }

                                    break;
                                case "System.Single":
                                case "System.Double":
                                case "System.Decimal":
                                    //controllo tipo 
                                    try
                                    {
                                        if (!Control.Text.Contains(",") && Control.Text.Contains("."))
                                        {
                                            int pos = Control.Text.LastIndexOf(".");
                                            Control.Text = Control.Text.Substring(0, pos) + "," + Control.Text.Substring(pos + 1);
                                        }
                                        double i = Convert.ToDouble(Control.Text);
                                        if (sFormat.Length > 0) Control.Text = i.ToString(sFormat);
                                        else Control.Text = i.ToString();
                                        //Control.Text = String.Format(i.ToString(), sFormat);
                                    }
                                    catch (Exception)
                                    {
                                        sOut = "Richiesto numero";
                                    }

                                    break;
                                case "System.DateTime":
                                    //controllo ed eventualmente correggo formattazione 
                                    Control.Text = Control.Text.Replace(".", ":");
                                    string s = Control.Text;
                                    switch (s.ToUpper())
                                    {
                                        case "T":
                                            s = DateTime.Now.ToString();
                                            Control.Text = s;
                                            break;
                                        case "D":
                                            s = DateTime.Today.ToShortDateString();
                                            Control.Text = s;
                                            break;
                                    }
                                    //try
                                    //{
                                    //    DateTime d = Convert.ToDateTime(s);

                                    //    s = d.Day.ToString().PadLeft(2,'0');
                                    //    s += "/" + d.Month.ToString().PadLeft(2, '0');
                                    //    s += "/" + d.Year.ToString().PadLeft(4, '0');
                                    //    Control.Text = s;
                                    //    //Control.Text = Control.Text.Replace(" ", "/");
                                    //    //Control.Text = Control.Text.Replace("-", "/");
                                    //    //Control.Text = Control.Text.Replace(".", "/");

                                    //    //if (Information.IsNumeric(Control.Text) & Control.Text.Length == 4)
                                    //    //{
                                    //    //    Control.Text = Control.Text.Substring(1, 2) + "/" + Control.Text.Substring(3);
                                    //    //}
                                    //    //else if (Information.IsNumeric(Control.Text) & (Control.Text.Length == 6 | Control.Text.Length == 8)) ;
                                    //    //{
                                    //    //    Control.Text = Control.Text.Substring(1, 2) + "/" + Control.Text.Substring(3, 2) + "/" + Control.Text.Substring(5);
                                    //    //}
                                    //}
                                    //catch
                                    //{
                                    //    sOut = "Richiesta una data";
                                    //}


                                    //controllo tipo 
                                    try
                                    {
                                        DateTime d = Convert.ToDateTime(s.Replace(".",":"));

                                        //s = d.Day.ToString().PadLeft(2, '0');
                                        //s += "/" + d.Month.ToString().PadLeft(2, '0');
                                        //s += "/" + d.Year.ToString().PadLeft(4, '0');
                                        //Control.Text = s;

                                        //System.DateTime i = Convert.ToDateTime(Control.Text);
                                        //if (sFormat == "")
                                        //    sFormat = "Short Date";
                                        //Control.Text = String.Format(i.ToString(), sFormat);
                                    }
                                    catch (Exception)
                                    {
                                        sOut = "Richiesta data";
                                        //Control.Text = s;
                                    }

                                    break;
                                case "System.Boolean":
                                    //controllo tipo 
                                    try
                                    {
                                        bool i = Convert.ToBoolean(Control.Text);
                                    }
                                    catch (Exception)
                                    {
                                        sOut = "Richiesto booleano";
                                    }

                                    break;
                            }
                        }

                        break;
                }
                return sOut;
            }

            public static void DataGridViewLayout(DataGridView dg, bool StateReadOnly, bool EnableSelect)
            {
                //selettore righe
                dg.RowHeadersWidth = 24;

                //colori
                dg.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
                dg.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 230, 230);
                if (!EnableSelect)
                {
                    dg.AlternatingRowsDefaultCellStyle.SelectionBackColor = dg.DefaultCellStyle.SelectionBackColor;// dg.AlternatingRowsDefaultCellStyle.BackColor;
                    dg.DefaultCellStyle.SelectionForeColor = dg.DefaultCellStyle.ForeColor;
                    dg.DefaultCellStyle.SelectionBackColor = Color.FromArgb(215, 215, 215);// dg.DefaultCellStyle.BackColor;
                }
                dg.ForeColor = Color.Black;
                dg.DefaultCellStyle.SelectionForeColor = Color.Black;

                //font
                dg.Font = new Font(dg.Parent.Font.FontFamily, (float)10);
                dg.DefaultCellStyle.Font = new Font(dg.Parent.Font.FontFamily, (float)10);
                dg.RowHeadersDefaultCellStyle.Font = new Font(dg.Parent.Font.FontFamily, (float)10);
                dg.RowsDefaultCellStyle.Font = new Font(dg.Parent.Font.FontFamily, (float)10);
                dg.RowTemplate.DefaultCellStyle.Font = new Font(dg.Parent.Font.FontFamily, (float)10);
                for (int i = 0; i < dg.Columns.Count; i++)
                    dg.Columns[i].DefaultCellStyle.Font = new Font(dg.Parent.Font.FontFamily, (float)10);


                //modalità selezione
                if (StateReadOnly) dg.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                else dg.SelectionMode = DataGridViewSelectionMode.CellSelect;

            }

            public static void DataGridLoadColumnWidth(DataGridView DataGrid, object Suffix)
            {
                string sName = GetPath(DataGrid);
                if (Suffix != null) if (Suffix.ToString().Length > 0) sName += "." + Suffix.ToString();

                DbBridge db = new DbBridge();
                DataTable dt = new DataTable();
                if (CONF_COLONNE)
                {
                    string s = "SELECT * FROM Conf_Colonne";
                    s += " WHERE Controllo='" + sName + "'";
                    s += " ORDER BY Posizione";
                    db.ReadDataTable(dt, s);
                }

                int pos = 0;
                bool esisteAutosizeFill = false;
                if (dt.Rows.Count == 0)
                {
                    foreach (DataGridViewColumn c in DataGrid.Columns)
                    {
                        if (c.DataPropertyName.Length == 0) c.DataPropertyName = c.Name;
                        if (c.AutoSizeMode == DataGridViewAutoSizeColumnMode.Fill) esisteAutosizeFill = true;
                    }
                }
                else
                {
                    List<string> colonneConfigurate = new List<string>();

                    foreach (DataRow dr in dt.Rows)
                    {
                        //nome colonna
                        string nome = dr["Nome"].ToString();
                        string intestazione = dr["Intestazione"].ToString();
                        string campo = dr["Campo"].ToString();
                        bool visibile = Convert.ToBoolean(dr["Visibile"]);
                        bool solaLettura = Convert.ToBoolean(dr["SolaLettura"]);
                        bool dimensionabile = Convert.ToBoolean(dr["Dimensionabile"]);
                        bool autosizeFill = Convert.ToBoolean(dr["AutosizeFill"]);
                        int larghezza = Convert.ToInt32(dr["Larghezza"]);
                        string formato = dr["Formato"].ToString();

                        //controllo se esiste già
                        DataGridViewColumn c = null;
                        for (int i = 0; i < DataGrid.ColumnCount; i++)
                        {
                            if (DataGrid.Columns[i].DataPropertyName == campo ||
                                (DataGrid.Columns[i].DataPropertyName.Length == 0 && DataGrid.Columns[i].Name == campo))
                            {
                                c = DataGrid.Columns[i];
                                break;
                            }
                        }

                        //se non esiste ed è richiesta visibile la creo
                        if (visibile && c == null)
                        {
                            DataGrid.Columns.Add(nome, intestazione);
                            DataGrid.Columns[nome].HeaderText = intestazione;
                            DataGrid.Columns[nome].DataPropertyName = campo;
                            c = DataGrid.Columns[nome];
                        }

                        //proprietà
                        if (c != null)
                        {
                            c.DataPropertyName = campo;
                            c.Tag = "OK";
                            c.Visible = visibile;
                            c.ReadOnly = solaLettura;
                            c.Resizable = (dimensionabile ? DataGridViewTriState.False : DataGridViewTriState.True);
                            c.MinimumWidth = 10;
                            c.Width = larghezza;
                            c.DisplayIndex = Math.Min(pos++, DataGrid.Columns.Count - 1);
                            c.HeaderText = intestazione;
                            if (autosizeFill) c.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                            if (formato.Length > 0) DsBridge.DsFormUtils.SetColumnFormat(c, formato);

                            if (c.AutoSizeMode == DataGridViewAutoSizeColumnMode.Fill) esisteAutosizeFill = true;
                        }

                        colonneConfigurate.Add(nome);
                    }

                    //nascondo colonne in eccesso (fissate da vs ma nascoste da utente)
                    foreach (DataGridViewColumn c in DataGrid.Columns)
                    {
                        //if (!colonneConfigurate.Contains(c.Name)) c.Visible = false;
                        if (c.Tag.ToString() != "OK") c.Visible = false;
                        if (c.DataPropertyName == null || c.DataPropertyName.Length == 0) c.DataPropertyName = c.Name;
                    }
                    colonneConfigurate.Clear();
                }

                //autosize=fill per ultima colonna se non esiste già
                if (DataGrid.Columns.Count > 0 && !esisteAutosizeFill) DataGrid.Columns[DataGrid.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                //refresh
                DataGrid.Refresh();

                //int iPos = 0;
                //string sLastVisibleColName = "";
                //foreach (DataRow dr in dt.Rows)
                //{
                //    string sColName = dr["Nome"].ToString();
                //    if (DataGrid.Columns.Contains(sColName) == true)
                //    {
                //        bool Visible = (bool)dr["Visibile"];
                //        if (Visible)
                //        {
                //            DataGrid.Columns[sColName].MinimumWidth = 2;
                //            DataGrid.Columns[sColName].HeaderText = dr["Intestazione"].ToString();
                //            DataGrid.Columns[sColName].DataPropertyName = dr["Campo"].ToString();
                //            try { DataGrid.Columns[sColName].DisplayIndex = iPos++; }
                //            catch { }
                //            DataGrid.Columns[sColName].Width = (int)dr["Larghezza"];
                //            if ((bool)dr["Dimensionabile"]) DataGrid.Columns[sColName].Resizable = DataGridViewTriState.True;
                //            else DataGrid.Columns[sColName].Resizable = DataGridViewTriState.False;
                //            try { DataGrid.Columns[sColName].ReadOnly = (bool)dr["SolaLettura"]; }
                //            catch { }
                //            sLastVisibleColName = sColName;
                //        }
                //        DataGrid.Columns[sColName].Visible = (bool)dr["Visibile"];
                //    }
                //}

                //if (sLastVisibleColName.Length > 0)
                //{
                //    try
                //    {
                //        DataGrid.Columns[sLastVisibleColName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                //        DataGrid.Columns[sLastVisibleColName].MinimumWidth = 50;
                //    }
                //    catch { }
                //}

                //DataGrid.Refresh();
            }

            private static string GetPath(Control c)
            {
                if (c.Parent == null) return c.Name;
                else return GetFirstParent(c) + "." + c.Name;
            }

            private static string GetFirstParent(Control c)
            {
                if (c.Parent == null) return c.Name;
                else return GetFirstParent(c.Parent);
            }

            public static void DataGridResetColumnWidth(DataGridView DataGrid, object Suffix)
            {
                //salvo larghezza colonne per datagrid
                string sName = GetPath(DataGrid);
                if (Suffix != null) if (Suffix.ToString().Length > 0) sName += "." + Suffix.ToString();

                string s;
                DbBridge Db = new DbBridge();
                System.Data.SqlClient.SqlCommand Cmd = Db.Connection.CreateCommand();

                if (CONF_COLONNE)
                {
                    Cmd.Connection.Open();

                    s = "DELETE FROM Conf_Colonne";
                    s += " WHERE Controllo='" + sName + "'";
                    Cmd.CommandText = s;
                    Cmd.ExecuteScalar();

                    Cmd.Connection.Close();
                }
            }

            public static string GetColumnFormat(DataGridViewColumn c)
            {
                string format = "";
                switch (c.DefaultCellStyle.Alignment)
                {
                    case DataGridViewContentAlignment.BottomCenter:
                    case DataGridViewContentAlignment.MiddleCenter:
                    case DataGridViewContentAlignment.TopCenter:
                        format = "C";
                        break;
                    case DataGridViewContentAlignment.BottomLeft:
                    case DataGridViewContentAlignment.MiddleLeft:
                    case DataGridViewContentAlignment.TopLeft:
                        format = "L";
                        break;
                    case DataGridViewContentAlignment.BottomRight:
                    case DataGridViewContentAlignment.MiddleRight:
                    case DataGridViewContentAlignment.TopRight:
                        format = "R";
                        break;
                }
                if (c.DefaultCellStyle.Format.Length > 0) format += ";" + c.DefaultCellStyle.Format;
                return format;
            }

            public static void SetColumnFormat(DataGridViewColumn c, string format)
            {
                //Formato: parametri di allineamento e formato per valori numerici, separati da ';':
                // - Allineamento: L (sinistra), R (destra), C (centrato)
                // - Formato: Nx (numerico con x decimali, Px (percentuale con x decimali)

                string[] formats = format.Split(';');
                foreach (string s in formats)
                {
                    switch (s)
                    {
                        case "L":
                            c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                            break;
                        case "R":
                            c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            break;
                        case "C":
                            c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            break;
                        default:
                            if (s.StartsWith("N") || s.StartsWith("P")) c.DefaultCellStyle.Format = s;
                            break;
                    }
                }
            }

            public static void DataGridSaveColumnWidth(DataGridView DataGrid, object Suffix)
            {
                if (CONF_COLONNE)
                {
                    //salvo larghezza colonne per datagrid
                    string sName = GetPath(DataGrid);
                    if (Suffix != null) if (Suffix.ToString().Length > 0) sName += "." + Suffix.ToString();

                    string s;
                    DbBridge Db = new DbBridge();
                    System.Data.SqlClient.SqlCommand Cmd = Db.Connection.CreateCommand();

                    Cmd.Connection.Open();

                    s = "DELETE FROM Conf_Colonne";
                    s += " WHERE Controllo='" + sName + "'";
                    Cmd.CommandText = s;
                    Cmd.ExecuteScalar();

                    for (int i = 0; i < DataGrid.Columns.Count; i++)
                    {
                        //controllo largh. minima/massima
                        if (DataGrid.Visible && DataGrid.Columns[i].Visible)
                        {
                            int w = (int)DataGrid.Columns[i].Width;
                            if (w < 10) DataGrid.Columns[i].Visible = false;
                            if (w > 1500) w = 1500;
                            DataGrid.Columns[i].Width = w;
                        }

                        s = "INSERT INTO Conf_Colonne (Controllo,Posizione,Nome,Intestazione,Campo,Larghezza,Visibile,SolaLettura,Dimensionabile,AutosizeFill,Formato)";
                        s += " VALUES ('" + sName + "'";
                        s += "," + DataGrid.Columns[i].DisplayIndex.ToString();
                        s += ",'" + DataGrid.Columns[i].Name + "'";
                        s += ",'" + DataGrid.Columns[i].HeaderText + "'";
                        s += ",'" + DataGrid.Columns[i].DataPropertyName + "'";
                        s += "," + DataGrid.Columns[i].Width.ToString();
                        if (DataGrid.Columns[i].Visible == true) s += ",1";
                        else s += ",0";
                        if (DataGrid.Columns[i].ReadOnly == true) s += ",1";
                        else s += ",0";
                        if (DataGrid.Columns[i].Resizable == DataGridViewTriState.True) s += ",1";
                        else if (DataGrid.Columns[i].Resizable == DataGridViewTriState.False) s += ",0";
                        else s += ",Null";
                        if (DataGrid.Columns[i].AutoSizeMode == DataGridViewAutoSizeColumnMode.Fill) s += ",1";
                        else s += ",0";
                        s += ",'" + GetColumnFormat(DataGrid.Columns[i]) + "'";
                        s += ")";
                        Cmd.CommandText = s;
                        Cmd.ExecuteScalar();
                    }

                    Cmd.Connection.Close();
                }
            }
        }

        
    }
}
