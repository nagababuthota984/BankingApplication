using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApplication.Models
{
    public class Transaction
    {
        public string TransId { get; set; }
        public TransType Type { get; set; }
        public DateTime DoneOn { get; set; }
        public decimal TransactionAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public Transaction(TransType transtype, DateTime timestamp, decimal transactionamount, decimal balanceamount,string bankid,string accountid)
        {
            TransId = "TXN" + bankid + accountid + timestamp.ToString("yyyyMMdd");
            Type = transtype;
            DoneOn = timestamp;
            TransactionAmount = transactionamount;
            BalanceAmount = balanceamount;
        }
    }
    public enum TransType
    {
        Credit,
        Debit
    }
}
