namespace BankingApplication.Models
{

    public enum MainMenu
    {
        AccountHolder=1,
        BankStaff
    }
    public enum AccountHolderMenu
    {
        CreateAccount = 1,
        Deposit,
        Withdraw,
        Transfer,
        PrintStatement,
        AddBank,
        RemoveBank
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
        Transfer
    }
    public enum ModeOfTransfer
    {
        RTGS=1,
        IMPS,
        None
    }
}
