namespace DevIO.Business.Entities.Validations.Documents;

public class CnpjValidation
{
    public const int CnpjSize = 14;

    public static bool Validate(string cpnj)
    {
        var cnpjNumbers = Utils.OnlyNumbers(cpnj);

        if (!HasValidSize(cnpjNumbers))
        {
            return false;
        }

        return !HasRepeatedDigits(cnpjNumbers) && HasValidDigits(cnpjNumbers);
    }

    private static bool HasValidSize(string valor) => valor.Length == CnpjSize;

    private static bool HasRepeatedDigits(string valor)
    {
        string[] invalidNumbers =
        [
            "00000000000000",
            "11111111111111",
            "22222222222222",
            "33333333333333",
            "44444444444444",
            "55555555555555",
            "66666666666666",
            "77777777777777",
            "88888888888888",
            "99999999999999"
        ];
        
        return invalidNumbers.Contains(valor);
    }

    private static bool HasValidDigits(string valor)
    {
        var number = valor.Substring(0, CnpjSize - 2);

        var verificationDigit = new VerificationDigit(number)
            .WithMultipliersOfUpTo(2, 9)
            .Replacing("0", 10, 11);
        
        var firstDigit = verificationDigit.CalculateDigit();
        
        verificationDigit.AddDigit(firstDigit);
        
        var secondDigit = verificationDigit.CalculateDigit();

        return string.Concat(firstDigit, secondDigit) == valor.Substring(CnpjSize - 2, 2);
    }
}