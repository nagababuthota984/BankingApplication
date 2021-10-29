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
        public List<object> GetEmployeeByUserNameAndPassword(string userName,string password)
        {
            foreach (var bank in RBIStorage.banks)
            {
                Employee employee = bank.Employees.FirstOrDefault(e => (e.UserName == userName)  && (e.Password.Equals(password)));
                if (employee != null) return new List<object> { employee, bank };
            }
            return null;
        }
        public bool EmployeeLogin(string userName, string password)
        {
            List<object> employeeDetails = GetEmployeeByUserNameAndPassword(userName, password);
            if (employeeDetails !=null)
            {
                SessionContent.Bank = (Bank)employeeDetails[1];
                SessionContent.Employee = (Employee)employeeDetails[0];
                return true; 
            }
            else
            {
                return false;
            }

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
            Bank bank = RBIStorage.banks.FirstOrDefault(b => b.BankId.Equals(bankId));
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
            if(bank.SupportedCurrency.Any(c => c.CurrencyName.ToLower() == newCurrencyName.ToLower()))
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
                if (mode.Equals(ModeOfTransfer.IMPS))
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
                if (mode.Equals(ModeOfTransfer.IMPS))
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
                    bank.SelfRTGS = newValue; isModified = true;
                }
                else
                {
                    bank.SelfIMPS = newValue; isModified = true;
                }
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
            Account userAccount = accountService.GetAccountById(accountId);
            if (userAccount != null)
            {
                return userAccount.Transactions;
            }
            else
            {
                return null;
            }
        }
        public void RevertTransaction(Transaction transaction, Bank bank)
        {
            Account userAccount = accountService.GetAccountById(transaction.SenderAccountId);
            if (transaction.Type.Equals(TransactionType.Credit))
            {
                accountService.WithdrawAmount(userAccount, transaction.TransactionAmount);
                userAccount.Transactions.Remove(transaction);
            }
            else if (transaction.Type.Equals(TransactionType.Debit))
            {
                accountService.DepositAmount(userAccount, transaction.TransactionAmount, bank.DefaultCurrency);
                userAccount.Transactions.Remove(transaction);
            }
            else if (transaction.Type.Equals(TransactionType.Transfer))
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
            Employee employee = new Employee(name,age,dob,gender,role,bank);
            bank.Employees.Add(employee);
            return employee;
        }
    }
}






