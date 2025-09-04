using FluentValidation;

namespace TodoManager.Helpers
{
    public class ValidationHelper
    {
        public static async Task<IResult?> ValidateAsync<T>(T model,IValidator<T> validator)
        {
            var result = await validator.ValidateAsync(model);
            if (result.IsValid)
            {
                return null;
            }
            var errors = result.Errors.Select(error => new
            {
                field=error.PropertyName,
                error= error.ErrorMessage
            });
            return Results.BadRequest(errors);
        }
    }
}
