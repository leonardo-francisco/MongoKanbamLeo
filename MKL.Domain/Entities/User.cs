using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MKL.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string EmailUser { get; set; }
        public string UserPassword { get; set; }
        [NotMapped]
        public string Role { get; set; }
    }
}
