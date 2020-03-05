using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.AccountService.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private eShopAccountsContext _context;
        private GenericRepository<Users> _userRepository;

        public UnitOfWork(eShopAccountsContext context)
        {
            _context = context;
        }

        public IGenericRepository<Users> UserRepository
        {
            get
            {
                if (this._userRepository == null)
                {
                    this._userRepository = new GenericRepository<Users>(_context);
                }
                return _userRepository;
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
