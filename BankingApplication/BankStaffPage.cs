using System;
using BankingApplication.Models;
using BankingApplication.Services;
namespace BankingApplication.CLI
{
    public class BankStaffPage
    {
        public void UserInterface()
        {
            //string userName = UserInput.AskUser("Username");
            //string password = UserInput.AskUser("Password");
            //if(IsValidStaff(userName,password))
            //{
            //    StaffActions();
            //}
        }

        
        public void StaffActions()
        {
            
            while(true)
            {
                BankStaffMenu choice = (BankStaffMenu)int.Parse(UserInput.AskUser("Choice:\n1.Create Account\n2.UpdateOrDeleteAccount\n3.AddNewCurrency\n4.SetServiceCharge\n5.ViewTransactions\n6.RevertTransaction"));
                BankService bankService = new BankService();
                switch (choice)
                {
                    case BankStaffMenu.CreateAccount:
                        Console.WriteLine("\t-------Account Creation-------\n");
                        Customer newCustomer = new Customer();
                        Account NewAccount = new Account();
                        newCustomer.Name = UserInput.AskUser("Name");
                        newCustomer.Age = int.Parse(UserInput.AskUser("age"));
                        newCustomer.Gender = UserInput.AskUser("Gender");
                        newCustomer.Dob = DateTime.Parse(UserInput.AskUser("Date of Birth"));
                        newCustomer.ContactNumber = UserInput.AskUser("Contact Number");
                        newCustomer.AadharNumber = UserInput.AskUser("Aadhar Number");
                        newCustomer.PanNumber = UserInput.AskUser("PAN Number");
                        newCustomer.Address = UserInput.AskUser("Address");
                        NewAccount.BankName = UserInput.AskUser("Name of the bank");
                        NewAccount.AccountType = (AccountType)int.Parse(UserInput.AskUser("Account Type(1.Savings/2.Current)"));
                        NewAccount.Branch = UserInput.AskUser("Bank Branch");
                        NewAccount.Ifsc = UserInput.AskUser("IFSC");

                        bankService.CreateAccount(newCustomer, NewAccount);
                        UserOutput.AccountCreationSuccess(NewAccount.UserName, NewAccount.Password);
                        break;
                    case BankStaffMenu.AddNewEmployee:
                        Console.WriteLine("\n-----------New Employee-----------\n");
                        Staff newStaff = new Staff();
                        newStaff.Name = UserInput.AskUser("Employee Name");
                        newStaff.Age = int.Parse(UserInput.AskUser("Employee Age"));
                        newStaff.Gender = UserInput.AskUser("Employee Gender");
                        newStaff.BankId = UserInput.AskUser("BankId");
                        newStaff.Designation = (StaffDesignation)int.Parse(UserInput.AskUser("Employee Designation"));
                        bankService.AddStaff(newStaff);
                        
                        break;
                    default:Environment.Exit(0);
                        break;
                }
            }
            
        }
    }
}
