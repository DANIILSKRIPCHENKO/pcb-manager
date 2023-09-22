using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PcbManager.Main.WebHost.OpenApi
{
    public class SecurityRequirementsOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (!context.MethodInfo.GetCustomAttributes(true).Any(x => x is AuthorizeAttribute))
            {
                return;
            }

            var authorizeAttribute = (AuthorizeAttribute)
                context.MethodInfo
                    .GetCustomAttributes(true)
                    .Where(x => x is AuthorizeAttribute)
                    .Single();

            if (string.IsNullOrWhiteSpace(authorizeAttribute.AuthenticationSchemes))
            {
                return;
            }

            if (authorizeAttribute.AuthenticationSchemes!.Contains("ApiKey"))
            {
                ApplyApiKeySecurityRequirement(operation);
            }

            if (authorizeAttribute.AuthenticationSchemes!.Contains("Bearer"))
            {
                ApplyBearerSecurityRequirement(operation);
            }
        }

        private static void ApplyApiKeySecurityRequirement(OpenApiOperation operation)
        {
            operation.Security.Add(
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Name = "ApiKey",
                            In = ParameterLocation.Header,
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "ApiKey"
                            }
                        },
                        new List<string>()
                    }
                }
            );
        }

        private static void ApplyBearerSecurityRequirement(OpenApiOperation operation)
        {
            operation.Security.Add(
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new List<string>()
                    }
                }
            );

            operation.Security.Add(
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Name = "OAuth2",
                            In = ParameterLocation.Header,
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "OAuth2"
                            }
                        },
                        new List<string>()
                    }
                }
            );
        }
    }
}
