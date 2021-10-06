using System;

namespace BankingApplication.Exceptions
{
    public class AccountDoesntExistException : Exception
    {
        public AccountDoesntExistException(string message) : base(message)
        {

        }
    }
}
