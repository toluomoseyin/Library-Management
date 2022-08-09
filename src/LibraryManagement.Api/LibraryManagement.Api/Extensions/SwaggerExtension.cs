using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace LibraryManagement.Api.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddLibrarySwagger(this IServiceCollection serviceCollection, Action<SwaggerOptions> configureOptions)
        {
            SwaggerOptions SwaggerOptions = new(configureOptions);

            if (string.IsNullOrWhiteSpace(SwaggerOptions.Title))
            {
                throw new ArgumentNullException("'SwaggerOptions:Title' not configured");
            }
            if (string.IsNullOrWhiteSpace(SwaggerOptions.Version))
            {
                throw new ArgumentNullException("'SwaggerOptions:Version' not configured");
            }

            serviceCollection.AddEndpointsApiExplorer();
            serviceCollection.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(SwaggerOptions.Version, new OpenApiInfo { Title = SwaggerOptions.Title, Version = SwaggerOptions.Version });
                options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = "bearer",
                });
                options.OperationFilter<SwaggerHeaderFilter>();
            });

            return serviceCollection;
        }

        public static IApplicationBuilder UseLibrarySwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            return app;
        }
    }


    public class SwaggerOptions
    {
        public SwaggerOptions(Action<SwaggerOptions> configuration)
        {
            configuration(this);
        }
        public string Title { get; set; }
        public string Version { get; set; }
    }
    public class SwaggerHeaderFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Security == null)
                operation.Security = new List<OpenApiSecurityRequirement>();


            var scheme = new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearer" } };
            operation.Security.Add(new OpenApiSecurityRequirement
            {
                [scheme] = new List<string>()
            });
        }
    }
}
