namespace StockMarket.Search
{
    public class QueryObject
    {
        public string? Symbol { get; set; } = null;
        public string? ComapanyName { get; set; } = null;
        public string? sortBy {  get; set; } = null;
        public bool IsDescending {  get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
