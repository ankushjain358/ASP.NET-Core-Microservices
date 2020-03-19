using System;

namespace eShop.MobileRechargeService.Repository
{
    public interface IUnitOfWork
    {
        IGenericRepository<Providers> ProviderRepository { get; }
        IGenericRepository<RechargeOrders> RechargeOrderRepository { get; }

        void SaveChanges();
    }
}
