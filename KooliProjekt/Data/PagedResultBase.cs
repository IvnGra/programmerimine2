namespace KooliProjekt.Data
{
    public abstract class PagedResultBase
    {
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int RowCount { get; set; }
        public int FirstRowOnPage { get; set; }
        public int LastRowOnPage { get; set; }
        public int PageSize { get; set; }
    }
}