using System.ComponentModel.DataAnnotations;

namespace StockMarket.DTO.Stock
{
    public class UpdateDTO
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(10, ErrorMessage = "Symbol cannot be over 10 characters")]
        public string Symbol { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(10, ErrorMessage = "Comapany Name cannot be over 10 character")]
        public string CompanyName { get; set; } = string.Empty;
        
        [Required]
        [Range(1,10000000000000)]
        public decimal Purchase { get; set; }
        
        [Required]
        [Range(1, 50000000000000)]
        public long MarketCap { get; set; }
        
        [Required]
        [MaxLength(10, ErrorMessage = "Industry cannot be over 10 character")]
        public string Industry { get; set; } = string.Empty;

        [Required]
        [Range(0.001, 100)]
        public decimal LastDiv { get; set; }
    }
}
