using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Models;
using OnlineShop.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Data.Repository
{
    public class ProductSizeRepository : IRepository<ProductSize, (int, int)>
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<ProductSize> _dbSet;

        public ProductSizeRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<ProductSize>();
        }

        public ProductSize GetById((int, int) id)
        {
            return _dbSet.Find(id.Item1, id.Item2);
        }

        public async Task<ProductSize> GetByIdAsync((int, int) id)
        {
            return await _dbSet.FindAsync(id.Item1, id.Item2);
        }

        public IEnumerable<ProductSize> GetAll()
        {
            return _dbSet.ToList();
        }

        public async Task<IEnumerable<ProductSize>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public IQueryable<ProductSize> GetAllAttached()
        {
            return _dbSet.AsQueryable();
        }

        public void Add(ProductSize item)
        {
            _dbSet.Add(item);
        }

        public async Task AddAsync(ProductSize item)
        {
            await _dbSet.AddAsync(item);
        }

        public bool Delete((int, int) id)
        {
            var entity = GetById(id);
            if (entity == null)
            {
                return false;
            }

            _dbSet.Remove(entity);
            return true;
        }

        public async Task<bool> DeleteAsync((int, int) id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
            {
                return false;
            }

            _dbSet.Remove(entity);
            return true;
        }

        public bool Update(ProductSize item)
        {
            try
            {
                _dbSet.Attach(item);
                _context.Entry(item).State = EntityState.Modified;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(ProductSize item)
        {
            try
            {
                _dbSet.Attach(item);
                _context.Entry(item).State = EntityState.Modified;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
