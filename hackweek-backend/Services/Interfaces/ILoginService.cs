using hackweek_backend.dtos;

namespace hackweek_backend.Services.Interfaces
{
    public interface ILoginService
    {
        Task<string> Login(LoginDto request);
        bool HasAccessToUser(HttpContext httpContext, int idUser);
        string GetUserRole(HttpContext httpContext);
    }
}