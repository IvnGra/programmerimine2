namespace WpfApp1.Api
{
    public interface IApiClient
    {
        Task<List<User>> List();
        Task Save(User user);
        Task Delete(int id);
        Task<IEnumerable<object>> List<T>();

    }
}