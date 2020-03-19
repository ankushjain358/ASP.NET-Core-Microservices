using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using eShop.MobileRechargeService.DTO;
using eShop.MobileRechargeService.Repository;

namespace eShop.MobileRechargeService.Service
{
    public class MobileRechargeService : IMobileRechargeService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MobileRechargeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void DoRecharge(RechargeRequestDTO request, out int rechargeOrderId)
        {
            var rechargeOrderEntity = _mapper.Map<RechargeOrders>(request);
            _unitOfWork.RechargeOrderRepository.Insert(rechargeOrderEntity);
            _unitOfWork.SaveChanges();

            rechargeOrderId = rechargeOrderEntity.Id;
        }

        public List<ProviderDTO> GetProviderList()
        {
            var providerList = _unitOfWork.ProviderRepository.Get().ToList();
            return _mapper.Map<List<ProviderDTO>>(providerList);
        }

        public RechargeDetailResponseDTO GetRechargeDetail(int rechargeOrderId)
        {
            var rechargeOrderDetail = (from rechargeOrder in _unitOfWork.RechargeOrderRepository.Get()
                                       join provider in _unitOfWork.ProviderRepository.Get()
                                       on rechargeOrder.ProviderId equals provider.Id
                                       select new RechargeDetailResponseDTO
                                       {
                                           RechargeOrderId = rechargeOrder.Id,
                                           ProviderName = provider.Name,
                                           RechargeAmount = rechargeOrder.Amount
                                       }).FirstOrDefault();

            return rechargeOrderDetail;
        }
    }
}
