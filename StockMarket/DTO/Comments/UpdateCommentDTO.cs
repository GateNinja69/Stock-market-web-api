using System.ComponentModel.DataAnnotations;

namespace StockMarket.DTO.Comments
{
    public class UpdateCommentDTO
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must be of 5 characters")]
        [MaxLength(280, ErrorMessage = "Title cannot be over 280 characters")]
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}
