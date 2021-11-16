using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Data.Contracts;
using Entities;
using Entities.Factories;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class MsSqlLodgingRepository : ILodgingRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbConnection         _dbConnection;

        public MsSqlLodgingRepository(ApplicationDbContext context, DbConnection dbConnection)
        {
            _context      = context;
            _dbConnection = dbConnection;
        }

        public async Task Add(Lodging entity, CancellationToken cancellation)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync(
                $"INSERT INTO Lodgings (PeopleAmount, EntryDate, ExitDate, RoomCapacity, GuestType) VALUES ({entity.PeopleAmount}, {entity.EntryDate}, {entity.ExitDate}, {(byte)entity.RoomCapacity}, {entity.GuestType})",
                cancellation);
            await _context.SaveChangesAsync(cancellation);
        }

        public async Task<IEnumerable<Lodging?>> GetAll(CancellationToken cancellation)
        {
            await using DbCommand command = _dbConnection.CreateCommand();
            command.CommandText =
                "SELECT Id, PeopleAmount, EntryDate, ExitDate, RoomCapacity, GuestType FROM Lodgings";

            return await RetrieveAllData(command, cancellation);
        }

        private async Task<IEnumerable<Lodging?>> RetrieveAllData(DbCommand command,
            CancellationToken cancellation)
        {
            IList<Lodging?> lodgings = new List<Lodging?>();
            await _dbConnection.OpenAsync(cancellation);
            await using DbDataReader dbDataReader = await command.ExecuteReaderAsync(cancellation);
            while (await dbDataReader.ReadAsync(cancellation))
            {
                lodgings.Add(MapLodging(dbDataReader));
            }

            await _dbConnection.CloseAsync();
            return lodgings;
        }

        private static Lodging CreateLodging(string type) =>
            type.ToLower(CultureInfo.CurrentCulture) switch
            {
                "particular" => new GeneralLodging(),
                "socio" => new FellowLodging(),
                _ => new PremiumLodging()
            };

        private Lodging? MapLodging(IDataRecord dbDataReader)
        {
            Lodging lodging = CreateLodging(dbDataReader.GetString(5));
            lodging.Id           = dbDataReader.GetInt32(0);
            lodging.PeopleAmount = dbDataReader.GetInt32(1);
            lodging.EntryDate    = dbDataReader.GetDateTime(2);
            lodging.ExitDate     = dbDataReader.GetDateTime(3);
            lodging.RoomCapacity = RoomCapacityFactory.CreateRoomOfIndex(dbDataReader.GetByte(4));

            return lodging;
        }

        public async Task<Lodging?> GetById(int id, CancellationToken cancellation)
        {
            await using DbCommand command = _dbConnection.CreateCommand();
            command.CommandText =
                "SELECT Id, PeopleAmount, EntryDate, ExitDate, RoomCapacity, GuestType FROM Lodgings " +
                "WHERE Id = @Id";
            CreateDbParameter(command, "@Id", id);
            await _dbConnection.OpenAsync(cancellation);
            await using DbDataReader dbDataReader = await command.ExecuteReaderAsync(cancellation);
            return await dbDataReader.ReadAsync(cancellation) ? MapLodging(dbDataReader) : null;
        }

        private void CreateDbParameter(DbCommand command, string parameterName, object value)
        {
            DbParameter dbParameter = command.CreateParameter();
            dbParameter.ParameterName = parameterName;
            dbParameter.Value         = value;
            command.Parameters.Add(dbParameter);
        }

        public async Task RemoveById(int id, CancellationToken cancellation)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync(
                $"DELETE FROM Lodgings WHERE Id = {id}", cancellation);
            await _context.SaveChangesAsync(cancellation);
        }
    }
}
