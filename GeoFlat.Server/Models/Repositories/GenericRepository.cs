﻿using GeoFlat.Server.Models.Database.Entities.Contexts;
using GeoFlat.Server.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GeoFlat.Server.Models.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected ApplicationDbContext context;
        internal DbSet<T> dbSet;
        public readonly ILogger _logger;

        public GenericRepository(
            ApplicationDbContext context,
            ILogger logger)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
            _logger = logger;
        }

        public virtual async Task<T> GetById(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task<bool> Add(T entity)
        {
            await dbSet.AddAsync(entity);
            return true;
        }

        public virtual Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IEnumerable<T>> All()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.Where(predicate).ToListAsync();
        }

        public virtual Task<bool> Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
