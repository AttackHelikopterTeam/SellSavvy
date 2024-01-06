using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SellSavvy.Domain.Common;

namespace SellSavvy.Application
{
    public interface IRepository<T, TKey> where T : EntityBase<TKey>
    {
        DbSet<T> Table { get; }
    }
}

