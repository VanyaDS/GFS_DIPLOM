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
    public class RecordRepository : GenericRepository<Record>, IRecordRepository
    {
        public RecordRepository(ApplicationDbContext context, ILogger logger) : base(context, logger) { }

        public override async Task<IEnumerable<Record>> All()
        {
            try
            {
                return await dbSet.Include(flat => flat.Flat)
                                  .ThenInclude(flatGeo => flatGeo.Geolocation).
                                  ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(RecordRepository));
                return new List<Record>();
            }
        }
        public override async Task<IEnumerable<Record>> FindAllAsync(Expression<Func<Record, bool>> predicate)
        {
            return await dbSet.Include(flat => flat.Flat)
                              .ThenInclude(flatGeo => flatGeo.Geolocation)
                              .Where(predicate).ToListAsync();
        }
        public override async Task<Record> GetById(int id)
        {
            try
            {
                return await dbSet.Include(flat => flat.Flat)
                                  .ThenInclude(flatGeo => flatGeo.Geolocation)
                                  .Where(record => record.Id == id)
                                  .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetById function error", typeof(RecordRepository));
                return null;
            }
        }
        public override async Task<bool> Update(Record entity)
        {
            try
            {
                var existingRecord = await dbSet.Include(flat => flat.Flat)
                                                .ThenInclude(flatGeo => flatGeo.Geolocation)
                                                .Where(x => x.Id == entity.Id)
                                                .FirstOrDefaultAsync();

                existingRecord.PicturesPath = entity.PicturesPath;
                existingRecord.WithoutChildren = entity.WithoutChildren;
                existingRecord.HasFurniture = entity.HasFurniture;
                existingRecord.Description = entity.Description;
                existingRecord.ForDay = entity.ForDay;
                existingRecord.NotForStudents = entity.NotForStudents;
                existingRecord.WithoutAnimals = entity.WithoutAnimals;
                existingRecord.RecordTitle = entity.RecordTitle;
                existingRecord.RentStatus = entity.RentStatus;
                existingRecord.IsAgent = entity.IsAgent;
                existingRecord.WithInternet = entity.WithInternet;
                existingRecord.Price = entity.Price;
                existingRecord.RentType = entity.RentType;
                existingRecord.Flat.Area = entity.Flat.Area;
                existingRecord.Flat.RoomNumber = entity.Flat.RoomNumber;
                existingRecord.Flat.Floor = entity.Flat.Floor;
                existingRecord.Flat.Geolocation.StreetName = entity.Flat.Geolocation.StreetName;
                existingRecord.Flat.Geolocation.CityName = entity.Flat.Geolocation.CityName;
                existingRecord.Flat.Geolocation.HouseNumber = entity.Flat.Geolocation.HouseNumber;
                existingRecord.PublicationDate = DateTime.Now;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Upsert function error", typeof(RecordRepository));
                return false;
            }
        }
        public override async Task<bool> Delete(int id)
        {
            try
            {
                var exist = await dbSet.Include(flat => flat.Flat)
                                                .ThenInclude(flatGeo => flatGeo.Geolocation)
                                                .Where(x => x.Id == id)
                                                .FirstOrDefaultAsync();

                if (exist is null)
                {
                    return false;
                }

                dbSet.Remove(exist);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Delete function error", typeof(RecordRepository));
                return false;
            }
        }
    }
}
