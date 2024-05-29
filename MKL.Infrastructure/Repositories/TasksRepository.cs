using MKL.Domain.Entities;
using MKL.Domain.Interfaces;
using MKL.Infrastructure.Persistence;
using MongoDB.Driver;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKL.Infrastructure.Repositories
{
    public class TasksRepository : ITasksRepository
    {
        private readonly IMongoCollection<Tasks> _tasksCollection;

        public TasksRepository(MongoDbContext context) 
        {
            _tasksCollection = context.GetCollection<Tasks>("Tasks");
        }

        public async Task CreateAsync(Tasks task)
        {
            await _tasksCollection.InsertOneAsync(task);
        }

        public async Task DeleteAsync(Guid id)
        {
            var filter = Builders<Tasks>.Filter.Eq(p => p.Id, id);
            await _tasksCollection.DeleteOneAsync(filter);
        }

        public async Task<IEnumerable<Tasks>> GetAllAsync()
        {
            return await _tasksCollection.Find(Builders<Tasks>.Filter.Empty).ToListAsync();
        }

        public async Task<Tasks> GetByIdAsync(Guid id)
        {
            var filter = Builders<Tasks>.Filter.Eq(p => p.Id, id);
            var task = await _tasksCollection.Find(filter).FirstOrDefaultAsync();
            return task;
        }

        public async Task<IEnumerable<Tasks>> GetByProjectIdAsync(Guid projectId)
        {
            var filter = Builders<Tasks>.Filter.Eq(p => p.ProjectId, projectId);
            var tasks = await _tasksCollection.Find(filter).ToListAsync();
            return tasks;
        }

        public async Task UpdateAsync(Tasks task)
        {
            var filter = Builders<Tasks>.Filter.Eq(p => p.Id, task.Id);
            await _tasksCollection.ReplaceOneAsync(filter, task);
        }
    }
}
