using BankingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BankingApplication.Services
{
    public class AccountService
    {
        
        public Account FetchAccountByUserName(string username)
        {

            Bank bank = RBIStorage.banks.FirstOrDefault(b => b.Accounts.Any(a => a.UserName == username && !a.Status.Equals(AccountStatus.Closed)));
            Account account = bank.Accounts.FirstOrDefault(a => a.UserName == username && !a.Status.Equals(AccountStatus.Closed));
            return account; 
        }
        public  Account FetchAccountByAccNumber(string accNumber)
        {
            Bank bank = RBIStorage.banks.FirstOrDefault(b => b.Accounts.Any(a => a.AccountNumber == accNumber && !a.Status.Equals(AccountStatus.Closed)));
            Account account = bank.Accounts.FirstOrDefault(a => a.AccountNumber == accNumber && !a.Status.Equals(AccountStatus.Closed));
            return account;
        }

        public Account FetchAccountByAccountId(string accountId)
        {
            Bank bank = RBIStorage.banks.FirstOrDefault(b => b.Accounts.Any(a => a.AccountId == accountId && !a.Status.Equals(AccountStatus.Closed)));
            Account account = bank.Accounts.FirstOrDefault(a => a.AccountId == accountId && !a.Status.Equals(AccountStatus.Closed));
            return account;
        }

        public void UpdateAccount(Account userAccount,string property,string newValue)
        {

            PropertyInfo myProp = userAccount.Customer.GetType().GetProperty(property);
            if (property.ToLower().Equals("dob"))
            {
                DateTime newDate = Convert.ToDateTime(newValue);
                myProp.SetValue(userAccount.Customer, newDate, null);
            }
            else
            {
                myProp.SetValue(userAccount.Customer, newValue, null);
            }
            FileHelper.WriteData(RBIStorage.banks);
            
        }

        public void DeleteAccount(Account userAccount)
        {
            userAccount.Status = AccountStatus.Closed;
            FileHelper.WriteData(RBIStorage.banks);
        }
    }
}

 