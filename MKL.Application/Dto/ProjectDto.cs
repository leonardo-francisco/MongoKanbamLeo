using MKL.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKL.Application.Dto
{
    public class ProjectDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double? Progress { get; set; } // New field for progress tracking
        public List<TasksDto>? Tasks { get; set; } = new List<TasksDto>();
        public List<CommentDto>? Comments { get; set; } = new List<CommentDto>(); // New field for comments
        public List<AttachmentsDto>? Attachments { get; set; } = new List<AttachmentsDto>();
    }
}
