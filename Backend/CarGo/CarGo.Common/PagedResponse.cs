namespace CarGo.Common
{
    public class PagedResponse<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public required List<T> Data { get; set; }
    }
}