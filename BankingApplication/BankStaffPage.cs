using System;
using System.Collections.Generic;
using System.Linq;
using BankingApplication.Models;
using BankingApplication.Services;
namespace BankingApplication.CLI
{
    public class BankStaffPage
    {
        public void UserInterface()
        {
            Console.WriteLine("\n===================BANK STAFF LOGIN===================\n");
            string userName = UserInput.GetInputValue("Username");
            string password = UserInput.GetInputValue("Password");
            Console.WriteLine();
            if(!IsValidStaff(userName,password))
            {
                UserOutput.ShowMessage("Invalid Credentials\n");
            }
            else
            {
                try
                {
                    BankService bankService = new BankService();
                    AccountService acService = new AccountService();
                    Staff currentWorkingStaff = bankService.FetchStaffByUserName(userName);
                    Bank bank = bankService.GetBankByBankId(currentWorkingStaff.BankId);
                    if (currentWorkingStaff != null)
                    {
                        while (true)
                        {
                            Console.WriteLine("\n========================BANK STAFF MENU==========================\n");
                            BankStaffMenu choice = GetBankStaffMenuByInteger(Convert.ToInt32(UserInput.GetInputValue("Choice\n1.Create Account\n2.AddBank\n3.UpdateAccount\n4.Delete an Account\n5.AddNewEmployee\n6.AddNewCurrency\n7.SetServiceCharge\n8.ViewTransactions\n9.RevertTransaction\nAny other key to logout.")));
                            Console.WriteLine();
                            switch (choice)
                            {
                                case BankStaffMenu.CreateAccount:
                                    Console.WriteLine("\t-------Account Creation-------\n");
                                    Customer newCustomer = new Customer();
                                    Account NewAccount = new Account();
                                    newCustomer.Name = AskName();
                                    newCustomer.Age = AskAge();
                                    newCustomer.Gender = GetGenderByInteger(Convert.ToInt32(UserInput.GetInputValue("Gender:\n1.Male\n2.Female\n3.Prefer Not to say")));
                                    newCustomer.Dob = Convert.ToDateTime(UserInput.GetInputValue("Date of Birth"));
                                    newCustomer.ContactNumber = UserInput.GetInputValue("Contact Number");
                                    newCustomer.AadharNumber = UserInput.GetInputValue("Aadhar Number");
                                    newCustomer.PanNumber = UserInput.GetInputValue("PAN Number");
                                    newCustomer.Address = UserInput.GetInputValue("Address");
                                    NewAccount.AccountType = (AccountType)Convert.ToInt32(UserInput.GetInputValue("Account Type(1.Savings/2.Current)"));
                                    bankService.CreateAccount(newCustomer, NewAccount,currentWorkingStaff.BankId);
                                    string output = $"Account has been created!\nCredentials:Username - {NewAccount.UserName}\nPassword - {NewAccount.Password}\nAccount Number - {NewAccount.AccountNumber}\n";
                                    UserOutput.ShowMessage(output);
                                    break;
                                case BankStaffMenu.AddBank:
                                    string bankName = UserInput.GetInputValue("Name of the bank");
                                    string branch = UserInput.GetInputValue("Branch");
                                    string ifsc = UserInput.GetInputValue("IFSC");
                                    bankService.Add(bankName, branch, ifsc);
                                    Bank newBank = bankService.GetBankByIfsc(ifsc);
                                    if (newBank == null)
                                    {
                                        UserOutput.ShowMessage("Bank not created! Try again.");
                                    }
                                    else
                                    {
                                        UserOutput.ShowMessage($"Bank created with bank id - {newBank.BankId}");
                                    }
                                    break;
                                case BankStaffMenu.UpdateAccount:
                                    string accountId = UserInput.GetInputValue("Account Id");
                                    Account userAccount = acService.FetchAccountByAccountId(accountId);
                                    if (userAccount!=null)
                                    {
                                        Console.WriteLine("\nYou will only be able to modify customer related properties. Properties like account number, accound Id, Balance Cannot be changed!\n");
                                        string property = UserInput.GetInputValue("property you want to update");
                                        Console.WriteLine("Enter new value: ");
                                        string newValue = Console.ReadLine();
                                        new AccountService().UpdateAccount(userAccount, property, newValue);
                                        UserOutput.ShowMessage("Updated!"); 
                                    }
                                    else
                                    {
                                        UserOutput.ShowMessage("Account Does not exists.\n");
                                    }
                                    break;
                                case BankStaffMenu.DeleteAccount:
                                    accountId = UserInput.GetInputValue("Account Id");
                                    userAccount = acService.FetchAccountByAccountId(accountId);
                                    if (userAccount!=null)
                                    {
                                        acService.DeleteAccount(userAccount);
                                        if (userAccount.Status.Equals(AccountStatus.Closed))
                                        {
                                            UserOutput.ShowMessage("Account Deleted");
                                        }
                                        else
                                        {
                                            UserOutput.ShowMessage("Account was not deleted. Try again later.");
                                        } 
                                    }
                                    else
                                    {
                                        UserOutput.ShowMessage("Account Does not exists.\n");
                                    }
                                    break;
                                case BankStaffMenu.AddNewEmployee:
                                    Console.WriteLine("\n-----------New Employee-----------\n");
                                    Staff newStaff = new Staff();
                                    newStaff.Name = AskName();
                                    newStaff.Age = Convert.ToInt32(AskAge());
                                    newStaff.Dob = Convert.ToDateTime(UserInput.GetInputValue("Employee Date of Birth"));
                                    newStaff.Gender = GetGenderByInteger(Convert.ToInt32(UserInput.GetInputValue("Employee Gender")));
                                    newStaff.BankId = newStaff.BankId;
                                    newStaff.Designation = (StaffDesignation)Convert.ToInt32(UserInput.GetInputValue("Employee Designation"));
                                    bankService.AddStaff(newStaff);
                                    output = $"Employee {newStaff.Name} has been added! Credentials:\n{newStaff.UserName}\n{newStaff.Password}\n";
                                    UserOutput.ShowMessage(output);
                                    break;
                                case BankStaffMenu.AddNewCurrency:
                                    string bankId = currentWorkingStaff.BankId;
                                    string newCurrency = UserInput.GetInputValue("new currency type");
                                    decimal exchangeRate = Convert.ToDecimal(UserInput.GetInputValue("exchange rate"));
                                    bankService.AddNewCurrency(bank, newCurrency, exchangeRate);
                                    break;
                                case BankStaffMenu.SetServiceCharge:
                                    bankId = currentWorkingStaff.BankId;
                                    ModeOfTransfer mode = (ModeOfTransfer)Convert.ToInt32(UserInput.GetInputValue("Change service charge:\n1.RTGS\n2.IMPS"));
                                    bool isSelfBankTransfer = (Convert.ToInt32(UserInput.GetInputValue("Charge type:\n1.Money Transfer Within bank.\n2.Money transfer to other banks")).Equals(1)) ? true : false;
                                    decimal value = Convert.ToDecimal(UserInput.GetInputValue("New Charge Value:"));
                                    bankService.SetServiceCharge(mode, isSelfBankTransfer, bankId, value);
                                    if (bankService.GetServiceCharge(mode, isSelfBankTransfer, bankId).Equals(value))
                                    {
                                        UserOutput.ShowMessage("Updation success");
                                    }
                                    else
                                    {
                                        UserOutput.ShowMessage("Cannot update. Try again.");
                                    }
                                    break;
                                case BankStaffMenu.ViewTransactions:
                                    accountId = UserInput.GetInputValue("Account Id");
                                    List<Transaction> transactions = bankService.FetchAccountTransactions(accountId);
                                    if (transactions != null)
                                    {
                                        UserOutput.ShowTransactions(transactions);
                                    }
                                    else
                                    {
                                        UserOutput.ShowMessage("Account does not exists.\n");
                                    }
                                    break;
                                case BankStaffMenu.RevertTransaction:
                                    string transactionId = UserInput.GetInputValue("Transaction Id");
                                    Transaction transaction = new TransactionService().FetchTransactionByTransactionId(transactionId);
                                    if (transaction != null)
                                    {
                                        Console.WriteLine("Are you sure you want to revert the transaction(Y/N)?\n");
                                        if(Console.ReadLine().ToLower().Contains("y"))
                                        {
                                            bankService.RevertTransaction(transaction, bank);
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        UserOutput.ShowMessage("No such transaction found!\n");
                                    }
                                    break;
                                case BankStaffMenu.Logout:
                                    Program.WelcomeMenu();
                                    break;
                            }
                        }
                    }
                    else
                    {
                        UserOutput.ShowMessage("Staff doesn't exist");
                    }

                }
                catch(Exception ex)
                { 
                    Console.WriteLine(ex.Message); 
                }  

            }
        }
        private Gender GetGenderByInteger(int v)
        {
            if(v==1)
            {
                return Gender.Male;
            }
            else if(v==2)
            {
                return Gender.Female;
            }
            else if(v==3)
            {
                return Gender.PreferNotToSay;
            }
            else
            {
                return Gender.PreferNotToSay;
            }
        }
        private string AskName()
        {
            Console.WriteLine("Please enter customer's name:");
            string name = Console.ReadLine();
            while (name.Any(Char.IsDigit))
            {
                Console.WriteLine("Name should not have digits in it.Please enter the name again:\n");
                name = Console.ReadLine();
            } 
            return name;
        }
        private string AskAge()
        {
            Console.WriteLine("Please enter customer's age");
            int age = Convert.ToInt32(Console.ReadLine());
            while (age <= 0 || age>100)
            {
                Console.WriteLine("Please enter valid age. Re-enter age:");
                age = Convert.ToInt32(Console.ReadLine());
            }
            return age.ToString();
        }
        private BankStaffMenu GetBankStaffMenuByInteger(int v)
        {
            if (v == 1)
                return BankStaffMenu.CreateAccount;
            else if (v == 2)
                return BankStaffMenu.AddBank;
            else if (v == 3)
                return BankStaffMenu.UpdateAccount;
            else if (v == 4)
                return BankStaffMenu.DeleteAccount;
            else if (v == 5)
                return BankStaffMenu.AddNewEmployee;
            else if (v == 6)
                return BankStaffMenu.AddNewCurrency;
            else if (v == 7)
                return BankStaffMenu.SetServiceCharge;
            else if (v == 8)
                return BankStaffMenu.ViewTransactions;
            else if (v == 9)
                return BankStaffMenu.RevertTransaction;
            else if (v == 10)
                return BankStaffMenu.Logout;
            else
                return BankStaffMenu.Logout;
        }
        private bool IsValidStaff(string userName, string password)
        {
            if (RBIStorage.banks != null)
            {
                foreach (var bank in RBIStorage.banks)
                {
                    foreach (var employee in bank.Employees)
                    {
                        if (employee.UserName.Equals(userName))
                        {
                            if (employee.Password.Equals(password))
                            {
                                return true;
                            }
                        }
                    }
                }
                return false;
            }
            else
            {
                return false;
            }
        }
       
    }
}
