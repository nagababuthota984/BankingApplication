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
            //takes the details.. checks if the bank exists or else add the bank first then add this account to the bank which was created.
            if (RbiStorage.Banks.SingleOrDefault(bank => bank.BankName == NewAccount.BankName) == null)   
            {
                BankService.Add(NewAccount.BankName, NewAccount.Branch, NewAccount.Ifsc);
            }//bank created
            

            Bank bank = RbiStorage.Banks.Single(bank => bank.BankName == NewAccount.BankName);
            do
            {
                NewAccount.AccountNumber = GenerateRandomNumber(12).ToString();
            } while (IsDuplicateAccountNumber(NewAccount.AccountNumber,bank.BankId));
            //account number generated.
            NewAccount.UserName = $"{NewAccount.Name.Substring(0, 3)}{NewAccount.Dob:yyyy}";
            NewAccount.Password = $"{NewAccount.Dob:yyyyMMdd}";
            NewAccount.AccountId = $"{NewAccount.Name.Substring(0,3)}{NewAccount.Dob:yyyyMMdd}";
            NewAccount.Transactions = new List<Transaction>();
            bank.Accounts.Add(NewAccount);
            DataReaderWriter.WriteData(RbiStorage.Banks);
            return new List<string>() { NewAccount.Name, NewAccount.AccountNumber };


        }

       

        private bool IsDuplicateAccountNumber(string accountNumber, string bankid)
        {
            
            var RequiredBank = RbiStorage.Banks.SingleOrDefault(bank => bank.BankId == bankid);
            if (RequiredBank != null)
            {
                foreach(var Acc in RequiredBank.Accounts)
                {
                    if(Acc.AccountNumber==accountNumber)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static string ValidateAccount(string AccNumber,string bankname)
        {
            DataLoaderService.LoadData();
            Bank RequiredBank = RbiStorage.Banks.Single(bank => bank.BankName == bankname);
            if (RequiredBank != null)
            {
                foreach (var Acc in RequiredBank.Accounts)
                {
                    if (Acc.AccountNumber == AccNumber)
                    {
                        return Acc.Name;
                    }
                }
                throw new AccountDoesntExistException("Invalid account number. Please provide a valid one.");
            }
            else
            {
                throw new InvalidBankException("Invalid Bank details");
            }
            
        }
        internal static double GenerateRandomNumber(int length)
        {
            double Number = 0;
            Random r = new Random();          //account number generator.
            string NumberString = "";
            int i;
            for (i = 1; i < length; i++)
            {
                NumberString += r.Next(0, 9).ToString();
            }
            Number = Convert.ToInt64(NumberString);
            return Number;
        }
        public static List<Account> FetchAccountsFromBank(string bankname)
        {
            DataLoaderService.LoadData();
            Bank bank = RbiStorage.Banks.SingleOrDefault(bank => bank.BankName == bankname);
            if(bank != null)
            {
                return bank.Accounts;
            }
            else
            {
                throw new InvalidBankException("Please Enter Valid Bank Details.");

            }
        }
        public static Account FetchAccount(string accNumber,string bankname)
        {
            List<Account> Accounts = FetchAccountsFromBank(bankname);
            try
            {
                Account account = Accounts.Single(acc => acc.AccountNumber == accNumber);
                return account;
            }
            catch(Exception e)
            {
                throw new AccountDoesntExistException("Account does Not exists. Please enter valid account number.");
            }

        }
    }
}


        



