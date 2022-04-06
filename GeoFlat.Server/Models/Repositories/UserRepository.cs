using GeoFlat.Server.Helpers;
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
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context, ILogger logger) : base(context, logger) { }
        public override async Task<IEnumerable<User>> All()
        {
            try
            {
                return await dbSet.Include(acc => acc.Account)
                    .Where(acc => acc.Account.Role != RoleHealper.ADMIN)
                                              .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(UserRepository));
                return new List<User>();
            }
        }
        public override async Task<User> GetById(int id)
        {
            try
            {
                return await dbSet.Include(acc => acc.Account)
                                  .Where(user => user.Id == id)
                                  .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetById function error", typeof(UserRepository));
                return null;
            }
        }
        public override async Task<bool> Update(User entity)
        {
            try
            {
                var existingUser = await dbSet.Include(acc => acc.Account)
                                                .Where(x => x.Id == entity.Id)
                                                .FirstOrDefaultAsync();

                existingUser.Name = entity.Name;
                existingUser.Surname = entity.Surname;
                existingUser.PhoneNumber = entity.PhoneNumber;
                existingUser.Account.Password =HashingMD5.GetHashStringMD5(entity.Account.Password);
                existingUser.Account.Email = entity.Account.Email;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Upsert function error", typeof(UserRepository));
                return false;
            }
        }
        public override async Task<bool> Delete(int id)
        {
            try
            {
                var exist = await dbSet.Include(acc => acc.Account)
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
                _logger.LogError(ex, "{Repo} Delete function error", typeof(UserRepository));
                return false;
            }
        }
    }
}
