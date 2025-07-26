using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using OneBeyondApi.Infrastructure;
using System.Net.Mime;

namespace OneBeyondApi.Configuration
{
    public static class ApiConfig
    {
        public static void ConfigureApi(this IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(new ProducesAttribute(MediaTypeNames.Application.Json));
                options.Filters.Add(new ConsumesAttribute(MediaTypeNames.Application.Json));
            });

            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(ApiVersions.DefaultMajor, ApiVersions.DefaultMinor);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
                config.ApiVersionReader = ApiVersionReader.Combine(new HeaderApiVersionReader(Header.Version), new QueryStringApiVersionReader("api-version"));
            });
        }
    }
}
