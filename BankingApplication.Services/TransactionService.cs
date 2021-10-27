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
            Transaction newTransaction = new Transaction(userAccount,transtype,transactionamount,currency);
            userAccount.Transactions.Add(newTransaction);
        }
        public void CreateTransferTransaction(Account userAccount,Account receiverAccount,decimal transactionAmount,ModeOfTransfer mode, Currency currency)
        {
            Transaction senderTransaction = new Transaction(userAccount,receiverAccount, TransactionType.Transfer, transactionAmount, currency,mode);
            userAccount.Transactions.Add(senderTransaction);
            Transaction receiverTransaction = new Transaction(receiverAccount,userAccount, TransactionType.Transfer, transactionAmount, currency, mode);
            receiverTransaction.SenderAccountId = userAccount.AccountId;
            receiverTransaction.ReceiverAccountId = receiverAccount.AccountId;
            receiverAccount.Transactions.Add(receiverTransaction);
        }
        public void CreateBankTransaction(Bank bank, string accountId, decimal charges,Currency currency)
        {
            Transaction newBankTransaction = new Transaction(accountId, bank, TransactionType.ServiceCharge, charges, currency);
            bank.Transactions.Add(newBankTransaction);
        }
        public Transaction GetTransactionById(string transactionId)
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

        
    }
}
