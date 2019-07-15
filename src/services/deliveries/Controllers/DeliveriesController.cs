using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using deliveries.Controllers.Dto;
using deliveries.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace deliveries.Controllers
{
    [ApiController]
    public class DeliveriesController : ControllerBase
    {
        private readonly IDeliveriesRepository _deliveriesRepository;
        private readonly IMapper _mapper;

        public DeliveriesController(IDeliveriesRepository deliveriesRepository,IMapper mapper)
        {
            _deliveriesRepository = deliveriesRepository ?? throw new ArgumentNullException(nameof(deliveriesRepository));
            _mapper = mapper;
        }

        [HttpGet("api/orders/{orderId:guid}/deliveries")]
        public async Task<IActionResult> GetOrderDeliveries([FromRoute]Guid orderId)
        {
            if (orderId == Guid.Empty)
            {
                throw new ArgumentException(nameof(orderId));
            }

            var deliveries = await _deliveriesRepository.GetOrderDeliveries(orderId);

            return Ok(_mapper.Map<IEnumerable<DeliveryDto>>(deliveries));
        }

        [HttpGet("api/deliveries/{deliveryId:guid}")]
        public async Task<IActionResult> GetById([FromRoute]Guid deliveryId)
        {
            if (deliveryId == Guid.Empty)
            {
                throw new ArgumentException(nameof(deliveryId));
            }

            var delivery = await _deliveriesRepository.GetDelivery(deliveryId);

            return Ok(_mapper.Map<DeliveryDto>(delivery));
        }
    }
}