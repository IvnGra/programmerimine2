using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using System.Threading.Tasks;

namespace KooliProjekt.Services
{
    public class PredictionsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PredictionsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<PagedResult<Prediction>> List(int page, int pageSize)
        {
            return _unitOfWork.PredictionRepository.List(page, pageSize);
        }

        public Task<Prediction> Get(int id)
        {
            return _unitOfWork.PredictionRepository.Get(id);
        }

        public Task Save(Prediction prediction)
        {
            return _unitOfWork.PredictionRepository.Save(prediction);
        }

        public Task Delete(int id)
        {
            return _unitOfWork.PredictionRepository.Delete(id);
        }
    }
}
