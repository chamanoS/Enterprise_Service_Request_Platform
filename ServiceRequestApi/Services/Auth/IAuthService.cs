using System.Threading.Tasks;
using ServiceRequestApi.Contracts.DTOs.Auth;

namespace ServiceRequestApi.Services.Auth
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(LoginRequestDto dto);
    }
}
