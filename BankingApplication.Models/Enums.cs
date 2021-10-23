namespace BankingApplication.Models
{

    public enum MainMenu
    {
        AccountHolder=1,
        BankStaff
    }
    public enum AccountHolderMenu
    {
        Deposit=1,
        Withdraw,
        Transfer,
        PrintStatement,
        CheckBalance,
        LogOut
    }
    public enum Currency
    {
        INR=1,
        USD
    }
    public enum BankStaffMenu
    {
        CreateAccount=1,
        UpdateOrDeleteAccount,
        AddNewCurrency,
        SetServiceCharge,
        ViewTransactions,
        RevertTransaction
    }
    public enum AccountType
    {
        Savings=1,
        Current
    }
    public enum TransactionType
    {
        Credit,
        Debit,
        Transfer,
        ServiceCharge
    }
    public enum ModeOfTransfer
    {
        RTGS=1,
        IMPS,
        None
    }
}
