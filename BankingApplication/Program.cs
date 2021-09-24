using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;

namespace BankingApplication
{
    class Person
    {
        public string name ;
        public int age;
        public double contactNumber;
        public DateTime dob;
        public string address;
        public double aadharNumber;
        public double panNumber;
        public Person()
        {
            string name = "";
            int age = 0;
            string gender = "";
            double contactNumber = 0;
            DateTime dob;
            string address = "";
            double aadharNumber = 0;
            double panNumber = 0;
        }
    }
    class Account : Person
    {

        public double accountNumber;
        public string accountType;
        public double balance;
        public const int MIN_BALANCE = 250;
        public Account()
        {
            double accountNumber = 0;
            string accountType = "";
            double balance = 0;

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
<<<<<<< HEAD
            this.balance = this.MIN_BALANCE;
            this.accountNumber = Random.Next(032510100091540, 032511111111111); //need to be checked.
            Console.WriteLine(this.accountNumber);


            using (StreamReader r = new StreamReader("data.json"))
            {
                string json = r.ReadToEnd();
                List<Item> items = JsonConvert.DeserializeObject<List<Item>>(json);b
            }
=======
            this.balance = Account.MIN_BALANCE;


            string jsonFilePath = "C:\\Users\\nagab\\OneDrive\\Desktop\\Technovert\\Banking Application\\BankingApplication\\data.json";

            string json = File.ReadAllText(jsonFilePath);
            Dictionary<string, object> json_Dictionary = (new JavaScriptSerializer()).Deserialize<Dictionary<string, object>>(json);

            foreach (var item in json_Dictionary)
            {
                // parse here
            }

>>>>>>> 5b90ec49d3091ee0966057941de5815961926d46
        }
        public void depositAmount()
        {
            //deposits money and updates account details.
            int amount;
            double accNumber;
            Console.WriteLine("Enter your Account Number: ")
            accNumber = Convert.ToInt64(Console.WriteLine());
            if(accNumber == 1)   //check for validity.
            {
                Console.WriteLine("Enter amount to deposit: ");
                amount = Convert.ToInt32(Console.WriteLine);
                if(amount>0)
                {
                      //update current object's available balance.
                }
                else
                {
                    Console.WriteLine("Invalid amount to deposit.");
                    return;
                }
            }
        }
        public void withdrawAmount()
        {
            //withdraws money and updates account details.
            int amount = 0;
            Console.WriteLine("Enter amount to withdraw: ");
            amount = Convert.ToInt32(Console.ReadLine());
            if(amount>0)
            {
                //do
            }
            else if (amount > Account.TRANS_LIMIT)
            {
                Console.WriteLine("Couldn't perform transaction. Transaction limit reached.\n")
            }
            else if(amount>Account.DAY_LIMIT)
            {
                Console.WriteLine("Couldn't perform transaction. Daylimit reached!\n");
                return
            }
            
        }
        public void transferAmount()
        {
            //transfers money from one accc to another
            double senderAcc;
            double receiverAcc;
            int amount;
            Console.WriteLine("Enter Sender's account number: ");
            senderAcc = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine("Enter Receiver's account number: ");
            receiverAcc = Convert.ToInt64(Console.ReadLine());
            if(senderAcc ==1 && receiverAcc ==1)//check whether both of the accounts exist.
            {
                Console.WriteLine("Enter amount to Transfer: ");

            }

        }
        public void printTransactionHistory()
        {
            //prints user's acount history
        }
    }
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("\nWelcome to ABC Bank!\n");

            while(true)
            {
                Console.WriteLine("Choose any one option:\n1.Create Account\n2.Deposit\n3.Withdraw\n4.Transfer Amount\n5.Print Transaction history\n");
                int choice = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine(choice);
                Account acc = new Account();
                switch (choice)
                {
                   
                    case 1: Console.WriteLine("Account to be Created!\n");
                            acc.createAccount();
                            break;
                    case 2: Console.WriteLine("Amount deposited\n");
                            break;
                    case 3: Console.WriteLine("Amount Withdrawl\n");
                            break;
                    case 4: Console.WriteLine("Amount transfered\n");
                            break;
                    case 5: Console.WriteLine("Transaction History\n");
                            break;
                            
                    default:
                        break;
                }


            }
        }
    }
}
