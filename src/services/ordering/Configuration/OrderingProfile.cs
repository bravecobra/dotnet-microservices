using AutoMapper;
using ordering.Controllers.Dto;
using ordering.Domain;

namespace ordering.Configuration
{
    public class OrderingProfile: Profile
    {
        public OrderingProfile()
        {
            CreateMap<Order, OrderDto>();
        }
    }
}
