using MSNetwork18.CLI.DI_Container;
using MSNetwork18.CLI.Programs;
using System;
using System.Threading.Tasks;

namespace MSNetwork18.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "*** ** *MS Network 18 - Neum* ** ***";

            DIProvider.CreateServiceCollection();

            bool runApp = true;
            while (runApp)
            {
                Console.Clear();
                ProgramHelper.Stars();
                ProgramHelper.Title();
                ProgramHelper.Stars();
                Option();
                Console.Write("> ");
                OptionEnum option = (OptionEnum)Convert.ToInt16(Console.ReadLine());

                switch (option)
                {
                    #region << EXIT >>
                    case OptionEnum.EXIT:
                        {
                            runApp = false;
                            break;
                        }

                    #endregion << EXIT >>

                    #region << Database options >>
                    case OptionEnum.CreateDatabase:
                        {
                            DatabaseProgram program = new DatabaseProgram();
                            Task.Run(() => program.CreateDatabase()).Wait();
                            break;
                        }   
                    case OptionEnum.DeleteDatabase:
                        {
                            DatabaseProgram program = new DatabaseProgram();
                            Task.Run(() => program.DeleteDatabase()).Wait();
                            break;
                        }
                    case OptionEnum.ListDatabase:
                        {
                            DatabaseProgram program = new DatabaseProgram();
                            Task.Run(() => program.ListDatabase()).Wait();
                            break;
                        }
                    #endregion << Database options >>

                    #region << Collection options >>
                    case OptionEnum.CreateCollection:
                        {
                            CollectionProgram program = new CollectionProgram();
                            Task.Run(() => program.CreateCollection()).Wait();
                            break;
                        }
                    case OptionEnum.DeleteCollection:
                        {
                            CollectionProgram program = new CollectionProgram();
                            Task.Run(() => program.DeleteCollection()).Wait();
                            break;
                        }
                    case OptionEnum.ReadCollectionsOfDatabase:
                        {
                            CollectionProgram program = new CollectionProgram();
                            Task.Run(() => program.ReadAllCollectionsFromDatabase()).Wait();
                            break;
                        }

                    #endregion << Collection options >> 

                    #region << Document options >> 

                    case OptionEnum.ReadDocument:
                        {
                            DocumentProgram program = new DocumentProgram();
                            program.ReadDocument().Wait();
                            break;
                        }
                    case OptionEnum.DeleteDocument:
                        {
                            DocumentProgram program = new DocumentProgram();
                            program.DeleteDocument().Wait();
                            break;
                        }
                    case OptionEnum.InsertDocument:
                        {
                            DocumentProgram program = new DocumentProgram();
                            program.InsertDocument().Wait();
                            break;
                        }
                    case OptionEnum.UpdateDocument:
                        {
                            DocumentProgram program = new DocumentProgram();
                            program.UpdateDocument().Wait();
                            break;
                        }

                    #endregion << Document options >>

                    #region << Stored procedure

                    case OptionEnum.CallStoredProcedure:
                        {
                            StoredProcedureProgram program = new StoredProcedureProgram();
                            program.Run().Wait();
                            break;
                        }

                    #endregion << Stored procedure

                    #region << Trigger >>

                    case OptionEnum.ExecuteWithTrigger:
                        {
                            TriggerProgram program = new TriggerProgram();
                            program.Run().Wait();
                            break;
                        }

                    #endregion << Trigger >>

                    #region << UDF >>

                    case OptionEnum.UseUDF:
                        {
                            UDFProgram program = new UDFProgram();
                            program.Run().Wait();
                            break;
                        }

                    #endregion << UDF >>

                    #region << Exception >>

                    case OptionEnum.Exception:
                        {
                            ExceptionProgram program = new ExceptionProgram();
                            program.Run().Wait();
                            break;
                        }

                    #endregion << Exception >>

                    #region << Default >>
                    default:
                        {
                            runApp = false;
                            break;
                        }
                    #endregion << Default >>
                }

                if (runApp)
                {
                    ProgramHelper.Wait();
                }
            }
        }

        static void Option()
        {
            Console.WriteLine("11. Create database");
            Console.WriteLine("12. Delete database");
            Console.WriteLine("13. List database");

            ProgramHelper.Divider();
            Console.WriteLine("21. Create collection");
            Console.WriteLine("22. Delete collection");
            Console.WriteLine("23. Read collections from database");

            ProgramHelper.Divider();
            Console.WriteLine("31. Read document");
            Console.WriteLine("32. Delete document");
            Console.WriteLine("33. Insert document");
            Console.WriteLine("34. Update document");

            ProgramHelper.Divider();
            Console.WriteLine("41. Stored procedure");
            Console.WriteLine("42. Trigger");
            Console.WriteLine("43. Use User Defined Function, UDF");

            ProgramHelper.Divider();
            Console.WriteLine("51. Exception");
            ProgramHelper.Divider();

            Console.WriteLine("0. EXIT");
        }

        public enum OptionEnum
        {
            EXIT = 0,
            CreateDatabase = 11,
            DeleteDatabase,
            ListDatabase,
            CreateCollection = 21,
            DeleteCollection,
            ReadCollectionsOfDatabase,
            ReadDocument = 31,
            DeleteDocument,
            InsertDocument,
            UpdateDocument,
            CallStoredProcedure = 41,
            ExecuteWithTrigger,
            UseUDF,
            Exception = 51
        }
    }
}
