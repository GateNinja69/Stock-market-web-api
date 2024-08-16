using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockMarket.Data;
using StockMarket.DTO.Stock;
using StockMarket.Mapper;
using StockMarket.Model;
using StockMarket.Repository;
using StockMarket.Search;


namespace StockMarket.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly AppDbContext context;
        private readonly IStockRepository repository;
        public HomeController(AppDbContext context, IStockRepository repository)
        {
            this.context = context;
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject search)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);  // we add model state for Data Validation
            try
            {
                var result = await repository.GetAllStock(search);
                    
                var Stockdto = result.Select(s => s.ToStockDTO());
                return Ok(Stockdto);
            }
            
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Retrieving Data from Database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);  // we add model state for Data Validation
            try
            {
                var stock = await repository.GetStockById(id);
                if (stock == null)
                {

                    return NotFound($"this stock with {id} id not found");
                }
                return Ok(stock);
            }
            
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Retrieving Data from Database");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateStockDTO createdStock)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);  // we add model state for Data Validation
            try
            {
                var Created_Stock = createdStock.ToAddNewStock();
                await repository.CreateStock(Created_Stock);
                return CreatedAtAction(nameof(GetById), new { id = Created_Stock.Id }, Created_Stock);
            }
           
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Retrieving Data from Database");
            }
        }

        //[HttpPost]
        //public IActionResult Create(CreateStockDTO create)
        //{
        //    if (create == null)
        //    {
        //        return BadRequest();
        //    }
        //    Stock stock = new Stock()
        //    {
        //        Symbol = create.Symbol,
        //        CompanyName = create.CompanyName,
        //        LastDiv = create.LastDiv,
        //        MarketCap = create.MarketCap,
        //        Industry = create.Industry,
        //        Purchase = create.Purchase
        //    };
        //    context.Stocks.Add(stock);
        //    context.SaveChanges();
        //    return Ok(stock);
        //} 

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateStock(int id, UpdateDTO update)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);  // we add model state for Data Validation
            try
            {
                var StockToUpdate = await repository.UpdateStock(id, update);
                if (StockToUpdate == null)
                {
                    return NotFound();
                }
                
                
                
                return Ok(StockToUpdate.ToStockDTO());
            }
            
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Retrieving Data from Database");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteStock(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);  // we add model state for Data Validation
            try
            {
                var delete = await repository.DeleteStock(id);
                if(delete == null)
                {
                    return NotFound();
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
