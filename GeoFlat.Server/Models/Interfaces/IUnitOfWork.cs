using GeoFlat.Server.Models.Repositories;
using System.Threading.Tasks;

namespace GeoFlat.Server.Models.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IAccountRepository Accounts { get; }
        IComparisonRepository Comparisons { get; }
        IFavoriteRepository Favorites { get; }
        IFlatRepository Flats { get; }
        IGeolocationRepository Geolocations { get; }
        IMessageRepository Messages { get; }
        IRecordRepository Records { get; }
        IRoleRepository Roles { get; }
        Task CompleteAsync();
    }
}
