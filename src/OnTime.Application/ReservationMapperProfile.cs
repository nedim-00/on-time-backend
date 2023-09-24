using AutoMapper;
using OnTime.Application.Reservations;
using OnTime.Application.Reservations.Commands;

namespace OnTime.Application;

public class ReservationMapperProfile : Profile
{
    public ReservationMapperProfile()
    {
        _ = CreateMap<Reservation, ReservationResponseDto>();

        _ = CreateMap<CreateReservationCommand, Reservation>();
    }
}
