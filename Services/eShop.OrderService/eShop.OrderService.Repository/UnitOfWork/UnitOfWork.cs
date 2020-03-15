using eShop.OrderService.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.AccountService.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private eShopOrdersContext _context;
        private GenericRepository<Orders> _orderRepository;
        private GenericRepository<ServiceTypeEnumrations> _serviceTypeRepository;


        public UnitOfWork(eShopOrdersContext context)
        {
            _context = context;
        }

        public IGenericRepository<Orders> OrderRepository
        {
            get
            {
                if (this._orderRepository == null)
                {
                    this._orderRepository = new GenericRepository<Orders>(_context);
                }
                return _orderRepository;
            }
        }

        public IGenericRepository<ServiceTypeEnumrations> ServiceTypeRepository
        {
            get
            {
                if (this._serviceTypeRepository == null)
                {
                    this._serviceTypeRepository = new GenericRepository<ServiceTypeEnumrations>(_context);
                }
                return _serviceTypeRepository;
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
