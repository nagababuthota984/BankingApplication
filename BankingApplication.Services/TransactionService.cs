using BankingApplication.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApplication.Services
{
    public class TransactionService
    {
        public static void RecordTransaction(Account userAccount, TransactionType transtype, DateTime timestamp, decimal transactionamount, string bankid, bool isTransfer=false,ModeOfTransfer mode=ModeOfTransfer.None )
        {
            Transaction NewTrans = new Transaction
            {
                TransId = $"TXN{bankid}{userAccount.AccountId}{timestamp:yyyyMMdd}",
                Type = transtype,
                On = timestamp,
                TransactionAmount = transactionamount,
                BalanceAmount = userAccount.Balance,
                IsTransfer = isTransfer,
                TransferMode = mode
                
            };
            userAccount.Transactions.Add(NewTrans);


        }

        
    }
}
