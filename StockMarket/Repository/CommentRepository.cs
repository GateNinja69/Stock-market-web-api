using Microsoft.EntityFrameworkCore;
using StockMarket.Data;
using StockMarket.Model;

namespace StockMarket.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext context;
        public CommentRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Comment> CreateComment(Comment comment)
        {
            await context.Comments.AddAsync(comment);
            await context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment> DeleteComment(int id)
        {
            var result = await context.Comments.FirstOrDefaultAsync(i => i.Id==id);
            if(result == null)
            {
                return null;
            }
            context.Comments.Remove(result);
            context.SaveChanges();
            return result;
        }

        public async Task<List<Comment>> GetAllComment()
        {
            var comments = await context.Comments.ToListAsync();
            return comments;
        }

        public async Task<Comment> GetComment(int id)
        {
            var comment = await context.Comments.FindAsync(id);
            if(comment == null)
            {
                return null;
            }
            return comment;
        }

        public async Task<Comment> UpdateComment(int id, Comment comment)
        {
            var findComment = await context.Comments.FindAsync(id);
            if(findComment == null)
            {
                return null;
            }
            findComment.Title = comment.Title;
            findComment.Content = comment.Content;
            await context.SaveChangesAsync();
            return findComment;
        }
    }
}
