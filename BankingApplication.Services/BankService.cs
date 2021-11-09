using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankingApplication.Models;

namespace BankingApplication.Services
{
    public class BankService : IBankService
    {
        private IAccountService accountService = null;
        public BankService(IAccountService accService)
        {
            accountService = Factory.CreateAccountService();
        }
        public Bank CreateAndGetBank(string name, string branch, string ifsc)
        {
            Bank newBank = new Bank(name, branch, ifsc);
            RBIStorage.banks.Add(newBank);
            return newBank;
        }
        
        public bool IsValidEmployee(string userName, string password)
        {
            foreach (var bank in RBIStorage.banks)
            {
                Employee employee = bank.Employees.FirstOrDefault(e => (e.UserName.EqualInvariant(userName)) && (e.Password.Equals(password)));
                if (employee != null)
                {
                    SessionContext.Bank = bank;
                    SessionContext.Employee = employee;
                    return true;
                }
            }
            return false;
            

        }
        public void CreateAndAddAccount(Account newAccount, Bank bank)
        {
            bank.Accounts.Add(newAccount);
            JsonFileHelper.WriteData(RBIStorage.banks);
        }
        public void UpdateAccount(Account userAccount)
        {
            JsonFileHelper.WriteData(RBIStorage.banks);
        }

        public bool DeleteAccount(Account userAccount)
        {
            userAccount.Status = AccountStatus.Closed;
            JsonFileHelper.WriteData(RBIStorage.banks);
            return true;
        }

        
        public bool AddNewCurrency(Bank bank, string newName, decimal exchangeRate)
        {
            if (bank.SupportedCurrency.Any(c => c.Name.EqualInvariant(newName)))
            {
                return false;
            }
            bank.SupportedCurrency.Add(new Currency(newName, exchangeRate));
            JsonFileHelper.WriteData(RBIStorage.banks);
            return true;
        }
        
        public bool ModifyServiceCharge(ModeOfTransfer mode, bool isSelfBankCharge, Bank bank, decimal newValue)
        {
            bool isModified;
            if (isSelfBankCharge)
            {
                if (mode == ModeOfTransfer.RTGS)
                {
                    bank.SelfRTGS = newValue;
                }
                else
                {
                    bank.SelfIMPS = newValue;
                }
                isModified = true;
            }
            else
            {
                if (mode == ModeOfTransfer.RTGS)
                {
                    bank.OtherRTGS = newValue; isModified = true;
                }
                else
                {
                    bank.OtherIMPS = newValue; isModified = true;
                }
            }
            JsonFileHelper.WriteData(RBIStorage.banks);
            return isModified;
        }
        public List<Transaction> GetAccountTransactions(string accountId)
        {
            return accountService.GetAccountById(accountId)?.Transactions;
        }
        public bool RevertTransaction(Transaction transaction, Bank bank)
        {
            Account userAccount = accountService.GetAccountById(transaction.SenderAccountId);
            if (transaction.Type==TransactionType.Credit)
            {
                accountService.WithdrawAmount(userAccount, transaction.TransactionAmount);
                userAccount.Transactions.Remove(transaction);
            }
            else if (transaction.Type==TransactionType.Debit)
            {
                accountService.DepositAmount(userAccount, transaction.TransactionAmount, bank.DefaultCurrency);
                userAccount.Transactions.Remove(transaction);
            }
            else if (transaction.Type==TransactionType.Transfer)
            {
                Account receiverAccount = accountService.GetAccountById(transaction.ReceiverAccountId);
                accountService.WithdrawAmount(receiverAccount, transaction.TransactionAmount);
                receiverAccount.Transactions.Remove(transaction);
                accountService.DepositAmount(userAccount, transaction.TransactionAmount, bank.DefaultCurrency);
                userAccount.Transactions.Remove(transaction);


            }
            JsonFileHelper.WriteData(RBIStorage.banks);
            return true;

        }
        public Employee CreateAndGetEmployee(string name, int age, DateTime dob, Gender gender, EmployeeDesignation role, Bank bank)
        {
            Employee employee = new Employee(name, age, dob, gender, role, bank);
            bank.Employees.Add(employee);
            JsonFileHelper.WriteData(RBIStorage.banks);
            return employee;
        }

        public List<Transaction> GetTransactionsByDate(DateTime date, Bank bank)
        {
            List<Transaction> transactions = new List<Transaction>();
            foreach(Account account in bank.Accounts)
            {
                transactions.AddRange(account.Transactions.FindAll(tr => tr.On.Date == date));
            }
            transactions.AddRange(bank.Transactions.FindAll(tr => tr.On.Date == date));
            return transactions;
        }
        public List<Transaction> GetTransactions(Bank bank)
        {
            List<Transaction> transactions = new List<Transaction>();
            foreach (Account account in bank.Accounts)
            {
                transactions.AddRange(account.Transactions);
            }
            transactions.AddRange(bank.Transactions);
            return transactions;
        }
    }
}






