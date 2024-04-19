using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using class1;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Arraycar
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the car rental system!\n");

            List<Car> cars = new List<Car>()
        {
            new Car("Toyota", "Corolla", 50),
            new Car("Honda", "Civic", 60),
            new Car("Ford", "Mustang", 80)
        };

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Rent a car");
                Console.WriteLine("2. Return a car");
                Console.WriteLine("3. View available cars");
                Console.WriteLine("4. Exit");
                Console.Write("\nPlease enter your choice: ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            RentCar(cars);
                            break;
                        case 2:
                            ReturnCar(cars);
                            break;
                        case 3:
                            ViewAvailableCars(cars);
                            break;
                        case 4:
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid option. Please choose again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }

            Console.WriteLine("Thank you for using the car rental system!");
        }

        static void RentCar(List<Car> cars)
        {
            Console.WriteLine("\nRent a Car:");
            for (int i = 0; i < cars.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {cars[i].Make} {cars[i].Model} (Rental price: {cars[i].RentalPrice:C}/day) - {(cars[i].IsRented ? "Rented" : "Available")}");
            }

            Console.Write("Please enter the ID of the car you want to rent: ");
            if (int.TryParse(Console.ReadLine(), out int id) && id > 0 && id <= cars.Count)
            {
                if (cars[id - 1].Rent())
                {
                    Console.WriteLine($"Rented the {cars[id - 1].Make} {cars[id - 1].Model} for {cars[id - 1].RentalPrice:C}/day.");
                }
            }
            else
            {
                Console.WriteLine("Invalid car ID.");
            }
        }

        static void ReturnCar(List<Car> cars)
        {
            Console.WriteLine("\nReturn a Car:");
            for (int i = 0; i < cars.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {cars[i].Make} {cars[i].Model} (Rental price: {cars[i].RentalPrice:C}/day) - {(cars[i].IsRented ? "Rented" : "Available")}");
            }

            Console.Write("Please enter the ID of the car you want to return: ");
            if (int.TryParse(Console.ReadLine(), out int id) && id > 0 && id <= cars.Count)
            {
                if (cars[id - 1].Return())
                {
                    Console.WriteLine($"Returned the {cars[id - 1].Make} {cars[id - 1].Model}.");
                }
            }
            else
            {
                Console.WriteLine("Invalid car ID.");
            }
        }

        static void ViewAvailableCars(List<Car> cars)
        {
            Console.WriteLine("\nAvailable cars:");
            foreach (Car car in cars)
            {
                if (!car.IsRented)
                {
                    car.PrintInfo();
                }
            }
        }
    }
}