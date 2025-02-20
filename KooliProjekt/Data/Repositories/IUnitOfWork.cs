namespace KooliProjekt.Data.Repositories
{
    public interface IUnitOfWork
    {
        Task BeginTransaction();
        Task Commit();
        Task Rollback();

        IUserRepository UserRepository { get; }
        ITeamRepository TeamRepository { get; }
        IMatchRepository MatchRepository { get; }
        ITournamentRepository TournamentRepository { get; }
        IPredicitonRepository PredictionRepository { get; }
        ILeaderboardRepository LeaderboardReposiory { get; }

    }
}
