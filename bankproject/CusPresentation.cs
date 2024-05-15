using System;
using bankentities;
using Bank.BusinessLayer.BALContracts;
using Bank.BusinessLayer;
using bank.dataaccess;

namespace bank.presentation
{
    public static class CusPresentation
    {
        internal static void AddCustomer()
        {
            try
            {
                Customer customer = new Customer();
                Console.WriteLine("\n********ADD CUSTOMER*********");
                Console.Write("Customer Name: "); // Added a colon and space for readability
                customer.CustomerName = Console.ReadLine();
                Console.Write("Address: "); // Added a colon and space for readability
                customer.Address = Console.ReadLine();
                Console.Write("Country: "); // Added a colon and space for readability
                customer.Country = Console.ReadLine();
                Console.Write("Mobile: "); // Added a colon and space for readability
                customer.Mobile = Console.ReadLine();

                // Create an instance of CusDataAccess or any other class that implements ICusDataAccess
                ICusDataAccess cusDataAccess = new CusDataAccess();

                // Pass the cusDataAccess instance to the constructor of CusBAL
                ICusBAL cusBAL = new CusBAL(cusDataAccess);

                Guid newGuid = cusBAL.AddCustomer(customer);
                Console.WriteLine($"Customer ID: {newGuid}"); // Displaying customer ID
                Console.WriteLine("Customer Added.\n");


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.GetType());
            }
        }
    }
}
