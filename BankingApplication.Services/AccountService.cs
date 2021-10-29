using BankingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BankingApplication.Services
{
    public class AccountService
    {
        TransactionService transService = new TransactionService();
        public List<object> GetAccountByUserNameAndPassword(string username, string password)
        {
            foreach (var bank in RBIStorage.banks)
            {
                Account account = bank.Accounts.FirstOrDefault(a => (a.UserName == username) && (a.Status != AccountStatus.Closed) && (a.Password.Equals(password)));
                if (account != null) return new List<object> { account,bank};
            }
            return null;
        }

        public bool CustomerLogin(string userName, string password)
        {
            List<object> accountAndBank = GetAccountByUserNameAndPassword(userName,password);
            if (accountAndBank != null)
            {
                SessionContent.Account = (Account)accountAndBank[0];
                SessionContent.Bank = (Bank)accountAndBank[1];
                return true;
            }
            else
            {
                return false; 
            }

        }

        public Account GetAccountByAccNumber(string accNumber)
        {
            foreach (var bank in RBIStorage.banks)
            {
                Account account = bank.Accounts.FirstOrDefault(a => a.AccountNumber == accNumber && !a.Status.Equals(AccountStatus.Closed));
                if (account != null) return account;
            }
            return null;
        }

        public Account GetAccountById(string accountId)
        {
            foreach (Bank bank in RBIStorage.banks)
            {
                Account account = bank.Accounts.FirstOrDefault(a => a.AccountId == accountId && !a.Status.Equals(AccountStatus.Closed));
                if (account != null) return account;
            }
            return null;
        }

        public void UpdateAccount(Account userAccount, string property, string newValue)
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

        public bool DeleteAccount(Account userAccount)
        {
            userAccount.Status = AccountStatus.Closed;
            FileHelper.WriteData(RBIStorage.banks);
            return true;
        }
        public void DepositAmount(Account userAccount, decimal amount, Currency currency)
        {
            amount *= currency.ExchangeRate;
            userAccount.Balance += amount;
            transService.CreateTransaction(userAccount, TransactionType.Credit, amount, currency);
            FileHelper.WriteData(RBIStorage.banks);
        }
        public void WithdrawAmount(Account userAccount, decimal amount)
        {
            amount *= SessionContent.Bank.DefaultCurrency.ExchangeRate;
            userAccount.Balance -= amount;
            transService.CreateTransaction(userAccount, TransactionType.Debit, amount, SessionContent.Bank.DefaultCurrency);
            FileHelper.WriteData(RBIStorage.banks);
        }
        public void TransferAmount(Account senderAccount, Bank senderBank, Account receiverAccount, decimal amount, ModeOfTransfer mode)
        {
            amount *= SessionContent.Bank.DefaultCurrency.ExchangeRate;
            senderAccount.Balance -= amount;
            receiverAccount.Balance += amount;
            ApplyTransferCharges(senderAccount, senderBank, receiverAccount.BankId, amount, mode, SessionContent.Bank.DefaultCurrency);
            transService.CreateTransferTransaction(senderAccount, receiverAccount, amount, mode, SessionContent.Bank.DefaultCurrency);
            FileHelper.WriteData(RBIStorage.banks);
        }
        private void ApplyTransferCharges(Account senderAccount, Bank senderBank, string receiverBankId, decimal amount, ModeOfTransfer mode, Currency currency)
        {
            if (mode.Equals(ModeOfTransfer.RTGS))
            {
                //RTGS charge based on transfer to account within the same bank
                if (senderAccount.BankId.Equals(receiverBankId))
                {
                    decimal charges = (senderBank.SelfRTGS * amount) / 100;
                    senderAccount.Balance -= charges;
                    senderBank.Balance += charges;
                    transService.CreateBankTransaction(senderBank, senderAccount, charges, currency);
                }
                else
                {
                    decimal charges = (senderBank.OtherRTGS * amount) / 100;
                    senderAccount.Balance -= charges;
                    senderBank.Balance += charges;
                    transService.CreateBankTransaction(senderBank, senderAccount, charges, currency);
                }
            }
            else
            {
                if (senderAccount.BankId.Equals(receiverBankId))
                {
                    decimal charges = (senderBank.SelfIMPS * amount) / 100;
                    senderAccount.Balance -= charges;
                    senderBank.Balance += charges;
                    transService.CreateBankTransaction(senderBank, senderAccount, charges, currency);
                }
                else
                {
                    decimal charges = (senderBank.OtherIMPS * amount) / 100;
                    senderAccount.Balance -= charges;
                    senderBank.Balance += charges;
                    transService.CreateBankTransaction(senderBank, senderAccount, charges, currency);
                }
            }
        }

    }
}

