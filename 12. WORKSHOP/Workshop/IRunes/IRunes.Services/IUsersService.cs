namespace IRunes.Services
{
    public interface IUsersService
    {
        string GetUserIdUsername(string username, string password);

        string GetUserIdEmail(string email, string password);

        void Register(string username, string email, string password);

        bool UsernameExists(string username);

        bool EmailExists(string email);

        public string GetUserName(string userId);
    }
}
