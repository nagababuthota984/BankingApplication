using System;
using BankingApplication.Models;
using BankingApplication.Services;
namespace BankingApplication.CLI
{
    public class BankStaffPage
    {
        public void UserInterface()
        {
            string userName = UserInput.AskUser("Username");
            string password = UserInput.AskUser("Password");
            if(!IsValidStaff(userName,password))
            {
                UserOutput.ShowMessage("Invalid Credentials\n");
            }
            else
            {
                try
                {
                    while (true)
                    {
                        BankStaffMenu choice = (BankStaffMenu)int.Parse(UserInput.AskUser("Choice\n1.Create Account\n2.UpdateAccount\n3.Delete an Account\n4.AddNewEmployee\n5.AddNewCurrency\n6.SetServiceCharge\n7.ViewTransactions\n8.RevertTransaction"));
                        BankService bankService = new BankService();
                        AccountService acService = new AccountService();
                        Staff currentWorkingStaff = bankService.FetchStaffByUserName(userName);
                        switch (choice)
                        {
                            case BankStaffMenu.CreateAccount:
                                Console.WriteLine("\t-------Account Creation-------\n");
                                Customer newCustomer = new Customer();
                                Account NewAccount = new Account();
                                newCustomer.Name = UserInput.AskUser("Name");
                                newCustomer.Age = UserInput.AskUser("age");
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
                                string output = $"Account has been created!\nCredentials:Username - {NewAccount.UserName}\nPassword - {NewAccount.Password}\nAccount Number - {NewAccount.AccountNumber}\n";
                                UserOutput.ShowMessage(output);
                                break;
                            case BankStaffMenu.AddBank:
                                string bankName = UserInput.AskUser("Name of the bank");
                                string branch = UserInput.AskUser("Branch");
                                string ifsc = UserInput.AskUser("IFSC");
                                bankService.Add(bankName,branch, ifsc);
                                Bank bank = bankService.GetBankByIfsc(ifsc);
                                if(bank == null)
                                {
                                    UserOutput.ShowMessage("Bank not created! Try again.");
                                }
                                else
                                {
                                    UserOutput.ShowMessage($"Bank created with bank id - {bank.BankId}");
                                }
                                break;
                            case BankStaffMenu.UpdateAccount:
                                string accountId = UserInput.AskUser("Account Id");
                                Account userAccount = acService.FetchAccountByAccountId(accountId);
                                Console.WriteLine("\nYou will only be able to modify customer related properties. Properties like account number, accound Id, Balance Cannot be changed!\n");
                                string property = UserInput.AskUser("property you want to update");
                                Console.WriteLine("Enter new value: ");
                                string newValue = Console.ReadLine();
                                new AccountService().UpdateAccount(userAccount,property,newValue);
                                UserOutput.ShowMessage("Updated!");
                                break;
                            case BankStaffMenu.DeleteAccount:
                                accountId = UserInput.AskUser("Account Id");
                                userAccount = acService.FetchAccountByAccountId(accountId);
                                acService.DeleteAccount(userAccount);
                                if (userAccount.Status.Equals(AccountStatus.Closed))
                                {
                                    UserOutput.ShowMessage("Account Deleted");
                                }
                                else
                                {
                                    UserOutput.ShowMessage("Account was not deleted. Try again later.");
                                }
                                break;
                            case BankStaffMenu.AddNewEmployee:
                                Console.WriteLine("\n-----------New Employee-----------\n");
                                Staff newStaff = new Staff();
                                newStaff.Name = UserInput.AskUser("Employee Name");
                                newStaff.Age = int.Parse(UserInput.AskUser("Employee Age"));
                                newStaff.Dob = DateTime.Parse(UserInput.AskUser("Employee Date of Birth"));
                                newStaff.Gender = UserInput.AskUser("Employee Gender");
                                newStaff.BankId = UserInput.AskUser("BankId");
                                newStaff.Designation = (StaffDesignation)int.Parse(UserInput.AskUser("Employee Designation"));
                                bankService.AddStaff(newStaff);
                                output = $"Employee {newStaff.Name} has been added! Credentials:\n{newStaff.UserName}\n{newStaff.Password}\n";
                                UserOutput.ShowMessage(output);
                                break;
                            case BankStaffMenu.AddNewCurrency:
                                string bankId = UserInput.AskUser("bank Id");
                                string newCurrency = UserInput.AskUser("new currency type");
                                decimal exchangeRate = decimal.Parse(UserInput.AskUser("exchange rate"));
                                bankService.AddNewCurrency(bankId, newCurrency, exchangeRate);
                                break;
                            case BankStaffMenu.SetServiceCharge:
                                bankId = UserInput.AskUser("Bank Id");
                                ModeOfTransfer mode = (ModeOfTransfer)int.Parse(UserInput.AskUser("Change service charge:\n1.RTGS\n2.IMPS"));
                                bool isSelfBankTransfer = (int.Parse(UserInput.AskUser("Charge type:\n1.Money Transfer Within bank.\n2.Money transfer to other banks")).Equals(1)) ? true : false;
                                decimal value = decimal.Parse(UserInput.AskUser("New Charge Value:"));
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
                                accountId = UserInput.AskUser("Account Id");
                                UserOutput.ShowTransactions(bankService.FetchAccountTransactions(accountId));
                                break;
                            case BankStaffMenu.RevertTransaction:
                                string transactionId = UserInput.AskUser("Transaction Id");
                                bankService.RevertTransaction(transactionId, currentWorkingStaff.BankId);
                                break;
                            default:
                                Environment.Exit(0);
                                break;
                        }
                    }

                }
                catch(Exception ex)
                { 
                    Console.WriteLine(ex.Message); 
                }  

            }
        }

        

        private void UpdateAccountHandler(Account userAccount)
        {

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
