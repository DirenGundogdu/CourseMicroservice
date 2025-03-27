using Asp.Versioning;
using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Extensions;

public static class VersioningExt
{
    public static IServiceCollection AddVersioningExt(this IServiceCollection services) {
        services.AddApiVersioning(opt => {
            opt.DefaultApiVersion = new ApiVersion(1,0);
            opt.AssumeDefaultVersionWhenUnspecified = true;
            opt.ReportApiVersions = true;
            opt.ApiVersionReader = new UrlSegmentApiVersionReader();

            // opt.ApiVersionReader = ApiVersionReader.Combine(new HeaderApiVersionReader(), new QueryStringApiVersionReader(),
            //     new UrlSegmentApiVersionReader());

        }).AddApiExplorer(opt => {
            opt.GroupNameFormat = "'v'V";
            opt.SubstituteApiVersionInUrl = true;
        });

        return services;
    }

    public static ApiVersionSet AddVersionSetExt(this WebApplication app) {
        var apiVersionSet = app.NewApiVersionSet().HasApiVersion(new ApiVersion(1,0)).HasApiVersion(new ApiVersion(2,0)).ReportApiVersions().Build();

        return apiVersionSet;
    }
}