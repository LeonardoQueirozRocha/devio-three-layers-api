using FluentAssertions;
using FluentValidation.Results;

namespace DevIO.Util.Tests.Extensions;

public static class FluentValidationExtensions
{
    public static void AssertValid(this ValidationResult result)
    {
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }
    
    public static void AssertInvalid(
        this ValidationResult result,
        string expectedErrorCode,
        string expectedErrorMessage)
    {
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();

        var error = result.Errors.First();
        error.ErrorCode.Should().BeEquivalentTo(expectedErrorCode);
        error.ErrorMessage.Should().BeEquivalentTo(expectedErrorMessage);
    }
}