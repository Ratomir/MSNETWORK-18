using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MSNetwork18.DAL;
using MSNetwork18.DAL.Interface;
using System.IO;

namespace MSNetwork18.CLI.DI_Container
{
    public class DIProvider
    {
        public static ServiceProvider serviceProvider = null;

        public static void CreateServiceCollection()
        {
            if (serviceProvider == null)
            {
                ServiceCollection collection = new ServiceCollection();
                collection.AddTransient<IBaseRepository, BaseDatabaseRepository>();
                collection.AddTransient<ISQLCollectionRepository, SQLCollectionRepository>();
                collection.AddTransient<ISQLDatabaseRepository, SQLDatabaseRepository>();
                collection.AddTransient<ISQLDocumentRepository, SQLDocumentRepository>();
                collection.AddTransient<ISQLStoredProcedureRepository, SQLStoredProcedureRepository>();
                collection.AddTransient<ISQLTriggerRepository, SQLTriggerRepository>();
                collection.AddTransient<ISQLUDFRepository, SQLUDFRepository>();

                IConfiguration config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();

                collection.AddSingleton(config);

                serviceProvider = collection.BuildServiceProvider();
            }
        }

        public static ServiceProvider GetServiceProvider()
        {
            if (serviceProvider == null)
            {
                CreateServiceCollection();
            }
            return serviceProvider;
        }
    }
}
