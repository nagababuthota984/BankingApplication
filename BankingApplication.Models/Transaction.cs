using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApplication.Models
{
    public class Transaction
    {
        public Transaction()
        {

        }
        public Transaction(Account userAccount, TransactionType transtype, decimal transactionamount, Currency currency)
        {
            //..Creates a normal (credit/debit) transaction
            DateTime timestamp = DateTime.Now;
            this.TransId = $"TXN{userAccount.BankId}{userAccount.AccountId}{timestamp:yyyyMMddhhmmss}";
            this.Type = transtype;
            this.On = timestamp;
            this.SenderAccountId = userAccount.AccountId;
            this.ReceiverAccountId = userAccount.AccountId;
            this.SenderBankId = userAccount.BankId;
            this.ReceiverBankId = userAccount.BankId;
            this.TransactionAmount = transactionamount;
            this.BalanceAmount = userAccount.Balance;
            this.TransferMode = ModeOfTransfer.None;
        }

        public Transaction(string accountId, Bank bank, TransactionType serviceCharge, decimal charges, Currency currency)
        {
            DateTime timestamp = DateTime.Now;
            this.TransId = $"TXN{bank.BankId}{accountId}{timestamp:yyyyMMddhhmmss}";
            this.Type = TransactionType.ServiceCharge;
            this.SenderAccountId = accountId;
            this.ReceiverAccountId = bank.BankId;
            this.SenderBankId = bank.BankId;
            this.ReceiverBankId = bank.BankId;
            this.On = timestamp;
            this.TransactionAmount = charges;
            this.TransferMode = ModeOfTransfer.None;
            this.Currency = currency;
            this.BalanceAmount = bank.Balance;
        }

        public Transaction(Account userAccount, Account receiverAccount, TransactionType transfer, decimal transactionAmount, Currency currency, ModeOfTransfer mode)
        {
            //..Creates a transfer transaction
            DateTime timestamp = DateTime.Now;
            this.TransId = $"TXN{userAccount.BankId}{receiverAccount.AccountId}{timestamp:yyyyMMddhhmmss}";
            this.Type = transfer;
            this.SenderAccountId = userAccount.AccountId;
            this.ReceiverAccountId = receiverAccount.AccountId;
            this.SenderBankId = userAccount.BankId;
            this.ReceiverBankId = receiverAccount.BankId;
            this.Type = TransactionType.Transfer;
            this.On = timestamp;
            this.TransactionAmount = transactionAmount;
            this.BalanceAmount = receiverAccount.Balance;
            this.Currency = currency;
            this.TransferMode = mode;
        }


        public string TransId { get; set; }
        public TransactionType Type { get; set; }
        public string SenderAccountId { get; set; }
        public string ReceiverAccountId { get; set; }
        public string SenderBankId { get; set; }
        public string ReceiverBankId { get; set; }
        public DateTime On { get; set; }
        public decimal TransactionAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public Currency Currency { get; set; }
        public ModeOfTransfer TransferMode { get; set; }
    }
    
}
