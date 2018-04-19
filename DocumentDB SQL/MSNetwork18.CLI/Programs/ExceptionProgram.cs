using MSNetwork18.CLI.DI_Container;
using MSNetwork18.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System.Linq;

namespace MSNetwork18.CLI.Programs
{
    public class ExceptionProgram : BaseProgram
    {
        public ISQLDocumentRepository _documentRepository { get; set; }

        public ExceptionProgram()
        {
            _documentRepository = DIProvider.GetServiceProvider().GetService<ISQLDocumentRepository>();
        }

        public async Task Run()
        {
            ProgramHelper.EnterText("Start the exception program...");

            SqlQuerySpec query = new SqlQuerySpec()
            {
                QueryText = "SELECT *, c.InvalidProperty FROM c"
            };

            try
            {
                _documentRepository.ReadDocumentByQuery<Document>(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId), query).ToList();
            }
            catch (AggregateException aex)
            {
                Error(aex.InnerException.Message);
            }
            catch (Exception ex)
            {
                Error(ex.Message);
            }
        }

    }
}
