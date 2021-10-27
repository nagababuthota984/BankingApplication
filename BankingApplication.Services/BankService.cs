using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankingApplication.Models;

namespace BankingApplication.Services
{
    public class BankService
    {
        

        public void Add(string name, string branch, string ifsc)
        {
            Bank NewBank = new Bank
            {
                BankName = name,
                BankId = $"{name.Substring(0, 3)}{DateTime.Now:yyyyMMddhhmmss}",
                Branch = branch,
                Ifsc = ifsc,
                SelfRTGS = 0,
                SelfIMPS = 5,
                OtherRTGS = 2,
                OtherIMPS = 6,
                Balance = 0,
                SupportedCurrency = new List<Currency> { new Currency("INR", 1) },
                DefaultCurrency = new Currency("INR",1),
                Accounts = new List<Account>(),
                Transactions = new List<Transaction>(),
                Employees = new List<Staff>()
            };
            RBIStorage.banks.Add(NewBank);
        }

        public Staff FetchStaffByUserName(string userName)
        {
            foreach(var bank in RBIStorage.banks)
            {
                foreach(var employee in bank.Employees)
                {
                    if(employee.UserName.Equals(userName))
                    {
                        return employee;
                    }
                }
            }
            return null;
        }
        public void CreateAccount(Customer newCustomer, Account newAccount, string bankId)
        {
            Bank bank = RBIStorage.banks.FirstOrDefault(b => b.BankId.Equals(bankId));
            newAccount.AccountNumber = GenerateAccountNumber(bank.BankId);
            newAccount.UserName = $"{newCustomer.Name.Substring(0, 3)}{newCustomer.Dob:yyyy}";
            newAccount.Password = $"{newCustomer.Dob:yyyyMMdd}";
            newAccount.AccountId = $"{newCustomer.Name.Substring(0, 3)}{newCustomer.Dob:yyyyMMdd}";
            newAccount.Transactions = new List<Transaction>();
            newAccount.BankId = bank.BankId;
            newAccount.BankName = bank.BankName;
            newAccount.Branch = bank.Branch;
            newAccount.Ifsc = bank.Ifsc;
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
        public Bank GetBankByIfsc(string ifsc)
        {
            Bank bank = RBIStorage.banks.FirstOrDefault(b => b.Ifsc.Equals(ifsc));
            return bank;
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
        public void AddNewCurrency(string bankId, string newCurrencyName, decimal exchangeRate)
        {
            Bank bank = GetBankByBankId(bankId);
            bank.SupportedCurrency.Add(new Currency(newCurrencyName, exchangeRate));
            FileHelper.WriteData(RBIStorage.banks);
        }
        public decimal GetServiceCharge(ModeOfTransfer mode, bool isSelfBankTransfer, string bankId)
        {
            Bank bank = GetBankByBankId(bankId);
            if(isSelfBankTransfer)
            {
                if(mode.Equals(ModeOfTransfer.IMPS))
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
                if(mode.Equals(ModeOfTransfer.IMPS))
                {
                    return bank.OtherIMPS;
                }
                else
                {
                    return bank.OtherRTGS;
                }
            }
        }
        public void SetServiceCharge(ModeOfTransfer mode, bool isSelfBankCharge, string bankId, decimal newValue)
        {
            Bank bank = GetBankByBankId(bankId);
            if(isSelfBankCharge)
            {
                if(mode==ModeOfTransfer.RTGS)
                {
                    bank.SelfRTGS = newValue;
                }
                else
                {
                    bank.SelfIMPS = newValue;
                }
            }
            else
            {
                if(mode==ModeOfTransfer.RTGS)
                {
                    bank.OtherRTGS = newValue;
                }
                else
                {
                    bank.OtherIMPS = newValue;
                }
            }
            FileHelper.WriteData(RBIStorage.banks);
        }
        public List<Transaction> FetchAccountTransactions(string accountId)
        {
            Account userAccount = new AccountService().FetchAccountByAccountId(accountId);
            return new TransactionService().FetchTransactionHistory(userAccount);
        }
        public void RevertTransaction(string transactionId, string bankId)
        {
            Bank bank = GetBankByBankId(bankId);
            Transaction transaction = new TransactionService().FetchTransactionByTransactionId(transactionId);
            if(transaction!=null)
            {
                Account userAccount = new AccountService().FetchAccountByAccountId(transaction.SenderAccountId);
                bank = new BankService().GetBankByBankId(userAccount.BankId);
                TransactionService transService = new TransactionService();
                if (transaction.Type.Equals(TransactionType.Credit))
                {
                    transService.WithdrawAmount(userAccount, transaction.TransactionAmount);
                    userAccount.Transactions.Remove(transaction);
                }
                else if(transaction.Type.Equals(TransactionType.Debit))
                {
                    transService.DepositAmount(userAccount,transaction.TransactionAmount,bank.DefaultCurrency.CurrencyName);
                    userAccount.Transactions.Remove(transaction);
                }
                else if(transaction.Type.Equals(TransactionType.Transfer))
                {
                    Account receiverAccount = new AccountService().FetchAccountByAccountId(transaction.ReceiverAccountId);
                    transService.WithdrawAmount(receiverAccount, transaction.TransactionAmount);
                    receiverAccount.Transactions.Remove(transaction);
                    transService.DepositAmount(userAccount,transaction.TransactionAmount,bank.DefaultCurrency.CurrencyName);
                    userAccount.Transactions.Remove(transaction);


                }
                FileHelper.WriteData(RBIStorage.banks);
            }
            else
            {
                throw new TransactionDoesntExist("Invalid Transaction details");
            }
        }
    }
}






