using System;
using System.Collections.Generic;
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
                        { Name = "Aiki Co.", Url = new Uri("https://www.aiki.co.ir") },
                    License = new OpenApiLicense()
                    {
                        Name = "Commercial",
                        Url = new Uri("https://www.aiki.co.ir")
                    }
                };

                OpenApiSecurityScheme securityDefinition = new OpenApiSecurityScheme()
                {
                    Name = "Bearer",
                    BearerFormat = "JWT",
                    Scheme = "bearer",
                    Description = "Specify the authorization token.",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                };

                OpenApiSecurityRequirement securityRequirements = new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "adminAuth" }
                        }, new List<string>() }
                };

                options.SwaggerDoc("v1", apiinfo);
                options.AddSecurityDefinition("jwt_auth", securityDefinition);
                options.AddSecurityRequirement(securityRequirements);  
            });
            return services;
        }
    }
}