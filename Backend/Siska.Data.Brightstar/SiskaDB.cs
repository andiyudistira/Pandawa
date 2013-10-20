namespace Siska.Data
{
    using System;
    using System.IO;
    using System.Linq;
    using Siska.Core;
    using Siska.Data.BDao;

    public class SiskaDB : ISiskaDB, IDisposable
    {
        public bool IsInitialised { get; internal set; }

        public BsContext BsContext { get; internal set; }

        public SiskaDB()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SiskaApp");

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
                throw new DBException(ex.Message, ex.InnerException);
            }
        }

        private void CreateOrOpenDB(string dbPath)
        {
            var storeName = "SiskaDB";

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
                    BsContext.Dispose();
                }
            }
        }
    }
}
