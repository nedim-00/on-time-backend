using System.Security.Claims;
using System.Text;
using Destructurama;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OnTime.Api;
using OnTime.Domain.Enums;
using OnTime.Infrastructure;
using Serilog;

const string everyoneCorsPolicyName = "Everyone";

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Destructure.UsingAttributes()
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host
    .UseDefaultServiceProvider(
        (context, options) =>
        {
            options.ValidateScopes = context.HostingEnvironment.IsDevelopment() ||
                context.HostingEnvironment.IsEnvironment("Production");
            options.ValidateOnBuild = true;
        })
    .UseSerilog();

// Add services to the container.
ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
ConfigureApp(app);

app.Run();

static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    _ = services.AddEndpointsApiExplorer();
    _ = services.AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "bearer",
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer",
                    },
                },
                new string[] { }
            },
        });
    });

    _ = services.AddSingleton(Log.Logger);

    _ = services.AddHttpContextAccessor();

    _ = services.AddApplication(configuration);

    services.AddRouteModules();

    _ = services.AddCors(options => options.AddPolicy(
        everyoneCorsPolicyName,
        policy => policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod()));

    _ = services.AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
        .AddCookie()
        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, x => x.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Authentication:JwtSettings:Key"] ?? string.Empty)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
        });

    _ = services.AddAuthorization(options =>
     options.AddPolicy("User", policy =>
        {
            _ = policy.RequireAssertion(context =>
            {
                var roleClaim = context.User.FindFirst(ClaimTypes.Role);
                return roleClaim != null && (roleClaim.Value == UserRole.User.ToString() ||
                roleClaim.Value == UserRole.RegularRestaurantOwner.ToString() ||
                roleClaim.Value == UserRole.PremiumRestaurantOwner.ToString() ||
                roleClaim.Value == UserRole.UltimateRestaurantOwner.ToString() ||
                roleClaim.Value == UserRole.RestaurantStaff.ToString() ||
                roleClaim.Value == UserRole.Admin.ToString());
            });
            policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
        }));

    _ = services.AddAuthorization(options =>
     options.AddPolicy("RegularRestaurantOwner", policy =>
        {
            _ = policy.RequireAssertion(context =>
            {
                var roleClaim = context.User.FindFirst(ClaimTypes.Role);
                return roleClaim != null && (
                roleClaim.Value == UserRole.RegularRestaurantOwner.ToString() ||
                roleClaim.Value == UserRole.PremiumRestaurantOwner.ToString() ||
                roleClaim.Value == UserRole.UltimateRestaurantOwner.ToString() ||
                roleClaim.Value == UserRole.RestaurantStaff.ToString() ||
                roleClaim.Value == UserRole.Admin.ToString());
            });
            policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
        }));

    // _ = services.AddAuthorization(options =>
    // {
    //    // the two policies below might be problematic
    //    // since they check user role from claims instead of DB
    //    // which means that if a users role changes in the lifecycle of a token
    //    // that won't be reflected in the system until token expiration
    //    options.AddPolicy("Manager", policy =>
    //    {
    //        _ = policy.RequireAssertion(context =>
    //        {
    //            var roleClaim = context.User.FindFirst(ClaimTypes.Role);
    //            return roleClaim != null && roleClaim.Value == UserRole.RestaurantOwner.ToString();
    //        });
    //        policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
    //    });

    // options.AddPolicy("User", policy =>
    //    {
    //        _ = policy.RequireAssertion(context =>
    //        {
    //            var roleClaim = context.User.FindFirst(ClaimTypes.Role);
    //            return roleClaim != null && (roleClaim.Value == UserRole.User.ToString() || roleClaim.Value == UserRole.RestaurantOwner.ToString());
    //        });
    //        policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
    //    });

    // options.AddPolicy("ActiveUserOnly", policy =>
    // {
    //    policy.Requirements.Add(new UserStatusRequirement());
    //    _ = policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
    // });
    // });

    // _ = services.AddScoped<IAuthorizationHandler, UserStatusAuthorizationHandler>();

    // services.AddHostedService<TimeTriggerWorkerService>();
    _ = services.AddMvc();
}

static void ConfigureApp(WebApplication app)
{
    if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Production"))
    {
        _ = app.UseSwagger();
        _ = app.UseSwaggerUI();
    }

    _ = app.UseHttpsRedirection();
    _ = app.UseStaticFiles();

    app.UseRouteModules();

    _ = app.UseCors(everyoneCorsPolicyName);

    _ = app.UseAuthentication();

    _ = app.UseAuthorization();
}

public partial class Program
{
}
