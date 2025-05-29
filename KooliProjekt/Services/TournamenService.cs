using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using System.Threading.Tasks;

namespace KooliProjekt.Services
{
    public class TournamentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TournamentService(IUnitOfWork unitOfWork)  // <-- Accept IUnitOfWork, NOT ApplicationDbContext
        {
            _unitOfWork = unitOfWork;
        }

        public Task<PagedResult<Tournament>> List(int page, int pageSize)
        {
            return _unitOfWork.TournamentRepository.List(page, pageSize);
        }

        public Task<Tournament> Get(int id)
        {
            return _unitOfWork.TournamentRepository.Get(id);
        }

        public Task Save(Tournament tournament)
        {
            return _unitOfWork.TournamentRepository.Save(tournament);
        }

        public Task Delete(int id)
        {
            return _unitOfWork.TournamentRepository.Delete(id);
        }
    }
}
