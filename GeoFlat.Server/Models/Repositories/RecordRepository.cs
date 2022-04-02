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
    public class RecordRepository : GenericRepository<Record>, IRecordRepository
    {
        public RecordRepository(ApplicationDbContext context, ILogger logger) : base(context, logger) { }

        public override async Task<IEnumerable<Record>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(RecordRepository));
                return new List<Record>();
            }
        }

        public override async Task<bool> Update(Record entity)
        {
            try
            {
                var existingRecord = await dbSet.Where(x => x.Id == entity.Id)
                                                    .FirstOrDefaultAsync();

                if (existingRecord == null)
                    return await Add(entity);

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
                existingRecord.PublicationDate = entity.PublicationDate;
                existingRecord.FlatId = entity.FlatId;
                existingRecord.RentType = entity.RentType;
                existingRecord.UserId = entity.UserId;

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
                var exist = await dbSet.Where(x => x.Id == id)
                                        .FirstOrDefaultAsync();

                if (exist == null) return false;

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
