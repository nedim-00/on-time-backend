using Microsoft.AspNetCore.Mvc;
using OnTime.Application.Restaurants.Commands;
using OnTime.Application.Restaurants.Queries;

namespace OnTime.Api.Modules;

public class RestaurantModule : IRouteModule
{
    public void DefineEndpoints(IEndpointRouteBuilder app)
    {
        _ = app.MapPost("/restaurants", async ([FromServices] ISender mediator, CreateRestaurantCommand restaurant) =>
        {
            var result = await mediator.Send(restaurant);
            return MapResponse.Map(result);
        })
        .WithName("CreateRestaurant")
        .WithOpenApi()
        .WithTags("Restaurants")
        .WithSummary("Create a restaurant")
        .RequireAuthorization("RegularRestaurantOwner");

        _ = app.MapGet("/restaurants", async ([FromServices] ISender mediator, int pageNumber, int pageSize) =>
        {
            var result = await mediator.Send(new GetAllRestaurantsQuery(pageSize, pageNumber));
            return MapResponse.Map(result);
        })
        .WithName("GetAllRestaurants")
        .WithOpenApi()
        .WithTags("Restaurants")
        .WithSummary("Get all restaurants")
        .RequireAuthorization("User");

        _ = app.MapGet("/restaurants/{id:int}", async ([FromServices] ISender mediator, int id) =>
        {
            var query = new GetRestaurantQuery
            {
                Id = id,
            };
            var result = await mediator.Send(query);

            return MapResponse.Map(result);
        })
        .WithName("GetRestaurant")
        .WithOpenApi()
        .WithTags("Restaurants")
        .WithSummary("Get restaurant by id")
        .RequireAuthorization("User");

        _ = app.MapGet("/restaurants/popular", async ([FromServices] ISender mediator) =>
        {
            var query = new GetPopularRestaurantsQuery
            {
            };
            var result = await mediator.Send(query);

            return MapResponse.Map(result);
        })
        .WithName("GetPopularRestaurants")
        .WithOpenApi()
        .WithTags("Restaurants")
        .WithSummary("Get popular restaurants")
        .RequireAuthorization("User");

        _ = app.MapPut("/restaurants/{id:int}", async ([FromServices] ISender mediator, int id, UpdateRestaurantCommand command) =>
        {
            command.Id = id;
            var result = await mediator.Send(command);

            return MapResponse.Map(result);
        })
        .WithName("UpdateRestaurant")
        .WithOpenApi()
        .WithTags("Restaurants")
        .WithSummary("Update restaurant by id")
        .RequireAuthorization("RegularRestaurantOwner");

        _ = app.MapGet("/restaurants/owned-restaurants", async ([FromServices] IHttpContextAccessor httpContextAccessor, [FromServices] ISender mediator) =>
        {
            var httpContext = httpContextAccessor.HttpContext!;

            var query = new GetOwnedRestaurantsQuery
            {
                HttpContext = httpContextAccessor.HttpContext,
            };

            var result = await mediator.Send(query);
            return MapResponse.Map(result);
        })
        .WithName("GetOwnedRestaurants")
        .WithOpenApi()
        .WithTags("Restaurants")
        .WithSummary("Get owned restaurants for a specific owner")
        .RequireAuthorization("RegularRestaurantOwner");

        _ = app.MapGet("/restaurants/search", async ([FromServices] ISender mediator, int pageNumber, int pageSize, string? name) =>
        {
            var result = await mediator.Send(new SearchRestaurantQuery(pageSize, pageNumber, name ?? string.Empty));
            return MapResponse.Map(result);
        })
        .WithName("SearchRestaurants")
        .WithOpenApi()
        .WithTags("Restaurants")
        .WithSummary("Search restaurants")
        .RequireAuthorization("User");
    }
}
