using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter your Credit card number: ");
        string input = Console.ReadLine();
        string maskedCreditCard = MaskCreditCard(input);
        Console.WriteLine(maskedCreditCard);

        Console.WriteLine("Enter your Social Security number: ");
        string input2 = Console.ReadLine();
        string maskedSocialSecurity = MaskSocialSecurity(input2);
        Console.WriteLine(maskedSocialSecurity);
    }

    // Method to mask credit card numbers
    static string MaskCreditCard(string input)
    {
        // Define regex pattern for credit card numbers
        string pattern = @"\b(?:\d[ -]*?){13,16}\b"; // Matches 13 to 16 digits separated by spaces or dashes

        // Replace credit card numbers with mask
        string maskedInput = Regex.Replace(input, pattern, match =>
        {
            string digits = Regex.Replace(match.Value, @"\D", ""); // Remove non-digit characters
            return new string('X', digits.Length - 4) + digits.Substring(digits.Length - 4); // Mask all digits except the last four
        });

        return maskedInput;
    }

    // Method to mask social security numbers
    static string MaskSocialSecurity(string input2)
    {
        // Define regex pattern for social security numbers
        string pattern = @"\b(?:\d[ -]*?){8}\b"; // Matches XXX-XX-XXXX format

        // Replace social security numbers with mask

        string maskedInput = Regex.Replace(input2, pattern, match =>
        {
            string digits = Regex.Replace(match.Value, @"\D", ""); // Remove non-digit characters
            int middleLength = digits.Length - 5; // Calculate the length of digits to be masked in the middle

            // Mask the digits according to the desired pattern
            return new string('X', 3) + digits.Substring(3, middleLength) + new string('X', digits.Length - middleLength - 4);
        });


        return maskedInput;
    }
}
