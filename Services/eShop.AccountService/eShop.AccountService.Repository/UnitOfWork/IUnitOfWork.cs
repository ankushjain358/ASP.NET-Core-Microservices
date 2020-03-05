using System;

namespace eShop.AccountService.Repository
{
    public interface IUnitOfWork
    {
        IGenericRepository<Users> UserRepository { get; }
        void SaveChanges();
    }
}
