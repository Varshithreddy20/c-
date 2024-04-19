using System;

class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine($"Hi, I'm {MedicalBot.GetBotName()}. I'm here to help you in your medication.\n");

        Patient patient = new Patient();

        Console.WriteLine("Enter your (patient) details:");
        string errorMessage;
        Console.Write("Enter Patient Name: ");
        string name = Console.ReadLine();
        while (!patient.SetName(name, out errorMessage))
        {
            Console.WriteLine($"Error: {errorMessage}");
            Console.Write("Enter Patient Name: ");
            name = Console.ReadLine();
        }

        Console.Write("Enter Patient Age: ");
        int age;
        while (!int.TryParse(Console.ReadLine(), out age) || !patient.SetAge(age, out errorMessage))
        {
            Console.WriteLine($"Error: {errorMessage}");
            Console.Write("Enter Patient Age: ");
        }

        Console.Write("Enter Patient Gender: ");
        string gender = Console.ReadLine();
        while (!patient.SetGender(gender, out errorMessage))
        {
            Console.WriteLine($"Error: {errorMessage}");
            Console.Write("Enter Patient Gender: ");
            gender = Console.ReadLine();
        }

        Console.Write("Enter Medical History. Eg: Diabetes. Press Enter for None: ");
        string medicalHistory = Console.ReadLine();
        patient.SetMedicalHistory(medicalHistory);

        Console.WriteLine($"\nWelcome, {patient.GetName()}, {patient.GetAge()}.");

        Console.WriteLine("\nWhich of the following symptoms do you have:");
        Console.WriteLine("S1. Headache");
        Console.WriteLine("S2. Skin rashes");
        Console.WriteLine("S3. Dizziness");
        Console.Write("Enter the symptom code from above list (S1, S2 or S3): ");
        string symptomCode = Console.ReadLine();
        while (!patient.SetSymptomCode(symptomCode, out errorMessage))
        {
            Console.WriteLine($"Error: {errorMessage}");
            Console.Write("Enter the symptom code from above list (S1, S2 or S3): ");
            symptomCode = Console.ReadLine();
        }

       

        Console.WriteLine("Thank you for coming.");
    }

    
}