using System;
using System.Data;
using System.Data.SqlClient;

namespace Metra.ManutenzioneDatiAssemblaggio
{
    public class DbBridge
    {
        #region VARIABILI PRIVATE

        string _ConnectionString;
        SqlConnection _Cnn;
        SqlCommand _Cmd;
        private bool _showErrors = true;

        #endregion

        #region COSTRUTTORE

        public DbBridge(string ConnectionString)
        {
            try
            {
                _ConnectionString = ConnectionString;
                _Cnn = new SqlConnection(_ConnectionString);
                _Cmd = new SqlCommand();
                _Cmd.Connection = _Cnn;
            }
            catch (Exception)
            { }
        }

        public DbBridge()
        {
            try
            {
                _ConnectionString = Properties.Settings.Default.ConnectionString;
                _Cnn = new SqlConnection(_ConnectionString);
                _Cmd = new SqlCommand();
                _Cmd.Connection = _Cnn;
            }
            catch (Exception)
            { }
        }

        public DbBridge(DataSet dataSet)
        {
            DataSet = dataSet;
            try
            {
                _ConnectionString = Properties.Settings.Default.ConnectionString;
                _Cnn = new SqlConnection(_ConnectionString);
                _Cmd = new SqlCommand();
                _Cmd.Connection = _Cnn;
            }
            catch (Exception)
            { }
        }

        #endregion

        #region PROPRIETA'

        public DataSet DataSet;

        public SqlConnection Connection
        {
            get { return _Cnn; }
            set { _Cnn = value; }
        }

        public string Server
        {
            get
            {
                string[] list = _ConnectionString.ToUpper().Split(';');
                foreach (string s in list)
                {
                    if (s.StartsWith("DATA SOURCE="))
                    {
                        return s.Substring(12, s.Length - 12).Trim();
                    }
                }
                return "--";

                //string pattern = "(?<DS>Data Source\\s?=\\s?)(?<Server>\\w{1,50}\\\\\\w{1,50});(?<IC>Initial Catalog\\s?=\\s?)(?<Database>\\w{1,50})";
                //Regex r = new Regex(pattern);
                //if (r.IsMatch(_ConnectionString))
                //{
                //    return r.Matches(_ConnectionString)[0].Groups["Server"].Value.ToString();
                //}
                //else
                //{
                //    pattern = "(?<DS>Data Source\\s?=\\s?)(?<Server>\\w{1,50}\\\\\\w{1,50});(?<IC>Initial Catalog\\s?=\\s?)(?<Database>\\w{1,50})";
                //    r = new Regex(pattern);
                //    if (r.IsMatch(_ConnectionString))
                //    {
                //        return r.Matches(_ConnectionString)[0].Groups["Server"].Value.ToString();
                //    }
                //    else
                //    {
                //        return "--";
                //    }
                //}
            }
        }

        public string Database
        {
            get
            {
                string[] list = _ConnectionString.ToUpper().Split(';');
                foreach (string s in list)
                {
                    if (s.StartsWith("INITIAL CATALOG="))
                    {
                        return s.Substring(16, s.Length - 16).Trim();
                    }
                }
                return "--";

                //string pattern = "(?<DS>Data Source\\s?=\\s?)(?<Server>\\w{1,50}\\\\\\w{1,50});(?<IC>Initial Catalog\\s?=\\s?)(?<Database>\\w{1,50})";
                //Regex r = new Regex(pattern);
                //if (r.IsMatch(_ConnectionString))
                //{
                //    return r.Matches(_ConnectionString)[0].Groups["Database"].Value.ToString();
                //}
                //else
                //{
                //    return "--";
                //}
            }
        }

        public SqlCommand Command
        {
            get { return _Cmd; }
        }

        public bool ShowErrors
        {
            get { return _showErrors; }
            set { _showErrors = value; }
        }

        #endregion

        #region METODI PUBBLICI

        public enum LogType { NEW, EDIT, DELETE, SAVE };

        public void Log(LogType tipo, string lotto, string descrizione)
        {
            _Cmd.CommandText = "INSERT INTO Log" +
                "(" +
                " TS," +
                " USR," +
                " NumeroLotto," +
                " Operazione," +
                " Descrizione" +
                ")" +
                " VALUES" +
                "(" +
                " GETDATE()," +
                " @Usr," +
                " @NumeroLotto," +
                " @Operazione," +
                " @Descrizione" +
                ")";
            _Cmd.Parameters.AddWithValue("@Usr", Authentication.MachineName + "\\" + Authentication.UserName);
            _Cmd.Parameters.AddWithValue("@NumeroLotto", lotto);
            _Cmd.Parameters.AddWithValue("@Operazione", tipo.ToString());
            _Cmd.Parameters.AddWithValue("@Descrizione", descrizione);
            try
            {
                //salvataggio log
                _Cnn.Open();
                _Cmd.ExecuteNonQuery();
                _Cnn.Close();
            }
            catch
            {
                //errore in salvataggio log: non gestito
            }
        }


        public bool ReadDataTable(DataTable dt)
        {
            //Carico tabella da sql server
            bool bOk = false;

            if (dt != null)
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(_Cmd);
                    da.FillSchema(dt, System.Data.SchemaType.Source);
                    da.Fill(dt);
                    dt.TableName = _Cmd.CommandText;
                    //_Cmd.Parameters.Clear();
                    if (DataSet != null && !DataSet.Tables.Contains(dt.TableName)) DataSet.Tables.Add(dt);
                    bOk = true;
                }
                catch (Exception ex)
                {
                    if (_showErrors) System.Windows.Forms.MessageBox.Show(ex.Message + "\n\nQuery:" + _Cmd.CommandText, "inVoice", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    bOk = false;
                }
            }

