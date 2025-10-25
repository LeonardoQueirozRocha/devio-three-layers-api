using DevIO.Business.Interfaces;

namespace DevIO.Business.Notifications;

public class Notificator : INotificator
{
    private readonly List<Notification> _notifications = [];

    public bool HasNotifications =>
        _notifications.Count > 0;

    public List<Notification> GetNotifications() =>
        _notifications;

    public void Handle(Notification notification) =>
        _notifications.Add(notification);
}