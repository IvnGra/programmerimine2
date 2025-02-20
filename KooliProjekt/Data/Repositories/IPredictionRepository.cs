namespace KooliProjekt.Data.Repositories
{
    public interface IPredicitonRepository
    {
        Task<Prediction> Get(int id);
        Task<PagedResult<Prediction>> List(int page, int pageSize);
        Task Save(Prediction item);
        Task Delete(int id);
    }
}