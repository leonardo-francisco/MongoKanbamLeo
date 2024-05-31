using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MKL.Web.Models
{
    public class RegisterViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O nome do usuário é obrigatório")]
        public string Name { get; set; }
        [Required(ErrorMessage = "O e-mail do usuário é obrigatório")]
        public string EmailUser { get; set; }
        [Required(ErrorMessage = "A senha do usuário é obrigatório")]
        public string UserPassword { get; set; }
        
    }
}
