using Bank.BusinessLayer;
using Bank.BusinessLayer.BALContracts;
using bankentities;
using HarshaBank.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace bankproject
{
    static class AccPresentation
    
        {
            internal static void AddAccount()
            {
                try
                {
                    //create an object of Account
                    Account account = new Account();


                    //Create BL object
                    ICusBAL customersBusinessLogicLayer = new CusBAL();
                    IAccBAL accountsBusinessLogicLayer = new AccBAL();

                    if (customersBusinessLogicLayer.GetCustomers().Count <= 0)
                    {
                        Console.WriteLine("No customers exist");
                        return;
                    }

                    //display existing customers
                    Console.WriteLine("\n********ADD ACCOUNT*************");
                CustomersPresentation.ViewCustomers();

                    //read all details from the user
                    Console.Write("Enter the Customer Code for which you want to create a new account: ");
                    long customerCodeToEdit;
                    while (!long.TryParse(Console.ReadLine(), out customerCodeToEdit))
                    {
                        Console.Write("Enter the Customer Code for which you want to create a new account: ");
                    }

                    var existingCustomer = customersBusinessLogicLayer.GetCustomersByCondition(temp => temp.CustomerCode == customerCodeToEdit).FirstOrDefault();
                    if (existingCustomer == null)
                    {
                        Console.WriteLine("Invalid Customer Code.\n");
                        return;
                    }
                    account.CustomerID = existingCustomer.CustomerID;


                    //Invoke BL method
                    Guid newGuid = accountsBusinessLogicLayer.AddAccount(account);

                    //Display newly generated account number
                    List<Account> matchingAccounts = accountsBusinessLogicLayer.GetAccountsByCondition(item => item.AccountID == newGuid);
                    if (matchingAccounts.Count >= 1)
                    {
                        Console.WriteLine("New Account Number: " + matchingAccounts[0].AccountNumber);
                        Console.WriteLine("Account Added.\n");
                    }
                    else
                    {
                        Console.WriteLine("Account Not added");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.GetType());
                }
            }


            internal static void ViewAccounts()
            {
                try
                {
                    //Create BL object
                    ICusBAL customersBusinessLogicLayer = new CusBAL();
                    IAccBAL accountsBusinessLogicLayer = new AccBAL();

                    List<Account> allAccounts = accountsBusinessLogicLayer.GetAccounts();
                    if (allAccounts.Count == 0)
                    {
                        Console.WriteLine("No accounts\n");
                        return;
                    }

                    Console.WriteLine("\n**********ALL ACCOUNTS*************");
                    //read all accounts
                    foreach (var account in allAccounts)
                    {
                        Console.WriteLine("Account Number: " + account.AccountNumber);

                        //Get customer details based on CustomerID of account
                        Customer customer = customersBusinessLogicLayer.GetCustomersByCondition(temp => temp.CustomerID == account.CustomerID).FirstOrDefault();
                        if (customer != null)
                        {
                            Console.WriteLine("Customer Code: " + customer.CustomerCode);
                            Console.WriteLine("Customer Name: " + customer.CustomerName);
                        }

                        Console.WriteLine("Balance: " + account.Balance);
                        Console.WriteLine();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.GetType());
                }
            }


            internal static void UpdateAccount()
            {
                try
                {
                    //Create BL object
                    ICusBAL customersBusinessLogicLayer = new CusBAL();
                    IAccBAL accountsBusinessLogicLayer = new AccBAL();

                    if (accountsBusinessLogicLayer.GetAccounts().Count <= 0)
                    {
                        Console.WriteLine("No accounts exist");
                        return;
                    }

                    //display existing accounts
                    Console.WriteLine("\n********EDIT ACCOUNT*************");
                    ViewAccounts();

                    //read all details from the user

                    Console.Write("Enter the Account Number that you want to edit: ");
                    long accountCodeToEdit;
                    while (!long.TryParse(Console.ReadLine(), out accountCodeToEdit))
                    {
                    }
                    var existingAccount = accountsBusinessLogicLayer.GetAccountsByCondition(temp => temp.AccountNumber == accountCodeToEdit).FirstOrDefault();
                    if (existingAccount == null)
                    {
                        Console.WriteLine("Invalid Account Number.\n");
                        return;
                    }


                    Console.WriteLine();
                    CustomersPresentation.ViewCustomers();

                    //read all details from the user
                    Console.Write("Enter the Updated (existing) Customer Code: ");
                    long customerCodeToEdit;
                    while (!long.TryParse(Console.ReadLine(), out customerCodeToEdit))
                    {
                    }
                    var existingCustomer = customersBusinessLogicLayer.GetCustomersByCondition(temp => temp.CustomerCode == customerCodeToEdit).FirstOrDefault();
                    if (existingCustomer == null)
                    {
                        Console.WriteLine("Invalid Customer Code.\n");
                        return;
                    }
                    existingAccount.CustomerID = existingCustomer.CustomerID;


                    Console.Write("Balance: ");
                    existingAccount.Balance = long.Parse(Console.ReadLine());


                    bool isUpdated = accountsBusinessLogicLayer.UpdateAccount(existingAccount);

                    if (isUpdated)
                    {
                        Console.WriteLine("Account Updated.\n");
                    }
                    else
                    {
                        Console.WriteLine("Account not updated");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.GetType());
                }
            }


            internal static void SearchAccount()
            {
                try
                {
                //Create BL object
                ICusBAL customersBusinessLogicLayer = new CusBAL();
                IAccBAL accountsBusinessLogicLayer = new AccBAL();

                if (accountsBusinessLogicLayer.GetAccounts().Count <= 0)
                    {
                        Console.WriteLine("No accounts exist");
                        return;
                    }

                    //display existing accounts
                    Console.WriteLine("\n********SEARCH ACCOUNT*************");
                    ViewAccounts();

                    //read all details from the user

                    Console.Write("Enter the Account Number that you want to get: ");
                    long accountCodeToEdit;
                    while (!long.TryParse(Console.ReadLine(), out accountCodeToEdit))
                    {
                    }
                    var existingAccount = accountsBusinessLogicLayer.GetAccountsByCondition(temp => temp.AccountNumber == accountCodeToEdit).FirstOrDefault();
                    if (existingAccount == null)
                    {
                        Console.WriteLine("Invalid Account Number.\n");
                        return;
                    }
                    Console.WriteLine("Account Number: " + existingAccount.AccountNumber);

                    //Get customer details based on CustomerID of account
                    Customer customer = customersBusinessLogicLayer.GetCustomersByCondition(temp => temp.CustomerID == existingAccount.CustomerID).FirstOrDefault();
                    if (customer != null)
                    {
                        Console.WriteLine("Customer Code: " + customer.CustomerCode);
                        Console.WriteLine("Customer Name: " + customer.CustomerName);
                    }

                    Console.WriteLine("Balance: " + existingAccount.Balance);
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.GetType());
                }
            }


            internal static void DeleteAccount()
            {
                try
                {
                //Create BL object;
                IAccBAL accountsBusinessLogicLayer = new AccBAL();

                if (accountsBusinessLogicLayer.GetAccounts().Count <= 0)
                    {
                        Console.WriteLine("No accounts exist");
                        return;
                    }

                    //display existing accounts
                    Console.WriteLine("\n********DELETE ACCOUNT*************");
                    ViewAccounts();

                    //read all details from the user

                    Console.Write("Enter the Account Number that you want to delete: ");
                    long accountNumberToEdit;
                    while (!long.TryParse(Console.ReadLine(), out accountNumberToEdit))
                    {
                    }
                    var existingAccount = accountsBusinessLogicLayer.GetAccountsByCondition(temp => temp.AccountNumber == accountNumberToEdit).FirstOrDefault();
                    if (existingAccount == null)
                    {
                        Console.WriteLine("Invalid Account Number.\n");
                        return;
                    }


                    bool isDeleted = accountsBusinessLogicLayer.DeleteAccount(existingAccount.AccountID);

                    if (isDeleted)
                    {
                        Console.WriteLine("Account Deleted.\n");
                    }
                    else
                    {
                        Console.WriteLine("Account not deleted");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.GetType());
                }
            }
        }
    }

