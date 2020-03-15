using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShop.OrderService.DTO;
using eShop.OrderService.Service;
using eShop.Utilities.WebApiUtility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eShop.OrderService.Controllers
{
    [Route("api/[controller]")]
    [ProducesResponseType(500, Type = typeof(BaseResponse))]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [Route("get-orders")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<OrderDTO>>))]
        public ApiResponse<List<OrderDTO>> GetOrders(int userId)
        {
            List<OrderDTO> orderList = _orderService.GetOrders(userId);
            return new ApiResponse<List<OrderDTO>>(orderList);
        }

        [HttpPost]
        [Route("create-recharge-order")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<RechargeOrderResponseDTO>))]
        public ApiResponse<RechargeOrderResponseDTO> CreateRechargeOrder(RechargeOrderRequestDTO request)
        {
            RechargeOrderResponseDTO response = _orderService.CreateRechargeOrder(request);
            //TODO: Send message in Recharge Queue
            return new ApiResponse<RechargeOrderResponseDTO>(response);
        }
    }
}

// Pending
// 1. Automapper profiles
// 2. Startup file changes
// 3. Ocelot API endpoints
// 4. ReadMe.md
// 5. Default entry in SQL Server tables
// 6. RabbitMQ