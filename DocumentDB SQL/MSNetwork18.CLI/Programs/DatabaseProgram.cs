using Microsoft.Azure.Documents;
using MSNetwork18.CLI.DI_Container;
using MSNetwork18.DAL.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace MSNetwork18.CLI
{
    public class DatabaseProgram : BaseProgram
    {
        public ISQLDatabaseRepository _repository { get; set; }

        public DatabaseProgram() : base()
        {
            _repository = DIProvider.GetServiceProvider().GetService<ISQLDatabaseRepository>();    
        }

        public async Task CreateDatabase()
        {
            string databaseName = ProgramHelper.ReadDatabaseName();

            await _repository.CreateDatabaseAsync(databaseName);

            bool isDatabaseExist = await _repository.CheckIfDatabaseExistAsync(databaseName);

            WriteLine("Database >>> " + databaseName + " <<< created.");
        }

        public async Task DeleteDatabase()
        {
            string databaseName = ProgramHelper.ReadDatabaseName();
            bool result = await _repository.DeleteDatabase(databaseName);

            if (result)
            {
                WriteLine("Database >>> " + databaseName + " <<< deleted");
            }
            else
            {
                WriteLine("Database >>> " + databaseName + " <<< not deleted");
            }
        }

        public void ListDatabase()
        {
            string databaseName = ProgramHelper.ReadDatabaseName();
            List<Database> lstDatabase = _repository.ListDatabaseForAccount();
            ProgramHelper.Divider();
            foreach (var database in lstDatabase)
            {
                WriteLine("Id: " + database.Id);
                WriteLine("Self link: " + database.SelfLink);
                WriteLine("User link: " + database.UsersLink);
            }
            ProgramHelper.Divider();
        }
    }
}
