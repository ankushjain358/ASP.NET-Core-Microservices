using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using eShop.OrderService.DTO;
using eShop.OrderService.Service;
using eShop.Utilities.WebApiUtility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eShop.OrderService.Controllers
{
    [Route("api/[controller]")]
    [ProducesResponseType(500, Type = typeof(BaseResponse))]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMessagingQueueManager _messagingQueueManager;

        public OrderController(IOrderService orderService, IMessagingQueueManager messagingQueueManager)
        {
            _orderService = orderService;
            _messagingQueueManager = messagingQueueManager;
        }

        [HttpPost]
        [Route("get-orders")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<OrderDTO>>))]
        public ApiResponse<List<OrderDTO>> GetOrders()
        {
            int userId = Convert.ToInt32(User.Claims.First(item => item.Type == ClaimTypes.NameIdentifier).Value);
            List<OrderDTO> orderList = _orderService.GetOrders(userId);
            return new ApiResponse<List<OrderDTO>>(orderList);
        }

        [HttpPost]
        [Route("create-recharge-order")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<RechargeOrderResponseDTO>))]
        public ApiResponse<RechargeOrderResponseDTO> CreateRechargeOrder(RechargeOrderRequestDTO request)
        {
            request.UserId = Convert.ToInt32(User.Claims.First(item => item.Type == ClaimTypes.NameIdentifier).Value);

            // 1. Create order
            RechargeOrderResponseDTO response = _orderService.CreateRechargeOrder(request);

            // 2. Send message in Recharge Queue
            _messagingQueueManager.PublishMessageForMobileRecharge(response.OrderId, request.RechargeAmount, request.RechargeProviderId);

            return new ApiResponse<RechargeOrderResponseDTO>(response);
        }
    }
}