using System;
using bank.presentation;

namespace bankproject
{
    class Program
    {
        private static int mainMenuChoice;

        static void Main(string[] args)
        {
            Console.WriteLine("****** ACCION BANK ******");
            Console.WriteLine(" ### LOGIN PAGE ### ");

            string username = null, password = null;

            Console.Write(" Username : ");
            username = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(username))
            {
                Console.Write(" Password : ");
                password = Console.ReadLine();
            }

            if (username == "Varshith" && password == "bank")
            {
                Console.WriteLine(" Thank you ");
                do
                {
                    Console.WriteLine("\n:::MAIN MENU:::");
                    Console.WriteLine(" 1.Accounts");
                    Console.WriteLine(" 2.Funds");
                    Console.WriteLine(" 3.Savings");
                    Console.WriteLine(" 4.Current");
                    Console.WriteLine(" 5.Customer");
                    Console.WriteLine(" 0.Exit");

                    Console.Write("Enter the Choice : ");
                    mainMenuChoice = int.Parse(Console.ReadLine());

                    switch (mainMenuChoice)
                    {
                        case 1:
                            AccountsMenu();
                            break;
                        case 2:
                           FundsMenu();
                            break;
                        case 3:
                            SavingsMenu();
                            break;
                        case 4:
                            CurrentMenu(); 
                            break;
                        case 5:
                            CustomerMenu();
                            break;
                        case 0:
                            Console.WriteLine("<-<-<-<- KINDLY CLOSE THE BROWSER ->->->->");
                            break;
                        default:
                            Console.WriteLine("Invalid choice, please try again.");
                            break;
                    }
                } while (mainMenuChoice != 0);
                Console.WriteLine(" Thank you and Visit again ");
            }
            else
            {
                Console.WriteLine(" Username or Password is incorrect ");
            }
            Console.ReadKey();
        }
        static void AccountsMenu()
        {
            int AccountsMenuChoice = -1;
            do
            {
                Console.WriteLine("\n <<<<<<<<<<< ACCOUNTS MENU >>>>>>>>>>>");
                Console.WriteLine("1.Add account");
                Console.WriteLine("2.Edit account");
                Console.WriteLine("3.Update account");
                Console.WriteLine("4.Delete account");
                Console.WriteLine("5.View account");
                Console.WriteLine("0.Go back to main menu ");

                Console.Write("Enter the choice: ");
                AccountsMenuChoice = Convert.ToInt32(Console.ReadLine());

                switch (AccountsMenuChoice)
                {
                    case 1:
                        // Implement Add account functionality
                        break;
                    case 2:
                        // Implement Edit account functionality
                        break;
                    case 3:
                        // Implement Update account functionality
                        break;
                    case 4:
                        // Implement Delete account functionality
                        break;
                    case 5:
                        // Implement View account functionality
                        break;
                    case 0:
                        // Simply exits the loop and returns to main menu
                        break;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            } while (AccountsMenuChoice != 0);
        }
        static void FundsMenu()
        {
            int FundsMenuChoice = -1;
            do
            {
                Console.WriteLine("\n <<<<<<<<<<< FUNDS MENU Menu >>>>>>>>>>>");
                Console.WriteLine("1.Add Fund");
                Console.WriteLine("2.Update Nominee");
                Console.WriteLine("3.Add Nominee");
                Console.WriteLine("0.Go back to main menu ");

                Console.Write("Enter the choice: ");
                FundsMenuChoice = Convert.ToInt32(Console.ReadLine());

                switch (FundsMenuChoice)
                {
                    case 1:
                        // Implement Add account functionality
                        break;
                    case 2:
                        // Implement Edit account functionality
                        break;
                    case 3:
                        // Implement Update account functionality
                        break;
                    case 0:
                        // Simply exits the loop and returns to main menu
                        break;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            } while (FundsMenuChoice != 0);

        }
        static void SavingsMenu()
        {
            int SavingsMenuChoice = -1;
            do
            {
                Console.WriteLine("\n <<<<<<<<<<< SAVINGS MENU >>>>>>>>>>>");
                Console.WriteLine("1.Open Account");
                Console.WriteLine("2.Edit Account");
                Console.WriteLine("3.Update account");
                Console.WriteLine("4.Delete account");
                Console.WriteLine("0.Go back to main menu ");

                Console.Write("Enter the choice: ");
                SavingsMenuChoice = Convert.ToInt32(Console.ReadLine());

                switch (SavingsMenuChoice)
                {
                    case 1:
                        // Implement Add account functionality
                        break;
                    case 2:
                        // Implement Edit account functionality
                        break;
                    case 3:
                        // Implement Update account functionality
                        break;
                    case 4:
                        // Implement Delete account functionality
                        break;
                    case 0:
                        // Simply exits the loop and returns to main menu
                        break;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            } while (SavingsMenuChoice != 0);
        }
        static void CurrentMenu()
        {
            int CurrentMenuChoice = -1;
            do
            {
                Console.WriteLine("\n <<<<<<<<<<< CURRENT MENU >>>>>>>>>>>");
                Console.WriteLine("1.Open Account");
                Console.WriteLine("2.Edit Account");
                Console.WriteLine("3.Update account");
                Console.WriteLine("4.Delete account");
                Console.WriteLine("0.Go back to main menu ");

                Console.Write("Enter the choice: ");
                CurrentMenuChoice = Convert.ToInt32(Console.ReadLine());

                switch (CurrentMenuChoice)
                {
                    case 1:
                        // Implement Add account functionality
                        break;
                    case 2:
                        // Implement Edit account functionality
                        break;
                    case 3:
                        // Implement Update account functionality
                        break;
                    case 4:
                        // Implement Delete account functionality
                        break;
                    case 5:
                        // Implement View account functionality
                        break;
                    case 0:
                        // Simply exits the loop and returns to main menu
                        break;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            } while (CurrentMenuChoice != 0);
        }
        static void CustomerMenu()
        {
            int CustomerMenuChoice = -1;
            do
            {
                Console.WriteLine("\n <<<<<<<<<<< CUSTOMER MENU >>>>>>>>>>>");
                Console.WriteLine("1.Add Customer");
                Console.WriteLine("2.Edit Customer");
                Console.WriteLine("3.Update Customer");
                Console.WriteLine("4.Serach Customer");
                Console.WriteLine("5.Delete Customer");
                Console.WriteLine("6.View Customer");
                Console.WriteLine("0.Go back to main menu  ");

                Console.Write("Enter the choice: ");
                CustomerMenuChoice = Convert.ToInt32(Console.ReadLine());

                switch (CustomerMenuChoice)
                {
                    case 1:CusPresentation.AddCustomer();
                        // Implement Add account functionality
                        break;
                    
                }
            } while (CustomerMenuChoice != 0);
        }
    }
}
