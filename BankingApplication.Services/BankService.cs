using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankingApplication.Models;

namespace BankingApplication.Services
{
    public class BankService
    {
        AccountService accountService = new AccountService();
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
                    SessionContent.Bank = bank;
                    SessionContent.Employee = employee;
                    return true;
                }
            }
            return false;
            

        }
        public void CreateAccount(Account newAccount, Bank bank)
        {
            newAccount.BankId = bank.BankId;
            newAccount.BankName = bank.BankName;
            newAccount.Branch = bank.Branch;
            newAccount.Ifsc = bank.Ifsc;
            newAccount.AccountNumber = GenerateAccountNumber(bank.BankId);
            bank.Accounts.Add(newAccount);
            FileHelper.WriteData(RBIStorage.banks);
        }
        private string GenerateAccountNumber(string bankid)
        {
            string accNumber = null;
            do
            {
                accNumber = Utilities.GenerateRandomNumber(12).ToString();
            } while (Utilities.IsDuplicateAccountNumber(accNumber, bankid));
            return accNumber;
        }
        public Bank GetBankByBankId(string bankId)
        {
            Bank bank = RBIStorage.banks.FirstOrDefault(b => b.BankId.EqualInvariant(bankId));
            if (bank != null)
            {
                return bank;
            }
            else
            {
                throw new InvalidBankException("Bank Doesnt Exist.");
            }
        }
        public bool AddNewCurrency(Bank bank, string newCurrencyName, decimal exchangeRate)
        {
            if (bank.SupportedCurrency.Any(c => c.CurrencyName.EqualInvariant(newCurrencyName)))
            {
                return false;
            }
            bank.SupportedCurrency.Add(new Currency(newCurrencyName, exchangeRate));
            FileHelper.WriteData(RBIStorage.banks);
            return true;
        }
        public decimal GetServiceCharge(ModeOfTransfer mode, bool isSelfBankTransfer, string bankId)
        {
            Bank bank = GetBankByBankId(bankId);
            if (isSelfBankTransfer)
            {
                if (mode == ModeOfTransfer.IMPS)
                {
                    return bank.SelfIMPS;
                }
                else
                {
                    return bank.SelfRTGS;
                }
            }
            else
            {
                if (mode == ModeOfTransfer.IMPS)
                {
                    return bank.OtherIMPS;
                }
                else
                {
                    return bank.OtherRTGS;
                }
            }
        }
        public bool SetServiceCharge(ModeOfTransfer mode, bool isSelfBankCharge, Bank bank, decimal newValue)
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
            FileHelper.WriteData(RBIStorage.banks);
            return isModified;
        }
        public List<Transaction> GetAccountTransactions(string accountId)
        {
            return accountService.GetAccountById(accountId)?.Transactions;
        }
        public void RevertTransaction(Transaction transaction, Bank bank)
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
            FileHelper.WriteData(RBIStorage.banks);


        }
        public Employee CreateAndGetEmployee(string name, string age, DateTime dob, Gender gender, EmployeeDesignation role, Bank bank)
        {
            Employee employee = new Employee(name, age, dob, gender, role, bank);
            bank.Employees.Add(employee);
            return employee;
        }
    }
}






