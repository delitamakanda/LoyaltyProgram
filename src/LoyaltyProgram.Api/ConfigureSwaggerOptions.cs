using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.Extensions.Options;

namespace LoyaltyProgram.Api
{
    public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        public void Configure(SwaggerGenOptions options)
        {
        
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, new OpenApiInfo
                {
                    Title = $"Loyalty Program API {description.ApiVersion}",
                    Version = description.ApiVersion.ToString(),
                    Description = "API for managing loyalty program and rewards"
                });
            }

        }

        public void Configure(string? name, SwaggerGenOptions options)
        {
            Configure(options);
        }
    }
}