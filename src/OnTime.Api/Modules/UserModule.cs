using Microsoft.AspNetCore.Mvc;
using OnTime.Application.Users.Commands;
using OnTime.Application.Users.Queries;

namespace OnTime.Api.Modules;

public class UserModule : IRouteModule
{
    public void DefineEndpoints(IEndpointRouteBuilder app)
    {
        _ = app.MapPost("/users/register", async ([FromServices] ISender mediator, CreateUserCommand user) =>
        {
            var result = await mediator.Send(user);
            return MapResponse.Map(result);
        })
        .WithName("Register")
        .WithOpenApi()
        .WithTags("Users")
        .WithSummary("Register a user");

        _ = app.MapPost("/users/login", async ([FromServices] ISender mediator, AuthenticateUserCommand user) =>
        {
            var result = await mediator.Send(user);
            return MapResponse.Map(result);
        })
        .WithName("Login")
        .WithOpenApi()
        .WithTags("Users")
        .WithSummary("Login");

        _ = app.MapGet("/users/information", async ([FromServices] IHttpContextAccessor httpContextAccessor, [FromServices] ISender mediator) =>
        {
            var httpContext = httpContextAccessor.HttpContext!;

            var query = new GetUserInformationQuery
            {
                HttpContext = httpContextAccessor.HttpContext,
            };

            var result = await mediator.Send(query);
            return MapResponse.Map(result);
        })
        .WithName("Information")
        .WithOpenApi()
        .WithTags("Users")
        .WithSummary("Get User Information")
        .RequireAuthorization("User");

        _ = app.MapPatch("/users/update-role", async ([FromServices] ISender mediator, UpdateUserRoleCommand updateUser) =>
        {
            var result = await mediator.Send(updateUser);
            return MapResponse.Map(result);
        })
        .WithName("UpdateUserRole")
        .WithOpenApi()
        .WithTags("Users")
        .WithSummary("Update User Role")
        .RequireAuthorization("User");

        _ = app.MapPut("/users/{id:int}", async ([FromServices] ISender mediator, int id, UpdateUserCommand command) =>
        {
            command.Id = id;
            var result = await mediator.Send(command);

            return MapResponse.Map(result);
        })
.WithName("UpdateUser")
.WithOpenApi()
.WithTags("Users")
.WithSummary("Update user by id")
.RequireAuthorization("User");
    }
}
