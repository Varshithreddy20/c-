
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace methods2
{
    public class Medicalbot
    {
        public const string BotName = "Bob";

        public static string GetBotName()
        {
            return BotName;
        }

        public static void PrescribeMedication(Patient patient)
        {
            string prescription = "";
            switch (patient.GetSymptoms())
            {
                case "Headache":
                    prescription = "ibuprofen " + GetDosage("ibuprofen", patient.GetAge());
                    break;
                case "Skin rashes":
                    prescription = "diphenhydramine " + GetDosage("diphenhydramine", patient.GetAge());
                    break;
                case "Dizziness":
                    prescription = patient.GetMedicalHistory().Contains("Diabetes") ? "metformin 500 mg" : "dimenhydrinate " + GetDosage("dimenhydrinate", patient.GetAge());
                    break;
                default:
                    break;
            }
            patient.SetPrescription(prescription);
        }

        public static string GetDosage(string medicineName, int age)
        {
            switch (medicineName)
            {
                case "ibuprofen":
                    return (age < 18) ? "400 mg" : "800 mg";
                case "diphenhydramine":
                    return (age < 18) ? "50 mg" : "300 mg";
                case "dimenhydrinate":
                    return (age < 18) ? "50 mg" : "400 mg";
                default:
                    return "500 mg";
            }
        }
    }
}
