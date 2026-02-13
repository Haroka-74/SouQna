using FluentValidation;
using SouQna.Business.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace SouQna.Business.Services
{
    public class ValidationService(
        IServiceProvider serviceProvider
    ) : IValidationService
    {
        public async Task ValidateAsync<T>(T model)
        {
            var validator = serviceProvider.GetService<IValidator<T>>();

            if(validator is null)
                return;

            await validator.ValidateAndThrowAsync(model);
        }
    }
}