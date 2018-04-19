using Microsoft.Azure.Documents;
using MSNetwork18.CLI.DI_Container;
using MSNetwork18.DAL.Interface;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.Documents.Client;
using MSNetwork18.Model;
using System.IO;

namespace MSNetwork18.CLI.Programs
{
    public class StoredProcedureProgram : BaseProgram
    {
        enum SPOption
        {
            Read = 1,
            Create = 2
        };

        public ISQLStoredProcedureRepository _baseRepository { get; set; }
        public StoredProcedureProgram() : base()
        {
            _baseRepository = DIProvider.GetServiceProvider().GetService<ISQLStoredProcedureRepository>();
        }

        public async Task Run()
        {
            string databaseName = "";
            DocumentCollection collectionName = null;

            if (!InsertCollAndDatabase(ref databaseName, ref collectionName))
            {
                Warning("Collection >>> " + collectionName + " <<< don't exist.");
                return;
            }

            WriteLine("1. Call sp, insert StoreModel");
            WriteLine("2. Create");

            SPOption option = (SPOption)ProgramHelper.EnterInt("");

            switch (option)
            {
                case SPOption.Read:
                    {
                        StoreModel storeModel = new StoreModel()
                        {
                            Address = new AddressModel()
                            {
                                AddressType = "Back Office",
                                CountryRegionName = "Neum",
                                Location = new LocationModel()
                                {
                                    City = "Neum",
                                    StateProvinceName = "Jadran"
                                },
                                PostalCode = "88390"
                            },
                            Name = "Super new Network bank RVS"
                        };

                        try
                        {
                            Document resultSP = await _baseRepository.RunStoredProcedureAsync(UriFactory.CreateStoredProcedureUri(databaseName, collectionName.Id, "spCreateDocIfIdIsUnique"), storeModel);

                            ProgramHelper.Divider();
                            Success("Result: ");
                            WriteLine(resultSP.ToString());
                        }
                        catch (Exception ex)
                        {
                            Error(ex.Message);
                        }

                        break;
                    }
                case SPOption.Create:
                    {
                        StoredProcedure sprocDefinition = new StoredProcedure
                        {
                            Id = "spGetCountByRegion",
                            Body = File.ReadAllText(@"Scripts\spGetCountByRegion.js")
                        };

                        StoredProcedure result = await _baseRepository.CreateStoredProcedureAsync(UriFactory.CreateDocumentCollectionUri(databaseName, collectionName.Id), sprocDefinition);

                        Success("Created stored procedure " + result.Id + " RID: " + result.ResourceId);
                        break;
                    }
            }
        }
    }
}
