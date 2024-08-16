using System.ComponentModel.DataAnnotations;

namespace StockMarket.DTO.Account
{
    public class LoginDTO
    {
        [Required]
        public string? UserName {  get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
