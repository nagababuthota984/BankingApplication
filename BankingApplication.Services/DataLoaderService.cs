using BankingApplication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace BankingApplication.Services
{
    public class DataLoaderService
    {
        static string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
        static string filePath = projectDirectory +"\\BankingApplication.Database\\Data.json";
        public static void LoadData()
        {

            RbiStorage.Banks = ReadData();
        }
        public static List<Bank> ReadData()//reads data from json file
        {

            List<Bank> Bank = new List<Bank>();
            string data = File.ReadAllText(filePath);
            if (data != "{}")
            {
                Bank = JsonConvert.DeserializeObject<List<Bank>>(data);
            }

            return Bank;


        }
        public static void WriteData(List<Bank> dataToWrite)    //writes to json file
        {
            string serializedData = JsonConvert.SerializeObject(dataToWrite, Formatting.Indented);
            File.WriteAllText(filePath, serializedData);
            //data written into json.
        }

    }
}
