using System.Reflection;
using AspBoot.Configuration;
using AspBoot.Repository;
using AspBoot.Service;
using FluentValidation;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OshService.Data;
using OshService.Options;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace OshService;

public static class Program
{
    public const string DevelopmentCors = "DevelopmentCors";

    public const string SwaggerSchemaName = "OshService";
    public const string SwaggerSchemaVersion = "v1";
    public const string SwaggerRoute = "api/swagger";

    public static readonly OpenApiSecurityScheme SecurityScheme = new()
    {
        Description = "JWT token",
        Name = "Bearer",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = JwtBearerDefaults.AuthenticationScheme
    };

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors(setup =>
            setup.AddPolicy(DevelopmentCors, options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

        var jwt = builder.Configuration.GetParam<Jwt>();
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(setup =>
        {
            setup.RequireHttpsMetadata = jwt.RequireHttpsMetadata;
            setup.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwt.Issuer,
                ValidateAudience = true,
                ValidAudience = jwt.Audience,
                ValidateLifetime = true,
                IssuerSigningKey = jwt.GetSymmetricSecurityKey(),
                ValidateIssuerSigningKey = true
            };
        });
        builder.Services.AddAuthorization();
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddEndpointsApiExplorer().AddSwaggerGen(setup =>
        {
            setup.SwaggerDoc(SwaggerSchemaVersion,
                new OpenApiInfo
                {
                    Title = SwaggerSchemaName,
                    Version = SwaggerSchemaVersion,
                    Description = ""
                });
            setup.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, SecurityScheme);
            setup.EnableAnnotations();
            setup.OperationFilter<SecurityRequirementsOperationFilter>(true, JwtBearerDefaults.AuthenticationScheme);
            setup.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
        });
        builder.Services.AddFluentValidationRulesToSwagger();

        builder.Services.AddDbContext<DatabaseContext>(setup =>
            setup.UseNpgsql(builder.Configuration.GetConnectionString("Docker"))
        );

        builder.Services.AddControllers();
        builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        builder.Services.AddServices(Assembly.GetExecutingAssembly());
        builder.Services.AddRepositories(Assembly.GetExecutingAssembly());
        builder.Services.AddAutoMapper(setup =>
            setup.AddMaps(Assembly.GetExecutingAssembly())
        );

        var app = builder.Build();
        app.UseDefaultFiles();
        app.UseStaticFiles();
        app.MapControllers();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapFallbackToFile("/index.html");
        if (app.Environment.IsDevelopment())
        {
            app.UseCors(DevelopmentCors);
            app.UseSwagger(config => config.RouteTemplate = $"{SwaggerRoute}/{{documentname}}/swagger.json");
            app.UseSwaggerUI(config =>
            {
                config.RoutePrefix = SwaggerRoute;
                config.SwaggerEndpoint($"{SwaggerSchemaVersion}/swagger.json", SwaggerSchemaName);
                config.EnableFilter();
                config.DisplayRequestDuration();
                config.DefaultModelRendering(ModelRendering.Model);
                config.EnableTryItOutByDefault();
                config.EnablePersistAuthorization();
            });
        }

        app.Run();
    }
}
