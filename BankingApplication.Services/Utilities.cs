using BankingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankingApplication.Services
{
    public class Utilities
    {
        //Contains all the helper methods needed for AccountService and BankServices.

        //remove this
        internal static bool IsDuplicateAccountNumber(string accountNumber, string bankid)
        {

            var requiredBank = RBIStorage.banks.FirstOrDefault(bank => bank.BankId == bankid);
            if (requiredBank != null)
            {
                if(requiredBank.Accounts.Any(account => account.AccountNumber == accountNumber))
                {
                    return true;
                }
                
            }
            return false;
        }
        /// <summary>
        /// Account Number Generator
        /// </summary>
        /// <param name="length"></param>
        /// <returns>String</returns>
        internal static string GenerateRandomNumber(int length)
        {
            Random r = new Random();          
            string accountNumber = "";
            for (int i = 1; i < length; i++)
            {
                accountNumber += r.Next(0, 9).ToString();
            }
            return accountNumber;
        }
    }
}
