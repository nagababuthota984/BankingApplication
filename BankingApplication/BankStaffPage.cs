using System;
using System.Collections.Generic;
using System.Linq;
using BankingApplication.Models;
using BankingApplication.Services;
namespace BankingApplication.CLI
{

    public class BankEmployeePage
    {
        BankService bankService = new BankService();
        AccountService accountService = new AccountService();
        TransactionService transactionService = new TransactionService();
        public void UserInterface()
        {
            Console.WriteLine("\n===================BANK Employee LOGIN===================\n");
            string userName = UserInput.GetInputValue("Username");
            string password = UserInput.GetInputValue("Password");
            Console.WriteLine();
            Employee currentWorkingEmployee = bankService.GetEmployeeByUserName(userName);
            if (currentWorkingEmployee == null)
            {
                UserOutput.ShowMessage("Invalid Credentials\n");
            }
            else
            {
                try
                {
                    Bank bank = bankService.GetBankByBankId(currentWorkingEmployee.BankId);
                    {
                        while (true)
                        {
                            Console.WriteLine("\n========================BANK STAFF MENU==========================\n");
                            switch (GetBankEmployeeMenuByInteger(Convert.ToInt32(UserInput.GetInputValue(Constant.employeeMenu))))
                            {
                                case BankEmployeeMenu.CreateAccount:
                                    Console.WriteLine("\t-------Account Creation-------\n");
                                    string name = AskName();
                                    string age = AskAge();
                                    Gender gender = GetGenderByInteger(Convert.ToInt32(UserInput.GetInputValue("Gender:\n1.Male\n2.Female\n3.Prefer Not to say")));
                                    DateTime dob = Convert.ToDateTime(UserInput.GetInputValue("Date of Birth"));
                                    string contactNumber = UserInput.GetInputValue("Contact Number");
                                    string aadharNumber = UserInput.GetInputValue("Aadhar Number");
                                    string panNumber = UserInput.GetInputValue("PAN Number");
                                    string address = UserInput.GetInputValue("Address");
                                    AccountType accountType = (AccountType)Convert.ToInt32(UserInput.GetInputValue("Account Type(1.Savings/2.Current)"));
                                    Customer newCustomer = new Customer(name, age, gender, dob, contactNumber, aadharNumber, panNumber, address);
                                    Account newAccount = new Account(newCustomer,accountType);
                                    bankService.CreateAccount(newAccount, bank);
                                    UserOutput.ShowMessage($"Account has been created!\nCredentials:Username - {newAccount.UserName}\nPassword - {newAccount.Password}\nAccount Number - {newAccount.AccountNumber}\n");
                                    break;
                                case BankEmployeeMenu.AddBank:
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
                                case BankEmployeeMenu.UpdateAccount:
                                    string accountId = UserInput.GetInputValue("Account Id");
                                    Account userAccount = accountService.GetAccountById(accountId);
                                    if (userAccount != null)
                                    {
                                        Console.WriteLine("\nYou will only be able to modify customer related properties. Properties like account number, accound Id, Balance Cannot be changed!\n");
                                        string property = UserInput.GetInputValue("property you want to update");
                                        Console.WriteLine("Enter new value: ");
                                        string newValue = Console.ReadLine();
                                        accountService.UpdateAccount(userAccount, property, newValue);
                                        UserOutput.ShowMessage("Updated!");
                                    }
                                    else
                                    {
                                        UserOutput.ShowMessage("Account Does not exists.\n");
                                    }
                                    break;
                                case BankEmployeeMenu.DeleteAccount:
                                    accountId = UserInput.GetInputValue("Account Id");
                                    userAccount = accountService.GetAccountById(accountId);
                                    if (userAccount != null)
                                    {
                                        if (accountService.DeleteAccount(userAccount))
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
                                        UserOutput.ShowMessage("Account does not exists.\n");
                                    }
                                    break;
                                case BankEmployeeMenu.AddNewEmployee:
                                    Console.WriteLine("\n-----------New Employee-----------\n");
                                    name = AskName();
                                    age = AskAge();
                                    dob = Convert.ToDateTime(UserInput.GetInputValue("Employee Date of Birth"));
                                    gender = GetGenderByInteger(Convert.ToInt32(UserInput.GetInputValue("Employee Gender")));
                                    EmployeeDesignation role = (EmployeeDesignation)Convert.ToInt32(UserInput.GetInputValue("Employee Designation"));
                                    Employee newEmployee = bankService.CreateAndGetEmployee(name,age,dob,gender,role,bank);
                                    UserOutput.ShowMessage($"Employee {newEmployee.Name} has been added! Credentials:\n{newEmployee.UserName}\n{newEmployee.Password}\n"); 
                                    break;
                                case BankEmployeeMenu.AddNewCurrency:
                                    string newCurrency = UserInput.GetInputValue("new currency type");
                                    decimal exchangeRate = Convert.ToDecimal(UserInput.GetInputValue("exchange rate"));
                                    if (exchangeRate>0)
                                    {
                                        if (bankService.AddNewCurrency(bank, newCurrency, exchangeRate))
                                        {
                                            UserOutput.ShowMessage("New Currency Added!");
                                        }
                                        else
                                        {
                                            UserOutput.ShowMessage("Currency Already Exists!");
                                        } 
                                    }
                                    else
                                    {
                                        UserOutput.ShowMessage("Invalid exchange rate");
                                    }
                                    break;
                                case BankEmployeeMenu.SetServiceCharge:
                                    ModeOfTransfer mode = (ModeOfTransfer)Convert.ToInt32(UserInput.GetInputValue("Change service charge:\n1.RTGS\n2.IMPS"));
                                    bool isSelfBankTransfer = (Convert.ToInt32(UserInput.GetInputValue("Charge type:\n1.Money Transfer Within bank.\n2.Money transfer to other banks")).Equals(1)) ? true : false;
                                    decimal value = Convert.ToDecimal(UserInput.GetInputValue("New Charge Value:"));
                                    if (bankService.SetServiceCharge(mode, isSelfBankTransfer, bank, value))
                                    {
                                        UserOutput.ShowMessage("Updation success");
                                    }
                                    else
                                    {
                                        UserOutput.ShowMessage("Cannot update. Try again.");
                                    }
                                    break;
                                case BankEmployeeMenu.ViewTransactions:
                                    accountId = UserInput.GetInputValue("Account Id");
                                    List<Transaction> transactions = bankService.GetAccountTransactions(accountId);
                                    if (transactions != null)
                                    {
                                        UserOutput.ShowTransactions(transactions);
                                    }
                                    else
                                    {
                                        UserOutput.ShowMessage("Account does not exists.\n");
                                    }
                                    break;
                                case BankEmployeeMenu.RevertTransaction:
                                    string transactionId = UserInput.GetInputValue("Transaction Id");
                                    Transaction transaction = transactionService.GetTransactionById(transactionId);
                                    if (transaction != null)
                                    {
                                        if (transaction.SenderBankId.Equals(transaction.ReceiverBankId))
                                        {
                                            Console.WriteLine("Are you sure you want to revert the transaction(Y/N)?\n");
                                            if (Console.ReadLine().ToLower().Contains("y"))
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
                                            Console.WriteLine("Cannot revert transaction involving other bank.");
                                        }
                                    }
                                    else
                                    {
                                        UserOutput.ShowMessage("No such transaction found!\n");
                                    }
                                    break;
                                case BankEmployeeMenu.Logout:
                                    Program.WelcomeMenu();
                                    break;
                            }
                        }



                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
        }
        private Gender GetGenderByInteger(int v)
        {
            if (v == 1)
            {
                return Gender.Male;
            }
            else if (v == 2)
            {
                return Gender.Female;
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
            while (age <= 0 || age > 100)
            {
                Console.WriteLine("Please enter valid age. Re-enter age:");
                age = Convert.ToInt32(Console.ReadLine());
            }
            return age.ToString();
        }
        private BankEmployeeMenu GetBankEmployeeMenuByInteger(int v)
        {
            if (v == 1)
                return BankEmployeeMenu.CreateAccount;
            else if (v == 2)
                return BankEmployeeMenu.AddBank;
            else if (v == 3)
                return BankEmployeeMenu.UpdateAccount;
            else if (v == 4)
                return BankEmployeeMenu.DeleteAccount;
            else if (v == 5)
                return BankEmployeeMenu.AddNewEmployee;
            else if (v == 6)
                return BankEmployeeMenu.AddNewCurrency;
            else if (v == 7)
                return BankEmployeeMenu.SetServiceCharge;
            else if (v == 8)
                return BankEmployeeMenu.ViewTransactions;
            else if (v == 9)
                return BankEmployeeMenu.RevertTransaction;
            else if (v == 10)
                return BankEmployeeMenu.Logout;
            else
                return BankEmployeeMenu.Logout;
        }


    }
}
