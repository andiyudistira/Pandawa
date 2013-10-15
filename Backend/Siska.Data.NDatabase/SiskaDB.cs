namespace Siska.Data
{
    using System;
    using System.IO;
    using System.Linq;
    using NDatabase;

    public class SiskaDB : ISiskaDB, IDisposable
    {
        public bool IsInitialised { get; set; }

        public SiskaDB()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SiskaApp");

            if (Directory.Exists(dbPath))
            {
                CreateOrOpenDB(dbPath);
            }
            else
            {
                Directory.CreateDirectory(dbPath);
                CreateOrOpenDB(dbPath);
            }
            
            IsInitialised = true;
        }

        private void CreateOrOpenDB(string dbPath)
        {
            string filePath = Path.Combine(dbPath, "SiskaDB.ndb");            

            if (File.Exists(filePath))
            {
                using (var odb = OdbFactory.Open(filePath))
                {
                    
                }
            }
            else
            {
                using (var odb = OdbFactory.Open(filePath))
                {
                    
                }
            }

            //EloqueraDB = new DB("server=localhost;options=none;");
            ////DB.Configuration.ServerSettings.DatabasePath = dbPath;

            //EloqueraDB.DeleteDatabase("SiskaDB", true);

            //string[] dbList = EloqueraDB.GetDbList();

            //if (!string.IsNullOrEmpty(dbList.ToList().Find(pred => pred == "SiskaDB")))
            //{
            //    EloqueraDB.OpenDatabase("SiskaDB");
            //}
            //else
            //{
            //    EloqueraDB.CreateDatabase("SiskaDB");
            //    EloqueraDB.OpenDatabase("SiskaDB");
            //}
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                lock (this)
                {
                    //EloqueraDB.Dispose();
                }
            }
        }
    }
}
