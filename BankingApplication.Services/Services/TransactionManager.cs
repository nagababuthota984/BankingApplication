using System;
using System.Collections.Generic;
using System.Text;
using BankingApplication.Models;

namespace BankingApplication.Services
{
    public class TransactionManager
    {
        public TransactionManager()
        {
            DataLoader.LoadData();
        }
        public void CreateAccount()
        {
            AccountCreation acc = new AccountCreation();
            acc.createAccount();
            
        }
        public void Deposit()
        {
            DepositAmount da = new DepositAmount();
            da.depositAmount();
        }
        public void Withdraw()
        {
            Withdraw wd = new Withdraw();
            wd.withdrawAmount();
        }
        public void TransferToAccount()
        {
            Transfer tr = new Transfer();
            tr.transferAmount();
        }
        public void Statement()
        {
            PrintStatement ps = new PrintStatement();
            ps.printTransactionHistory();
        }
    }

}
