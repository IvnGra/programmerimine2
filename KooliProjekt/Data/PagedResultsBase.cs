namespace KooliProjekt.Data
{
    public abstract class PagedResultBase
    {
        public int CurrentPage { get; set; }  // Praegune leht
        public int PageCount { get; set; }  // Kokku lehekülgi
        public int PageSize { get; set; }  // Lehe suurus (näiteks 5)
        public int RowCount { get; set; }  // Kõik read (kogus)
        public int TotalPages => (int)Math.Ceiling((double)RowCount / PageSize);
    }
}