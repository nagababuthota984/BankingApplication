﻿namespace BankingApplication.Models
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
}
