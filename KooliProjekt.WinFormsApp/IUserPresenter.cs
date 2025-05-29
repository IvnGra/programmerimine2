namespace KooliProjekt.WinFormsApp
{
    public interface IUserPresenter
    {
        Task Initialize();
        Task SaveUser();
        Task DeleteUser();
        void NewUser();
        void UserSelected();
    }
}
