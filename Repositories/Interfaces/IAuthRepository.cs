using APPC.Models.Auth;
using APPC.Models.Common;

namespace APPC.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<ServiceResult<LoginResponseDto>> LoginRepoAsync(LoginRequestDto model);
    }
}
