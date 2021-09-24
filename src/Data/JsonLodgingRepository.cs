using Data.Contracts;
using Data.Utils;
using Entities;
using Microsoft.Extensions.Configuration;

namespace Data
{
    public class JsonLodgingRepository : JsonRepository<Lodging>, ILodgingRepository
    {
        public JsonLodgingRepository(IConfiguration configuration, IFileUpdater fileUpdater,
            IFileContentMapper fileMapper) : base(configuration["Persistence:Lodging"], fileUpdater,
            fileMapper)
        {
        }
    }
}
