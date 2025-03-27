using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Services;

namespace Shared.Extensions;

public static class CommonServiceExt
{
    public static IServiceCollection AddCommonServiceExt(this IServiceCollection services, Type assembly) {
        services.AddHttpContextAccessor();
        services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining(assembly));

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining(assembly);
        services.AddScoped<IIdentityService, IdentityServiceFake>();
        services.AddAutoMapper(assembly);
            
        return services;
    }
}