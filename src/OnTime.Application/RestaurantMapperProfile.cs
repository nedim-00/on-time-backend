using AutoMapper;
using OnTime.Application.Restaurants;
using OnTime.Application.Restaurants.Commands;

namespace OnTime.Application;

public class RestaurantMapperProfile : Profile
{
    public RestaurantMapperProfile()
    {
        _ = CreateMap<Restaurant, RestaurantResponseDto>();
        _ = CreateMap<Restaurant, BasicRestaurantInformationResponseDto>();
        _ = CreateMap<RestaurantResponseDto, BasicRestaurantInformationResponseDto>();
        _ = CreateMap<CreateRestaurantCommand, Restaurant>();
        _ = CreateMap<UpdateRestaurantCommand, Restaurant>();
    }
}
