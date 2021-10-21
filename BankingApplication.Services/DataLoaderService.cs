using BankingApplication.Database;
using BankingApplication.Models;


namespace BankingApplication.Services
{
    public class DataLoaderService
    {
        public static void LoadData()
        {

            RbiStorage.Banks = DataReaderWriter.ReadData();
        }

    }
}
