using GeoFlat.Server.Models.Database.Entities;
using GeoFlat.Server.Models.Database.Entities.Contexts;
using GeoFlat.Server.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GeoFlat.Server.Models.Repositories
{
    public class FavoriteRepository : GenericRepository<Favorite>, IFavoriteRepository
    {
        public FavoriteRepository(ApplicationDbContext context, ILogger logger) : base(context, logger) { }

        public override async Task<IEnumerable<Favorite>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(FavoriteRepository));
                return new List<Favorite>();
            }
        }
        public override async Task<Favorite> GetById(int id)
        {
            try
            {
                return await dbSet.Include(rec => rec.Record)
                                  .ThenInclude(flat => flat.Flat)
                                  .ThenInclude(geo => geo.Geolocation)
                                  .Where(fav => fav.Id == id)
                                  .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetById function error", typeof(FavoriteRepository));
                return null;
            }
        }
        public override async Task<IEnumerable<Favorite>> FindAllAsync(Expression<Func<Favorite, bool>> predicate)
        {
            return await dbSet.Include( fav => fav.Record)
                              .ThenInclude( rec =>rec.Flat)
                              .ThenInclude(flat => flat.Geolocation)
                              .Where(predicate).ToListAsync();
        }

        public override async Task<bool> Update(Favorite entity)
        {
            try
            {
                var existingFavorite = await dbSet.Where(x => x.Id == entity.Id)
                                                    .FirstOrDefaultAsync();

                if (existingFavorite == null)
                    return await Add(entity);

                existingFavorite.RecordId = entity.RecordId;
                existingFavorite.UserId = entity.UserId;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Upsert function error", typeof(FavoriteRepository));
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
                _logger.LogError(ex, "{Repo} Delete function error", typeof(FavoriteRepository));
                return false;
            }
        }
    }
}
