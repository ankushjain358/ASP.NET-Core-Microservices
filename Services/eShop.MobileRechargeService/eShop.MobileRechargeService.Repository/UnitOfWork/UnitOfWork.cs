using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.MobileRechargeService.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private eShopMobileRechargesContext _context;
        private GenericRepository<Providers> _providerRepository;
        private GenericRepository<RechargeOrders> _rechargeOrderRepositoryRepository;


        public UnitOfWork(eShopMobileRechargesContext context)
        {
            _context = context;
        }

        public IGenericRepository<RechargeOrders> RechargeOrderRepository
        {
            get
            {
                if (this._rechargeOrderRepositoryRepository == null)
                {
                    this._rechargeOrderRepositoryRepository = new GenericRepository<RechargeOrders>(_context);
                }
                return _rechargeOrderRepositoryRepository;
            }
        }

        public IGenericRepository<Providers> ProviderRepository
        {
            get
            {
                if (this._providerRepository == null)
                {
                    this._providerRepository = new GenericRepository<Providers>(_context);
                }
                return _providerRepository;
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
