using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKL.Domain.Entities
{
    public class Tasks
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime DueDate { get; set; }
        public double Progress { get; set; } // New field for progress tracking
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
