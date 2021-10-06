using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApplication.Exceptions
{
    public class InsufficientBalanceException:Exception
    {
        public InsufficientBalanceException(string message): base(message)
        {
                
        }
    }
}
