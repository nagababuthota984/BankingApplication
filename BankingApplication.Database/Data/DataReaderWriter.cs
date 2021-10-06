using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace BankingApplication.Database
{
    public class DataReaderWriter
    {
        //Reads and writes data from transactions and accounts json files.
        public static Dictionary<double, Dictionary<string, string>> readAccounts()//reads data from json file
        {

            Dictionary<double, Dictionary<string, string>> jsonObject = new Dictionary<double, Dictionary<string, string>>();
            string data = File.ReadAllText("C:\\Users\\nagab\\OneDrive\\Desktop\\Technovert\\Banking Application\\BankingApplication.Database\\Data\\accounts.json");
            jsonObject = JsonConvert.DeserializeObject<Dictionary<double, Dictionary<string, string>>>(data);
            return jsonObject;


        }
        public static void writeAccounts(Dictionary<double, Dictionary<string, string>> dataToWrite)    //writes to json file
        {
            string serializedData = JsonConvert.SerializeObject(dataToWrite, Formatting.Indented);
            File.WriteAllText("C:\\Users\\nagab\\OneDrive\\Desktop\\Technovert\\Banking Application\\BankingApplication.Database\\Data\\accounts.json", serializedData);
            //data written into json.
        }
        public static Dictionary<double, string> readTransactions()
        {
            string data = File.ReadAllText("C:\\Users\\nagab\\OneDrive\\Desktop\\Technovert\\Banking Application\\BankingApplication.Database\\Data\\transactions.json");
            return JsonConvert.DeserializeObject<Dictionary<double, string>>(data);
        }
        public static void writeTransactions(Dictionary<double, string> transactions)
        {
            string serializedData = JsonConvert.SerializeObject(transactions, Formatting.Indented);
            File.WriteAllText("C:\\Users\\nagab\\OneDrive\\Desktop\\Technovert\\Banking Application\\BankingApplication.Database\\Data\\transactions.json", serializedData);
        }
    }
}
