﻿using BankingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankingApplication.Services
{
    public class AccountService
    {
        public AccountService()
        {
            if (RBIStorage.banks == null)     //initializes the json file if it is empty.
            {
                RBIStorage.banks = new List<Bank>();
                FileHelper.WriteData(RBIStorage.banks);
            }
        }
        public static Account FetchAccountByUserName(string username)
        {
            foreach(var bank in RBIStorage.banks)
            {
                foreach(var account in bank.Accounts)
                {
                    if(account.UserName.Equals(username) && !account.Status.Equals(AccountStatus.Closed))
                    {
                        return account;
                    }
                }
            }
            throw new AccountDoesntExistException("Account Doesn't Exists");
        }
        public static Account FetchAccountByAccNumber(string accNumber)
        {
            foreach (var bank in RBIStorage.banks)
            {
                foreach (var account in bank.Accounts)
                {
                    if (account.AccountNumber.Equals(accNumber) && !account.Status.Equals(AccountStatus.Closed))
                    {
                        return account;
                    }
                }
            }
            throw new AccountDoesntExistException("Account Doesn't Exists");
        }

        public Account FetchAccountByAccountId(string accountId)
        {
            foreach(var bank in RBIStorage.banks)
            {
                foreach(var account in bank.Accounts)
                {
                    if(account.AccountId.Equals(accountId) && !account.Status.Equals(AccountStatus.Closed))
                    {
                        return account;
                    }
                }
            }

            throw new AccountDoesntExistException("Account Doesn't Exist");
        }

        public void UpdateAccount(Account userAccount)
        {
            
        }

        public void DeleteAccount(Account userAccount)
        {
            userAccount.Status = AccountStatus.Closed;
            FileHelper.WriteData(RBIStorage.banks);
        }
    }
}

