using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Appcent.Core;

namespace Appcent.Data
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        IEnumerable<TEntity> GetAll();

        TEntity GetById(int id);

        void Insert(TEntity entity);

        void Insert(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Update(IEnumerable<TEntity> entities);

        void Delete(TEntity entity);

        void Delete(IEnumerable<TEntity> entities);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> GetByIdAsync(int id);

        Task InsertAsync(TEntity entity);

        Task InsertAsync(IEnumerable<TEntity> entities);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);

        IQueryable<TEntity> Table { get; }

        IQueryable<TEntity> TableNoTracking { get; }
    }
}