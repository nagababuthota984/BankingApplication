using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankingApplication.Database;
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
        
            
            RbiStorage.Banks.Add(NewBank);
            return;
        }
        public bool Remove(string name)
        {
            Bank bank = RbiStorage.Banks.SingleOrDefault(e => e.BankName == name);
            if (bank != null)
            {
                RbiStorage.Banks.Remove(bank);
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<string> CreateAccount(Account NewAccount)
        {

            DataLoaderService.LoadData();
            //add the bank if it not exists already
            if (RbiStorage.Banks.SingleOrDefault(bank => bank.BankName == NewAccount.BankName) == null)   
            {
                BankService.Add(NewAccount.BankName, NewAccount.Branch, NewAccount.Ifsc);
            }//bank created
            

            Bank bank = RbiStorage.Banks.Single(bank => bank.BankName == NewAccount.BankName);
            do
            {
                NewAccount.AccountNumber = Utilities.GenerateRandomNumber(12).ToString();
            } while (Utilities.IsDuplicateAccountNumber(NewAccount.AccountNumber,bank.BankId));
            //account number generated.
            NewAccount.UserName = $"{NewAccount.Name.Substring(0, 3)}{NewAccount.Dob:yyyy}";
            NewAccount.Password = $"{NewAccount.Dob:yyyyMMdd}";
            NewAccount.AccountId = $"{NewAccount.Name.Substring(0,3)}{NewAccount.Dob:yyyyMMdd}";
            NewAccount.Transactions = new List<Transaction>();
            bank.Accounts.Add(NewAccount);
            DataReaderWriter.WriteData(RbiStorage.Banks);
            return new List<string>() { NewAccount.Name, NewAccount.AccountNumber };


        }

       

        

        
        
        
    }
}


        



