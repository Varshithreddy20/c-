using Bank.BusinessLayer;
using Bank.BusinessLayer.BALContracts;
using bankentities;
using System;
using System.Collections.Generic;
using System.Linq;


namespace HarshaBank.Presentation
{
    static class CustomersPresentation
    {
        internal static void AddCustomer()
        {
            try
            {
                //create an object of Customer
                Customer customer = new Customer();

                //read all details from the user
                Console.WriteLine("\n********ADD CUSTOMER*************");
                Console.Write("Customer Name: ");
                customer.CustomerName = Console.ReadLine();
                Console.Write("Address: ");
                customer.Address = Console.ReadLine();
                Console.Write("Country: ");
                customer.Country = Console.ReadLine();
                Console.Write("Mobile: ");
                customer.Mobile = Console.ReadLine();

                //Create BL object
                ICusBAL customersBusinessLogicLayer = new CusBAL();
                Guid newGuid = customersBusinessLogicLayer.AddCustomer(customer);

                List<Customer> matchingCustomers = customersBusinessLogicLayer.GetCustomersByCondition(item => item.CustomerID == newGuid);
                if (matchingCustomers.Count >= 1)
                {
                    Console.WriteLine("New Customer Code: " + matchingCustomers[0].CustomerCode);
                    Console.WriteLine("Customer Added.\n");
                }
                else
                {
                    Console.WriteLine("Customer Not added");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.GetType());
            }
        }


        internal static void ViewCustomers()
        {
            try
            {
                //Create BL object
                ICusBAL customersBusinessLogicLayer = new CusBAL();

                List<Customer> allCustomers = customersBusinessLogicLayer.GetCustomers();
                if (allCustomers.Count == 0)
                {
                    Console.WriteLine("No customers\n");
                    return;
                }

                //read all customers
                Console.WriteLine("\n**********ALL CUSTOMERS*************");
                foreach (var customer in allCustomers)
                {
                    Console.WriteLine("Customer Code: " + customer.CustomerCode);
                    Console.WriteLine("Customer Name: " + customer.CustomerName);
                    Console.WriteLine("Address: " + customer.Address);
                    Console.WriteLine("Country: " + customer.Country);
                    Console.WriteLine("Mobile: " + customer.Mobile);
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.GetType());
            }
        }


        internal static void UpdateCustomer()
        {
            try
            {
                //Create BL object
                ICusBAL customersBusinessLogicLayer = new CusBAL();

                if (customersBusinessLogicLayer.GetCustomers().Count <= 0)
                {
                    Console.WriteLine("No customers exist");
                    return;
                }

                //display existing customers
                Console.WriteLine("\n********EDIT CUSTOMER*************");
                ViewCustomers();

                //read all details from the user

                Console.Write("Enter the Customer Code that you want to edit: ");
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


                Console.WriteLine("NEW CUSTOMER DETAILS:");
                Console.Write("Customer Name: ");
                existingCustomer.CustomerName = Console.ReadLine();
                Console.Write("Address: ");
                existingCustomer.Address = Console.ReadLine();
                Console.Write("Country: ");
                existingCustomer.Country = Console.ReadLine();
                Console.Write("Mobile: ");
                existingCustomer.Mobile = Console.ReadLine();


                bool isUpdated = customersBusinessLogicLayer.UpdateCustomer(existingCustomer);

                if (isUpdated)
                {
                    Console.WriteLine("Customer Updated.\n");
                }
                else
                {
                    Console.WriteLine("Customer not updated");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.GetType());
            }
        }


        internal static void SearchCustomer()
        {
            try
            {
                //Create BL object
                ICusBAL customersBusinessLogicLayer = new CusBAL();

                if (customersBusinessLogicLayer.GetCustomers().Count <= 0)
                {
                    Console.WriteLine("No customers exist");
                    return;
                }

                //display existing customers
                Console.WriteLine("\n********SEARCH CUSTOMER*************");
                ViewCustomers();

                //read all details from the user

                Console.Write("Enter the Customer Code that you want to get: ");
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

                Console.WriteLine("Customer Code: " + existingCustomer.CustomerCode);
                Console.WriteLine("Customer Name: " + existingCustomer.CustomerName);
                Console.WriteLine("Address: " + existingCustomer.Address);
                Console.WriteLine("Country: " + existingCustomer.Country);
                Console.WriteLine("Mobile: " + existingCustomer.Mobile);
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.GetType());
            }
        }


        internal static void DeleteCustomer()
        {
            try
            {
                //Create BL object
                ICusBAL customersBusinessLogicLayer = new CusBAL();

                if (customersBusinessLogicLayer.GetCustomers().Count <= 0)
                {
                    Console.WriteLine("No customers exist");
                    return;
                }

                //display existing customers
                Console.WriteLine("\n********DELETE CUSTOMER*************");
                ViewCustomers();

                //read all details from the user

                Console.Write("Enter the Customer Code that you want to delete: ");
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


                bool isDeleted = customersBusinessLogicLayer.DeleteCustomer(existingCustomer.CustomerID);

                if (isDeleted)
                {
                    Console.WriteLine("Customer Deleted.\n");
                }
                else
                {
                    Console.WriteLine("Customer not deleted");
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
