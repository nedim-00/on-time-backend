using System.Reflection;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnTime.Application;
using OnTime.Application.Users;
using OnTime.Application.Users.Commands;
using OnTime.Domain;
using OnTime.SharedCore;

namespace OnTime.Infrastructure;

public static class DependecyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        _ = services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionHandlerBehavior<,>));
        _ = services.AddTransient<ISystemClock, SystemClock>();

        _ = services.AddMediatR(cfg
        => cfg.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(CreateUserCommand))!));
        _ = services.AddAutoMapper(typeof(UserMapperProfile).Assembly);
        _ = services.AddEntityFrameworkSqlServer().AddDbContext<OnTimeDbContext>(options
            => options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("OnTime.Infrastructure")));

        _ = services.AddScoped<IUserService, UserService>();
        _ = services.AddSingleton<ICreateTokenService, CreateTokenService>();

        _ = services.AddTransient<IUserRepository, UserRepository>();
        _ = services.AddTransient<IRestaurantRepository, RestaurantRepository>();
        _ = services.AddTransient<IReservationRepository, ReservationRepository>();

        _ = services.AddFluentValidationAutoValidation();
        _ = services.AddTransient(typeof(IPaginationRepository<>), typeof(PaginationRepository<>));

        return services;
    }
}
