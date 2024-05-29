using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MKL.Application.Dto
{
    public class TasksDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        //public MKL.Domain.Enums.TaskStatus Status { get; set; }
        public string Status { get; set; }
        public DateTime DueDate { get; set; }
        public double Progress { get; set; } // New field for progress tracking
        public Guid ProjectId { get; set; }
    }
}
