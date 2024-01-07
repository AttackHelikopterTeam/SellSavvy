using System;
using Microsoft.EntityFrameworkCore;
using SellSavvy.Application;
using SellSavvy.Domain.Common;
using SellSavvy.Persistence.Contexts;

namespace SellSavvy.Persistence
{
    public class WriteRepository<T, TKey> : IWriteRepository<T, TKey> where T : EntityBase<TKey>

    {
        private readonly SellSavvyIdentityContext _context;

        public WriteRepository(SellSavvyIdentityContext context)
        {
            _context = context;
        }

        protected DbSet<T> Table => _context.Set<T>();
        protected SellSavvyIdentityContext Context => _context;

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public async Task RemoveAsync(TKey id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity != null)
                _context.Set<T>().Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }


    }
}

