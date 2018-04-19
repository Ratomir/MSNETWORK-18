using Microsoft.Azure.Documents;
using System;
using System.Threading.Tasks;

namespace MSNetwork18.DAL.Interface
{
    public interface ISQLTriggerRepository : IBaseRepository
    {
        Task<Trigger> CreateTriggerAsync(Uri collection, Trigger trigger);
    }
}
