using System.ComponentModel.DataAnnotations.Schema;

namespace MKL.Web.Models
{
    public class RegisterViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string EmailUser { get; set; }
        public string UserPassword { get; set; }
        
    }
}
