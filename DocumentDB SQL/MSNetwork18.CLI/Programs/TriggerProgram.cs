using Microsoft.Azure.Documents.Spatial;
using MSNetwork18.CLI.DI_Container;
using MSNetwork18.DAL.Interface;
using MSNetwork18.Model.Zips;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System.IO;

namespace MSNetwork18.CLI.Programs
{
    public class TriggerProgram : BaseProgram
    {
        enum SPOption
        {
            InsertPreTrigger = 1,
            ReplacePostTrigger = 2,
            CreateTriggers = 3
        };

        public ISQLTriggerRepository _triggerRepository { get; set; }
        public ISQLDocumentRepository _documentRepository { get; set; }

        public TriggerProgram()
        {
            _triggerRepository = DIProvider.GetServiceProvider().GetService<ISQLTriggerRepository>();
            _documentRepository = DIProvider.GetServiceProvider().GetService<ISQLDocumentRepository>();
        }

        public async Task Run()
        {
            WriteLine("1. Insert, pre trigger");
            WriteLine("2. Update, pre trigger");
            WriteLine("3. Create triggers");

            SPOption option = (SPOption)ProgramHelper.EnterInt("");

            switch (option)
            {
                case SPOption.InsertPreTrigger:
                    {
                        ZipModel model = new ZipModel()
                        {
                            City = "Sarajevo",
                            Pop = 1854,
                            State = "BiH",
                            Loc = new Point(43.8607994, 18.4018904),
                            Id = "989898"
                        };

                        Document findZipModel = await _documentRepository.ReadDocumentByIdAsync<Document>(model.Id, UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId));
                        if (findZipModel != null)
                        {
                            await _documentRepository.DeleteDocument(DatabaseId, CollectionId, findZipModel.Id);
                        }

                        Document document = await _documentRepository.InsertDocument(DatabaseId, CollectionId, model, new RequestOptions { PreTriggerInclude = new[] { "setCreatedDate" } });
                        Success("New document successfully created.");
                        break;
                    }
                case SPOption.ReplacePostTrigger:
                    {
                        ZipModel model = await _documentRepository.ReadDocumentByIdAsync<ZipModel>("989898", UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId));

                        model.City = "Neum";

                        ZipModel updatedModel = await _documentRepository.UpdateDocument<ZipModel>(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, model.Id), model, new RequestOptions { PreTriggerInclude = new[] { "setUpdatedDate" } });


                        Success("Document with id >>> " + updatedModel.Id + " <<< updated.");
                        Success("Created date time >>> " + updatedModel.CreatedDateTime.Value.ToString("MM/dd/yyyy hh:mm:ss tt") + " <<<.");
                        Success("Updated date time >>> " + updatedModel.UpdatedDateTime.Value.ToString("MM/dd/yyyy hh:mm:ss tt") + " <<< updated.");
                        break;
                    }
                case SPOption.CreateTriggers:
                    {
                        Trigger setCreatedDate = new Trigger()
                        {
                            Id = "setCreatedDate",
                            Body = File.ReadAllText(@"Scripts\trSetCreatedDate.js"),
                            TriggerOperation = TriggerOperation.Create,
                            TriggerType = TriggerType.Pre
                        };

                        Trigger setUpdatedDate = new Trigger()
                        {
                            Id = "setUpdatedDate",
                            Body = File.ReadAllText(@"Scripts\trSetUpdatedDate.js"),
                            TriggerOperation = TriggerOperation.Update,
                            TriggerType = TriggerType.Pre
                        };

                        Trigger newCreatedTrigger = await _triggerRepository.CreateTriggerAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId), setCreatedDate);
                        WriteLine(string.Format("Created trigger {0}; RID: {1}", newCreatedTrigger.Id, newCreatedTrigger.ResourceId));

                        Trigger newUpdatedTrigger = await _triggerRepository.CreateTriggerAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId), setUpdatedDate);
                        WriteLine(string.Format("Created trigger {0}; RID: {1}", newUpdatedTrigger.Id, newUpdatedTrigger.ResourceId));

                        break;
                    }
            }
        }
    }
}
