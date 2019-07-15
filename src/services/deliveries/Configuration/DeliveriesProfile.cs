using AutoMapper;
using deliveries.Controllers.Dto;
using deliveries.Domain;

namespace deliveries.Configuration
{
    public class DeliveriesProfile: Profile
    {
        public DeliveriesProfile()
        {
            CreateMap<Delivery, DeliveryDto>()
                .ForMember(d => d.From, m => m.MapFrom(s =>s.FromAddress))
                .ForMember(d => d.To, m => m.MapFrom(s => s.ToAddress));
            CreateMap<Address, AddressDto>();
        }
    }
}
