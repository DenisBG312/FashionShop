using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Models;
using OnlineShop.Data.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            TType entity = dbSet
                .Find(id);

            return entity;
        }

        public async Task<TType> GetByIdAsync(TId id)
        {
            TType entity = await dbSet
                .FindAsync(id);

            return entity;
        }

        public IEnumerable<TType> GetAll()
        {
            return this.dbSet.ToArray();
        }

        public async Task<IEnumerable<TType>> GetAllAsync()
        {
            return await this.dbSet.ToArrayAsync();
        }

        public IQueryable<TType> GetAllAttached()
        {
            return this.dbSet.AsQueryable();
        }

        public void Add(TType item)
        {
            dbSet.Add(item);
        }

        public async Task AddAsync(TType item)
        {
            await dbSet.AddAsync(item);
        }

        public bool Delete(TId id)
        {
            TType entity = GetById(id);

            if (entity == null)
            {
                return false;
            }

            dbSet.Remove(entity);

            return true;
        }

        public async Task<bool> DeleteAsync(TId id)
        {
            TType entity = await GetByIdAsync(id);

            if (entity == null)
            {
                return false;
            }

            dbSet.Remove(entity);

            return true;
        }

        public bool Update(TType item)
        {
            try
            {
                dbSet.Attach(item);
                _context.Entry(item).State = EntityState.Modified;

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(TType item)
        {
            try
            {
                dbSet.Attach(item);
                _context.Entry(item).State = EntityState.Modified;

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<List<SelectListItem>> GetGendersAsync()
        {
            return await _context.Genders
                .Select(g => new SelectListItem
                {
                    Value = g.Id.ToString(),
                    Text = g.Name
                }).ToListAsync();
        }

        public async Task<List<SelectListItem>> GetClothingTypesAsync()
        {
            return await _context.ClothingTypes
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToListAsync();
        }
    }
}
