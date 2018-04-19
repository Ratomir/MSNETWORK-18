using System;

namespace MSNetwork18.CLI
{
    public class ProgramHelper
    {
        public static void Stars() => Console.WriteLine("********************************************");
        public static void Title() => Console.WriteLine("      Azure Cosmos DB - MS Network 2018");
        public static void Divider() => Console.WriteLine("---------------------------------");

        public static int EnterInt(string text)
        {
            Console.Write(text + " > ");
            return Convert.ToInt32(Console.ReadLine());
        }

        public static string EnterText(string text)
        {
            Console.Write(text + " > ");
            return Console.ReadLine();
        }

        public static float EnterDecimal(string text)
        {
            Console.Write(text + " > ");
            return (float)Convert.ToDouble(Console.ReadLine());
        }

        public static DateTime EnterDate(string text)
        {
            Console.Write(text + " > ");
            return Convert.ToDateTime(Console.ReadLine());
        }

        public static char EnterChar(string text)
        {
            Console.Write(text + " > ");
            return Convert.ToChar(Console.ReadKey());
        }

        public static bool EnterBool(string text)
        {
            Console.Write(text + " > ");
            return Convert.ToBoolean(Console.ReadKey());
        }

        public static void Wait()
        {
            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }

        public static string ReadDatabaseName()
        {
            Console.Write("Enter database name > ");
            return Console.ReadLine();
        }

        public static string ReadCollectionName()
        {
            Console.Write("Enter collection name > ");
            return Console.ReadLine();
        }
    }
}
