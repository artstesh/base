using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using artstesh.data.DbContext;
using artstesh.data.Entities;
using Microsoft.EntityFrameworkCore;

namespace artstesh.data.Repositories
{
    public class SubscribeRepository : ISubscribeRepository
    {
        private readonly DataContext _context;

        public SubscribeRepository(DataContext context)
        {
            _context = context;
        }
        
        public async Task<int> Add(Subscribe subscribe)
        {
            var sub = await _context.Subscribes.FirstOrDefaultAsync(e => e.Email.Equals(subscribe.Email));
            if (sub == null)
                _context.Subscribes.Add(subscribe);
            else
                sub.IsActive = true;
            await _context.SaveChangesAsync();
            return sub?.Id ?? subscribe.Id;
        }

        public async Task<List<Subscribe>> Get()
        {
            var result = await _context.Subscribes.Where(e => e.IsActive).ToListAsync();
            return result;
        }

        public async Task<bool> Unsubscribe(string secret)
        {
            var sub = await _context.Subscribes.FirstOrDefaultAsync(e => e.Secret.Equals(secret));
            if(sub == null)
                return false;
            sub.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}