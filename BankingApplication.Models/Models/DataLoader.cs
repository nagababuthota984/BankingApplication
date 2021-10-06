using BankingApplication.Database;


namespace BankingApplication.Models
{
    public class DataLoader
    {
        public static void LoadData()
        {

            Account.accounts = DataReaderWriter.readAccounts();
            Account.transactions = DataReaderWriter.readTransactions();
        }
    }
}
