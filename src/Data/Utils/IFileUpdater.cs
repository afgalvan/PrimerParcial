using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Utils
{
    public record UpdateContent<TEntity>(string FileName, IEnumerable<TEntity> Entities);

    public interface IFileUpdater
    {
        public Task UpdateFileWith<TEntity>(UpdateContent<TEntity> updateContent,
            CancellationToken cancellation);
    }
}
