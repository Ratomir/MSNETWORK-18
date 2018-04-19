using Microsoft.Azure.Documents;
using System.Threading.Tasks;

namespace MSNetwork18.CLI
{
    public class CollectionProgram : BaseProgram
    {
        public CollectionProgram() : base()
        {
        }

        public void ReadAllCollectionsFromDatabase()
        {
            string databaseName = ProgramHelper.ReadDatabaseName();

            _collectionRepository.ReadAllCollections(databaseName).ForEach(t => InfoAboutCollection(t));
        }

        public async Task CreateCollection()
        {
            string databaseName = ProgramHelper.ReadDatabaseName();
            string collectionName = ProgramHelper.ReadCollectionName();

            ProgramHelper.Divider();
            DocumentCollection documentCollection = await _collectionRepository.CreateCollection(databaseName, collectionName);

            InfoAboutCollection(documentCollection);
        }

        public async Task DeleteCollection()
        {
            string databaseName = ProgramHelper.ReadDatabaseName();
            string collectionName = ProgramHelper.ReadCollectionName();

            bool deleteResult = await _collectionRepository.DeleteCollection(databaseName, collectionName);

            if (deleteResult)
            {
                Success("Collection >>> " + collectionName + " <<< deleted.");
            }
            else
            {
                Error("Collection >>> " + collectionName + " <<< was not deleted.");
            }
        }

        public void InfoAboutCollection(DocumentCollection collection)
        {
            Info("Collection id:\t" + collection.Id);
            WriteLine("Resource id:\t" + collection.ResourceId);
            WriteLine("Self link:\t" + collection.SelfLink);
            WriteLine("Documents link:\t" + collection.DocumentsLink);
            WriteLine("UDFs link:\t" + collection.UserDefinedFunctionsLink);
            WriteLine("Store procedure link:\t" + collection.StoredProceduresLink);
            WriteLine("Triggers link:\t" + collection.TriggersLink);
            WriteLine("Time stamp:\t" + collection.Timestamp);
            ProgramHelper.Divider();
        }

    }
}
