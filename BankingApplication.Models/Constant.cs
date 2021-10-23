using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BankingApplication.Models
{
    public class Constant
    {
        public static string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
        public static string filePath = projectDirectory + "\\BankingApplication.Database\\Data.json";
    }
}
