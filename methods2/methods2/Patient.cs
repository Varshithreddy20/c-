using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace methods2
{
    public class Patient
    {
        public string name;
        public int age;
        public string gender;
        public string medicalHistory;
        public string symptomCode;
        public string prescription;

        public string GetName()
        {
            return name;
        }

        public bool SetName(string name)
        {
            if (!string.IsNullOrEmpty(name) && name.Length >= 2)
            {
                this.name = name;
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetAge()
        {
            return age;
        }

        public bool SetAge(int age)
        {
            if (age >= 0 && age <= 100)
            {
                this.age = age;
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GetGender()
        {
            return gender;
        }

        public bool SetGender(string gender)
        {
            if (gender.ToLower() == "male" || gender.ToLower() == "female" || gender.ToLower() == "other")
            {
                this.gender = gender;
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GetMedicalHistory()
        {
            return medicalHistory;
        }

        public void SetMedicalHistory(string medicalHistory)
        {
            this.medicalHistory = medicalHistory;
        }

        public bool SetSymptomCode(string symptomCode)
        {
            if (symptomCode.ToLower() == "s1" || symptomCode.ToLower() == "s2" || symptomCode.ToLower() == "s3")
            {
                this.symptomCode = symptomCode;
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GetSymptoms()
        {
            switch (symptomCode.ToLower())
            {
                case "s1":
                    return "Headache";
                case "s2":
                    return "Skin rashes";
                case "s3":
                    return "Dizziness";
                default:
                    return "Unknown";
            }
        }

        public string GetPrescription()
        {
            return prescription;
        }

        public void SetPrescription(string prescription)
        {
            this.prescription = prescription;
        }
    }
}
