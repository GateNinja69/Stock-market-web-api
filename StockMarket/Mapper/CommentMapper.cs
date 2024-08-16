using StockMarket.DTO.Comments;
using StockMarket.Model;

namespace StockMarket.Mapper
{
    public static class CommentMapper
    {
        public static CommentDTO ToCommentDto(this Comment comment)
        {
            return new CommentDTO
            {
                Id = comment.Id,
                StockId = comment.StockId,
                Title = comment.Title,
                Content = comment.Content,
                CreatedDate = comment.CreatedDate
            };
        }

        public static Comment CreateCommentMapper(this CreateCommentDTO comment, int StockId)
        {
            return new Comment
            {
                Title = comment.Title,
                Content = comment.Content,
                StockId = StockId


            };

        }
        public static Comment UpdateCommentMapper(this UpdateCommentDTO comment)
        {
            return new Comment
            {
                Title = comment.Title,
                Content = comment.Content
                
            };
        }
    }
}
    