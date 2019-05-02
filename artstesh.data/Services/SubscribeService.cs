using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using artstesh.data.Converters;
using artstesh.data.Models;
using artstesh.data.Repositories;

namespace artstesh.data.Services
{
    public class SubscribeService : ISubscribeService
    {
        private readonly ISubscribeRepository _repository;

        public SubscribeService(ISubscribeRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Add(SubscribeModel subscribe)
        {
            subscribe.BeginDate = DateTime.Now;
            subscribe.IsActive = true;
            subscribe.Secret = Guid.NewGuid().ToString("N");
            return await _repository.Add(subscribe.FromModel());
        }

        public async Task<List<SubscribeModel>> Get()
        {
            var result = await _repository.Get();
            return result.Select(e => e.ToModel()).ToList();
        }

        public async Task<bool> Unsubscribe(string secret)
        {
            return await _repository.Unsubscribe(secret);
        }
    }
}