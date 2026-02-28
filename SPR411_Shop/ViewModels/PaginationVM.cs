namespace SPR411_Shop.ViewModels
{
    public class PaginationVM
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 24;
        public int PageCount { get; set; } = 1;
        public int TotalCount { get; set; } = 0;
    }
}
