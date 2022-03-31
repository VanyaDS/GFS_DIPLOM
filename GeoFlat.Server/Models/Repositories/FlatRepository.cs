using GeoFlat.Server.Models.Database.Entities;
using GeoFlat.Server.Models.Database.Entities.Contexts;
using GeoFlat.Server.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoFlat.Server.Models.Repositories
{
    public class FlatRepository : GenericRepository<Flat>, IFlatRepository
    {
        public FlatRepository(ApplicationDbContext context, ILogger logger) : base(context, logger) { }

        public override async Task<IEnumerable<Flat>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(FlatRepository));
                return new List<Flat>();
            }
        }

        public override async Task<bool> Update(Flat entity)
        {
            try
            {
                var existingFlat = await dbSet.Where(x => x.Id == entity.Id)
                                                    .FirstOrDefaultAsync();

                if (existingFlat == null)
                    return await Add(entity);

                existingFlat.RoomNumber = entity.RoomNumber;
                existingFlat.Area = entity.Area;
                existingFlat.Floor = entity.Floor;
                existingFlat.GeolocationId = entity.GeolocationId;             

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Upsert function error", typeof(FlatRepository));
                return false;
            }
        }

        public override async Task<bool> Delete(int id)
        {
            try
            {
                var exist = await dbSet.Where(x => x.Id == id)
                                        .FirstOrDefaultAsync();

                if (exist == null) return false;

                dbSet.Remove(exist);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Delete function error", typeof(FlatRepository));
                return false;
            }
        }
    }
}
