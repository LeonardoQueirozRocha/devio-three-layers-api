using DevIO.Business.Notifications;

namespace DevIO.Business.Interfaces;

public interface INotificator
{
    bool HasNotifications { get; }

    List<Notification> GetNotifications();

    void Handle(Notification notification);
}