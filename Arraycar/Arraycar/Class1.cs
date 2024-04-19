using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace class1
{
    public class Car
    {
            private string make;
            private string model;
            private decimal rentalPrice;
            private bool rented;

            public string Make
            {
                get { return make; }
            }

            public string Model
            {
                get { return model; }
            }

            public decimal RentalPrice
            {
                get { return rentalPrice; }
            }

            public bool IsRented
            {
                get { return rented; }
            }

            public Car(string make, string model, decimal rentalPrice)
            {
                this.make = make;
                this.model = model;
                this.rentalPrice = rentalPrice;
                this.rented = false; // By default, car is not rented
            }

            public bool Rent()
            {
                if (!rented)
                {
                    rented = true;
                    return true;
                }
                else
                {
                    Console.WriteLine("Sorry, the selected car is not available for rent.");
                    return false;
                }
            }

            public bool Return()
            {
                if (rented)
                {
                    rented = false;
                    return true;
                }
                else
                {
                    Console.WriteLine("Error: Car is not currently rented.");
                    return false;
                }
            }

            public void PrintInfo()
            {
                Console.WriteLine($"{make} {model} (Rental price: {rentalPrice:C}/day) - {(rented ? "Rented" : "Available")}");
            }
        }
    }
