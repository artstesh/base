using System;
using System.Threading.Tasks;
using artstesh.data.Converters;
using artstesh.data.Models;
using artstesh.data.Repositories;

namespace artstesh.data.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _repository;

        public FeedbackService(IFeedbackRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Add(FeedbackModel model)
        {
            var entity = model.FromModel();
            entity.Created = DateTime.Now;
            return await _repository.Add(entity);
        }
    }
}