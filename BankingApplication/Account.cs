using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace BankingApplication
{
    public class Account : Person
    {

        // public double accountNumber;
        public double accountNumber;
        public string accountType;
        public double balance;
        public const int MIN_BALANCE = 250;
        public static Dictionary<double, Dictionary<string, string>> accounts;
        public static Dictionary<double, string> transactions;
        public Account()
        {
            double accountNumber = 0;
            string accountType = "";
            double balance = 0;
            accounts = readAccounts();
            transactions = readTransactions();


        }
        public void createAccount()
        {
            
            
            //collects basic details and creates an account.
            Console.WriteLine("Enter your Full Name(as in ID Proof): ");
            this.name = Console.ReadLine();
            Console.WriteLine("Enter your Age: ");
            this.age = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter your Gender: ");
            this.gender = Console.ReadLine();
            Console.WriteLine("Enter your Phone number: ");
            this.contactNumber = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine("Enter your Date of Birth: ");
            this.dob = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Enter your Complete Address: ");
            this.address = Console.ReadLine();
            Console.WriteLine("Enter your Aadhar Number: ");
            this.aadharNumber = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine("Enter your Pan Number: ");
            this.panNumber = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine("Enter the Account type: ");
            this.accountType = Console.ReadLine();
            this.balance = Account.MIN_BALANCE;
            
            Random random = new Random();          //account number generator.
            string r = "";
            int i;
            for (i = 1; i < 11; i++)
            {
                r += random.Next(0, 9).ToString();
            }
            this.accountNumber = Convert.ToDouble(r);
            Console.WriteLine(this.accountNumber);


            //Dictionary<double,Dictionary<string,string>> accounts = readAccounts();
            
            if(!accounts.ContainsKey(this.accountNumber))
            { 
                accounts.Add(this.accountNumber, new Dictionary<string,string>());
                //Console.WriteLine(accounts[this.accountNumber]); 
            }
            accounts[this.accountNumber]["name"] = this.name;
            accounts[this.accountNumber]["age"] = Convert.ToString(this.age);
            accounts[this.accountNumber]["gender"] = this.gender;
            accounts[this.accountNumber]["contactNumber"] = Convert.ToString(this.contactNumber);
            accounts[this.accountNumber]["dob"] = Convert.ToString(this.dob);
            accounts[this.accountNumber]["address"] = this.address;
            accounts[this.accountNumber]["aadharNumber"] = Convert.ToString(this.aadharNumber);
            accounts[this.accountNumber]["panNumber"] = Convert.ToString(this.panNumber);
            accounts[this.accountNumber]["accountType"] = this.accountType;
            accounts[this.accountNumber]["balance"] = Convert.ToString(this.balance);

            //writing to json
            writeAccounts(accounts);

           



        }
        public static Dictionary<double,Dictionary<string,string>> readAccounts()//reads data from json file
        {

            Dictionary<double, Dictionary<string,string>> jsonObject = new Dictionary<double, Dictionary<string,string>>();

            string data = File.ReadAllText("C:\\Users\\nagab\\OneDrive\\Desktop\\Technovert\\Banking Application\\BankingApplication\\accounts.json");
            jsonObject = JsonConvert.DeserializeObject<Dictionary<double,Dictionary<string,string>>>(data);
           
            return jsonObject;
            

        }
        public static void writeAccounts(Dictionary<double, Dictionary<string, string>> dataToWrite)    //writes to json file
        {
            string serializedData = JsonConvert.SerializeObject(dataToWrite,Formatting.Indented);
            File.WriteAllText("C:\\Users\\nagab\\OneDrive\\Desktop\\Technovert\\Banking Application\\BankingApplication\\accounts.json", serializedData);
            //data written into json.
        }
        public static Dictionary<double,string> readTransactions()
        {
            string data = File.ReadAllText("C:\\Users\\nagab\\OneDrive\\Desktop\\Technovert\\Banking Application\\BankingApplication\\transactions.json");
            return JsonConvert.DeserializeObject<Dictionary<double, string>>(data);
        }
        public static void writeTransactions(Dictionary<double,string> transactions)
        {
            string serializedData = JsonConvert.SerializeObject(transactions, Formatting.Indented);
            File.WriteAllText("C:\\Users\\nagab\\OneDrive\\Desktop\\Technovert\\Banking Application\\BankingApplication\\transactions.json", serializedData);
        }

        public void depositAmount()
        {
            //deposits money and updates account details.
            int amount;
            double accNumber;
            Console.WriteLine("Enter your Account Number: ");
            accNumber = Convert.ToDouble(Console.ReadLine());
            
            if (accounts.ContainsKey(accNumber))   //check for validity.
            {
                Console.WriteLine("Hello! {0}",accounts[accNumber]["name"]);
                Console.WriteLine("Enter amount to deposit: ");
                amount = Convert.ToInt32(Console.ReadLine());
                if (amount > 0)
                {
                    //update current object's available balance.
                    string details = DateTime.Now + " " + amount+"INR" + " Credited";
                    amount = Convert.ToInt32(accounts[accNumber]["balance"]) + amount;
                    accounts[accNumber]["balance"] = Convert.ToString(amount);
                    writeAccounts(accounts);
                    Console.WriteLine("Amount has been credited successfully!");
                    Console.WriteLine("Current balance ->{0}\n\n", accounts[accNumber]["balance"]);

                    //making the transaction
                    if (!transactions.ContainsKey(accNumber))
                    {
                        transactions.Add(accNumber, details);
                    }
                    else
                    {
                        transactions[accNumber] += "," + details;
                    }
                    writeTransactions(transactions);
                }
                else
                {
                    Console.WriteLine("Invalid amount to deposit.");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Account doesn't exist!");
            }
        }
        public void withdrawAmount()
        {
            //withdraws money and updates account details.
            int amount = 0;
            double accNumber;
            Console.WriteLine("Enter your Account Number: ");
            accNumber = Convert.ToDouble(Console.ReadLine());

            if (accounts.ContainsKey(accNumber))   //check for validity.
            {
                Console.WriteLine("Hello! {0}", accounts[accNumber]["name"]);
                Console.WriteLine("Enter amount to withdraw: ");
                amount = Convert.ToInt32(Console.ReadLine());
                if (amount > 0)
                {
                    if (amount <= Convert.ToInt32(accounts[accNumber]["balance"]))
                    {
                        //update current object's available balance.
                        string details = DateTime.Now + " " + amount + "INR" + " Dedited";
                        amount = Convert.ToInt32(accounts[accNumber]["balance"]) - amount;
                        accounts[accNumber]["balance"] = Convert.ToString(amount);
                        writeAccounts(accounts);
                        Console.WriteLine("Amount has been debited successfully!");
                        Console.WriteLine("Current balance ->{0}\n\n", accounts[accNumber]["balance"]);
                        //making the transaction
                        if (!transactions.ContainsKey(accNumber))
                        {
                            transactions.Add(accNumber, details);
                        }
                        else
                        {
                            transactions[accNumber] += "," + details;
                        }
                        writeTransactions(transactions);


                    }
                    else
                    {
                        Console.WriteLine("Insufficient account balance.\n");
                    }


                }
                else
                {
                    Console.WriteLine("Invalid amount to withdraw.\n");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Account doesn't exist.");
                return;
            }
        }
        public void transferAmount()
        {
            //transfers money from one accc to another
            double senderAcc;
            double receiverAcc;
            int amount;
            Console.WriteLine("Enter Sender's account number: ");
            senderAcc = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter Receiver's account number: ");
            receiverAcc = Convert.ToDouble(Console.ReadLine());
            if (accounts.ContainsKey(senderAcc))//check whether both of the accounts exist.
            {
                if (accounts.ContainsKey(receiverAcc))
                {
                    Console.WriteLine("Enter amount to Transfer: ");
                    amount = Convert.ToInt32(Console.ReadLine());
                    if (amount > 0)
                    {
                        if (amount <= Convert.ToInt32(accounts[senderAcc]["balance"]))
                        {
                            //generate transaction details for both sender and receiver.
                            string receiverDetails = DateTime.Now + " " + amount + "INR" + " Transfered from " + accounts[senderAcc]["name"];
                            string senderDetails = DateTime.Now + " " + amount + "INR" + " Transfered to " + accounts[receiverAcc]["name"];
                                                                                                            
                            int senderBalance = Convert.ToInt32(accounts[senderAcc]["balance"]) - amount;   //calculate sender and receiver balances
                            int receiverBalance = Convert.ToInt32(accounts[receiverAcc]["balance"]) + amount;

                            accounts[senderAcc]["balance"] = Convert.ToString(senderBalance);            //update sender and receiver balances.
                            accounts[receiverAcc]["balance"] = Convert.ToString(receiverBalance);

                            writeAccounts(accounts);

                            Console.WriteLine("Amount has been transferred to {0} successfully!", accounts[receiverAcc]["name"]);
                            Console.WriteLine("Current balance ->{0}\n\n", accounts[senderAcc]["balance"]);


                            //lgging the transaction for sender
                            if (!transactions.ContainsKey(senderAcc))
                            {
                                transactions.Add(senderAcc, senderDetails);
                            }
                            else
                            {
                                transactions[senderAcc] += "," + senderDetails;
                            }
                            //logging the transaction for receiver.
                            if (!transactions.ContainsKey(receiverAcc))
                            {
                                transactions.Add(receiverAcc, receiverDetails);
                            }
                            else
                            {
                                transactions[receiverAcc] += "," + receiverDetails;
                            }


                            writeTransactions(transactions);


                        }
                        else
                        {
                            Console.WriteLine("Insufficient account balance.\n");
                        }



                    }
                    else
                    {
                        Console.WriteLine("Invalid receipient account number.");
                    }

                }
                else
                {
                    Console.WriteLine("Invalid sender account number.");
                }

            }
        }
        public void printTransactionHistory()
        {
            //prints user's acount history
            Console.WriteLine("Enter your account number: ");
            double accNumber = Convert.ToDouble(Console.ReadLine());
            if(accounts.ContainsKey(accNumber))
            {
                if(transactions.ContainsKey(accNumber))
                {
                    string trans = transactions[accNumber];
                    string[] transList = trans.Split(",");
                    int count = 1;
                    foreach(string transaction in transList)
                    {
                        Console.WriteLine("{0}\t{1}",count,transaction);
                        count++;
                    }
                    Console.WriteLine("\n");
                }
                else
                {
                    Console.WriteLine("None transactions recorded so far!");
                }
            }
            else
            {
                Console.WriteLine("Account doesn't exist.");
                return;
            }

        }
    }
}
