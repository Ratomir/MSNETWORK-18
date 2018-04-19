using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using MSNetwork18.DAL.Interface;
using System;
using System.Threading.Tasks;

namespace MSNetwork18.DAL
{
    public class SQLUDFRepository : BaseDatabaseRepository, ISQLUDFRepository
    {
        public SQLUDFRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<UserDefinedFunction> CreateUDFAsync(Uri collection, UserDefinedFunction udf, RequestOptions requestOptions = null)
        {
            return await Client.CreateUserDefinedFunctionAsync(collection, udf, requestOptions); 
        }
    }
}
