using System.ComponentModel.DataAnnotations;

namespace MKL.Web.Models
{
    public class ProjectViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O nome do projeto é obrigatório")]
        public string Name { get; set; }
        [Required(ErrorMessage = "A descrição do projeto é obrigatório")]
        public string Description { get; set; }
        [Required(ErrorMessage = "A data de início do projeto é obrigatório")]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
