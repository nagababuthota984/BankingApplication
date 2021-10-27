using System.IO;

namespace BankingApplication.Models
{
    public class Constant
    {
        public static string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
        public static string filePath = projectDirectory + "\\BankingApplication.Database\\Data.json";
        public static string employeeMenu = "Choice\n1.Create Account\n2.AddBank\n3.UpdateAccount\n4.Delete an Account\n5.AddNewEmployee\n6.AddNewCurrency\n7.SetServiceCharge\n8.ViewTransactions\n9.RevertTransaction\nAny other key to logout.";
    }
}
