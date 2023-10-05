using hackweek_backend.DTOs;
using hackweek_backend.Models;

namespace hackweek_backend.Services.Interfaces
{
    public interface ILoginService
    {
        Task Login(LoginDto request);
    }
}