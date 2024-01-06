using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SellSavvy.Domain.Common;
using SellSavvy.Application;

using SellSavvy.Persistence.Contexts;

namespace SellSavvy.Persistence
{
    public class ReadRepository<T, Tkey> : SellSavvy.Domain.Common.IReadRepository<T, Tkey> where T : EntityBase<Tkey>
    {
        private readonly SellSavvyIdentityContext _context;

        public ReadRepository(SellSavvyIdentityContext context)
        {
            _context = context;
        }

        private DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll()
        {
            return Table.AsQueryable();
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate)
        {
            return Table.Where(predicate).AsQueryable();
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate, bool trackChanges)
        {
            return trackChanges ? Table.Where(predicate).AsQueryable() : Table.AsNoTracking().Where(predicate).AsQueryable();
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate, bool trackChanges)
        {
            return trackChanges ? Table.Where(predicate).AsQueryable() : Table.AsNoTracking().Where(predicate).AsQueryable();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await Table.FirstOrDefaultAsync(predicate);
        }

        public async Task<T?> GetByIdAsync(Tkey id)
        {
            if (typeof(T).GetProperty("Id").PropertyType == typeof(Guid))
            {
                // Assuming Id is of type Guid
                return await Table.FirstOrDefaultAsync(x => x.Id.ToString() == id.ToString());
            }
            else if (typeof(T).GetProperty("Id").PropertyType == typeof(int))
            {
                // Assuming Id is of type int
                if (int.TryParse(id.ToString(), out int parsedId))
                {
                    return await Table.FirstOrDefaultAsync(x => x.Id.Equals(parsedId));
                }
            }

            // Handle other types of Id if necessary
            return null;
        }
    }
}

