using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using System.Threading.Tasks;

namespace KooliProjekt.Services
{
    public class TeamService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TeamService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<PagedResult<Team>> List(int page, int pageSize)
        {
            return _unitOfWork.TeamRepository.List(page, pageSize);
        }

        public Task<Team> Get(int id)
        {
            return _unitOfWork.TeamRepository.Get(id);
        }

        public Task Save(Team team)
        {
            return _unitOfWork.TeamRepository.Save(team);
        }

        public Task Delete(int id)
        {
            return _unitOfWork.TeamRepository.Delete(id);
        }
    }
}
