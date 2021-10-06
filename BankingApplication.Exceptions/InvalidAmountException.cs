using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApplication.Exceptions
{
    public class InvalidAmountException:Exception
    {
        public InvalidAmountException(string message):base(message)
        {

        }
    }
}
