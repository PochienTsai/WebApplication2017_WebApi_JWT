using Swashbuckle.Application;
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Description;
using WebActivatorEx;
//using System.Web;

using WebApplication2017_WebApi_JWT;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace WebApplication2017_WebApi_JWT
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.OperationFilter<SwaggerAuthorizeOperationFilter>();
                        c.OperationFilter<FileOperationFilter>();
                        c.SingleApiVersion("v1", "WebApplication2017_WebApi_JWT");
                        c.DescribeAllEnumsAsStrings(camelCase: false);
                        c.ApiKey("apiKey")
                          .Description("Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"")
                          .Name("Authorization")
                          .In("header");
                        c.IncludeXmlComments(GetXmlCommentsPath());
                    })
                .EnableSwaggerUi(c =>
                {
                    // 配置 Swagger UI 來使用 JWT 認證
                    c.EnableApiKeySupport("Authorization", "header");
                    // c.ShowExtensions(true);
                    c.SetValidatorUrl("https://online.swagger.io/validator");
                    // c.UImaxDisplayedTags(100);
                    // c.UIfilter("''");
                });
        }

        private static string GetXmlCommentsPath() { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"bin\" + Assembly.GetExecutingAssembly().GetName().Name + ".xml"); }

        public static bool ResolveVersionSupportByRouteConstraint(ApiDescription apiDesc, string targetApiVersion)
        {
            return (apiDesc.Route.RouteTemplate.ToLower().Contains(targetApiVersion.ToLower()));
        }

        private class ApplyDocumentVendorExtensions : IDocumentFilter
        {
            public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
            {
                // Include the given data type in the final SwaggerDocument
                //
                //schemaRegistry.GetOrRegister(typeof(ExtraType));
            }
        }

        public class AssignOAuth2SecurityRequirements : IOperationFilter
        {
            public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
            {
                // Correspond each "Authorize" role to an oauth2 scope
                var scopes = apiDescription.ActionDescriptor.GetFilterPipeline()
                    .Select(filterInfo => filterInfo.Instance)
                    .OfType<AuthorizeAttribute>()
                    .SelectMany(attr => attr.Roles.Split(','))
                    .Distinct();

                if (scopes.Any())
                {
                    if (operation.security == null)
                        operation.security = new List<IDictionary<string, IEnumerable<string>>>();

                    var oAuthRequirements = new Dictionary<string, IEnumerable<string>>
                    {
                        { "oauth2", scopes }
                    };

                    operation.security.Add(oAuthRequirements);
                }
            }
        }

        private class ApplySchemaVendorExtensions : ISchemaFilter
        {
            public void Apply(Schema schema, SchemaRegistry schemaRegistry, Type type)
            {
                // Modify the example values in the final SwaggerDocument
                //
                if (schema.properties != null)
                {
                    foreach (var p in schema.properties)
                    {
                        switch (p.Value.format)
                        {
                            case "int32":
                                p.Value.example = 123;
                                break;
                            case "double":
                                p.Value.example = 9858.216;
                                break;
                        }
                    }
                }
            }
        }
    }
}
