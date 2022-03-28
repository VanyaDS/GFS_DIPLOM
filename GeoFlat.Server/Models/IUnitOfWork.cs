using GeoFlat.Server.Models.Repositories;
using System.Threading.Tasks;

namespace GeoFlat.Server.Models
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        Task CompleteAsync();
    }
}
