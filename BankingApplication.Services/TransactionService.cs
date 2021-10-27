﻿using BankingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankingApplication.Services
{
    public class TransactionService
    {

        private void CreateTransaction(Account userAccount, TransactionType transtype, decimal transactionamount, Currency currency)
        {
            DateTime timestamp = DateTime.Now;
            Transaction NewTrans = new Transaction
            {
                TransId = $"TXN{userAccount.BankId}{userAccount.AccountId}{timestamp:yyyyMMddhhmmss}",
                Type = transtype,
                On = timestamp,
                SenderAccountId = userAccount.AccountId,
                ReceiverAccountId = userAccount.AccountId,
                TransactionAmount = transactionamount,
                BalanceAmount = userAccount.Balance,
                TransferMode = ModeOfTransfer.None
                
            };
            userAccount.Transactions.Add(NewTrans);
        }
        private void CreateTransferTransaction(Account userAccount,Account receiverAccount,decimal transactionAmount,ModeOfTransfer mode, Currency currency)
        {
            DateTime timestamp = DateTime.Now;
            Transaction senderTransaction = new Transaction
            {
                TransId = $"TXN{userAccount.BankId}{userAccount.AccountId}{timestamp:yyyyMMddhhmmss}",
                SenderAccountId = userAccount.AccountId,
                ReceiverAccountId = receiverAccount.AccountId,
                Type = TransactionType.Transfer,
                On = timestamp,
                TransactionAmount = transactionAmount,
                BalanceAmount = userAccount.Balance,
                Currency = currency,
                TransferMode = mode
            };
            userAccount.Transactions.Add(senderTransaction);
            Transaction receiverTransaction = new Transaction
            {
                TransId = $"TXN{receiverAccount.BankId}{receiverAccount.AccountId}{timestamp:yyyyMMddhhmmss}",
                SenderAccountId = userAccount.AccountId,
                ReceiverAccountId = receiverAccount.AccountId,
                Type = TransactionType.Transfer,
                On = timestamp,
                TransactionAmount = transactionAmount,
                BalanceAmount = receiverAccount.Balance,
                Currency = currency,
                TransferMode = mode
            };
            receiverAccount.Transactions.Add(receiverTransaction);
        }
        private void CreateBankTransaction(Bank bank, string accountId, decimal charges,Currency currency)
        {
            DateTime timestamp = DateTime.Now;
            Transaction newBankTransaction = new Transaction
            {
                TransId = $"TXN{bank.BankId}{accountId}{timestamp:yyyyMMddhhmmss}",
                Type = TransactionType.ServiceCharge,
                SenderAccountId = accountId,
                ReceiverAccountId = bank.BankId,
                On = timestamp,
                TransactionAmount = charges,
                TransferMode = ModeOfTransfer.None,
                Currency = currency,
                BalanceAmount = bank.Balance
            };
            bank.Transactions.Add(newBankTransaction);
        }
        public void DepositAmount(Account userAccount, decimal amount, string currencyName)
        {
            
            Bank bank = new BankService().GetBankByBankId(userAccount.BankId);
            Currency currency = bank.SupportedCurrency.FirstOrDefault(c => c.CurrencyName.Equals(currencyName));
            if (currency != null)
            {
                amount = amount * currency.ExchangeRate;
                userAccount.Balance += amount;
                CreateTransaction(userAccount, TransactionType.Credit, amount, currency);
                FileHelper.WriteData(RBIStorage.banks);
            }
            else
            {
                throw new UnsupportedCurrencyException("Currency Not Supported");
            }

        }

        public void WithdrawAmount(Account userAccount,decimal amount)
        {

            Bank bank = new BankService().GetBankByBankId(userAccount.BankId);
            Currency currency = bank.DefaultCurrency;
            if (currency != null)
            {
                amount = amount * currency.ExchangeRate;
                if (amount <= 0)
                {
                    throw new InvalidAmountException("Please enter a valid amount to withdraw.");
                }
                else if (amount > userAccount.Balance)
                {
                    throw new InsufficientBalanceException("Insufficient funds.");
                }
                else
                {
                    userAccount.Balance -= amount;
                    CreateTransaction(userAccount, TransactionType.Debit, amount,currency);
                    FileHelper.WriteData(RBIStorage.banks);
                }
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
            CreateTransferTransaction(senderAccount, receiverAccount, amount, mode ,currency);
            FileHelper.WriteData(RBIStorage.banks);
        }
        private void ApplyTransferCharges(Account senderAccount, string receiverBankId, decimal amount, ModeOfTransfer mode,Currency currency)
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
                    CreateBankTransaction(bank, senderAccount.AccountId, charges, currency);
                }
                else 
                {
                    decimal charges = (bank.OtherRTGS * amount) / 100;
                    senderAccount.Balance -= charges;
                    bank.Balance += charges;
                    CreateBankTransaction(bank, senderAccount.AccountId, charges, currency);
                }
            }
            else
            {
                if(senderAccount.BankId.Equals(receiverBankId))
                {
                    decimal charges = (bank.SelfIMPS * amount) / 100;
                    senderAccount.Balance -= charges;
                    bank.Balance += charges;
                    CreateBankTransaction(bank, senderAccount.AccountId, charges, currency);
                }
                else
                {
                    decimal charges = (bank.OtherIMPS * amount) / 100;
                    senderAccount.Balance -= charges;
                    bank.Balance += charges;
                    CreateBankTransaction(bank, senderAccount.AccountId, charges, currency);
                }
            }
        }

        internal Transaction FetchTransactionByTransactionId(string transactionId)
        {
            Transaction transaction = null;
            string bankId = transactionId.Substring(3, 11);
            string accountId = transactionId.Substring(14, 11);
            Bank bank = RBIStorage.banks.FirstOrDefault(b => b.BankId.Equals(bankId));
            if (bank != null)
            {
                Account account = bank.Accounts.FirstOrDefault(b => b.AccountId.Equals(accountId));
                if (account != null)
                {
                    transaction = account.Transactions.FirstOrDefault(t => t.TransId.Equals(transactionId));
                    return transaction;
                }
            }
            return transaction;
        }

        public List<Transaction> FetchTransactionHistory(Account userAccount)
        {
            return userAccount.Transactions;
        }
    }
}
