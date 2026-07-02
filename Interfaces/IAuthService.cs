using TaskManagement.DTOs;

namespace TaskManagement.Interfaces
{
    public interface IAuthService
    {
        LoginResponseDto? Authenticate(LoginDto login);
    }
}