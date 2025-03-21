using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Services
{
    public interface IPredictionService
    {
        Task<PagedResult<Prediction>> List(int page, int pageSize, PredictionsSearch search = null);
        Task Create(Prediction prediction);
        Task<Prediction> Get(int id);
        Task Save(Prediction list);
        Task Delete(int id);
    }
}