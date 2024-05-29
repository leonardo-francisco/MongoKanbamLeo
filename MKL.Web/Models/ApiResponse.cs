using MKL.Application.Dto;

namespace MKL.Web.Models
{
    public class ApiResponse
    {
        public UserDto User { get; set; }
        public string Token { get; set; }
    }
}
