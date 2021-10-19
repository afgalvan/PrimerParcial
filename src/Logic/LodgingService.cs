using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Contracts;
using Entities;
using Logic.Exceptions;
using Logic.Filters;

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

        [RequiredArguments(ErrorMessage = "Datos de liquidación inválidos.")]
        public async Task AddLodging(Lodging lodging, CancellationToken cancellation)
        {
            if (lodging.StayDays < 1)
            {
                throw new InvalidArgumentException("Días de estancia inválidos");
            }
            lodging.Id = await GenerateId(cancellation);
            await _lodgingRepository.Add(lodging, cancellation);
        }

        private async Task<int> GenerateId(CancellationToken cancellation)
        {
            IEnumerable<Lodging> lodgings = await _lodgingRepository.GetAll(cancellation);
            return lodgings.Any() ? lodgings.Last().Id + 1 : 0;
        }

        [RequiredReturn(typeof(Lodging), ErrorMessage = "Liquidación no encontrada.")]
        private async Task<Lodging> GetLodgingById(int id, CancellationToken cancellation)
        {
            return await _lodgingRepository.GetWhere(lodging => lodging.Id == id, cancellation);
        }

        public async Task DeleteById(int id, CancellationToken cancellation)
        {
            await GetLodgingById(id, cancellation);
            await _lodgingRepository.RemoveWhere(lodging => lodging.Id == id, cancellation);
        }

        public IEnumerable<string> GetAvailableGuestTypes()
        {
            return new[] { "Particular", "Miembro", "Premium" };
        }

        public IEnumerable<string> GetAvailableRooms()
        {
            return new[] { "Familiar", "Sencilla", "Doble", "Suite", };
        }
    }
}
