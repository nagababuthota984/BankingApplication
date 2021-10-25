using System;
using System.Runtime.Serialization;

namespace BankingApplication.Services
{
    public class TransactionDoesntExist : Exception
    {
        
        public TransactionDoesntExist(string message) : base(message)
        {
        }
    }
}