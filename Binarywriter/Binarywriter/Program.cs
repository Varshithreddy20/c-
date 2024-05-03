using System;
using System.IO;

namespace Binarywriter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //binarywrite
            short countryid = 1;
            string countryName = "india";
            long population = 636464738;
            string region = "asia";
            string filepath = @"C:\Users\al6113\Desktop\sample\Binarywriter\india.txt";

            using (FileStream filestream = new FileStream(filepath, FileMode.Create, FileAccess.Write))
            using (BinaryWriter bw = new BinaryWriter(filestream))
            {
                bw.Write(countryid);
                bw.Write(countryName);
                bw.Write(population);
                bw.Write(region);
            }

            Console.WriteLine("india binary file is created");

            //binaryread
            using (FileStream filestream2 = new FileStream(filepath, FileMode.Open, FileAccess.Read))
            using (BinaryReader br = new BinaryReader(filestream2))
            {
                short countryid_from_file = br.ReadInt16();
                string countryName_from_file = br.ReadString();
                long population_from_file = br.ReadInt64(); 
                string region_from_file = br.ReadString();

                Console.WriteLine($"Country ID: {countryid_from_file}");
                Console.WriteLine($"Country Name: {countryName_from_file}");
                Console.WriteLine($"Population: {population_from_file}");
                Console.WriteLine($"Region: {region_from_file}");
            }
        }
    }
}
