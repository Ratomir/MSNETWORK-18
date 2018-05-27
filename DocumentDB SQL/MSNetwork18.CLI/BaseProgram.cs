using MSNetwork18.CLI.DI_Container;
using MSNetwork18.DAL.Interface;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.Documents;

namespace MSNetwork18.CLI
{
    public class BaseProgram : IDisposable
    {
        protected readonly string DatabaseId;
        protected readonly string CollectionId;

        public ISQLCollectionRepository _collectionRepository { get; set; }

        public BaseProgram()
        {
            _collectionRepository = DIProvider.GetServiceProvider().GetService<ISQLCollectionRepository>();
            DatabaseId = _collectionRepository.DatabaseId;
            CollectionId = _collectionRepository.CollectionId;
        }

        #region >>> IDisposable Support <<<

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion >>> IDisposable Support <<<

        public virtual void WriteLine(string text)
        {
            Console.WriteLine(text);
        }

        public virtual void Write(string text)
        {
            Console.Write(text);
        }

        public void Error(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            WriteLine(text);
            Console.ForegroundColor = ConsoleColor.Green;
        }

        public void Info(string text)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            WriteLine(text);
            Console.ForegroundColor = ConsoleColor.Green;
        }

        public void Success(string text)
        {
            Console.ForegroundColor = ConsoleColor.White;
            WriteLine(text);
            Console.ForegroundColor = ConsoleColor.Green;
        }

        public void Warning(string text)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            WriteLine(text);
            Console.ForegroundColor = ConsoleColor.Green;
        }

        public bool InsertCollAndDatabase(ref string databaseName, ref DocumentCollection collectionName)
        {
            databaseName = ProgramHelper.ReadDatabaseName();
            string collectionId = ProgramHelper.ReadCollectionName();

            bool ifCollectionExist = _collectionRepository.CheckIfCollectionExistAsync(databaseName, collectionId);

            if (ifCollectionExist)
            {
                collectionName = _collectionRepository.GetDocumentCollection(databaseName, collectionId);
                return true;
            }

            collectionName = new DocumentCollection()
            {
                Id = collectionId
            };
            return false;
        }
    }
}
