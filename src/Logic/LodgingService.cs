using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Contracts;
using Entities;

namespace Logic
{
    public class LodgingService
    {
        private readonly ILodgingRepository _lodgingRepository;

        public LodgingService(ILodgingRepository lodgingRepository)
        {
            _lodgingRepository = lodgingRepository;
        }

        public async Task<IEnumerable<Lodging>> GetAllLodging(CancellationToken cancellation)
        {
            return await _lodgingRepository.GetAll(cancellation);
        }

        public async Task AddLodging(Lodging lodging, CancellationToken cancellation)
        {
            lodging.Id = await GenerateId(cancellation);
            await _lodgingRepository.Add(lodging, cancellation);
        }

        private async Task<int> GenerateId(CancellationToken cancellation)
        {
            IEnumerable<Lodging> lodgings = await _lodgingRepository.GetAll(cancellation);
            return lodgings.Any() ? lodgings.Last().Id + 1 : 0;
        }
    }
}
