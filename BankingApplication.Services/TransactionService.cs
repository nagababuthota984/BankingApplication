using BankingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankingApplication.Services
{
    public class TransactionService
    {

        public void CreateTransaction(Account userAccount, TransactionType transtype, decimal transactionamount, Currency currency)
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
        public void CreateTransferTransaction(Account userAccount,Account receiverAccount,decimal transactionAmount,ModeOfTransfer mode, Currency currency)
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
        public void CreateBankTransaction(Bank bank, string accountId, decimal charges,Currency currency)
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
        

        
        public Transaction FetchTransactionByTransactionId(string transactionId)
        {
            Transaction transaction = null;
            if (transactionId.Substring(0, 3) == "TXN" || transactionId.Length >=38)
            {
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
            }
            return transaction;
        }

        public List<Transaction> FetchTransactionHistory(Account userAccount)
        {
            return userAccount.Transactions;
        }
    }
}
