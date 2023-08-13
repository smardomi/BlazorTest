using BlazorApp1.Pages;
using FluentValidation;

namespace BlazorApp1.Validations
{
    public class WeatherForecastValidator : AbstractValidator<WeatherForecast>
    {
        public WeatherForecastValidator()
        {
            RuleFor(address => address.Summary)
                .NotEmpty().WithMessage("Required")
                .MaximumLength(5).WithMessage("max 5");


                //.MustAsync(BeUnique).WithMessage("Hii");
            
        }

        private Task<bool> BeUnique(string summary, CancellationToken token)
        {
            return Task.FromResult(summary.Length == 3);
        }
    }
}
