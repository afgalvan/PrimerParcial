using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Utils
{
    public interface IFileContentMapper
    {
        public Task<IEnumerable<TEntity>> MapFileContent<TEntity>(string filePath);
    }
}
