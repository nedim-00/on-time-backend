using Microsoft.AspNetCore.Mvc;
using OnTime.Application.Reservations.Commands;
using OnTime.Application.Reservations.Queries;

namespace OnTime.Api.Modules;

public class ReservationModule : IRouteModule
{
    public void DefineEndpoints(IEndpointRouteBuilder app)
    {
        _ = app.MapPost("/reservations", async ([FromServices] ISender mediator, CreateReservationCommand reservation) =>
        {
            var result = await mediator.Send(reservation);
            return MapResponse.Map(result);
        })
        .WithName("CreateReservation")
        .WithOpenApi()
        .WithTags("Reservations")
        .WithSummary("Create a reservation")
        .RequireAuthorization("User");

        _ = app.MapGet("/reservations/user", async ([FromServices] IHttpContextAccessor httpContextAccessor, [FromServices] ISender mediator, int pageNumber, int pageSize) =>
        {
            var query = new GetUserReservationsQuery
            {
                HttpContext = httpContextAccessor.HttpContext,
                PageSize = pageSize,
                PageNumber = pageNumber,
            };
            var result = await mediator.Send(query);

            return MapResponse.Map(result);
        })
        .WithName("GetUserReservations")
        .WithOpenApi()
        .WithTags("Reservations")
        .WithSummary("Get user reservations")
        .RequireAuthorization("User");

        _ = app.MapGet("/reservations/restaurant", async ([FromServices] ISender mediator, int id, int pageNumber, int pageSize) =>
        {
            var query = new GetRestaurantReservationsQuery
            {
                Id = id,
                PageSize = pageSize,
                PageNumber = pageNumber,
            };
            var result = await mediator.Send(query);

            return MapResponse.Map(result);
        })
        .WithName("GetRestaurantReservations")
        .WithOpenApi()
        .WithTags("Reservations")
        .WithSummary("Get restaurant reservations")
        .RequireAuthorization("RegularRestaurantOwner");

        _ = app.MapGet("/reservations/next", async ([FromServices] IHttpContextAccessor httpContextAccessor, [FromServices] ISender mediator) =>
        {
            var query = new GetNextReservationQuery
            {
                HttpContext = httpContextAccessor.HttpContext,
            };
            var result = await mediator.Send(query);

            return MapResponse.Map(result);
        })
        .WithName("GetNextReservation")
        .WithOpenApi()
        .WithTags("Reservations")
        .WithSummary("Get next reservation for a user")
        .RequireAuthorization("User");

        _ = app.MapGet("/reservations/{id:int}", async ([FromServices] IHttpContextAccessor httpContextAccessor, [FromServices] ISender mediator, int id) =>
        {
            var query = new GetReservationQuery
            {
                Id = id,
            };
            var result = await mediator.Send(query);

            return MapResponse.Map(result);
        })
        .WithName("GetReservationById")
        .WithOpenApi()
        .WithTags("Reservations")
        .WithSummary("Get reservation by id")
        .RequireAuthorization("User");

        _ = app.MapPatch("/reservations/status", async ([FromServices] ISender mediator, UpdateReservationStatusCommand command) =>
        {
            var result = await mediator.Send(command);

            return MapResponse.Map(result);
        })
        .WithName("UpdateReservationStatus")
        .WithOpenApi()
        .WithTags("Reservations")
        .WithSummary("Update reservation status by id")
        .RequireAuthorization("User");
    }
}
