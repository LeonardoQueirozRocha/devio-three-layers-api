namespace DevIO.Business.Constants;

public static class ValidationMessage
{
    public const string NotEmptyMessage =
        "The {PropertyName} field needs to be informed";

    public const string LengthMinMaxMessage =
        "The {PropertyName} field needs to have between {MinLength} and {MaxLength} characters";
    
    public const string LengthMaxMessage =
        "The {PropertyName} needs to have {MaxLenght} characters";

    public const string EqualMessage =
        "The document field needs to have {ComparisonValue} characters and {PropertyValue} were provided";

    public const string InvalidDocumentMessage =
        "The informed document is invalid";

    public const string GreaterThanMessage =
        "The {PropertyName} field needs to be greater than {ComparisonValue}";
}