namespace DevIO.Business.Entities.Validations.Documents;

public class CpfValidation
{
    public const int CpfSize = 11;

    public static bool Validate(string cpf)
    {
        var cpfNumber = Utils.OnlyNumbers(cpf);

        if (!IsValidSize(cpfNumber))
        {
            return false;
        }

        return !HasRepeatedDigits(cpfNumber) && HasValidDigits(cpfNumber);
    }

    private static bool IsValidSize(string valor) => valor.Length == CpfSize;

    private static bool HasRepeatedDigits(string valor)
    {
        string[] invalidNumbers =
        [
            "00000000000",
            "11111111111",
            "22222222222",
            "33333333333",
            "44444444444",
            "55555555555",
            "66666666666",
            "77777777777",
            "88888888888",
            "99999999999"
        ];
        
        return invalidNumbers.Contains(valor);
    }

    private static bool HasValidDigits(string valor)
    {
        var number = valor.Substring(0, CpfSize - 2);
        
        var verificationDigit = new VerificationDigit(number)
            .WithMultipliersOfUpTo(2, 11)
            .Replacing("0", 10, 11);
        
        var firstDigit = verificationDigit.CalculateDigit();
        
        verificationDigit.AddDigit(firstDigit);
        
        var secondDigit = verificationDigit.CalculateDigit();

        return string.Concat(firstDigit, secondDigit) == valor.Substring(CpfSize - 2, 2);
    }
}