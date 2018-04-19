using Microsoft.Azure.Documents;
using Microsoft.Extensions.Configuration;
using MSNetwork18.DAL.Interface;
using System;
using System.Threading.Tasks;

namespace MSNetwork18.DAL
{
    public class SQLTriggerRepository : BaseDatabaseRepository, ISQLTriggerRepository
    {
        public SQLTriggerRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<Trigger> CreateTriggerAsync(Uri collection, Trigger trigger)
        {
            return await Client.CreateTriggerAsync(collection, trigger);
        }
    }
}
