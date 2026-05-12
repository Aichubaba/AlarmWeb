namespace BlazorApp2.Services
{
    public class AuthService : IAuthService
    {
        public bool Authenticate(string username, string password)
        {
            return (username, password) switch
            {
                ("admin", "admin123") => true,
                ("user", "user123") => true,
                _ => false
            };
        }
    }
}
