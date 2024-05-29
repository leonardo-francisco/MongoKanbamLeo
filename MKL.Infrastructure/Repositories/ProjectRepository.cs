using MKL.Domain.Entities;
using MKL.Domain.Interfaces;
using MKL.Infrastructure.Persistence;
using MongoDB.Driver;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MKL.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IMongoCollection<Project> _projectCollection;
        private readonly IMongoCollection<Tasks> _tasksCollection;
        private readonly IMongoCollection<Comment> _commentCollection;
        private readonly IMongoCollection<Attachments> _attachmentCollection;

        public ProjectRepository(MongoDbContext context)
        {
            _projectCollection = context.GetCollection<Project>("Projects");
            _tasksCollection = context.GetCollection<Tasks>("Tasks");
            _commentCollection = context.GetCollection<Comment>("Comments");
            _attachmentCollection = context.GetCollection<Attachments>("Attachments");
        }

        public async Task CreateAsync(Project project)
        {
            await _projectCollection.InsertOneAsync(project);
        }

        public async Task DeleteAsync(Guid id)
        {
            var filter = Builders<Project>.Filter.Eq(p => p.Id, id);
            await _projectCollection.DeleteOneAsync(filter);
        }

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            var projects = await _projectCollection.Aggregate()
            .Lookup<Project, Tasks, Project>(
                _tasksCollection,
                p => p.Id,
                t => t.ProjectId,
                p => p.Tasks)
            .ToListAsync();

            return projects;
        }

        public async Task<Project> GetByIdAsync(Guid id)
        {
            var filter = Builders<Project>.Filter.Eq(p => p.Id, id);
            var project = await _projectCollection.Find(filter).FirstOrDefaultAsync();

            if (project != null)
            {
                var taskFilter = Builders<Tasks>.Filter.Eq(t => t.ProjectId, id);
                project.Tasks = await _tasksCollection.Find(taskFilter).ToListAsync();

                var commentFilter = Builders<Comment>.Filter.Eq(c => c.ProjectId, id);
                project.Comments = await _commentCollection.Find(commentFilter).ToListAsync();

                var attachmentFilter = Builders<Attachments>.Filter.Eq(a => a.ProjectId, id);
                project.Attachments = await _attachmentCollection.Find(attachmentFilter).ToListAsync();
            }

            return project;
        }

        public async Task UpdateAsync(Project project)
        {
            var filter = Builders<Project>.Filter.Eq(p => p.Id, project.Id);
            await _projectCollection.ReplaceOneAsync(filter, project);
        }
    }
}
