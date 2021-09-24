using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface IRepository<TEntity>
    {
        public Task Add(TEntity entity, CancellationToken cancellation);
        public Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellation);
        public Task RemoveWhere(Predicate<TEntity> predicate, CancellationToken cancellation);
        public Task<TEntity?> GetWhere(Func<TEntity, bool> predicate, CancellationToken cancellation);
    }
}
