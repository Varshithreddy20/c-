using System;
using methods2;



class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hi, I'm Bob. I'm here to help you with your medication.");
        Console.WriteLine("Enter your (patient) details:");

        Patient patient = new Patient();

        Console.Write("Enter Patient Name: ");
        while (!patient.SetName(Console.ReadLine()))
        {
            Console.Write("Invalid input. Enter a valid name (at least 2 characters): ");
        }

        Console.Write("Enter Patient Age: ");
        while (!patient.SetAge(Convert.ToInt32(Console.ReadLine())))
        {
            Console.Write("Invalid input. Enter a valid age (between 0 and 100): ");
        }

        Console.Write("Enter Patient Gender: ");
        while (!patient.SetGender(Console.ReadLine()))
        {
            Console.Write("Invalid input. Enter a valid gender (Male, Female, or Other): ");
        }

        Console.Write("Enter Medical History. Eg: Diabetes. Press Enter for None: ");
        patient.SetMedicalHistory(Console.ReadLine());

        Console.WriteLine("\nWelcome, " + patient.GetName() + ", " + patient.GetAge() + ".");
        Console.WriteLine("Which of the following symptoms do you have:\nS1. Headache\nS2. Skin rashes\nS3. Dizziness");

        Console.Write("Enter the symptom code from the above list (S1, S2, or S3): ");
        while (!patient.SetSymptomCode(Console.ReadLine()))
        {
            Console.Write("Invalid input. Enter a valid symptom code (S1, S2, or S3): ");
        }
        Console.ReadKey();

        Medicalbot.PrescribeMedication(patient);

        Console.WriteLine("\nYour prescription based on your age, symptoms, and medical history:\n" + patient.GetPrescription());
        Console.WriteLine("\nThank you for coming.");
    }
}
