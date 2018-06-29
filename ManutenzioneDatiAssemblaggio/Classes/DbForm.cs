namespace Metra.ManutenzioneDatiAssemblaggio
{
    public class DbForm
    {
        //public class DbDataRow
        //{
        //    public System.Windows.Forms.ErrorProvider ErrorProvider;
        //    DataTable _dt;
        //    DataRow _dr;
        //    List<DbDataRowField> _aFields;

        //    //---------------- 
        //    //inizializzazione 
        //    //---------------- 

        //    public DbDataRow(DataTable dt, DataRow dr)
        //    {
        //        _dt = dt;
        //        _dr = dr;
        //        _aFields = new List<DbDataRowField>();
        //    }

        //    //--------- 
        //    //propriet� 
        //    //--------- 

        //    public DbDataRowField Fields(Control c)
        //    {
        //        int i = 0;
        //        for (i = 0; i < _aFields.Count; i++)
        //        {
        //            if (_aFields[i].Control == c) break;
        //        }
        //        if (i < _aFields.Count) return _aFields[i];
        //        else return null;
        //    }

        //    public DbDataRowField Fields(string FieldName)
        //    {
        //        int i = 0;
        //        for (i = 0; i < _aFields.Count; i++)
        //        {
        //            if (_aFields[i].Field == FieldName) break;
        //        }
        //        if (i < _aFields.Count) return _aFields[i];
        //        else return null;
        //    }

        //    public DataTable DataTable
        //    {
        //        get { return _dt; }
        //        set { _dt = value; }
        //    }

        //    public DataRow DataRow
        //    {
        //        get { return _dr; }
        //        set { _dr = value; }
        //    }

        //    //--------------------------------- 
        //    //collegamento controlli form/campi 
        //    //--------------------------------- 

        //    public bool AddField(Control c)
        //    {
        //        return AddField(c, "", false, false, true);
        //    }

        //    public bool AddField(Control c, bool Required)
        //    {
        //        return AddField(c, "", Required, false, true);
        //    }

        //    public bool AddField(Control c, bool Required, bool Locked)
        //    {
        //        return AddField(c, "", Required, Locked, true);
        //    }
            
        //    public bool AddField(Control c, string Field)
        //    {
        //        return AddField(c, Field, false, false, true);
        //    }

        //    public bool AddField(Control c, string Field, bool Required)
        //    {
        //        return AddField(c, Field, Required, false, true);
        //    }

        //    public bool AddField(Control c, string Field, bool Required, bool Locked)
        //    {
        //        return AddField(c, Field, Required, Locked, true);
        //    }

        //    public bool AddField(Control c, string FieldName, bool Required, bool Locked, bool AllowZeroLength)
        //    {
        //        //controllo esistenza campo con lo stesso nome del controllo (senza il prefisso) 
        //        string sFieldName;
        //        if (FieldName.Length == 0)
        //            sFieldName = c.Name.Substring(3);
        //        else
        //            sFieldName = FieldName;
        //        int iIndice = GetFieldIndex(sFieldName);
        //        if (sFieldName.Length > 0 & iIndice >= 0)
        //        {
        //            //campo esistente 
        //            DbDataRowField Field = new DbDataRowField(c, _dr, iIndice);
        //            Field.Required = Required;
        //            Field.Locked = Locked;
        //            Field.AllowZeroLength = AllowZeroLength;
        //            Field.ErrorProvider = this.ErrorProvider;
        //            _aFields.Add(Field);
        //            return true;
        //        }
        //        else
        //        {
        //            //campo non esistente
        //            MessageBox.Show("Campo '"+sFieldName+"' non esistente!");
        //            return false;
        //        }
        //    }

        //    public bool AddField(RadioButton opt, string FieldName, int RelatedValue)
        //    {
        //        //radio button con valore collegato di tipo INTEGER 
        //        int iIndice = GetFieldIndex(FieldName);
        //        if (FieldName.Length > 0 & iIndice >= 0)
        //        {
        //            Control c = (Control)opt;
        //            DbDataRowField Field = new DbDataRowField(c, _dr, iIndice);
        //            Field.RelatedValueInteger = RelatedValue;
        //            _aFields.Add(Field);
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }

        //    public bool AddField(RadioButton opt, string FieldName, string RelatedValue)
        //    {
        //        //radio button con valore collegato di tipo STRING 
        //        int iIndice = GetFieldIndex(FieldName);
        //        if (FieldName.Length > 0 & iIndice >= 0)
        //        {
        //            Control c = (Control)opt;
        //            DbDataRowField Field = new DbDataRowField(c, _dr, iIndice);
        //            Field.RelatedValueString = RelatedValue;
        //            _aFields.Add(Field);
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }

        //    //------------------------- 
        //    //utility interne di classe 
        //    //------------------------- 

        //    private int GetFieldIndex(string sFieldName)
        //    {
        //        int iIndex = -1;
        //        for (int i = 0; i <= _dr.Table.Columns.Count - 1; i++)
        //        {
        //            if (_dr.Table.Columns[i].ColumnName == sFieldName)
        //            {
        //                iIndex = i;
        //                break;
        //            }
        //        }
        //        return iIndex;
        //    }

        //    //---- 
        //    //dati 
        //    //---- 

        //    public void Clear()
        //    {
        //        foreach (DbDataRowField Field in _aFields)
        //        {
        //            Field.Clear();
        //        }
        //    }

        //    public DbFormResult LoadDataRow()
        //    {
        //        DbFormResult err = new DbFormResult();

        //        //se � nuova non faccio Accept altrimenti poi non riesco a salvare
        //        if (_dr.RowState!=DataRowState.Added & _dr.RowState!=DataRowState.Detached) _dr.AcceptChanges();

        //        //carico i controlli 
        //        foreach (DbDataRowField Field in _aFields)
        //        {
        //            bool b=Field.Load();
        //            if (!b)
        //            {
        //                err.Ok = false;
        //                err.Descrizione += Field.Field + ";";
        //            }
        //        }

        //        return err;
        //    }

        //    public DbFormResult SaveDataRow()
        //    {
        //        DbFormResult ret=new DbFormResult();
        //        ret.Function = "SaveDataRow";

        //        //controllo validit� dati (in base al tipo) 
        //        foreach (DbDataRowField Field in _aFields)
        //        {
        //            string s = Field.CheckValue();
        //            if (s.Length > 0)
        //                ret.Descrizione += "\n - " + Field.FieldDescription + ": " + s;
        //        }
        //        if (ret.Descrizione.Length > 0)
        //        {
        //            ret.Descrizione = "Attenzione:\n" + ret.Descrizione;
        //        }

        //        //se ok salvo i controlli 
        //        if (ret.Descrizione.Length == 0)
        //        {
        //            foreach (DbDataRowField Field in _aFields)
        //            {
        //                Field.Save();
        //            }
        //        }            
                
        //        //se ok controllo se deve essere aggiunta
        //        if (ret.Descrizione.Length == 0)
        //        {
        //            if (_dr.RowState == DataRowState.Detached)
        //            {
        //                _dt.Rows.Add(_dr);
        //            }
        //        }

        //        //ritorno eventuale errore 
        //        ret.Ok = ret.Descrizione.Length == 0;
        //        return ret;
        //    }

        //    //-------------- 
        //    //controllo dati 
        //    //-------------- 

        //    public bool CheckValues()
        //    {
        //        bool bOk = true;

        //        foreach (DbDataRowField c in this._aFields)
        //        {
        //            bOk = bOk & c.CheckValue() == "";
        //        }
        //        return bOk;
        //    }

        //    public string ModifiedFields()
        //    {
        //        string sMod = "";

        //        //analizzo campi 
        //        foreach (DbDataRowField Field in _aFields)
        //        {
        //            if (Field.IsModified())
        //                sMod = sMod + ";" + Field.Field;
        //        }
        //        if (sMod.Length > 0) sMod = sMod.Substring(1);
        //        return sMod;
        //    }

        //    //public string ModifiedChildTables()
        //    //{
        //    //    string sMod = "";

        //    //    //analizzo tabelle child 
        //    //    foreach (DbFormTable Child in _aChildTables)
        //    //    {
        //    //        if (Child.IsModified(_iRow))
        //    //            sMod = sMod + ";" + Child.ChildTable.TableName;
        //    //    }
        //    //    if (sMod.Length > 0)
        //    //        sMod = sMod.Substring(2);
        //    //    return sMod;
        //    //}

        //    public bool IsModified()
        //    {
        //        //controllo campi 
        //        bool bFields = ModifiedFields().Length > 0;// _dr.RowState != DataRowState.Unchanged;

        //        //controllo tabelle child 
        //        //bool bTables = ModifiedChildTables() != "";

        //        return bFields;//| bTables;
        //    }
        //}

        //public class DbDataRowField
        //{
        //    public System.Windows.Forms.ErrorProvider ErrorProvider;
        //    DataRow _dr;
        //    Control _cControl;
        //    Color _cControlForeColor;
        //    Color _cControlBackColor;
        //    Color _cErrorBackColor = Color.Orange;
        //    int _iColIndex;
        //    bool _bRequired = false;
        //    bool _bLocked = false;
        //    bool _bAllowZeroLength = true;
        //    bool _bComboCheck = false;
        //    string _sFieldDescription;
        //    string _sDataSourceField="";
        //    int _iRelatedType = 0; //1=string,2=integer 
        //    string _sRelatedValue = "";
        //    int _iRelatedValue = 0;
        //    string _sFormat = "";

        //    //---------------- 
        //    //inizializzazione 
        //    //---------------- 

        //    public DbDataRowField(Control Controllo, DataRow dr, int ColIndex)
        //    {
        //        //parametri 
        //        _cControl = Controllo;
        //        _cControlForeColor = Controllo.ForeColor;
        //        _cControlBackColor = Controllo.BackColor;
        //        _dr = dr;
        //        _iColIndex = ColIndex;
        //        _sFieldDescription = Field;

        //        //per textbox e combobox lunghezza max 
        //        if (_dr.Table.Columns[_iColIndex].MaxLength > 0)
        //        {
        //            string sCtrlType = Control.GetType().ToString();
        //            if (sCtrlType == "System.Windows.Forms.TextBox")
        //            {
        //                TextBox c = Controllo as TextBox;
        //                if ((c != null))
        //                    c.MaxLength = _dr.Table.Columns[_iColIndex].MaxLength;
        //            }
        //            if (sCtrlType == "System.Windows.Forms.ComboBox")
        //            {
        //                ComboBox c = Controllo as ComboBox;
        //                if ((c != null))
        //                    c.MaxLength = _dr.Table.Columns[_iColIndex].MaxLength;
        //            }
        //        }

        //        //eventi
        //        _cControl.Validating += Control_Validating;
        //        _cControl.KeyPress += Control_KeyPress;
        //    }

        //    //--------- 
        //    //propriet� 
        //    //--------- 

        //    public Control Control
        //    {
        //        get { return _cControl; }
        //    }

        //    public Color ControlForeColor
        //    {
        //        get { return _cControlForeColor; }
        //    }

        //    public Color ControlBackColor
        //    {
        //        get { return _cControlBackColor; }
        //    }

        //    public string Field
        //    {
        //        get { return _dr.Table.Columns[_iColIndex].ColumnName; }
        //    }

        //    public string FieldDescription
        //    {
        //        get { return _sFieldDescription; }
        //        set { _sFieldDescription = value; }
        //    }

        //    public object DataSource
        //    {
        //        get
        //        {
        //            string sCtrlType =_cControl.GetType().ToString();
        //            if (sCtrlType == "System.Windows.Forms.ComboBox")
        //            { 
        //                return ((ComboBox)_cControl).DataSource; 
        //            }
        //            else
        //            { 
        //                return null; 
        //            }
        //        }
        //        set 
        //        {
        //            string sCtrlType =_cControl.GetType().ToString();
        //            if (sCtrlType == "System.Windows.Forms.ComboBox")
        //            {
        //                ((ComboBox)_cControl).DataSource = value; 
        //            }
        //        }
        //    }

        //    public string DataSourceField
        //    {
        //        get { return _sDataSourceField; }
        //        set { _sDataSourceField = value; }
        //    }

        //    public string DisplayMember
        //    {
        //        get
        //        {
        //            string sCtrlType = _cControl.GetType().ToString();
        //            if (sCtrlType == "System.Windows.Forms.ComboBox")
        //            {
        //                return ((ComboBox)_cControl).DisplayMember;
        //            }
        //            else
        //            {
        //                return null;
        //            }
        //        }
        //        set
        //        {
        //            string sCtrlType = _cControl.GetType().ToString();
        //            if (sCtrlType == "System.Windows.Forms.ComboBox")
        //            {
        //                ((ComboBox)_cControl).DisplayMember = value;
        //            }
        //        }
        //    }            

        //    public int Index
        //    {
        //        get { return _iColIndex; }
        //    }

        //    public bool Required
        //    {
        //        get { return _bRequired; }
        //        set { _bRequired = value; }
        //    }

        //    public bool Locked
        //    {
        //        get { return _bLocked; }
        //        set
        //        { 
        //            _bLocked = value;
        //            _cControl.TabStop = !_bLocked;
        //        }
        //    }

        //    public bool AllowZeroLength
        //    {
        //        get { return _bAllowZeroLength; }
        //        set { _bAllowZeroLength = value; }
        //    }

        //    public int RelatedValueInteger
        //    {
        //        get { return _iRelatedValue; }
        //        set
        //        {
        //            _iRelatedValue = value;
        //            _iRelatedType = 2;
        //        }
        //    }

        //    public string RelatedValueString
        //    {
        //        get { return _sRelatedValue; }
        //        set
        //        {
        //            _sRelatedValue = value;
        //            _iRelatedType = 1;
        //        }
        //    }

        //    public bool RelatedIsString
        //    {
        //        get { return _iRelatedType == 1; }
        //    }

        //    public bool RelatedIsInteger
        //    {
        //        get { return _iRelatedType == 2; }
        //    }

        //    public string Format
        //    {
        //        get { return _sFormat; }
        //        set { _sFormat = value; }
        //    }

        //    public bool ComboCheck
        //    {
        //        get { return _bComboCheck; }
        //        set { _bComboCheck = value; }
        //    }

        //    //--------------- 
        //    //gestione eventi 
        //    //--------------- 

        //    private void Control_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        //    {
        //        //eseguo su validating del controllo 
        //        Control c = sender as Control;
        //        if ((c != null))
        //        {

        //            string sErr = this.CheckValue();
        //            if (ErrorProvider == null)
        //            {
        //                if (sErr.Length == 0)
        //                {
        //                    c.BackColor = this.ControlBackColor;
        //                }
        //                else
        //                {
        //                    c.BackColor = _cErrorBackColor;
        //                }
        //            }
        //            else
        //            {
        //                ErrorProvider.SetError(c, sErr);
        //            }
        //        }
        //    }
            
        //    private void Control_KeyPress(object sender, KeyPressEventArgs e)
        //    {
        //        if (_bLocked) e.KeyChar = (char)0;
        //    }

        //    //------------------- 
        //    //controllo contenuti 
        //    //------------------- 

        //    public string CheckValue()
        //    {   
        //        //variabile per errore
        //        string sErr="";

        //        //controllo per combobox
        //        string sCtrlType =_cControl.GetType().ToString();
        //        if (sCtrlType == "System.Windows.Forms.ComboBox" & _bComboCheck)
        //        {
        //            ComboBox cmb = (ComboBox)_cControl;
        //            if (cmb.DataSource != null & cmb.Text.Length>0)
        //            {
        //                string s = cmb.DisplayMember + "='" + cmb.Text + "'";
        //                try
        //                {
        //                    DataTable dt = (DataTable)cmb.DataSource;
        //                    bool b = dt.Select(s).Length > 0;
        //                    if (!b) sErr = "Valore non ammesso";
        //                }
        //                catch (Exception)
        //                { }
        //            }
        //        }

        //        //controllo validit� dato
        //        if (sErr.Length == 0)
        //        {
        //            if (sCtrlType == "System.Windows.Forms.ComboBox" & DisplayMember != DataSourceField)
        //            {
        //                sErr = "";
        //            }
        //            else
        //            {
        //                sErr = DbFormUtils.CheckField(this.Control, _dr.Table.Columns[_iColIndex], _sFormat);
        //            }
        //        }

        //        //controllo se dato � richiesto
        //        if (sErr.Length == 0 & this._bRequired & this._cControl.Text.ToString() == "")
        //        {
        //            sErr = "Richiesto dato";
        //            this.Control.BackColor = _cErrorBackColor;
        //        }

        //        //colore sfondo
        //        if (sErr.Length == 0)
        //        {
        //            this.Control.BackColor = this._cControlBackColor;
        //        }
        //        else
        //        {
        //            this.Control.BackColor = _cErrorBackColor;
        //        }

        //        //esito
        //        return sErr;
        //    }

        //    public void Clear()
        //    {
        //        //azzero il controllo 
        //        string sCtrlType = this.Control.GetType().ToString();
        //        switch (sCtrlType)
        //        {
        //            case "System.Windows.Forms.CheckBox":
        //                CheckBox chk = (CheckBox)this.Control;
        //                chk.Checked = false;
        //                break;
        //            case "System.Windows.Forms.RadioButton":
        //                RadioButton opt = (RadioButton)this.Control;
        //                opt.Checked = false;
        //                break;
        //            default:
        //                this.Control.Text = "";
        //                break;
        //        }
        //    }

        //    public bool IsModified()
        //    {
        //        //controllo se il valore del controllo � cambiato 
        //        string sCtrl = _cControl.GetType().ToString();
        //        bool bMod = false;
        //        switch (sCtrl)
        //        {
        //            case "System.Windows.Forms.TextBox":
        //            case "System.Windows.Forms.NumericUpDown":
        //            case "System.Windows.Forms.ComboBox":
        //            case "System.Windows.Forms.DateTimePicker":

        //                string sText = _cControl.Text;
        //                if (sCtrl == "System.Windows.Forms.ComboBox") sText = GetComboRelatedText().ToString();

        //                switch (_dr.Table.Columns[_iColIndex].DataType.ToString())
        //                {
        //                    case "System.DateTime":
        //                        System.DateTime d1 = DateTime.MinValue;
        //                        System.DateTime d2 = DateTime.MinValue;

        //                        if (_dr[_iColIndex].ToString() != "")
        //                        {
        //                            d1 = (System.DateTime)_dr[_iColIndex];
        //                        }

        //                        try
        //                        {
        //                            d2 = Convert.ToDateTime(sText); 
        //                        }
        //                        catch (Exception)
        //                        {
        //                        }

        //                        if (d1 != d2) bMod = true;

        //                        break;
        //                    case "System.Int16":
        //                    case "System.Int32":
        //                    case "System.Int64":
        //                        if (_sFormat.Length > 0)
        //                        {
        //                            if (Convert.ToInt64(_dr[_iColIndex]).ToString(_sFormat) != sText) bMod = true;
        //                        }
        //                        else
        //                        {
        //                            if (_dr[_iColIndex].ToString() != sText) bMod = true;
        //                        }
        //                        break;
        //                    case "System.Single":
        //                    case "System.Double":
        //                    case "System.Decimal":
        //                        if (_sFormat.Length > 0)
        //                        {
        //                            if (Convert.ToDouble(_dr[_iColIndex]).ToString(_sFormat) != sText) bMod = true;
        //                        }
        //                        else
        //                        {
        //                            if (_dr[_iColIndex].ToString() != sText) bMod = true;
        //                        }
        //                        break;
        //                    default:
        //                        if (_dr[_iColIndex].ToString() != sText) bMod = true;
        //                        break;
        //                }
        //                break;
        //            case "System.Windows.Forms.CheckBox":
        //                System.Windows.Forms.CheckBox chk;
        //                chk = (CheckBox)_cControl;
        //                bool b=false;
        //                if (_dr[_iColIndex] != DBNull.Value) b = Convert.ToBoolean(_dr[_iColIndex]);
        //                if (b != chk.Checked) bMod = true;

        //                break;
        //            case "System.Windows.Forms.RadioButton":
        //                RadioButton opt;
        //                opt = (RadioButton)_cControl;
        //                if (_iRelatedType == 1)
        //                {
        //                    if (opt.Checked)
        //                    {
        //                        if ((string)_dr[_iColIndex] != _sRelatedValue) bMod = true;
        //                    }
        //                }
        //                else if (_iRelatedType == 2)
        //                {
        //                    if (opt.Checked)
        //                    {
        //                        if ((int)_dr[_iColIndex] != _iRelatedValue) bMod = true;
        //                    }
        //                }

        //                break;
        //        }
        //        return bMod;
        //    }

        //    public bool Empty()
        //    {
        //        bool bEmpty = false;
        //        string sCtrlType = _cControl.GetType().ToString();
        //        switch (sCtrlType)
        //        {
        //            case "System.Windows.Forms.TextBox":
        //            case "System.Windows.Forms.NumericUpDown":
        //            case "System.Windows.Forms.ComboBox":
        //                if (_cControl.Text == "")
        //                    bEmpty = true;

        //                break;
        //            case "System.Windows.Forms.CheckBox":
        //                break;
        //            case "System.Windows.Forms.RadioButton":
        //                break;
        //        }
        //        return bEmpty;
        //    }

        //    //---- 
        //    //dati 
        //    //---- 

        //    public bool Save()
        //    {
        //        Type t = _cControl.GetType();
        //        string sCtrlType = _cControl.GetType().ToString();
        //        string sDataType = _dr[_iColIndex].GetType().ToString();

        //        try
        //        {
        //            switch (sCtrlType)
        //            {
        //                case "System.Windows.Forms.TextBox":
        //                case "System.Windows.Forms.NumericUpDown":
        //                case "System.Windows.Forms.DateTimePicker":
        //                    if (_cControl.Text == "") _dr[_iColIndex] = DBNull.Value;
        //                    else _dr[_iColIndex] = _cControl.Text;
        //                    break;
        //                case "System.Windows.Forms.ComboBox":
        //                    object Val = GetComboRelatedText();
        //                    if (Val.ToString() == "" | Val == null) _dr[_iColIndex] = DBNull.Value;
        //                    else _dr[_iColIndex] = Val;
        //                    break;
        //                case "System.Windows.Forms.CheckBox":
        //                    CheckBox chk;
        //                    chk = (CheckBox)_cControl;
        //                    _dr[_iColIndex] = chk.Checked;
        //                    break;
        //                case "System.Windows.Forms.RadioButton":
        //                    RadioButton opt;
        //                    opt = (RadioButton)_cControl;
        //                    if (_iRelatedType == 1)
        //                    {
        //                        //stringa 
        //                        if (opt.Checked)
        //                            _dr[_iColIndex] = _sRelatedValue;
        //                    }
        //                    else if (_iRelatedType == 2)
        //                    {
        //                        //intero 
        //                        if (opt.Checked)
        //                            _dr[_iColIndex] = _iRelatedValue;
        //                    }

        //                    break;
        //            }
        //        }
        //        catch (Exception) { }

        //        return true;
        //    }

        //    public bool Load()
        //    {
        //        string sCtrlType = _cControl.GetType().ToString();
        //        switch (sCtrlType)
        //        {
        //            case "System.Windows.Forms.TextBox":
        //            case "System.Windows.Forms.NumericUpDown":
        //            case "System.Windows.Forms.DateTimePicker":
        //                _cControl.Text = _dr[_iColIndex].ToString().TrimEnd();
        //                DbFormUtils.CheckField(_cControl, _dr.Table.Columns[_iColIndex], _sFormat);
        //                break;
        //            case "System.Windows.Forms.ComboBox":
        //                _cControl.Text = GetComboText();
        //                DbFormUtils.CheckField(_cControl, _dr.Table.Columns[_iColIndex], _sFormat);
        //                break;
        //            case "System.Windows.Forms.CheckBox":
        //                CheckBox chk;
        //                chk = (CheckBox)_cControl;
        //                if (_dr[_iColIndex] == DBNull.Value) chk.Checked = false;
        //                else chk.Checked = (bool)_dr[_iColIndex];
        //                break;
        //            case "System.Windows.Forms.RadioButton":
        //                RadioButton opt;
        //                opt = (RadioButton)_cControl;
        //                if (_iRelatedType == 1)
        //                {
        //                    opt.Checked = _dr[_iColIndex].ToString() == _sRelatedValue;
        //                }
        //                else if (_iRelatedType == 2)
        //                {
        //                    opt.Checked = _dr[_iColIndex].ToString() == _iRelatedValue.ToString();
        //                }
        //                break;
        //        }
        //        return true;
        //    }

        //    public string GetComboText()
        //    {
        //        //recupero il testo da visualizzare 
        //        string Ret = _dr[_iColIndex].ToString();
        //        ComboBox cmb = (ComboBox)_cControl;
        //        if (cmb.DisplayMember.Length != 0 & DataSourceField.Length != 0 & cmb.DisplayMember != DataSourceField)
        //        {
        //            if (cmb.DataSource != null)
        //            {
        //                try
        //                {
        //                    DataTable dt = (DataTable)cmb.DataSource;
        //                    if (dt != null)
        //                    {
        //                        string s = DataSourceField + "=";
        //                        switch (dt.Columns[cmb.DisplayMember].DataType.ToString())
        //                        {
        //                            case "System.String":
        //                                s += "'" + Ret + "'";
        //                                break;
        //                            default:
        //                                s += Ret;
        //                                break;
        //                        }
        //                        DataRow dr = dt.Select(s)[0];
        //                        Ret = dr[DisplayMember].ToString(); 
        //                    }
        //                }
        //                catch (Exception)
        //                {
        //                    //Ret = null;
        //                }
        //            }
        //        }
        //        return Ret;
        //    }

        //    public object GetComboRelatedText()
        //    {
        //        //recupero il testo da salvare
        //        object Ret = _cControl.Text;
        //        ComboBox cmb = (ComboBox)_cControl;
        //        if (cmb.DisplayMember.Length != 0 & DataSourceField.Length != 0 & cmb.DisplayMember != Field & cmb.DataSource != null)
        //        {
        //            try
        //            {
        //                DataTable dt = (DataTable)cmb.DataSource;
        //                string s = cmb.DisplayMember + "=";
        //                switch (dt.Columns[cmb.DisplayMember].DataType.ToString())
        //                {
        //                    case "System.String":
        //                        s += "'" + cmb.Text + "'";
        //                        break;
        //                    default:
        //                        s += cmb.Text;
        //                        break;
        //                }
        //                DataRow dr = dt.Select(s)[0];
        //                Ret = dr[DataSourceField];
        //            }
        //            catch (Exception)
        //            {
        //            }
        //        }
        //        return Ret;
        //    }
        //}

        //public class DbDataTable
        //{
        //    DataTable _dt;
        //    DataView _dv;
        //    string _RowIndexField;
        //    string _LinkField;
        //    object _LinkValue;
        //    string _Guid;
        //    DataGridView _dgGrid;

        //    //---------------- 
        //    //inizializzazione 
        //    //---------------- 

        //    public DbDataTable(DataGridView Grid, DataTable Table)
        //    {
        //        Init(Grid, Table, null, null, null);
        //    }
            
        //    public DbDataTable(DataGridView Grid, DataTable Table, string RowIndexField)
        //    {
        //        Init(Grid, Table, RowIndexField, null, null);
        //    }

        //    public DbDataTable(DataGridView Grid, DataTable Table, string RowIndexField, string LinkField, Object LinkValue)
        //    {
        //        Init(Grid, Table,RowIndexField, LinkField, LinkValue);
        //    }

        //    private void Init(DataGridView Grid, DataTable Table, string RowIndexField, string LinkField, Object LinkValue)
        //    {
        //        //inizializzazione parametri
        //        _dgGrid = Grid;
        //        _dt = Table;
        //        _LinkField = LinkField;
        //        _LinkValue = LinkValue;
        //        _RowIndexField = RowIndexField;
        //        if (DbForm.DbFormUtils.GetColumn(_dt,_RowIndexField)==null) _RowIndexField = null;

        //        //controllo campo id per eventuale assegnazione del guid
        //        if (DbForm.DbFormUtils.GetColumn(_dt, "ID") != null)
        //            if (_dt.Columns["ID"].DataType.ToString() == "System.Guid")
        //                _Guid = "ID";

        //        //eventi griglia
        //        _dgGrid.CellValidating += new DataGridViewCellValidatingEventHandler(DataGrid_CellValidating);
        //        _dgGrid.DataError += new DataGridViewDataErrorEventHandler(DataGrid_DataError);

        //        //eventi datatable
        //        _dt.TableNewRow += new DataTableNewRowEventHandler(_dt_TableNewRow);
        //        _dt.RowDeleted += new DataRowChangeEventHandler(_dt_RowDeleted);

        //    }

        //    void _dt_RowDeleted(object sender, DataRowChangeEventArgs e)
        //    {
        //        if (_RowIndexField != null)
        //        {
        //            //ricalcolo posizioni
        //            int i = 0;
        //            foreach (DataRow dr in _dt.Rows)
        //            {
        //                if (dr.RowState != DataRowState.Deleted) dr[_RowIndexField] = i++;
        //            }
        //        }
        //    }

        //    void _dt_TableNewRow(object sender, DataTableNewRowEventArgs e)
        //    {
        //        if (_Guid!=null) e.Row["ID"] = Guid.NewGuid();
        //        if (_LinkField != null) e.Row[_LinkField] = _LinkValue;
        //        if (_RowIndexField != null) e.Row[_RowIndexField] = e.Row.Table.Rows.Count;
        //    }

        //    //---- 
        //    //dati 
        //    //---- 

        //    public void Clear()
        //    {
        //        _dgGrid.CellValidating -= DataGrid_CellValidating;
        //        _dt.Clear();
        //        _dgGrid.DataSource = null;
        //        _dgGrid.Columns.Clear();
        //    }

        //    public void SaveColumns()
        //    {
        //        //salvo larghezza colonne
        //        DbFormUtils.DataGridSaveColumnWidth(_dgGrid,_dgGrid.Tag);
        //    }

        //    public DbFormResult SaveDataTable()
        //    {
        //        //devo aggiornare i campi di collegamento/ordinamento 
        //        DbFormResult ris = new DbFormResult();
        //        ris.Function = "SaveDataTable";

        //        //salvo larghezza colonne
        //        DbFormUtils.DataGridSaveColumnWidth(_dgGrid,_dgGrid.Tag);

        //        //esito
        //        ris.Ok = true;
        //        return ris;
        //    }

        //    public void LoadDataTable()
        //    {
        //        LoadDataTable("",true);
        //    }

        //    public void LoadDataTable(bool EnableSelect)
        //    {
        //        LoadDataTable("", EnableSelect);
        //    }

        //    public void LoadDataTable(string sCrit,bool EnableSelect)
        //    {
        //        //colonne "PRESET"
        //        int iNumCol = _dgGrid.Columns.Count;
        //        for (int i = 0; i < _dgGrid.Columns.Count; i++)
        //        {
        //            _dgGrid.Columns[i].Tag = "PRESET";
        //            if (_dgGrid.Columns[i].DataPropertyName.Length == 0) _dgGrid.Columns[i].DataPropertyName = _dgGrid.Columns[i].Name;
        //        }

        //        //selezione righe
        //        _dv = new DataView(_dt, sCrit, _RowIndexField, DataViewRowState.CurrentRows);
        //        _dgGrid.DataSource = _dv;

        //        //nascondo colonne non "PRESET"
        //        if (iNumCol > 0)
        //            for (int i = 0; i < _dgGrid.Columns.Count; i++)
        //                if (_dgGrid.Columns[i].Tag == null)
        //                    _dgGrid.Columns[i].Visible = false;

        //        //convalido le modifiche 
        //        for (int i = 0; i <= _dv.Count - 1; i++)
        //        {
        //            _dv[i].Row.AcceptChanges();
        //        }

        //        //layout
        //        DbForm.DbFormUtils.DataGridViewLayout(_dgGrid,EnableSelect);
        //    }

        //    public bool IsModified()
        //    {
        //        //controllo se esistono righe modificate 
        //        bool bMod = false;

        //        foreach (DataRowView r in this._dv)
        //        {
        //            if (r.Row.RowState != DataRowState.Unchanged)
        //            {
        //                bMod = true;
        //                break;
        //            }
        //        }
        //        return bMod;
        //    }

        //    //--------------- 
        //    //gestione eventi 
        //    //--------------- 

        //    private void DataGrid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        //    {
        //        if (!_dgGrid.CurrentRow.ReadOnly)
        //        {
        //            //controllo valore proposto (eventualmente viene riformattato) 
        //            string sTxt = e.FormattedValue.ToString();
        //            TextBox txt = new TextBox();
        //            txt.Text = sTxt;
        //            string sErr = DbFormUtils.CheckField(txt, _dt.Columns[_dgGrid.Columns[e.ColumnIndex].DataPropertyName], "");
        //            string sTxtNew = txt.Text;

        //            //controllo eventuale errore 
        //            if (sErr.Length > 0)
        //            {
        //                //valore errato: msg e annullo operzione validazione 
        //                MessageBox.Show(sErr, null, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //                e.Cancel = true;
        //            }
        //            else
        //            {
        //                //valore ok, controllo se � stato riformattato 
        //                //If dgGrid.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString <> txt.Text Then 
        //                if (sTxt != sTxtNew)
        //                {
        //                    //riporto valore riformattato (per numeri/date) 
        //                    _dgGrid.CancelEdit();
        //                    _dgGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = sTxtNew;
        //                }
        //            }
        //        }
        //    }

        //    private void DataGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        //    {
        //        DataGridView dg = (DataGridView)sender;
        //        string s = "Errore: " + e.Exception.Message;
        //        MessageBox.Show(s, null, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //        e.Cancel = true;
        //    }

        //    //-------
        //    //utility
        //    //-------

        //    public bool MoveLine(int Direction)
        //    {
        //        if (Direction > 0) Direction = 1;
        //        if (Direction < 0) Direction = -1;
        //        if (_RowIndexField.Length > 0 & Direction!=0)
        //        {
        //            Point pCell = _dgGrid.CurrentCellAddress;
        //            int iCurr = pCell.Y;
        //            int iSwap = iCurr + Direction;

        //            if (iSwap >= 0 & iSwap < _dgGrid.Rows.Count-1)
        //            {
        //                //scambio riga con quella adiacente
        //                int i = (int)_dgGrid.Rows[iCurr].Cells[_RowIndexField].Value;
        //                _dgGrid.Rows[iCurr].Cells[_RowIndexField].Value = _dgGrid.Rows[iSwap].Cells[_RowIndexField].Value;
        //                _dgGrid.Rows[iSwap].Cells[_RowIndexField].Value = i;

        //                //riordino
        //                _dgGrid.Sort(_dgGrid.Columns[_RowIndexField], System.ComponentModel.ListSortDirection.Ascending);
        //                _dgGrid.Refresh();

        //                //seleziono riga
        //                _dgGrid.CurrentCell = _dgGrid.Rows[pCell.Y + Direction].Cells[pCell.X];
                        
        //                return true;
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }

        //    //--------- 
        //    //propriet� 
        //    //--------- 

        //    public DataTable DataTable
        //    {
        //        get { return _dt; }
        //    }
        //}

        //public class DbFormUtils
        //{
        //    public static DataColumn GetColumn(DataTable dt, string ColumnName)
        //    {
        //        try
        //        {
        //            return dt.Columns[ColumnName];
        //        }
        //        catch (Exception)
        //        {
        //            return null;
        //        }
        //    }

        //    public static void LockControl(Control Control)
        //    {
        //        Control.TabStop = false;
        //        Control.KeyPress += new KeyPressEventHandler(Control_KeyPress);
        //    }

        //    static void Control_KeyPress(object sender, KeyPressEventArgs e)
        //    {
        //        e.KeyChar = (char)0;
        //    }

        //    public static string CheckField(Control Control, DataColumn Column, string sFormat)
        //    {
        //        //controllo che il contenuto del controllo sia compatibile con la colonna (tipo di dati) 
        //        string sCtrlType;
        //        string sDataType;
        //        string sOut = "";

        //        if (Column == null)
        //            return "";

        //        sCtrlType = Control.GetType().ToString();
        //        sDataType = Column.DataType.ToString();
        //        switch (sCtrlType)
        //        {
        //            case "System.Windows.Forms.ComboBox":
        //            case "System.Windows.Forms.TextBox":
        //            case "System.Windows.Forms.NumericUpDown":
        //            case "System.Windows.Forms.DateTimePicker":
        //                if (Control.Text.Length > 0)
        //                {
        //                    //controllo la propriet� "text" 
        //                    switch (sDataType)
        //                    {
        //                        case "System.String":
        //                            //stringa va sempre bene 
        //                            if (sFormat.Length > 0)
        //                                Control.Text = String.Format(Control.Text, sFormat);
        //                            break;
        //                        case "System.Int16":
        //                            //controllo tipo 
        //                            try
        //                            {
        //                                //controllo la virgola 
        //                                Control.Text = Control.Text.Replace(".", ",");
        //                                Int16 i = Convert.ToInt16(Control.Text);
        //                                if (Control.Text.Contains(","))
        //                                {
        //                                    sOut = "Richiesto numero intero";
        //                                }
        //                                else
        //                                {
        //                                    if (sFormat.Length > 0) Control.Text = i.ToString(sFormat);
        //                                    else Control.Text = i.ToString();
        //                                    //if (sFormat.Length > 0)
        //                                    //    Control.Text = String.Format(i.ToString(), sFormat);
        //                                }
        //                            }
        //                            catch (Exception)
        //                            {
        //                                sOut = "Richiesto numero";
        //                            }

        //                            break;
        //                        case "System.Int32":
        //                            //controllo tipo 
        //                            try
        //                            {
        //                                Control.Text = Control.Text.Replace(".", ",");
        //                                Int32 i = Convert.ToInt32(Control.Text);
        //                                if (Control.Text.Contains(","))
        //                                {
        //                                    sOut = "Richiesto numero intero";
        //                                }
        //                                else
        //                                {
        //                                    if (sFormat.Length > 0) Control.Text = i.ToString(sFormat);
        //                                    else Control.Text = i.ToString();
        //                                    //if (sFormat.Length > 0)
        //                                    //    Control.Text = String.Format(i.ToString(), sFormat);
        //                                }
        //                            }
        //                            catch (Exception)
        //                            {
        //                                sOut = "Richiesto numero";
        //                            }

        //                            break;
        //                        case "System.Int64":
        //                            //controllo tipo 
        //                            try
        //                            {
        //                                Control.Text = Control.Text.Replace(".", ",");
        //                                Int64 i = Convert.ToInt64(Control.Text);
        //                                if (Control.Text.Contains(","))
        //                                {
        //                                    sOut = "Richiesto numero intero";
        //                                }
        //                                else
        //                                {
        //                                    if (sFormat.Length > 0) Control.Text=i.ToString(sFormat);
        //                                    else Control.Text=i.ToString();
        //                                    //if (sFormat.Length > 0)
        //                                    //    Control.Text = String.Format(i.ToString(), sFormat);
        //                                }
        //                            }
        //                            catch (Exception)
        //                            {
        //                                sOut = "Richiesto numero";
        //                            }

        //                            break;
        //                        case "System.Single":
        //                        case "System.Double":
        //                        case "System.Decimal":
        //                            //controllo tipo 
        //                            try
        //                            {
        //                                Control.Text = Control.Text.Replace(".", ",");
        //                                double i = Convert.ToDouble(Control.Text);
        //                                if (sFormat.Length > 0) Control.Text = i.ToString(sFormat);
        //                                else Control.Text = i.ToString();
        //                                    //Control.Text = String.Format(i.ToString(), sFormat);
        //                            }
        //                            catch (Exception ex)
        //                            {
        //                                sOut = "Richiesto numero";
        //                            }

        //                            break;
        //                        case "System.DateTime":
        //                            //controllo ed eventualmente correggo formattazione 
        //                            string s = Control.Text;
        //                            //try
        //                            //{
        //                            //    DateTime d = Convert.ToDateTime(s);

        //                            //    s = d.Day.ToString().PadLeft(2,'0');
        //                            //    s += "/" + d.Month.ToString().PadLeft(2, '0');
        //                            //    s += "/" + d.Year.ToString().PadLeft(4, '0');
        //                            //    Control.Text = s;
        //                            //    //Control.Text = Control.Text.Replace(" ", "/");
        //                            //    //Control.Text = Control.Text.Replace("-", "/");
        //                            //    //Control.Text = Control.Text.Replace(".", "/");

        //                            //    //if (Information.IsNumeric(Control.Text) & Control.Text.Length == 4)
        //                            //    //{
        //                            //    //    Control.Text = Control.Text.Substring(1, 2) + "/" + Control.Text.Substring(3);
        //                            //    //}
        //                            //    //else if (Information.IsNumeric(Control.Text) & (Control.Text.Length == 6 | Control.Text.Length == 8)) ;
        //                            //    //{
        //                            //    //    Control.Text = Control.Text.Substring(1, 2) + "/" + Control.Text.Substring(3, 2) + "/" + Control.Text.Substring(5);
        //                            //    //}
        //                            //}
        //                            //catch
        //                            //{
        //                            //    sOut = "Richiesta una data";
        //                            //}


        //                            //controllo tipo 
        //                            try
        //                            {
        //                                DateTime d;
        //                                bool ok = DateTime.TryParse(s, out d);
        //                                if (!ok) d = DateTime.Parse(s.Replace(".", ":"));
                                        
        //                                //s = d.Day.ToString().PadLeft(2, '0');
        //                                //s += "/" + d.Month.ToString().PadLeft(2, '0');
        //                                //s += "/" + d.Year.ToString().PadLeft(4, '0');
                                        
        //                                //Control.Text = s;

        //                                //System.DateTime i = Convert.ToDateTime(Control.Text);
        //                                //if (sFormat == "")
        //                                //    sFormat = "Short Date";
        //                                //Control.Text = String.Format(i.ToString(), sFormat);
        //                            }
        //                            catch (Exception)
        //                            {
        //                                sOut = "Richiesta data";
        //                                //Control.Text = s;
        //                            }

        //                            break;
        //                        case "System.Boolean":
        //                            //controllo tipo 
        //                            try
        //                            {
        //                                bool i = Convert.ToBoolean(Control.Text);
        //                            }
        //                            catch (Exception)
        //                            {
        //                                sOut = "Richiesto booleano";
        //                            }

        //                            break;
        //                    }
        //                }

        //                break;
        //        }
        //        return sOut;
        //    }

        //    public static void DataGridViewLayout(DataGridView dg,bool EnableSelect)
        //    {
        //        ////colonne
        //        //DataGridLoadColumnWidth(dg,dg.Tag);
        //        //int iLast = 0;
        //        //for (int i = 0; i < dg.Columns.Count; i++)
        //        //{
        //        //    dg.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
        //        //    if (dg.Columns[i].Visible == true) if (dg.Columns[i].DisplayIndex > iLast) iLast = i;
        //        //}
        //        //if (iLast < dg.Columns.Count)
        //        //{
        //        //    dg.Columns[iLast].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        //        //    dg.Columns[iLast].MinimumWidth = 50;
        //        //}

        //        //righe
        //        dg.RowHeadersWidth = 24;

        //        //colori
        //        dg.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
        //        if (!EnableSelect)
        //        {
        //            dg.AlternatingRowsDefaultCellStyle.SelectionBackColor = dg.AlternatingRowsDefaultCellStyle.BackColor;
        //            dg.DefaultCellStyle.SelectionForeColor = dg.DefaultCellStyle.ForeColor;
        //            dg.DefaultCellStyle.SelectionBackColor = dg.DefaultCellStyle.BackColor;
        //        }

        //        ////font
        //        //dg.Font = new Font(dg.Parent.Font.FontFamily, (float)10);
        //        //dg.DefaultCellStyle.Font = new Font(dg.Parent.Font.FontFamily, (float)10);
        //        //dg.RowHeadersDefaultCellStyle.Font = new Font(dg.Parent.Font.FontFamily, (float)10);
        //        //dg.RowsDefaultCellStyle.Font = new Font(dg.Parent.Font.FontFamily, (float)10);
        //        //dg.RowTemplate.DefaultCellStyle.Font = new Font(dg.Parent.Font.FontFamily, (float)10);
        //        //for (int i=0;i<dg.Columns.Count;i++)
        //        //    dg.Columns[i].DefaultCellStyle.Font = new Font(dg.Parent.Font.FontFamily, (float)10);

        //        //dg.ForeColor = Color.Black;
        //        //dg.DefaultCellStyle.SelectionForeColor = Color.Black;

        //        ////modalit� selezione
        //        //if (dg.ReadOnly) dg.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        //        //else dg.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
              
        //    }

        //    public static void DataGridLoadColumnWidth(DataGridView DataGrid,object Suffix)
        //    {
        //        //string sName = GetPath(DataGrid);
        //        //if (Suffix != null) if (Suffix.ToString().Length > 0) sName += "." + Suffix.ToString();

        //        //DbLink db = new DbLink();
        //        //DataTable dt = new DataTable();
        //        //string s = "SELECT * FROM Conf_Colonne";
        //        //s += " WHERE Controllo='" + sName + "'";
        //        //s += " ORDER BY Posizione";
        //        //db.ReadDataTable(dt, s);

        //        //foreach (DataRow dr in dt.Rows)
        //        //{
        //        //    string sColName = dr["Nome"].ToString();
        //        //    if (DataGrid.Columns.Contains(sColName) == true)
        //        //    {
        //        //        DataGrid.Columns[sColName].HeaderText = dr["Intestazione"].ToString();
        //        //        DataGrid.Columns[sColName].DataPropertyName = dr["Campo"].ToString();
        //        //        int iPos = (int)dr["Posizione"];
        //        //        if (iPos<DataGrid.Columns.Count) DataGrid.Columns[sColName].DisplayIndex = iPos;
        //        //        DataGrid.Columns[sColName].Width = (int)dr["Larghezza"];
        //        //        DataGrid.Columns[sColName].Visible = (bool)dr["Visibile"];
        //        //        DataGrid.Columns[sColName].ReadOnly = (bool)dr["SolaLettura"];
        //        //        if ((bool)dr["Dimensionabile"]) DataGrid.Columns[sColName].Resizable = DataGridViewTriState.True;
        //        //        else DataGrid.Columns[sColName].Resizable = DataGridViewTriState.False;
        //        //    }
        //        //}
        //        //DataGrid.Refresh();
        //    }

        //    private static string GetPath(Control c)
        //    {
        //        if (c.Parent == null) return c.Name;
        //        else return GetFirstParent(c) + "." + c.Name;
        //    }

        //    private static string GetFirstParent(Control c)
        //    {
        //        if (c.Parent == null) return c.Name;
        //        else return GetFirstParent(c.Parent);
        //    }

        //    public static void DataGridSaveColumnWidth(DataGridView DataGrid,object Suffix)
        //    {
        //        ////salvo larghezza colonne per datagrid
        //        //if (DataGrid.AllowUserToResizeColumns)
        //        //{
        //        //    string sName = GetPath(DataGrid);
        //        //    if (Suffix != null) if (Suffix.ToString().Length>0) sName += "." + Suffix.ToString();

        //        //    string s;
        //        //    DbLink Db = new DbLink();
        //        //    System.Data.SqlClient.SqlCommand Cmd = Db.Connection.CreateCommand();

        //        //    Cmd.Connection.Open();

        //        //    s = "DELETE FROM Conf_Colonne";
        //        //    s += " WHERE Controllo='" + sName + "'";
        //        //    Cmd.CommandText = s;
        //        //    Cmd.ExecuteScalar();

        //        //    for (int i = 0; i < DataGrid.Columns.Count; i++)
        //        //    {
        //        //        //controllo largh. minima/massima (30px/600px)
        //        //        if (DataGrid.Columns[i].Visible)
        //        //        {
        //        //            int w = (int)DataGrid.Columns[i].Width;
        //        //            if (w < 30) w = 30;
        //        //            if (w > 1500) w = 1500;
        //        //            DataGrid.Columns[i].Width = w;
        //        //        }

        //        //        s = "INSERT INTO Conf_Colonne (Controllo,Posizione,Nome,Intestazione,Campo,Larghezza,Visibile,SolaLettura,Dimensionabile)";
        //        //        s += " VALUES ('" + sName + "'";
        //        //        s += "," + DataGrid.Columns[i].DisplayIndex.ToString();
        //        //        s += ",'" + DataGrid.Columns[i].Name + "'";
        //        //        s += ",'" + DataGrid.Columns[i].HeaderText + "'";
        //        //        s += ",'" + DataGrid.Columns[i].DataPropertyName + "'";
        //        //        s += "," + DataGrid.Columns[i].Width.ToString();
        //        //        if (DataGrid.Columns[i].Visible == true) s += ",1";
        //        //        else s += ",0";
        //        //        if (DataGrid.Columns[i].ReadOnly == true) s += ",1";
        //        //        else s += ",0";
        //        //        if (DataGrid.Columns[i].Resizable == DataGridViewTriState.True) s += ",1";
        //        //        else if (DataGrid.Columns[i].Resizable == DataGridViewTriState.False) s += ",0";
        //        //        else s += ",Null";
        //        //        s += ")";
        //        //        Cmd.CommandText = s;
        //        //        Cmd.ExecuteScalar();
        //        //    }

        //        //    Cmd.Connection.Close();
        //        //}
        //    }
        //}

        //public class DbFormResult
        //{
        //    public bool Ok=true;
        //    public string Descrizione="";
        //    public string Function = "";
        //}
    }
}