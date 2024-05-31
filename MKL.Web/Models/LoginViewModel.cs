using System.ComponentModel.DataAnnotations;

namespace MKL.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O e-mail do usuário é obrigatório")]
        public string EmailUser { get; set; }
        [Required(ErrorMessage = "A senha do usuário é obrigatório")]
        public string UserPassword { get; set; }
    }
}
