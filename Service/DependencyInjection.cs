using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Service.DTOs.Admin.Countries;
using Service.Services.Interfaces;
using Service.Services;
using Service.Helpers;

namespace Service
{
	public static class DependencyInjection
	{
        public static IServiceCollection AddServiceLayer(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation(config =>
            { config.DisableDataAnnotationsValidation = true; });

            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IValidator<CountryCreateDto>, CountryCreateDtoValidator>();
            return services;
        }
        
    }
}

