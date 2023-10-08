using hackweek_backend.DTOs;
using hackweek_backend.Models;

namespace hackweek_backend.Services.Interfaces
{
    public interface ILoginService
    {
        Task<string> Login(LoginDto request);
    }
}