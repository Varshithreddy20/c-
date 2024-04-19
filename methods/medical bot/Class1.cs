using System;

public class MedicalBot
{
    public const string BotName = "Bob";

    public static string GetBotName()
    {
        return BotName;
    }

    public static void PrescribeMedication(Patient patient)
    {
        string medicine = "";
        switch (patient.GetSymptoms().ToLower())
        {
            case "headache":
                medicine = "ibuprofen";
                break;
            case "skin rashes":
                medicine = "diphenhydramine";
                break;
            case "dizziness":
                if (patient.GetMedicalHistory().ToLower().Contains("diabetes"))
                    medicine = "metformin";
                else
                    medicine = "dimenhydrinate";
                break;
            default:
                Console.WriteLine("Unknown symptoms.");
                return;
        }

        string dosage = GetDosage(medicine, patient.GetAge());

        Console.WriteLine($"\nYour prescription based on your age, symptoms, and medical history:\n{medicine} {dosage}\n");
    }

    private static string GetDosage(string medicineName, int age)
    {
        switch (medicineName.ToLower())
        {
            case "ibuprofen":
                return age < 18 ? "400 mg" : "800 mg";
            case "diphenhydramine":
                return age < 18 ? "50 mg" : "300 mg";
            case "dimenhydrinate":
                return age < 18 ? "50 mg" : "400 mg";
            case "metformin":
                return "500 mg";
            default:
                return "";
        }
    }
}