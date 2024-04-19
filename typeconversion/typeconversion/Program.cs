using System;

public class Program
{
    public static void Main(string[] args)
    {
        byte a = 10;
        short aShort = a; 

        int b = 10;
        short bShort = (short)b; 

        string c = "10.34";
        double cDouble = double.Parse(c); 

        decimal cDecimal;
        decimal.TryParse(c, out cDecimal); 

        decimal d = 20.3m;
        string dString = d.ToString(); 

        Console.WriteLine("aShort: " + aShort);
        Console.WriteLine("bShort: " + bShort);
        Console.WriteLine("cDouble: " + cDouble);
        Console.WriteLine("cDecimal: " + cDecimal);
        Console.WriteLine("dString: " + dString);
    }
}
