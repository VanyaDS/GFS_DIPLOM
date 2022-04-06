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
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        public AccountRepository(ApplicationDbContext context, ILogger logger) : base(context, logger) { }
        public override async Task<IEnumerable<Account>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(AccountRepository));
                return new List<Account>();
            }
        }
        public override async Task<bool> Update(Account entity)
        {
            try
            {
                var existingAccount = await dbSet.Where(x => x.Id == entity.Id)
                                                    .FirstOrDefaultAsync();

                if (existingAccount == null)
                    return await Add(entity);

                existingAccount.Email = entity.Email;
                existingAccount.Role = entity.Role;
                existingAccount.Password = entity.Password;


                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Upsert function error", typeof(AccountRepository));
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
                _logger.LogError(ex, "{Repo} Delete function error", typeof(AccountRepository));
                return false;
            }
        }
    }
}
