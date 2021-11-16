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
        private readonly string[] _availableGuestTypes = { "Particular", "Miembro", "Premium" };
        private readonly string[] _availableRooms = { "Familiar", "Sencilla", "Doble", "Suite", };

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

            await _lodgingRepository.Add(lodging, cancellation);
        }

        [RequiredReturn(typeof(Lodging), ErrorMessage = "Liquidación no encontrada.")]
        private async Task<Lodging> GetLodgingById(int id, CancellationToken cancellation)
        {
            return await _lodgingRepository.GetById(id, cancellation);
        }

        public async Task DeleteById(int id, CancellationToken cancellation)
        {
            await GetLodgingById(id, cancellation);
            await _lodgingRepository.RemoveById(id, cancellation);
        }

        public IEnumerable<string> GetAvailableGuestTypes()
        {
            return _availableGuestTypes;
        }

        public IEnumerable<string> GetAvailableRooms()
        {
            return _availableRooms;
        }
    }
}
