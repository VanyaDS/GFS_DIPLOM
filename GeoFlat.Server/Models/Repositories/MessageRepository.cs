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
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        public MessageRepository(ApplicationDbContext context, ILogger logger) : base(context, logger) { }

        public override async Task<IEnumerable<Message>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(MessageRepository));
                return new List<Message>();
            }
        }

        public override async Task<bool> Update(Message entity)
        {
            try
            {
                var existingMessage = await dbSet.Where(x => x.Id == entity.Id)
                                                    .FirstOrDefaultAsync();

                existingMessage.IsRead = entity.IsRead;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Upsert function error", typeof(MessageRepository));
                return false;
            }
        }
        public override async Task<IEnumerable<Message>> FindAllAsync(Expression<Func<Message, bool>> predicate)
        {
            return await dbSet.Include(res => res.UserRecipient)
                              .ThenInclude(acc => acc.Account)
                              .Include(send => send.UserSender)
                              .ThenInclude(acc => acc.Account)
                              .Where(predicate).ToListAsync();
        }
        public override async Task<Message> GetById(int id)
        {
            try
            {
                return await dbSet.Include(res => res.UserRecipient)
                                  .ThenInclude(acc => acc.Account)
                                  .Include(send => send.UserSender)
                                  .ThenInclude(acc => acc.Account)
                                  .Where(mes => mes.Id == id)
                                  .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetById function error", typeof(MessageRepository));
                return null;
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
                _logger.LogError(ex, "{Repo} Delete function error", typeof(MessageRepository));
                return false;
            }
        }
    }
}
