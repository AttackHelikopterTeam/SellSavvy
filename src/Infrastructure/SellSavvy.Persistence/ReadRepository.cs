using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SellSavvy.Domain.Common;
using SellSavvy.Application;
using SellSavvy.Persistence.Contexts;

namespace SellSavvy.Persistence
{
    public class ReadRepository<T, TKey> : IReadRepository<T, TKey> where T : EntityBase<TKey>
    {
        private readonly SellSavvyIdentityContext _context;

        public ReadRepository(SellSavvyIdentityContext context)
        {
            _context = context;
        }

        private DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> metot, bool tracking)
        {
            var query = Table.Where(metot).AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return query;
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate, bool tracking)
        {
            var query = Table.Where(predicate).AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return await query.ToListAsync();
        }

        public async Task<T> GetById(TKey id, bool tracking)
        {
            var entity = await Table.FindAsync(id);
            if (entity != null && !tracking)
                _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }
    }
}