using AutoMapper;

namespace OnTime.Application.Reservations.Commands;

public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, ApplicationResponse<ReservationResponseDto>>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IMapper _mapper;

    public CreateReservationCommandHandler(IReservationRepository reservationRepository, IRestaurantRepository restaurantRepository, IMapper mapper)
    {
        _reservationRepository = reservationRepository ??
            throw new ArgumentNullException(nameof(reservationRepository));

        _restaurantRepository = restaurantRepository ??
            throw new ArgumentNullException(nameof(restaurantRepository));

        _mapper = mapper ??
            throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ApplicationResponse<ReservationResponseDto>> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        var restaurantReservations = _reservationRepository.GetRestaurantReservations(request.RestaurantId, request.Date);
        var restaurant = await _restaurantRepository.GetById(request.RestaurantId);

        var noTableWithSpecificSeats = true;

        foreach (var table in restaurant.Tables)
        {
            if (table.Capacity == request.NumberOfGuests)
            {
                var freeSlot = true;
                noTableWithSpecificSeats = false;
                foreach (var reservation in restaurantReservations)
                {
                    if (request.StartTime < reservation.EndTime &&
                        reservation.StartTime < request.EndTime &&
                        reservation.TableId == table.Id)
                    {
                        freeSlot = false;
                    }
                }

                if (freeSlot)
                {
                    var newReservation = _mapper.Map<Reservation>(request);
                    newReservation.TableId = table.Id;
                    _ = await _reservationRepository.Add(newReservation);
                    return ApplicationResponse.Success(_mapper.Map<ReservationResponseDto>(newReservation));
                }
            }
        }

        return noTableWithSpecificSeats
            ? ApplicationResponse.BadRequest<ReservationResponseDto>("There is no table with that number of seats.")
            : ApplicationResponse.BadRequest<ReservationResponseDto>("No available seats for that time period.");
    }
}
