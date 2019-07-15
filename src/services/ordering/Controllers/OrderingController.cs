using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ordering.Controllers.Dto;
using ordering.Persistence;

namespace ordering.Controllers
{ 
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IMapper _mapper;

        public OrdersController(IOrdersRepository ordersRepository, IMapper mapper)
        {
            _ordersRepository = ordersRepository ?? throw new ArgumentNullException(nameof(ordersRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [Route("api/clients/{clientId:guid}/orders")]
        public async Task<IActionResult> GetOrders([FromRoute]Guid clientId)
        {
            if (clientId == Guid.Empty)
            {
                throw new ArgumentException(nameof(clientId));
            }

            var orders = await _ordersRepository.GetClientOrders(clientId);

            return Ok(_mapper.Map<IEnumerable<OrderDto>>(orders));
        }

        [HttpGet("api/orders/{orderId:guid}")]
        public async Task<IActionResult> Get([FromRoute]Guid orderId)
        {
            if (orderId == Guid.Empty)
            {
                throw new ArgumentException(nameof(orderId));
            }

            var order = await _ordersRepository.GetOrder(orderId);

            return Ok(_mapper.Map<OrderDto>(order));
        }
    }
}