using Microsoft.AspNetCore.Identity.Data;
using TaskManagement.DTOs;

namespace TaskManagement.Interfaces
{
    public interface IAuthService
    {
        LoginRequest? Authenticate(LoginDto login);
    }
}
