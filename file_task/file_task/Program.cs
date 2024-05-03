using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace file_task
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //creating directory 
            string countriesFolderPath = @"C:\Users\al6113\Desktop\sample\FILE\COUNTRIES";
            Directory.CreateDirectory(countriesFolderPath);
            Console.WriteLine("countries folder created");

            //creating sub directories in the directory 
            string IndiaPath = countriesFolderPath + @"\India";
            string UKPath = countriesFolderPath + @"\UK";
            string USAPath = countriesFolderPath + @"\USA";
            Directory.CreateDirectory(IndiaPath);
            Directory.CreateDirectory(UKPath);
            Directory.CreateDirectory(USAPath);
            Console.WriteLine("Sub Directories India, UK and USA are created");

            //creating text files 
            string captialsFilePath = countriesFolderPath + @"\capitals.txt";
            string populationFilePath = countriesFolderPath + @"\population.txt";
            string statesFilePath = countriesFolderPath + @"\states.txt";
            File.Create(captialsFilePath).Close();
            File.Create(populationFilePath).Close();
            File.Create(statesFilePath).Close();
            Console.WriteLine("Created 3 text files");

            //moveing the folder
            string renamedcountriesFolderPath=  @"C:\Users\al6113\Desktop\sample\FILE\renamedCountries";
            Directory.Move(countriesFolderPath, renamedcountriesFolderPath);
            Console.WriteLine("countries folder moved to renamedcountries");

            //getfiles
            string[]files = Directory.GetFiles(renamedcountriesFolderPath);
            Console.WriteLine("\nfiles");
            foreach (string file in files)
            {
                Console.WriteLine(file);
            }

            //delete 
            Directory.Delete(renamedcountriesFolderPath,true);
            Console.WriteLine("folder is deleted");

        }
    }
}
