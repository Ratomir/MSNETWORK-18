using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents.Client;
using MSNetwork18.DAL.Interface;
using MSNetwork18.Model.Email;
using MSNetwork18.Model.StoredProcedure;
using MSNetwork18.WebAPI.Model;
using System.Threading.Tasks;

namespace MSNetwork18.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Email")]
    public class EmailController : Controller
    {
        public ISQLDocumentRepository _documentRepository { get; set; }
        public ISQLStoredProcedureRepository _storedRepository { get; set; }

        public EmailController(ISQLDocumentRepository documentRepository, ISQLStoredProcedureRepository storedProcedure)
        {
            _documentRepository = documentRepository;
            _storedRepository = storedProcedure;
        }

        [HttpGet("senders")]
        public async Task<IActionResult> Get()
        {
            DataListSPModel<DistinctSenders> senders = await _storedRepository.RunStoredProcedureAsync<DataListSPModel<DistinctSenders>>(UriFactory.CreateStoredProcedureUri("msnet18sql", "email", "spGetDistinctSenders"));

            return Ok(senders.Data);
        }

        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(string id)
        {
            EmailModel emailModel = await _documentRepository.ReadDocumentByIdAsync<EmailModel>(id, UriFactory.CreateDocumentCollectionUri("msnet18sql", "email"));
            return Ok(emailModel);
        }
        
    }
}
