using System;
using System.Collections.Generic;
using AutoMapper;
using eShop.AccountService.Repository;
using eShop.OrderService.DTO;
using System.Linq;
using eShop.OrderService.Repository;

namespace eShop.OrderService.Service
{
    public class OrderService : IOrderService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public RechargeOrderResponseDTO CreateRechargeOrder(RechargeOrderRequestDTO request)
        {
            var orderEntity = _mapper.Map<Orders>(request);
            _unitOfWork.OrderRepository.Insert(orderEntity);
            _unitOfWork.SaveChanges();

            return _mapper.Map<RechargeOrderResponseDTO>(orderEntity);
        }

        public List<OrderDTO> GetOrders(int userId)
        {
            List<Orders> orderList = _unitOfWork.OrderRepository.Get(item => item.UserId == userId).ToList();
            return _mapper.Map<List<OrderDTO>>(orderList);
        }
    }
}
