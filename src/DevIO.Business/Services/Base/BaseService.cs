using DevIO.Business.Entities.Base;
using DevIO.Business.Interfaces;
using DevIO.Business.Notifications;
using FluentValidation;
using FluentValidation.Results;

namespace DevIO.Business.Services.Base;

public abstract class BaseService(INotificator notificator)
{
    protected void Notify(ValidationResult validationResult) =>
        validationResult.Errors
            .ForEach(erro =>
                Notify(erro.ErrorMessage));

    protected void Notify(string message) =>
        notificator.Handle(new Notification(message));

    protected bool Validate<TValidation, TEntity>(TValidation validation, TEntity entity)
        where TValidation : AbstractValidator<TEntity>
        where TEntity : Entity
    {
        var validationResult = validation.Validate(entity);

        if (validationResult.IsValid)
        {
            return true;
        }

        Notify(validationResult);

        return false;
    }
}