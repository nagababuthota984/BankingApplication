using BankingApplication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BankingApplication.Services
{
    public class FileHelper
    {
        
        
        public static List<Bank> GetData()//reads data from json file
        {
             
            string data = File.ReadAllText(Constant.filePath);
            List<Bank> banksData = JsonConvert.DeserializeObject<List<Bank>>(data);
            return banksData;
        }
        
        public static void WriteData(List<Bank> dataToWrite)    //writes to json file
        {
            string serializedData = JsonConvert.SerializeObject(dataToWrite, Formatting.Indented);
            File.WriteAllText(Constant.filePath, serializedData);
            //data written into json.
        }

    }
}
