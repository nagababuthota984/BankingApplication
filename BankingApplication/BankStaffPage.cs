using System;
using System.Collections.Generic;
using System.Text;
using BankingApplication.Services;
namespace BankingApplication.CLI
{
    public class BankStaffPage
    {
        public static void ShowMenu()
        {
            //1.Create an Account\n2.Update/delete an account\n3.Add new currency with exchange rate\n
            //4.Set service charge(same/other)\n5.view transactions\n6.revert a transaction
            AccountHolderPage.ShowMenu();
        }
    }
}
