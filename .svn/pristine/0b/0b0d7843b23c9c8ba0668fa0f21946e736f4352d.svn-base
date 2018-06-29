using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

//Rivedere con gestione sia versione db che exe (usando campo tipo)
//direi di tenere in linea solamente l'ultima versione dell'exe
//lanciare in automatico la versione appena scaricata

namespace Metra.ManutenzioneDatiAssemblaggio
{
    static class Versioning
    {
        public enum ElementType { Exe, Database };

        /// <summary>
        /// Confronto versione corrente con quella pubblicata in db:
        /// 
        ///  - se pubblicata > corrente: scarico versione da db (solo se non sono in ide) 
        ///  - se corrente > pubblicata: salvo versione in db
        ///  - se corrente = pubblicata: non faccio nulla
        ///  
        /// </summary>
        /// <param name="IsExeChanged"></param>
        public static void CheckCurrentVersion(out bool IsExeChanged)
        {
            IsExeChanged = false;

            //controllo esistenza tabella versioni
            CreateVersionTable();

            //versione eseguibile
            IsExeChanged = false;
            Version ExeCurr = new Version(Application.ProductVersion);
            Version ExeLast = GetLastVersionNumber(ElementType.Exe);
            if (ExeLast > ExeCurr)
            {
                //posso ricaricare da db solo se non sono in ide
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    IsExeChanged = false;
                }
                else
                {
                    IsExeChanged = LoadExeVersion(ExeLast);
                }
            }
            else if (ExeCurr > ExeLast)
            {
                //aggiorno versione in database
                SaveCurrentExeVersion();
            }
        }

