using Microsoft.AspNetCore.Mvc;
using StockMarket.Data;
using StockMarket.DTO.Comments;
using StockMarket.DTO.Stock;
using StockMarket.Mapper;
using StockMarket.Model;
using StockMarket.Repository;
using StockMarket.Search;

namespace StockMarket.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : Controller
    {
        private readonly ICommentRepository commentRepository;
        private readonly IStockRepository stockRepository;
        private readonly AppDbContext context;

        public CommentController(ICommentRepository commentRepository , IStockRepository stockRepository, AppDbContext context)
        {
            this.commentRepository = commentRepository;
            this.stockRepository = stockRepository;
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComment() 
        {

            if(!ModelState.IsValid)
                return BadRequest(ModelState);  // we add model state for Data Validation
            
            var comment = await commentRepository.GetAllComment();
            var commentFromDTO = comment.Select(s => s.ToCommentDto());
            return Ok(commentFromDTO);
        }

        [HttpPost("{StockId:int}")]
        public async Task<IActionResult> CreateComment(int StockId, CreateCommentDTO comment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);  // we add model state for Data Validation

            if (!await stockRepository.StockExist(StockId))
            {
                return BadRequest($"Stock does not exist");
            }
            var comments = comment.CreateCommentMapper(StockId);
            await commentRepository.CreateComment(comments);
            return CreatedAtAction(nameof(GetCommentById), new { id = comments.Id }, comments.ToCommentDto());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);  // we add model state for Data Validation

            var comment = await commentRepository.GetComment(id);
            if(comment == null)
            {
                return NotFound();
            }
            return Ok(comment);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateComment(int id, UpdateCommentDTO updatecomment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);  // we add model state for Data Validation

            var comments = await commentRepository.UpdateComment(id, updatecomment.UpdateCommentMapper());
            if(comments == null)
            {
                return NotFound($"Comment Not Found");
            }
            return Ok(comments);

        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteCommentbyId(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);  // we add model state for Data Validation

            try
            {
                var result = await commentRepository.DeleteComment(id);
                if (result == null)
                {
                    return NotFound($"Comment with id {id} not found");
                }
                return Ok($"The Stock With Id: {id} has been deleted successfully");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Retrieving Data from Database");
            }
        }
    }
    
    
}
