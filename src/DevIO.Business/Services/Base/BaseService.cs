using DevIO.Business.Entities.Base;
using FluentValidation;

namespace DevIO.Business.Services.Base;

public abstract class BaseService
{
    protected bool Validate<TValidation, TEntity>(TValidation validation, TEntity entity)
        where TValidation : AbstractValidator<TEntity>
        where TEntity : Entity
    {
        return validation.Validate(entity).IsValid;
    }
}