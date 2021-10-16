using BankingApplication.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace BankingApplication.Database
{
    public class DataReaderWriter
    {
        //Reads and writes data from transactions and accounts json files.
        public static List<Bank> ReadData()//reads data from json file
        {

            List<Bank> Bank = new List<Bank>();
            string data = File.ReadAllText("C:\\Users\\nagab\\OneDrive\\Desktop\\Technovert\\Banking Application\\BankingApplication.Database\\accounts.json");
            if (data != "{}")
            {
                Bank = JsonConvert.DeserializeObject<List<Bank>>(data);
            }
            
            return Bank;


        }
        public static void WriteData(List<Bank> dataToWrite)    //writes to json file
        {
            string serializedData = JsonConvert.SerializeObject(dataToWrite, Formatting.Indented);
            File.WriteAllText("C:\\Users\\nagab\\OneDrive\\Desktop\\Technovert\\Banking Application\\BankingApplication.Database\\accounts.json", serializedData);
            //data written into json.
        }
    }
}
