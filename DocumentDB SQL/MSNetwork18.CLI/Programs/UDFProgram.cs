using MSNetwork18.CLI.DI_Container;
using MSNetwork18.DAL.Interface;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System.Linq;
using System.IO;

namespace MSNetwork18.CLI.Programs
{
    public class UDFProgram : BaseProgram
    {
        enum UDFOption
        {
            Use = 1,
            Create = 2
        }

        public ISQLUDFRepository _udfRepository { get; set; }
        public ISQLDocumentRepository _documentRepository { get; set; }

        public UDFProgram()
        {
            _udfRepository = DIProvider.GetServiceProvider().GetService<ISQLUDFRepository>();
            _documentRepository = DIProvider.GetServiceProvider().GetService<ISQLDocumentRepository>();
        }

        public async Task Run()
        {
            WriteLine("1. Use UDF");
            WriteLine("2. Create");

            UDFOption option = (UDFOption)ProgramHelper.EnterInt("");

            switch (option)
            {
                case UDFOption.Use:
                    {
                        // The function work with zips collection and msnet18sql database.
                        // If you need a different collection or database, please write a piece of code to input data into the program.
                        string id = ProgramHelper.EnterText("Id ");
                        SqlQuerySpec query = new SqlQuerySpec()
                        {
                            QueryText = "SELECT c.id, c.loc, udf.udfCityState(c) as CityState FROM c where c.id = @id",
                            Parameters = new SqlParameterCollection()
                            {
                                new SqlParameter("@id", id)
                            }
                        };

                        Document document = _documentRepository.ReadDocumentByQuery<Document>(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId), query).ToList().FirstOrDefault();

                        WriteLine(document.ToString());

                        break;
                    }
                case UDFOption.Create:
                    {
                        UserDefinedFunction udfDefinition = new UserDefinedFunction
                        {
                            Id = "udfRegex",
                            Body = File.ReadAllText(@"Scripts\udfRegex.js")
                        };

                        string databaseName = "";
                        DocumentCollection collectionName = null;

                        if (!InsertCollAndDatabase(ref databaseName, ref collectionName))
                        {
                            Warning("Collection >>> " + collectionName + " <<< don't exist.");
                            collectionName = await _collectionRepository.CreateCollection(databaseName, collectionName.Id);
                            ProgramHelper.Wait();
                        }

                        UserDefinedFunction newUDFunction = await _udfRepository.CreateUDFAsync(UriFactory.CreateDocumentCollectionUri(databaseName, collectionName.Id), udfDefinition);

                        WriteLine(string.Format("Created trigger {0}; RID: {1}", newUDFunction.Id, newUDFunction.ResourceId));

                        break;
                    }
                default:
                    break;
            }
        }
    }
}
