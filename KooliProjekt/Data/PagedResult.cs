namespace KooliProjekt.Data
{
    public class PagedResult<T> : PagedResultBase where T : class
    {
        public IList<T> Results { get; set; }
        public IEnumerable<object> Items { get; set; }

        public PagedResult()
        {
            Results = new List<T>();
        }
    }
}