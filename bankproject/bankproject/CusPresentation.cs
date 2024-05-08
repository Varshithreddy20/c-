using Bank.BusinessLayer.BALContracts;
using bankentities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Bank.BusinessLayer;

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
                Console.Write("Customer Name");
                customer.CustomerName = Console.ReadLine();
                Console.Write("Address");
                customer.Address = Console.ReadLine();
                Console.Write("Country");
                customer.Country = Console.ReadLine();
                Console.Write("Mobile");
                customer.Mobile = Console.ReadLine();

                ICusBAL cusBAL = new CusBAL();
                Guid newGuid = cusBAL.AddCustomer(customer);
                Console.WriteLine(newGuid);
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
