namespace FCxLabs.Application.Extensions;

public static class StringExtensions
{
    public static bool IsPasswordValid(this string password)
    {
        bool containsNonAlphanumeric = password.Any(char.IsLetterOrDigit);
        bool containsUpperCase = password.Any(char.IsUpper);
        bool isValid = containsNonAlphanumeric && containsUpperCase;
        return isValid;
    }

    public static bool IsCPFValid(this string strCPF)
    {
        string value = strCPF.Replace(".", "");
        value = value.Replace("-", "");
        if (value.Length != 11)
            return false;

        bool isEqual = true;

        for (int i = 1; i < 11 && isEqual; i++)
            if (value[i] != value[0])
                isEqual = false;

        if (isEqual || value == "12345678909")
            return false;

        int[] numbers = new int[11];

        for (int i = 0; i < 11; i++)
            numbers[i] = int.Parse(
                value[i].ToString());

        int sum = 0;

        for (int i = 0; i < 9; i++)
            sum += (10 - i) * numbers[i];

        int result = sum % 11;

        if (result == 1 || result == 0)
        {
            if (numbers[9] != 0)
                return false;
        }

        else if (numbers[9] != 11 - result)
            return false;

        sum = 0;

        for (int i = 0; i < 10; i++)
            sum += (11 - i) * numbers[i];

        result = sum % 11;

        if (result == 1 || result == 0)
        {
            if (numbers[10] != 0)
                return false;
        }
        else
        if (numbers[10] != 11 - result)
            return false;

        return true;
    }
}