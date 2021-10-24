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
            if(RBIStorage.banks==null)
            {
                RBIStorage.banks = new List<Bank>();
                FileHelper.WriteData(RBIStorage.banks);
            }
        }

        public void Add(string name, string branch, string ifsc)
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
                Balance = 0,
                Accounts = new List<Account>(),
                Transactions = new List<Transaction>(),
                Employees = new List<Staff>()
            };


            RBIStorage.banks.Add(NewBank);
        }
        public bool Remove(string name)
        {
            Bank bank = RBIStorage.banks.SingleOrDefault(e => e.BankName == name);
            if (bank != null)
            {
                RBIStorage.banks.Remove(bank);
                return true;
            }
            else
            {
                return false;
            }
        }
        public void CreateAccount(Customer newCustomer, Account newAccount)
        {
            //Checking if the bank exists in the database. else adding it now.
            Bank bank = RBIStorage.banks.FirstOrDefault(b => b.BankName.Equals(newAccount.BankName));
            if (bank == null)
            {
                Add(newAccount.BankName, newAccount.Branch, newAccount.Ifsc);
                bank = RBIStorage.banks.FirstOrDefault(b => b.BankName.Equals(newAccount.BankName));
            }
            newAccount.AccountNumber = GenerateAccountNumber(bank.BankId);
            newAccount.UserName = $"{newCustomer.Name.Substring(0, 3)}{newCustomer.Dob:yyyy}";
            newAccount.Password = $"{newCustomer.Dob:yyyyMMdd}";
            newAccount.AccountId = $"{newCustomer.Name.Substring(0, 3)}{newCustomer.Dob:yyyyMMdd}";
            newAccount.Transactions = new List<Transaction>();
            newAccount.BankId = bank.BankId;
            newCustomer.AccountId = newAccount.AccountId;
            newAccount.Customer = newCustomer;
            bank.Accounts.Add(newAccount);
            FileHelper.WriteData(RBIStorage.banks);
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
        public static Bank GetBankByBankId(string bankId)
        {
            Bank bank = RBIStorage.banks.FirstOrDefault(b => b.BankId.Equals(bankId));
            if(bank!=null)
            {
                return bank;
            }
            else
            {
                throw new InvalidBankException("Bank Doesnt Exist.");
            }
        }
        public void AddStaff(Staff newStaff)
        {
            Bank bank = GetBankByBankId(newStaff.BankId);
            newStaff.StaffId = $"{newStaff.BankId}{Utilities.GenerateRandomNumber(4)}";
            newStaff.UserName = $"{newStaff.Name.Substring(0, 3)}{newStaff.StaffId.Substring(4, 3)}";
            newStaff.Password = $"{newStaff.Dob:yyyyMMdd}";
            bank.Employees.Add(newStaff);
            FileHelper.WriteData(RBIStorage.banks);
        }

        public void AddNewCurrency(string bankId, string newCurrency,decimal exchangeRate)
        {
            
        }
    }
}






