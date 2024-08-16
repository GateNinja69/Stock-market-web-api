using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using StockMarket.Data;
using StockMarket.DTO.Stock;
using StockMarket.Model;
using StockMarket.Search;

namespace StockMarket.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly AppDbContext context;
        public StockRepository(AppDbContext context) 
        {
            this.context = context;
        }


        public async Task<Stock> CreateStock(Stock stock)
        {
            await context.Stocks.AddAsync(stock);
            await context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock> DeleteStock(int id)
        {
            var stock = await context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (stock == null)
            {
                return null;
            }
            context.Stocks.Remove(stock);
            await context.SaveChangesAsync();
            return stock;
        }

        public async Task<List<Stock>> GetAllStock(QueryObject search)
        {
            var stocks = context.Stocks.Include(c => c.Comments).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search.ComapanyName))
            {
                stocks = stocks.Where(s => s.CompanyName.Contains(search.ComapanyName));
            }
            if (!string.IsNullOrWhiteSpace(search.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol.Contains(search.Symbol));
            }
            if(!string.IsNullOrWhiteSpace(search.sortBy))
            {
                if(search.sortBy.Equals("Symbol" , StringComparison.OrdinalIgnoreCase))
                {
                    stocks = search.IsDescending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
                }
            }
            var skipNumber = (search.PageNumber - 1) * search.PageSize;

            return await stocks.Skip(skipNumber).Take(search.PageSize).ToListAsync();
        }

        public async Task<Stock> GetStockById(int id)
        {
            var stock = await context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(i => i.Id == id);
            if (stock == null)
            {

                return null;
            }
            return stock;
        }

        public async Task<bool> StockExist(int id)
        {
            //var stock = await context.Stocks.FindAsync(id);
            //if(stock == null)
            //{
            //    return false;
            //}
            //return true;
            return await context.Stocks.AnyAsync(i => i.Id == id);
            
        }

        public async Task<Stock> UpdateStock(int id, UpdateDTO update)
        {
            var existingStock = await context.Stocks.FirstOrDefaultAsync(x => x.Id ==id);
            if(existingStock == null)
            {
                return null;
            }
            existingStock.Id = update.Id;
            existingStock.Symbol = update.Symbol;
            existingStock.CompanyName = update.CompanyName;
            existingStock.LastDiv = update.LastDiv;
            existingStock.MarketCap = update.MarketCap;
            existingStock.Industry = update.Industry;
            existingStock.Purchase = update.Purchase;
            await context.SaveChangesAsync();
            return existingStock;
        }
    }
}
