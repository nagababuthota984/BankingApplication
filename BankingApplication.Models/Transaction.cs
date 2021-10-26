using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApplication.Models
{
    public class Transaction
    {
        public string TransId { get; set; }
        public TransactionType Type { get; set; }
        public string SenderAccountId { get; set; }
        public string ReceiverAccountId { get; set; }
        public DateTime On { get; set; }
        public decimal TransactionAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public Currency Currency { get; set; }
        public ModeOfTransfer TransferMode { get; set; }
    }
    
}
