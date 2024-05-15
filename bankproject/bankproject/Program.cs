
using bankproject;
using HarshaBank.Presentation;
using System;

class Program
{
    //Application execution starts here
    static void Main()
    {
        //display title
        Console.WriteLine("************** Accion Bank *****************");
        Console.WriteLine("::Login Page::");

        //declare variables to store username and password;
        string userName = null, password = null;


        //check username and password
        while (true)
        {
            //read userName from keyboard
            Console.Write("Username (Press ENTER to exit): ");
            userName = Console.ReadLine();

            //read password from keyboard only if username is entered
            if (userName != "")
            {
                Console.Write("Password: ");
                password = Console.ReadLine();
            }
            else
            {
                break;
            }

            //declare variable to store menu choice
            int mainMenuChoice = -1;

            if (userName == "Varshith" && password == "bank")
            {


                do
                {
                    //show main menu
                    Console.WriteLine("\n:::Main menu:::");
                    Console.WriteLine("1. Customers");
                    Console.WriteLine("2. Accounts");
                    Console.WriteLine("3. Funds Transfer");
                    Console.WriteLine("4. Account Statement");
                    Console.WriteLine("0. Exit");

                    //accept menu choice from keyboard
                    Console.Write("Enter choice: ");
                    while (!int.TryParse(Console.ReadLine(), out mainMenuChoice))
                    {
                        Console.Write("Enter choice: ");
                    }

                    //switch-case to check menu choice
                    switch (mainMenuChoice)
                    {
                        case 1: CustomersMenu(); break;
                        case 2: AccountsMenu(); break;
                        case 3: TransferPresentation.AddTransaction(); break;
                        case 4: TransferPresentation.ViewTransactions(); break;
                        case 0: break;
                    }
                } while (mainMenuChoice != 0);
            }
            else
            {
                Console.WriteLine("Invalid username or password.\n");
            }

            if (mainMenuChoice == 0)
                break;
        }

        //about to exit
        Console.WriteLine("Thank you! Visit again.");
        Console.ReadKey();
    }

    static void CustomersMenu()
    {
        //variable to store customers menu choice
        int customerMenuChoice = -1;

        //do-while loop starts
        do
        {
            //print customers menu
            Console.WriteLine("\n:::Customers menu:::");
            Console.WriteLine("1. Add Customer");
            Console.WriteLine("2. Delete Customer");
            Console.WriteLine("3. Update Customer");
            Console.WriteLine("4. Search Customers");
            Console.WriteLine("5. View Customers");
            Console.WriteLine("0. Back to Main Menu");

            //accept customers menu choice
            Console.Write("Enter choice: ");
            customerMenuChoice = Convert.ToInt32(Console.ReadLine());

            //switch case
            switch (customerMenuChoice)
            {
                case 1: CustomersPresentation.AddCustomer(); break;
                case 2: CustomersPresentation.DeleteCustomer(); break;
                case 3: CustomersPresentation.UpdateCustomer(); break;
                case 4: CustomersPresentation.SearchCustomer(); break;
                case 5: CustomersPresentation.ViewCustomers(); break;
            }
        } while (customerMenuChoice != 0);
    }


    static void AccountsMenu()
    {
        //variable to store accounts menu choice
        int accountsMenuChoice;

        //do-while loop starts
        do
        {
            //print  accounts menu
            Console.WriteLine("\n:::Accounts menu:::");
            Console.WriteLine("1. Add Account");
            Console.WriteLine("2. Delete Account");
            Console.WriteLine("3. Update Account");
            Console.WriteLine("4. Search Accounts");
            Console.WriteLine("5. View Accounts");
            Console.WriteLine("0. Back to Main Menu");

            //accept accounts menu choice
            Console.Write("Enter choice: ");
            while (!(int.TryParse(Console.ReadLine(), out accountsMenuChoice)))
            {
                Console.Write("Enter choice: ");
            }


            //switch case
            switch (accountsMenuChoice)
            {
                case 1: AccPresentation.AddAccount(); break;
                case 2: AccPresentation.DeleteAccount(); break;
                case 3: AccPresentation.UpdateAccount(); break;
                case 4: AccPresentation.SearchAccount(); break;
                case 5: AccPresentation.ViewAccounts(); break;
            }
        } while (accountsMenuChoice != 0);
    }
}


