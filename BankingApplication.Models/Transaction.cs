using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApplication.Models
{
    public class Transaction
    {
        public string TransId { get; set; }
        public TransType Type { get; set; }
        public string SenderAccountId { get; set; }
        public string ReceiverAccountId { get; set; }
        public DateTime On { get; set; }
        public decimal TransactionAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public Transaction(TransType transtype, DateTime timestamp, decimal transactionamount, decimal balanceamount,string bankid,string senderaccountid)
        {
            TransId = $"TXN{bankid}{senderaccountid}{timestamp:yyyyMMdd}";
            Type = transtype;
            On = timestamp;
            TransactionAmount = transactionamount;
            BalanceAmount = balanceamount;
        }
    }
    
}
