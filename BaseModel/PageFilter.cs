namespace BaseModel
{
    public class PageFilter
    {
        public bool SortAscending { get; set; }

        public int PageNumber { get; set; }
        public int ItemsPerPage { get; set; }

        public string SortHeader { get; set; }
    }

    public class PageFilter<T> : PageFilter
    {
        public T FilterModel { get; set; }
    }
}
