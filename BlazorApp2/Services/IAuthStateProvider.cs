namespace BlazorApp2.Services
{
    public interface IAuthStateProvider
    {
        Task LoginAsync(string username, string role);
        Task LogoutAsync();
    }
}
