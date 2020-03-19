using eShop.OrderService.DTO;
using eShop.Utilities.SharedDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.OrderService.Service
{
    public interface IOrderService
    {
        List<OrderDTO> GetOrders(int userId);

        RechargeOrderResponseDTO CreateRechargeOrder(RechargeOrderRequestDTO request);

        void UpdateOrderStatusWithExternalId(OrderStatusMessageDTO orderStatus);
    }
}
