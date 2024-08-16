using StockMarket.DTO.Stock;
using StockMarket.Model;
using StockMarket.Search;

namespace StockMarket.Repository
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllStock(QueryObject search);
        Task<Stock> GetStockById(int id);
        Task<Stock> UpdateStock(int id, UpdateDTO update);
        Task<Stock> DeleteStock(int id);
        Task<Stock> CreateStock(Stock stock);
        Task<bool> StockExist(int id);
    }
}
