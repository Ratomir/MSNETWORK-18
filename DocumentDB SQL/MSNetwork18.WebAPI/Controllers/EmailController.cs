using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents.Client;
using MSNetwork18.DAL.Interface;
using MSNetwork18.Model.Email;
using MSNetwork18.Model.StoredProcedure;
using MSNetwork18.WebAPI.Model;
using MSNetwork18.WebAPI.Model.Base;
using System.Threading.Tasks;

namespace MSNetwork18.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Email")]
    public class EmailController : Controller
    {
        public ISQLDocumentRepository _documentRepository { get; set; }
        public ISQLStoredProcedureRepository _storedRepository { get; set; }
        public ConfigurationModel ConfigurationModel { get; set; }

        public EmailController(ISQLDocumentRepository documentRepository, ISQLStoredProcedureRepository storedProcedure, ConfigurationModel configurationModel)
        {
            _documentRepository = documentRepository;
            _storedRepository = storedProcedure;
            ConfigurationModel = configurationModel;
        }

        [HttpGet("senders")]
        public async Task<IActionResult> Get()
        {
            DataListSPModel<DistinctSenders> senders = await _storedRepository.RunStoredProcedureAsync<DataListSPModel<DistinctSenders>>(UriFactory.CreateStoredProcedureUri(ConfigurationModel.Database, "email", "spGetDistinctSenders"));

            return Ok(senders.Data);
        }

        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(string id)
        {
            EmailModel emailModel = await _documentRepository.ReadDocumentByIdAsync<EmailModel>(id, UriFactory.CreateDocumentCollectionUri(ConfigurationModel.Database, "email"));
            return Ok(emailModel);
        }
        
    }
}
