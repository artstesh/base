using System;
using System.Threading.Tasks;
using artstesh.data.DbContext;
using artstesh.data.Entities;

namespace artstesh.data.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly DataContext _context;

        public FeedbackRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(Feedback feedback)
        {
            try
            {
                _context.Feedbacks.Add(feedback);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}