using StockMarket.Model;

namespace StockMarket.Repository
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllComment();


        Task<Comment> GetComment(int id);
        Task<Comment> CreateComment(Comment comment);
        Task<Comment> UpdateComment(int id, Comment comment);
        Task<Comment?> DeleteComment(int id);
    }
}
