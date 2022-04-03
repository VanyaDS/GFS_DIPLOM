using GeoFlat.Server.Models.Database.Entities.Contexts;
using GeoFlat.Server.Models.Interfaces;
using GeoFlat.Server.Models.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace GeoFlat.Server.Models
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public IUserRepository Users { get; private set; }

        public IAccountRepository Accounts { get; private set; }

        public IComparisonRepository Comparisons { get; private set; }

        public IFavoriteRepository Favorites { get; private set; }

        public IFlatRepository Flats { get; private set; }

        public IGeolocationRepository Geolocations { get; private set; }

        public IMessageRepository Messages { get; private set; }

        public IRecordRepository Records { get; private set; }

        public UnitOfWork(ApplicationDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");

            Users = new UserRepository(context, _logger);
            Accounts = new AccountRepository(context, _logger);
            Comparisons = new ComparisonRepository(context, _logger);
            Favorites = new FavoriteRepository(context, _logger);
            Flats = new FlatRepository(context, _logger);
            Geolocations = new GeolocationRepository(context, _logger);
            Messages = new MessageRepository(context, _logger);
            Records = new RecordRepository(context, _logger);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
