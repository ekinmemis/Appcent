using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Appcent.Core;

using Microsoft.EntityFrameworkCore;

namespace Appcent.Data
{
    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbContext _context;

        private DbSet<TEntity> _entities;

        public EfRepository(DbContext context)
        {
            _context = context;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Entities.ToList();
        }

        public TEntity GetById(int id)
        {
            if (id == 0)
                throw new ArgumentNullException(nameof(id));

            return Entities.Find(id);
        }

        public void Insert(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            Entities.Add(entity);
            _context.SaveChanges();
        }

        public void Insert(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            Entities.AddRange(entities);
            _context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Update(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentException(nameof(entities));

            Entities.UpdateRange(entities);
            _context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            Entities.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            Entities.RemoveRange(entities);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Entities.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            if (id == 0)
                throw new ArgumentNullException(nameof(id));

            return await Entities.FindAsync(id);
        }

        public async Task InsertAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            await Entities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task InsertAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            await Entities.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            Entities.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual IQueryable<TEntity> Table
        {
            get
            {
                return this.Entities;
            }
        }

        public virtual IQueryable<TEntity> TableNoTracking
        {
            get
            {
                return this.Entities.AsNoTracking();
            }
        }

        protected virtual DbSet<TEntity> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<TEntity>();
                return _entities;
            }
        }
    }
}