using System;

public class DebitCard
{
    private string _pin;

    public string Pin
    {
        get { return _pin; }
        set
        {
            if (IsValidPin(value))
            {
                _pin = value;
            }
            else
            {
                Console.WriteLine("Error: Invalid PIN. PIN should be exactly 4 digits or 6 digits.");
            }
        }
    }

    private bool IsValidPin(string pin)
    {
       
        if ((pin.Length == 4 || pin.Length == 6) && IsDigitsOnly(pin))
        {
            return true;
        }
        return false;
    }

    private bool IsDigitsOnly(string str)
    {
        foreach (char c in str)
        {
            if (c < '0' || c > '9')
            {
                return false;
            }
        }
        return true;
    }
}
