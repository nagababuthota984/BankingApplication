using BankingApplication.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApplication.Services
{
    public class TransactionService
    {

        private void CreateTransaction(Account userAccount, TransactionType transtype, decimal transactionamount )
        {
            DateTime timestamp = DateTime.Now;
            Transaction NewTrans = new Transaction
            {
                TransId = $"TXN{userAccount.BankId}{userAccount.AccountId}{timestamp:yyyyMMdd}",
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
        private void CreateTransferTransaction(Account userAccount,Account receiverAccount,decimal transactionAmount,ModeOfTransfer mode)
        {
            DateTime timestamp = DateTime.Now;
            Transaction senderTransaction = new Transaction
            {
                TransId = $"TXN{userAccount.BankId}{userAccount.AccountId}{timestamp:yyyyMMdd}",
                SenderAccountId = userAccount.AccountId,
                ReceiverAccountId = receiverAccount.AccountId,
                Type = TransactionType.Transfer,
                On = timestamp,
                TransactionAmount = transactionAmount,
                BalanceAmount = userAccount.Balance,
                TransferMode = mode
            };
            userAccount.Transactions.Add(senderTransaction);
            Transaction receiverTransaction = new Transaction
            {
                TransId = $"TXN{receiverAccount.BankId}{receiverAccount.AccountId}{timestamp:yyyyMMdd}",
                SenderAccountId = userAccount.AccountId,
                ReceiverAccountId = receiverAccount.AccountId,
                Type = TransactionType.Transfer,
                On = timestamp,
                TransactionAmount = transactionAmount,
                BalanceAmount = receiverAccount.Balance,
                TransferMode = mode
            };
            receiverAccount.Transactions.Add(receiverTransaction);
        }
        private void CreateBankTransaction(Bank bank, string accountId, decimal charges)
        {
            DateTime timestamp = DateTime.Now;
            Transaction newBankTransaction = new Transaction
            {
                TransId = $"TXN{bank.BankId}{accountId}{timestamp}",
                Type = TransactionType.ServiceCharge,
                SenderAccountId = accountId,
                ReceiverAccountId = bank.BankId,
                On = timestamp,
                TransactionAmount = charges,
                TransferMode = ModeOfTransfer.None,
                BalanceAmount = bank.Balance
            };
            bank.Transactions.Add(newBankTransaction);
        }
        public void DepositAmount(Account userAccount, decimal amount)
        {
            if (amount > 0)
            {
                userAccount.Balance += amount;
                CreateTransaction(userAccount, TransactionType.Credit, amount);
                FileHelper.WriteData(RBIStorage.banks);
            }
            else
            {
                throw new InvalidAmountException("Please enter valid amount to deposit.");
            }
        }
        public void WithdrawAmount(Account userAccount,decimal amount)
        {
            if(amount<=0)
            {
                throw new InvalidAmountException("Please enter a valid amount to withdraw.");
            }
            else if(amount>userAccount.Balance)
            {
                throw new InsufficientBalanceException("Insufficient funds.");
            }
            else
            {
                userAccount.Balance -= amount;
                CreateTransaction(userAccount, TransactionType.Debit, amount);
                FileHelper.WriteData(RBIStorage.banks);
            }
        }
        public void TransferAmount(Account senderAccount,Account receiverAccount,decimal amount,ModeOfTransfer mode)
        {
            if (amount <= senderAccount.Balance)
            {
                senderAccount.Balance -= amount;
                receiverAccount.Balance += amount;
                ApplyTransferCharges(senderAccount, receiverAccount.BankId, amount, mode);
                CreateTransferTransaction(senderAccount, receiverAccount, amount, mode);
                FileHelper.WriteData(RBIStorage.banks);
            }
            else
            {
                throw new InvalidAmountException("Invalid amount to transfer.");
            }
        }
        private void ApplyTransferCharges(Account senderAccount, string receiverBankId, decimal amount, ModeOfTransfer mode)
        {
            Bank bank = BankService.GetBankByBankId(senderAccount.BankId);
            if (mode.Equals(ModeOfTransfer.RTGS))
            {
                //RTGS charge based on transfer to account within the same bank
                if (senderAccount.BankId.Equals(receiverBankId))
                {
                    decimal charges = (bank.SelfRTGS * amount) / 100;
                    senderAccount.Balance -= charges;
                    bank.Balance += charges;
                    CreateBankTransaction(bank, senderAccount.AccountId, charges);
                }
                else 
                {
                    decimal charges = (bank.OtherRTGS * amount) / 100;
                    senderAccount.Balance -= charges;
                    bank.Balance += charges;
                    CreateBankTransaction(bank, senderAccount.AccountId, charges);
                }
            }
            else
            {
                if(senderAccount.BankId.Equals(receiverBankId))
                {
                    decimal charges = (bank.SelfIMPS * amount) / 100;
                    senderAccount.Balance -= charges;
                    bank.Balance += charges;
                    CreateBankTransaction(bank, senderAccount.AccountId, charges);
                }
                else
                {
                    decimal charges = (bank.OtherIMPS * amount) / 100;
                    senderAccount.Balance -= charges;
                    bank.Balance += charges;
                    CreateBankTransaction(bank, senderAccount.AccountId, charges);
                }
            }
        }
        public List<Transaction> FetchTransactionHistory(Account userAccount)
        {
            return userAccount.Transactions;
        }
        
        
    }
}
