using BookingTask.Models.DTOs;

namespace BookingTask.Services.Interfaces
{
    public interface IAccountService
    {
        Task RegisterUser(RegisterUserDto dto);
        Task<string> GenerateJwt(LoginUserDto dto);
    }
}
