using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankingApplication.Models;

namespace BankingApplication.Services
{
    public class BankService
    {
        public BankService()
        {
            DataLoaderService.LoadData();
        }
        public static void Add(string name, string branch, string ifsc)
        {
            Bank NewBank = new Bank
            {
                BankName = name,
                BankId = $"{name.Substring(0, 3)}{DateTime.Now:yyyyMMdd}",
                Branch = branch,
                Ifsc = ifsc,
                SelfRTGS = 0,
                SelfIMPS = 5,
                OtherRTGS = 2,
                OtherIMPS = 6,
                CurrencyType = Currency.INR,
                Accounts = new List<Account>()
            };
        
            
            RBIStorage.Banks.Add(NewBank);
            return;
        }
        public bool Remove(string name)
        {
            Bank bank = RBIStorage.Banks.SingleOrDefault(e => e.BankName == name);
            if (bank != null)
            {
                RBIStorage.Banks.Remove(bank);
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<string> CreateAccount(Customer newCustomer, Account NewAccount)
        {

            DataLoaderService.LoadData();
            //add the bank if it not exists already
            SetUpBank(NewAccount.BankName,NewAccount.Branch,NewAccount.Password);
            Bank bank = Utilities.FetchBank(NewAccount.BankName);

            NewAccount.AccountNumber = GenerateAccountNumber(bank.BankId);
            
            //account number generated.
            NewAccount.BankId = bank.BankId;
            
            NewAccount.UserName = $"{newCustomer.Name.Substring(0, 3)}{newCustomer.Dob:yyyy}";
            NewAccount.Password = $"{newCustomer.Dob:yyyyMMdd}";
            NewAccount.AccountId = $"{newCustomer.Name.Substring(0,3)}{newCustomer.Dob:yyyyMMdd}";
            
            newCustomer.AccountId = NewAccount.AccountId;
            NewAccount.CustomerOfAccount = newCustomer;
            
            NewAccount.Transactions = new List<Transaction>();
            bank.Accounts.Add(NewAccount);
            DataLoaderService.WriteData(RBIStorage.Banks);
            return new List<string>() { newCustomer.Name, NewAccount.AccountNumber };


        }

        private string GenerateAccountNumber(string bankid)
        {
            string accNumber = "";
            do
            {
                accNumber = Utilities.GenerateRandomNumber(12).ToString();
            } while (Utilities.IsDuplicateAccountNumber(accNumber, bankid));
            return accNumber;
        }

        private void SetUpBank(string bankName,string branch,string ifsc)
        {
            if (RBIStorage.Banks.SingleOrDefault(bank => bank.BankName == bankName) == null)
            {
                BankService.Add(bankName, branch, ifsc);
            }//bank created
        }
    }
}


        



