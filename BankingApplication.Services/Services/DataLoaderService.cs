using BankingApplication.Database;



namespace BankingApplication.Services
{
    public class DataLoaderService
    {
        public static void LoadData()
        {

            DataStructures.Accounts = DataReaderWriter.readAccounts();
            DataStructures.Transactions = DataReaderWriter.readTransactions();
        }
        
    }
}
