using BankingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BankingApplication.Services
{
    public class AccountService : IAccountService
    {
        ITransactionService transService = new TransactionService();
        IDataProvider dataProvider = new JsonFileHelper();

        public bool IsValidCustomer(string userName, string password)
        {
            foreach (var bank in RBIStorage.banks)
            {
                Account account = bank.Accounts.FirstOrDefault(a => (a.UserName == userName) && (a.Status != AccountStatus.Closed) && (a.Password.Equals(password)));
                if (account != null)
                {
                    SessionContent.Bank = bank;
                    SessionContent.Account = account;
                    return true;
                };
            }
            return false;
        }

        public Account GetAccountByAccNumber(string accNumber)
        {
            foreach (var bank in RBIStorage.banks)
            {
                Account account = bank.Accounts.FirstOrDefault(a => (a.AccountNumber.EqualInvariant(accNumber)) && (a.Status!=AccountStatus.Closed));
                if (account != null) return account;
            }
            return null;
        }

        public Account GetAccountById(string accountId)
        {
            foreach (Bank bank in RBIStorage.banks)
            {
                Account account = bank.Accounts.FirstOrDefault(a => a.AccountId.EqualInvariant(accountId) && (a.Status != AccountStatus.Closed));
                if (account != null) return account;
            }
            return null;
        }

        public void UpdateAccount(Account userAccount)
        {
            dataProvider.WriteData(RBIStorage.banks);
        }

        public bool DeleteAccount(Account userAccount)
        {
            userAccount.Status = AccountStatus.Closed;
            dataProvider.WriteData(RBIStorage.banks);
            return true;
        }
        public void DepositAmount(Account userAccount, decimal amount, Currency currency)
        {
            amount *= currency.ExchangeRate;
            userAccount.Balance += amount;
            transService.CreateTransaction(userAccount, TransactionType.Credit, amount, currency);
            dataProvider.WriteData(RBIStorage.banks);
        }
        public void WithdrawAmount(Account userAccount, decimal amount)
        {
            amount *= SessionContent.Bank.DefaultCurrency.ExchangeRate;
            userAccount.Balance -= amount;
            transService.CreateTransaction(userAccount, TransactionType.Debit, amount, SessionContent.Bank.DefaultCurrency);
            dataProvider.WriteData(RBIStorage.banks);
        }
        public void TransferAmount(Account senderAccount, Bank senderBank, Account receiverAccount, decimal amount, ModeOfTransfer mode)
        {
            amount *= SessionContent.Bank.DefaultCurrency.ExchangeRate;
            senderAccount.Balance -= amount;
            receiverAccount.Balance += amount;
            ApplyTransferCharges(senderAccount, senderBank, receiverAccount.BankId, amount, mode, SessionContent.Bank.DefaultCurrency);
            transService.CreateTransferTransaction(senderAccount, receiverAccount, amount, mode, SessionContent.Bank.DefaultCurrency);
            dataProvider.WriteData(RBIStorage.banks);
        }
        public void ApplyTransferCharges(Account senderAccount, Bank senderBank, string receiverBankId, decimal amount, ModeOfTransfer mode, Currency currency)
        {
            if (mode == ModeOfTransfer.RTGS)
            {
                //RTGS charge based on transfer to account within the same bank
                if (senderAccount.BankId.EqualInvariant(receiverBankId))
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
                if (senderAccount.BankId.EqualInvariant(receiverBankId))
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

