using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using deliveries.Domain;

namespace deliveries.Persistence.Impl
{
    public class DeliveriesRepository : IDeliveriesRepository
    {
        private static readonly Random _random = new Random();

        public async Task<IEnumerable<Delivery>> GetOrderDeliveries(Guid orderId)
        {
            var numberOfDeliveries = _random.Next(0, 10);
            var deliveries = new List<Delivery>(numberOfDeliveries);

            for (var i = 0; i < numberOfDeliveries; i++)
            {
                var deliveryId = Guid.NewGuid();
                var delivery = await CreateMockDelivery(orderId, deliveryId);

                deliveries.Add(delivery);
            }

            return deliveries;
        }

        public async Task<Delivery> GetDelivery(Guid id)
        {
            var delivery = await CreateMockDelivery(Guid.NewGuid(), id);

            return delivery;
        }

        private static Task<Delivery> CreateMockDelivery(Guid orderId, Guid deliveryId)
        {
            return Task.FromResult<Delivery>(new Delivery
            {
                Id = deliveryId,
                OrderId = orderId,
                Comment = "Random comment",
                PhoneNumber = "911",
                FromAddress = CreateMockAddress("Lithuania", "Kaunas", "", "", 0.0, 0.0),
                ToAddress = CreateMockAddress("Lithuania", "Vilnius", "", "", 0.0, 0.0)
            });
        }

        private static Address CreateMockAddress(string country, string city, string streetAddress, string zipCode,
            double latitude, double longitude)
        {
            return new Address
            {
                Id = Guid.NewGuid(),
                Country = country,
                City = city,
                StreetAddress = streetAddress,
                ZipCode = zipCode,
                Latitude = latitude,
                Longitude = longitude
            };
        }
    }
}