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
            return SerializationHelper.GetData<Bank>(data);
        }

        public static void WriteData(List<Bank> dataToWrite)    //writes to json file
        {
            string serializedData = SerializationHelper.WriteData(dataToWrite);
            File.WriteAllText(Constant.filePath, serializedData);
            //data written into json.
        }

    }


    public class SerializationHelper
    {
        public static List<T> GetData<T>(string data)
        {
            if (string.IsNullOrEmpty(data)) return new List<T>();
            return JsonConvert.DeserializeObject<List<T>>(data);
        }
        public static string  WriteData<T>(List<T> dataToWrite)
        {
            if (dataToWrite != null)
                return JsonConvert.SerializeObject(dataToWrite, Formatting.Indented);
            else
                return null;
        }
    }
}