            //esito
            return bOk;
        }

        public bool ReadDataTable(DataTable dt, string sQuery)
        {
            return ReadDataTable(dt, sQuery, true, false);
        }

        public bool ReadDataTable(DataTable dt, string sQuery, bool readSchema)
        {
            return ReadDataTable(dt, sQuery, readSchema, false);
        }

        public bool ReadDataTable(DataTable dt, string sQuery, bool readSchema, bool append)
        {
            //Carico tabella da sql server
            bool bOk = false;

            if (dt != null)
            {
                try
                {
                    if (!append) dt.Rows.Clear();
                    _Cmd.CommandText = sQuery;
                    SqlDataAdapter da = new SqlDataAdapter(_Cmd);
                    if (readSchema) da.FillSchema(dt, System.Data.SchemaType.Source);
                    da.Fill(dt);
                    dt.TableName = sQuery;
                    if (DataSet != null && !DataSet.Tables.Contains(dt.TableName)) DataSet.Tables.Add(dt);
                    bOk = true;
                }
                catch (Exception ex)
                {
                    if (_showErrors) System.Windows.Forms.MessageBox.Show(ex.Message + "\n\nQuery:" + sQuery, "inVoice", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    bOk = false;
                }
            }

            //esito
            return bOk;
        }

        public bool ReadDataTableSchema(DataTable dt, string sQuery)
        {
            //Carico schema tabella da sql server
            bool bOk = false;

            try
            {
                dt.Rows.Clear();
                _Cmd.CommandText = sQuery;
                SqlDataAdapter da = new SqlDataAdapter(_Cmd);
                da.FillSchema(dt, System.Data.SchemaType.Source);
                dt.TableName = sQuery;
                bOk = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                bOk = false;
            }

            //esito
            return bOk;
        }

        public Result UpdateDataTable(DataTable dt)
        {
            //salvo su sql server
            Result ris = new Result();
            ris.Function = "UpdateDataTable";

            ris.Ok = false;
            ris.Descrizione = "";
            if (_ConnectionString.Length > 0)
            {
                if (dt.GetChanges() != null)
                {
                    try
                    {
                        string s = dt.TableName;
                        if (s.IndexOf("WHERE ") > 0) s = s.Substring(0, s.IndexOf("WHERE "));
                        SqlDataAdapter da = new SqlDataAdapter(s, _Cnn);
                        SqlCommandBuilder cb = new SqlCommandBuilder(da);
                        int i = 0;
                        if (DataSet == null)
                        {
                            i = da.Update(dt);
                        }
                        else
                        {
                            i = da.Update(DataSet, dt.TableName);
                        }
                        ris.Ok = true;
                    }
                    catch (Exception ex)
                    {
                        ris.Ok = false;
                        ris.Descrizione = dt.TableName + ": " + ex.Message;
                        if (_showErrors) System.Windows.Forms.MessageBox.Show(ris.Descrizione, "Metra", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    }
                }
                else ris.Ok = true;
            }

            //esito
            return ris;
        }

        public bool IsModified(DataTable dt)
        {
            bool bMod = false;
            foreach (DataRow dr in dt.Rows)
                if (dr.RowState != DataRowState.Unchanged && dr.RowState != DataRowState.Deleted)
                {
                    string s1;
                    string s2;
                    for (int i = 0; i < dr.Table.Columns.Count; i++)
                    {
                        s1 = dr[i].ToString();
                        try { s2 = dr[i, DataRowVersion.Original].ToString(); }
                        catch { s2 = "_" + s1; }
                        Console.WriteLine((s1 == s2).ToString() + " (" + dr.Table.Columns[i].ColumnName + ") : " + s1 + " - " + s2);
                    }
                    bMod = true;
                    break;
                }
            return bMod;
        }

        public string GetSqlText(string ViewName)
        {
            string Sql;
            string s = "SELECT * FROM information_schema.views";
            s += " WHERE TABLE_NAME='" + ViewName + "'";
            DataTable dt = new DataTable();
            ReadDataTable(dt, s);
            if (dt.Rows.Count > 0)
            {
                Sql = dt.Rows[0]["VIEW_DEFINITION"].ToString();
                Sql = Sql.Replace("\r\n", " ");
                while (Sql.IndexOf("  ") > 0)
                    Sql = Sql.Replace("  ", " ");
                Sql = Sql.Replace("CREATE VIEW dbo." + ViewName + " AS ", "");
                Sql = Sql.Replace("CREATE VIEW [dbo].[" + ViewName + "] AS ", "");
            }
            else
                Sql = null;

            return Sql;
        }

        public string SqlDate(DateTime d)
        {
            string s2 = d.Year.ToString() + "-" + d.Month.ToString() + "-" + d.Day.ToString();
            return "CONVERT(DATETIME, '" + s2 + "', 102)";
        }

        public string SqlBool(Boolean b)
        {
            if (b) return "0";
            else return "-1";
        }

        #endregion

        #region METODI PRIVATI

        private void DisplayData(DataTable table)
        {
            Console.WriteLine("============================");
            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn col in table.Columns)
                {
                    Console.WriteLine("{0} = {1}", col.ColumnName, row[col]);
                }
                Console.WriteLine("============================");
            }
        }

        #endregion
    }
}
