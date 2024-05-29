using MKL.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKL.Application.Dto
{
    public class AttachmentsDto
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public Guid ProjectId { get; set; }
        
        public Guid UserId { get; set; }
        
    }
}
