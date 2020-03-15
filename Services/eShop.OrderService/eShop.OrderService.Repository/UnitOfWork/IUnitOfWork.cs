using eShop.OrderService.Repository;
using System;

namespace eShop.AccountService.Repository
{
    public interface IUnitOfWork
    {
        IGenericRepository<Orders> OrderRepository { get; }
        IGenericRepository<ServiceTypeEnumrations> ServiceTypeRepository { get; }
        void SaveChanges();
    }
}
