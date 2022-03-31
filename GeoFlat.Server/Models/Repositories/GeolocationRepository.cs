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
    public class GeolocationRepository : GenericRepository<Geolocation>, IGeolocationRepository
    {
        public GeolocationRepository(ApplicationDbContext context, ILogger logger) : base(context, logger) { }

        public override async Task<IEnumerable<Geolocation>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(GeolocationRepository));
                return new List<Geolocation>();
            }
        }

        public override async Task<bool> Update(Geolocation entity)
        {
            try
            {
                var existingGeolocation = await dbSet.Where(x => x.Id == entity.Id)
                                                    .FirstOrDefaultAsync();

                if (existingGeolocation == null)
                    return await Add(entity);

                existingGeolocation.HouseNumber = entity.HouseNumber;
                existingGeolocation.StreetName = entity.StreetName;
                existingGeolocation.CityName = entity.CityName;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Upsert function error", typeof(GeolocationRepository));
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
                _logger.LogError(ex, "{Repo} Delete function error", typeof(GeolocationRepository));
                return false;
            }
        }
    }
}
