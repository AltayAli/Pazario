using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Pazario.API.OpenApi
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
            options.AddSecurityDefinition("Bearer", CreateSecurityScheme());
            options.AddSecurityRequirement(GetSecurityRequirement);

            foreach (var apiVersionDescription in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(apiVersionDescription.GroupName, CreateVersionInfo(apiVersionDescription));
            }
        }

        private static OpenApiSecurityRequirement GetSecurityRequirement(OpenApiDocument document)
        {
            return new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecuritySchemeReference("Bearer", document),
                    new List<string>()
                }
            };
        }

        private static OpenApiInfo CreateVersionInfo(ApiVersionDescription apiVersionDescription)
        {
            var openApiInfo = new OpenApiInfo
            {
                Title = apiVersionDescription.GroupName,
                Version = apiVersionDescription.ApiVersion.ToString(),
            };

            if (apiVersionDescription.IsDeprecated)
            {
                openApiInfo.Description += " This API version has been deprecated.";
            }

            return openApiInfo;
        }

        private static OpenApiSecurityScheme CreateSecurityScheme()
            => new()
            {
                Description = @"JWT Authorization header using the Bearer scheme.
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            };

        public void Configure(string? name, SwaggerGenOptions options)
        {
            Configure(options);
        }
    }
}
