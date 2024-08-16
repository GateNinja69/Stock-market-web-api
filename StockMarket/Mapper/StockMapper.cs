using StockMarket.DTO.Stock;
using StockMarket.DTO.Comments;

using StockMarket.Model;

namespace StockMarket.Mapper
{
    public static class StockMapper
    {
        public static StockDTO ToStockDTO(this Stock stock)
        {
            StockDTO result = new StockDTO()
            {
                Id = stock.Id,
                Symbol = stock.Symbol,
                CompanyName = stock.CompanyName,
                LastDiv = stock.LastDiv,
                MarketCap = stock.MarketCap,
                Industry = stock.Industry,
                Purchase = stock.Purchase,
                Comments = stock.Comments.Select(c => c.ToCommentDto()).ToList()
            };
            return result;
        }

        public static Stock  ToAddNewStock(this CreateStockDTO create)
        {
            return new Stock()
            {
                Symbol = create.Symbol,
                CompanyName = create.CompanyName,
                LastDiv = create.LastDiv,
                MarketCap = create.MarketCap,
                Industry = create.Industry,
                Purchase = create.Purchase
                
            };
        }
    }
}
