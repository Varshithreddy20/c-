using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace file_stream
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filepath = @"C:\Users\al6113\Desktop\sample\file_stream\pratice";
           
            FileInfo fileinfo = new FileInfo(filepath);
            FileStream filestream =File.Open(filepath, FileMode.CreateNew,FileAccess.Write);
            
            //content in the file
            string content = " Accion Labs";
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(content);
           
            //write 
            filestream.Write(bytes, 0, bytes.Length);
            Console.WriteLine("AccionLabs created");
           
            //content 2
            string content2 = " \nlocated in bengaluru";
            byte[] byte2 = System.Text.Encoding.ASCII.GetBytes(content2);
            filestream.Write(byte2, 0, byte2.Length);
            filestream.Close();
            Console.WriteLine(" content added successfully");

            //file reading
            
            FileStream filestream2 = File.OpenRead(filepath);
            byte[] byte3 = new byte[filestream2.Length];
            filestream2.Read(byte3, 0, byte3.Length);
            string content3=Encoding.ASCII.GetString(byte3);

            Console.WriteLine( "\n also in hyd ");

        }
    }
}
