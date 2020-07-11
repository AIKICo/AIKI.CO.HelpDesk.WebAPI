using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace AIKI.CO.HelpDesk.WebAPI.BuilderExtensions
{
    public static class AddSwaggerExtensions
    {
        public static IServiceCollection AddAikiSwagger(this IServiceCollection services, IConfiguration config)
        {
            services.AddSwaggerGen(options =>
            {
                
                var apiinfo = new OpenApiInfo
                {
                    Title = "AIKI Help Desk Web API",
                    Version = "v1",
                    Description = "AIKI Help Desk API",
                    Contact = new OpenApiContact
                    {
                        Email = "qermezkon@gmail.com",
                        Name = "Mohammad Mehrnia", 
                        Url = new Uri("https://github.com/AIKICo/AIKI.CO.HelpDesk.WebAPI")
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "MIT License",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                };
                /*var xmlCommentsFile =
                    $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath =
                    Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
                options.IncludeXmlComments(xmlCommentsFullPath);*/
                options.SwaggerDoc("v1", apiinfo);
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });
            services.AddSwaggerGenNewtonsoftSupport();
            return services;
        }
    }
}