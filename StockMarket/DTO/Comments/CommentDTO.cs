using System.ComponentModel.DataAnnotations;

namespace StockMarket.DTO.Comments
{
    public class CommentDTO
    {
        

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int StockId { get; set; }
        
    }
}
