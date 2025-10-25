using DevIO.Business.Notifications;
using FluentAssertions;

namespace DevIO.Business.Tests.Notifications;

public class NotificatorTests
{
    private const string ClassName = nameof(Notificator);

    private readonly Notificator _notificator = new();

    [Fact(DisplayName = $"{ClassName} {nameof(Notificator.Handle)} should add message")]
    public void Handle_ShouldAddMessage()
    {
        // Arrange
        var notication = new Notification("test message");

        // Act
        _notificator.Handle(notication);

        // Assert
        _notificator.HasNotifications.Should().BeTrue();
    }

    [Fact(DisplayName =
        $"{ClassName} {nameof(Notificator.Handle)} should have empty collection when do not have messages")]
    public void Handle_ShouldHaveEmptyCollection_WhenDoNotHaveMessages()
    {
        // Arrange && Act && Assert
        _notificator.HasNotifications.Should().BeFalse();
    }
    
    [Fact(DisplayName = $"{ClassName} {nameof(Notificator.GetNotifications)} should return messages")]
    public void GetNotifications_ShouldReturnMessages()
    {
        // Arrange
        var notication = new Notification("test message");
        _notificator.Handle(notication);

        // Act
        var result = _notificator.GetNotifications();

        // Assert
        result.Should().NotBeEmpty();
    }
}