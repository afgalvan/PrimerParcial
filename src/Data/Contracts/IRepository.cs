using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Entities;

namespace Data.Contracts
{
    public interface IRepository<TEntity>
    {
        public Task Add(TEntity entity, CancellationToken cancellation);
        public Task<IEnumerable<Lodging?>> GetAll(CancellationToken cancellation);
        Task<Lodging?> GetById(int id, CancellationToken cancellation);
        public Task RemoveById(int id, CancellationToken cancellation);
    }
}
