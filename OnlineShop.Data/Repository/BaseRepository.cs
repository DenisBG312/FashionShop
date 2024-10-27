using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Repository.Interfaces;

namespace OnlineShop.Data.Repository
{
    public class BaseRepository<TType, TId> : IRepository<TType, TId> 
        where TType : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<TType> dbSet;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            dbSet = _context.Set<TType>();
        }
        public TType GetById(TId id)
        {
            throw new NotImplementedException();
        }

        public Task<TType> GetByIdAsync(TId id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TType> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TType>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public IQueryable<TType> GetAllAttached()
        {
            throw new NotImplementedException();
        }

        public void Add(TType item)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(TType item)
        {
            throw new NotImplementedException();
        }

        public bool Delete(TId id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(TId id)
        {
            throw new NotImplementedException();
        }

        public bool Update(TType item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(TType item)
        {
            throw new NotImplementedException();
        }
    }
}
