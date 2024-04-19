public class Patient
{
    private string name;
    private int age;
    private string gender;
    private string medicalHistory;
    private string symptomCode;
    private string prescription;

    public string GetName()
    {
        return name;
    }

    public bool SetName(string name, out string errorMessage)
    {
        if (!string.IsNullOrEmpty(name) && name.Length >= 2)
        {
            this.name = name;
            errorMessage = "";
            return true;
        }
        errorMessage = "Name should contain at least two or more characters.";
        return false;
    }

    public int GetAge()
    {
        return age;
    }

    public bool SetAge(int age, out string errorMessage)
    {
        if (age >= 0 && age <= 100)
        {
            this.age = age;
            errorMessage = "";
            return true;
        }
        errorMessage = "Age should be between 0 and 100.";
        return false;
    }

    public string GetGender()
    {
        return gender;
    }

    public bool SetGender(string gender, out string errorMessage)
    {
        if (gender.ToLower() == "male" || gender.ToLower() == "female" || gender.ToLower() == "other")
        {
            this.gender = gender;
            errorMessage = "";
            return true;
        }
        errorMessage = "Gender should be either Male, Female, or Other.";
        return false;
    }

    public string GetMedicalHistory()
    {
        return medicalHistory;
    }

    public void SetMedicalHistory(string medicalHistory)
    {
        this.medicalHistory = medicalHistory;
    }

    public bool SetSymptomCode(string symptomCode, out string errorMessage)
    {
        if (symptomCode.ToLower() == "s1" || symptomCode.ToLower() == "s2" || symptomCode.ToLower() == "s3")
        {
            this.symptomCode = symptomCode;
            errorMessage = "";
            return true;
        }
        errorMessage = "Invalid Symptom Code. Symptom Code should be S1, S2, or S3.";
        return false;
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
