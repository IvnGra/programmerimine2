using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using System.Threading.Tasks;

namespace KooliProjekt.Services
{
    public class LeaderboardService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LeaderboardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<PagedResult<Leaderboard>> List(int page, int pageSize)
        {
            return _unitOfWork.LeaderboardRepository.List(page, pageSize);
        }

        public Task<Leaderboard> Get(int id)
        {
            return _unitOfWork.LeaderboardRepository.Get(id);
        }

        public Task Save(Leaderboard leaderboard)
        {
            return _unitOfWork.LeaderboardRepository.Save(leaderboard);
        }

        public Task Delete(int id)
        {
            return _unitOfWork.LeaderboardRepository.Delete(id);
        }
    }
}
