using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using System.Threading.Tasks;

namespace KooliProjekt.Services
{
    public class MatchService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MatchService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<PagedResult<Match>> List(int page, int pageSize)
        {
            return _unitOfWork.MatchRepository.List(page, pageSize);
        }

        public Task<Match> Get(int id)
        {
            return _unitOfWork.MatchRepository.Get(id);
        }

        public Task Save(Match match)
        {
            return _unitOfWork.MatchRepository.Save(match);
        }

        public Task Delete(int id)
        {
            return _unitOfWork.MatchRepository.Delete(id);
        }
    }
}
