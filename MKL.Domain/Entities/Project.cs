using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MKL.Domain.Entities
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double? Progress { get; set; } // New field for progress tracking
        public List<Tasks>? Tasks { get; set; } = new List<Tasks>();
        public List<Comment>? Comments { get; set; } = new List<Comment>(); // New field for comments
        public List<Attachments>? Attachments { get; set; } = new List<Attachments>();
    }
}
