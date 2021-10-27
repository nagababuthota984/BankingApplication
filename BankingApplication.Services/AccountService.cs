using BankingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BankingApplication.Services
{
    public class AccountService
    {
        TransactionService transService = new TransactionService();

        public Account FetchAccountByUserName(string username)
        {

            Bank bank = RBIStorage.banks.FirstOrDefault(b => b.Accounts.Any(a => a.UserName == username && !a.Status.Equals(AccountStatus.Closed)));
            if (bank != null)
            {
                Account account = bank.Accounts.FirstOrDefault(a => a.UserName == username && !a.Status.Equals(AccountStatus.Closed));
                return account;
            }
            return null;
        }
        public Account FetchAccountByAccNumber(string accNumber)
        {
            Bank bank = RBIStorage.banks.FirstOrDefault(b => b.Accounts.Any(a => a.AccountNumber == accNumber && !a.Status.Equals(AccountStatus.Closed)));
            if (bank != null)
            {
                Account account = bank.Accounts.FirstOrDefault(a => a.AccountNumber == accNumber && !a.Status.Equals(AccountStatus.Closed));
                return account;
            }
            return null;
        }

        public Account FetchAccountByAccountId(string accountId)
        {
            foreach (Bank bank in RBIStorage.banks)
            {
                return bank.Accounts.FirstOrDefault(a => a.AccountId == accountId && !a.Status.Equals(AccountStatus.Closed));
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

        public void DeleteAccount(Account userAccount)
        {
            userAccount.Status = AccountStatus.Closed;
            FileHelper.WriteData(RBIStorage.banks);
        }
        public void DepositAmount(Account userAccount, decimal amount, Currency currency)
        {
            amount = amount * currency.ExchangeRate;
            userAccount.Balance += amount;
            transService.CreateTransaction(userAccount, TransactionType.Credit, amount, currency);
            FileHelper.WriteData(RBIStorage.banks);
        }
        public void WithdrawAmount(Account userAccount, decimal amount, Bank bank)
        {

            Currency currency = bank.DefaultCurrency;
            if (currency != null)
            {
                amount = amount * currency.ExchangeRate;
                userAccount.Balance -= amount;
                transService.CreateTransaction(userAccount, TransactionType.Debit, amount, currency);
                FileHelper.WriteData(RBIStorage.banks);
            }
            else
            {
                throw new UnsupportedCurrencyException("Currency Not Supported");
            }
        }
        public void TransferAmount(Account senderAccount, Account receiverAccount, decimal amount, ModeOfTransfer mode)
        {
            Bank bank = new BankService().GetBankByBankId(senderAccount.BankId);
            Currency currency = bank.DefaultCurrency;
            amount = amount * currency.ExchangeRate;
            senderAccount.Balance -= amount;
            receiverAccount.Balance += amount;
            ApplyTransferCharges(senderAccount, receiverAccount.BankId, amount, mode, currency);
            transService.CreateTransferTransaction(senderAccount, receiverAccount, amount, mode, currency);
            FileHelper.WriteData(RBIStorage.banks);
        }
        private void ApplyTransferCharges(Account senderAccount, string receiverBankId, decimal amount, ModeOfTransfer mode, Currency currency)
        {
            Bank bank = new BankService().GetBankByBankId(senderAccount.BankId);
            if (mode.Equals(ModeOfTransfer.RTGS))
            {
                //RTGS charge based on transfer to account within the same bank
                if (senderAccount.BankId.Equals(receiverBankId))
                {
                    decimal charges = (bank.SelfRTGS * amount) / 100;
                    senderAccount.Balance -= charges;
                    bank.Balance += charges;
                    transService.CreateBankTransaction(bank, senderAccount.AccountId, charges, currency);
                }
                else
                {
                    decimal charges = (bank.OtherRTGS * amount) / 100;
                    senderAccount.Balance -= charges;
                    bank.Balance += charges;
                    transService.CreateBankTransaction(bank, senderAccount.AccountId, charges, currency);
                }
            }
            else
            {
                if (senderAccount.BankId.Equals(receiverBankId))
                {
                    decimal charges = (bank.SelfIMPS * amount) / 100;
                    senderAccount.Balance -= charges;
                    bank.Balance += charges;
                    transService.CreateBankTransaction(bank, senderAccount.AccountId, charges, currency);
                }
                else
                {
                    decimal charges = (bank.OtherIMPS * amount) / 100;
                    senderAccount.Balance -= charges;
                    bank.Balance += charges;
                    transService.CreateBankTransaction(bank, senderAccount.AccountId, charges, currency);
                }
            }
        }

    }
}

