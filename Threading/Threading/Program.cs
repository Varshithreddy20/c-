using System;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadingBasicsAssignment
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Task 1: Printing numbers from 1 to 10 with one-second delay
            Thread numberThread = new Thread(PrintNumbers);
            numberThread.Name = "Number Thread";
            numberThread.Start();

            // Task 2: Printing the alphabet from 'A' to 'Z' with half-second delay
            Thread alphabetThread = new Thread(PrintAlphabet);
            alphabetThread.Name = "Alphabet Thread";
            alphabetThread.Start();

            // Task 3: Generating and printing random numbers for 5 seconds
            Thread randomThread = new Thread(PrintRandomNumbers);
            randomThread.Name = "Random Number Thread";
            randomThread.Start();

            Console.WriteLine("Main thread is running.");

            // Wait for all threads to complete their tasks
            numberThread.Join();
            alphabetThread.Join();
            randomThread.Join();

            Console.WriteLine("All tasks completed. Exiting gracefully.");
        }

        // Task 1: Print numbers from 1 to 10 with one-second delay
        static void PrintNumbers()
        {
            Console.WriteLine("Task 1: Number thread is running.");
            for (int i = 1; i <= 10; i++)
            {
                Console.WriteLine($"Number: {i}");
                Thread.Sleep(1000); // Delay for one second
            }
        }

        // Task 2: Print the alphabet from 'A' to 'Z' with half-second delay
        static void PrintAlphabet()
        {
            Console.WriteLine("Task 2: Alphabet thread is running.");
            for (char c = 'A'; c <= 'Z'; c++)
            {
                Console.WriteLine($"Alphabet: {c}");
                Thread.Sleep(500); // Delay for half a second
            }
        }

        // Task 3: Generate and print random numbers for 5 seconds
        static void PrintRandomNumbers()
        {
            Console.WriteLine("Task 3: Random number thread is running.");
            Random rand = new Random();
            DateTime startTime = DateTime.Now;
            while (DateTime.Now - startTime < TimeSpan.FromSeconds(5))
            {
                int randomNumber = rand.Next(1, 101); // Generate random number between 1 and 100
                Console.WriteLine($"Random Number: {randomNumber}");
                Thread.Sleep(250); // Delay for quarter of a second
            }
        }
    }
}
