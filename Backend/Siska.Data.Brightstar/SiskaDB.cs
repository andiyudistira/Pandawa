namespace Siska.Data
{
    using System;
    using System.IO;
    using System.Linq;
    using BrightstarDB.Client;
    using Siska.Core;
    using Siska.Data.Dao;

    public class SiskaDB : ISiskaDB, IDisposable
    {
        public bool IsInitialised { get; internal set; }

        public virtual BsContext BsContext { get; set; }

        public SiskaDB()
        {
            string dbPath = string.Empty;

            try
            {
                if (System.Configuration.ConfigurationManager.AppSettings["Platform"].ToString().Equals("Desktop"))
                {
                    dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SiskaApp");
                }
                else
                {
                    dbPath = Path.Combine(Environment.CurrentDirectory, "SiskaApp");
                }

                if (string.IsNullOrEmpty(dbPath))
                {
                    dbPath = "SiskaApp";
                }
            }
            catch (Exception ex)
            {
                throw new DBException(string.Format("There's an error while checking on the db path: {0}", ex.Message),
                    ex.InnerException);
            }

            try
            {
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
            catch (Exception ex)
            {
                throw new DBException(string.Format("There's an error while creating or opening the db: {0}", ex.Message), 
                    ex.InnerException);
            }
        }

        private void CreateOrOpenDB(string dbPath)
        {
            string storeName = string.Empty;

            if (System.Configuration.ConfigurationManager.AppSettings["Environment"].ToString().Equals("Test"))
            {
                storeName = "SiskaDB_Test"; 
            }
            else
            {
                storeName = "SiskaDB";
            }

            string connectionString =
                string.Format(@"Type=embedded;storesDirectory={0};StoreName={1};", dbPath, storeName);
            
            BsContext = new BsContext(connectionString);
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
                    BrightstarService.Shutdown();
                    BsContext.Dispose();
                }
            }
        }
    }
}
