using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace streamwriter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            String filepath = @"C:\Users\al6113\Desktop\sample\streamwriter\india.txt";
            FileStream filestream = new FileStream(filepath, FileMode.Create, FileAccess.Write);
            StreamWriter streamwriter = new StreamWriter(filestream);
            streamwriter.WriteLine("Hello");
            streamwriter.WriteLine("dfjdskfjlkdsjfjfoisjfjdfs");
            streamwriter.Close();

            Console.WriteLine("INDIA TEXT FILE IS CREATED");

            //StreamReader streamreader = new StreamReader(filepath); 
            FileStream filestream2 = new FileStream(filepath, FileMode.Open,FileAccess.Read);
            using (StreamReader streamReader = new StreamReader(filestream2)) {
                string content_from_file = streamReader.ReadToEnd();
                Console.WriteLine("\nfile read.file content is : ");
                Console.WriteLine(content_from_file);
            }
            
        }
    }
}