        public static Version GetLastVersionNumber(ElementType elemType)
        {
            //ultima versione disponibile in db
            DbBridge db = new DbBridge();
            string s = "SELECT TOP 1 Major,Minor,Build,Revision" +
                " FROM Versioni" +
                " WHERE Applicazione=@AppName" +
                " AND Tipo=@ElemType" +
                " ORDER BY Major DESC,Minor DESC,Build DESC,Revision DESC";
            DataTable dt = new DataTable();
            db.Command.CommandText = s;
            db.Command.Parameters.Add("AppName", SqlDbType.Char);
            db.Command.Parameters.Add("ElemType", SqlDbType.Char);
            db.Command.Parameters["AppName"].Value = Application.ProductName;
            db.Command.Parameters["ElemType"].Value = elemType;
            SqlDataAdapter da = new SqlDataAdapter(db.Command);
            da.Fill(dt);
            //db.ReadDataTable(dt, s);

            //in formato stringa
            s = "";
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    s += dt.Rows[0][i].ToString() + ".";
                }
                s = s.Substring(0, s.Length - 1);
            }
            else
            {
                s = "0.0.0.0";
            }

            //restituisco formato versione
            return new Version(s);
        }

        private static bool LoadExeVersion(Version v)
        {
            string FileName = Application.ExecutablePath;
            string BackupExtension = DateTime.Today.ToShortDateString().Replace('/', '.');
            string BackupFileName = System.IO.Path.ChangeExtension(FileName, BackupExtension);

            //backup file originale (rinomino)
            //bool Backup = true;
            bool Ok = true;
            if (File.Exists(FileName))
            {
                try { if (File.Exists(BackupFileName)) File.Delete(BackupFileName); }
                catch { Ok = false; }
                if (Ok)
                {
                    try { System.IO.File.Move(FileName, BackupFileName); }
                    catch { Ok = false; }
                }
            }
            //else
            //{
            //    Backup = false;
            //}

            //se ok procedo
            if (Ok)
            {
                string s = "SELECT Bin FROM Versioni" +
                    " WHERE Tipo='" + ElementType.Exe.ToString() + "'" +
                    " AND Applicazione='" + Application.ProductName.Replace("'", "''") + "'" +
                    " AND Major = " + v.Major.ToString() +
                    " AND Minor = " + v.Minor.ToString() +
                    " AND Build = " + v.Build.ToString() +
                    " AND Revision = " + v.Revision.ToString();

                DbBridge db = new DbBridge();
                db.Connection.Open();
                db.Command.CommandText = s;
                using (SqlDataReader reader = db.Command.ExecuteReader(System.Data.CommandBehavior.Default))
                {
                    if (reader.Read())
                    {
                        // read in using GetValue and cast to byte array
                        byte[] fileData = (byte[])reader.GetValue(0);

                        // write bytes to disk as file
                        using (System.IO.FileStream fs = new System.IO.FileStream(
                            FileName,
                            System.IO.FileMode.Create,
                            System.IO.FileAccess.ReadWrite))
                        {
                            // use a binary writer to write the bytes to disk
                            using (System.IO.BinaryWriter bw = new System.IO.BinaryWriter(fs))
                            {
                                bw.Write(fileData);
                                try
                                {
                                    bw.Close();
                                }
                                catch
                                {
                                    Ok = false;
                                }
                            }
                        }
                    }

                    // close reader to database
                    reader.Close();
                }
                db.Connection.Close();
            }

            //esito
            return Ok;
        }

        private static void SaveCurrentExeVersion()
        {
            Version v = new Version(Application.ProductVersion);
            string sPathToFileToSave = Application.ExecutablePath;
            using (System.IO.FileStream fs = new System.IO.FileStream(
                sPathToFileToSave,
                System.IO.FileMode.Open,
                System.IO.FileAccess.Read))
            {
                byte[] fileData = new byte[fs.Length];
                fs.Read(fileData, 0, System.Convert.ToInt32(fs.Length));
                fs.Close();

                DbBridge db = new DbBridge();

                //cancello se esiste già
                db.Command.CommandText = "DELETE FROM Versioni" +
                    " WHERE Tipo='" + ElementType.Exe.ToString() + "'" +
                    " AND Applicazione='" + Application.ProductName.Replace("'", "''") + "'" +
                    " AND Major=" + v.Major.ToString() +
                    " AND Minor=" + v.Minor.ToString() +
                    " AND Build=" + v.Build.ToString() +
                    " AND Revision=" + v.Revision.ToString();
                db.Connection.Open();
                db.Command.ExecuteNonQuery();
                db.Connection.Close();

                //salvo versione in database
                db.Command.CommandText = "INSERT INTO Versioni" +
                    " (Bin, Data, Tipo, Applicazione, Major, Minor, Build, Revision)" +
                    " VALUES" +
                    " (@Bin, @Data,'" + ElementType.Exe.ToString() + "','" + Application.ProductName.Replace("'", "''") + "'," + v.Major.ToString() + "," + v.Minor.ToString() + "," + v.Build.ToString() + "," + v.Revision.ToString() + ")";

                SqlParameter paramFileField = new SqlParameter();
                paramFileField.ParameterName = "Bin";
                paramFileField.SqlDbType = System.Data.SqlDbType.Image;
                paramFileField.Value = fileData;
                db.Command.Parameters.Add(paramFileField);

                SqlParameter paramDateCreated = new SqlParameter();
                paramDateCreated.ParameterName = "Data";
                paramDateCreated.SqlDbType = System.Data.SqlDbType.DateTime;
                paramDateCreated.Value = DateTime.Now;
                db.Command.Parameters.Add(paramDateCreated);

                db.Connection.Open();
                db.Command.ExecuteNonQuery();
                db.Connection.Close();
            }

        }

        #region MANUNTENZIONE DATABASE

        private static bool ExistsTable(DbBridge db, string Name)
        {
            bool Esiste;
            string s;

            s = "SELECT Name FROM SysObjects WHERE xtype='u' and Name = '" + Name + "'";
            db.Command.CommandText = s;
            bool IsOpen = db.Connection.State == ConnectionState.Open;
            if (!IsOpen) db.Connection.Open();
            Esiste = db.Command.ExecuteScalar() != null;
            if (!IsOpen) db.Connection.Close();

            return Esiste;
        }

        private static bool ExistsField(DbBridge db, string TableName, string FieldName)
        {
            bool Esiste;
            string s;

            s = "SELECT [" + FieldName + "] FROM [" + TableName + "] WHERE 1=2";
            db.Command.CommandText = s;
            bool IsOpen = db.Connection.State == ConnectionState.Open;
            if (!IsOpen) db.Connection.Open();
            try
            {
                db.Command.ExecuteScalar();
                Esiste = true;
            }
            catch
            {
                Esiste = false;
            }
            if (!IsOpen) db.Connection.Close();

            return Esiste;
        }

        private static bool TryAddField(DbBridge db, string TableName, string FieldName, string FieldType)
        {
            bool Creato = false;

            if (!ExistsField(db, TableName, FieldName))
            {
                db.Command.CommandText = "ALTER TABLE [" + TableName + "] ADD [" + FieldName + "] " + FieldType;
                bool IsOpen = db.Connection.State == ConnectionState.Open;
                if (!IsOpen) db.Connection.Open();
                try
                {
                    db.Command.ExecuteNonQuery();
                    Creato = true;
                }
                catch (Exception)
                {
                    Creato = false;
                }
                if (!IsOpen) db.Connection.Close();
            }

            return Creato;
        }

        private static void CreateVersionTable()
        {
            DbBridge db = new DbBridge();
            string s;
            if (!ExistsTable(db, "Versioni"))
            {
                s = "CREATE TABLE [dbo].[Versioni]" +
                    "(" +
                    " [ID] [int] IDENTITY(1,1) NOT NULL," +
                    " [Tipo] [char](10) COLLATE Latin1_General_CI_AS NOT NULL," +
                    " [Data] [datetime] NULL," +
                    " [Major] [int] NULL," +
                    " [Minor] [int] NULL," +
                    " [Build] [int] NULL," +
                    " [Revision] [int] NULL," +
                    " [Bin] [varbinary](max) NULL," +
                    " CONSTRAINT [PK_Versioni] PRIMARY KEY CLUSTERED " +
                    " (" +
                    " [ID] ASC" +
                    " )WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]" +
                    ") ON [PRIMARY]";

                db.Command.CommandText = s;
                db.Connection.Open();
                db.Command.ExecuteNonQuery();
                db.Connection.Close();
            }
        }

        #endregion
    }
}
