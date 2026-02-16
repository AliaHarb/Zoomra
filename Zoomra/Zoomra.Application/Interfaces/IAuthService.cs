using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoomra.Application.Interfaces;
using Zoomra.Domain.DTOS;

namespace Zoomra.Application.Helpers
{
    public interface IAuthService
    {
        Task<AuthResultDto> RegisterAsync(RegisterDto model);

        Task<AuthResultDto> LoginAsync(LoginDto model);

        Task<AuthResultDto> RefreshTokenAsync(RefreshTokenDto model);

        Task ForgetPasswordAsync(ForgetPasswordDto model);

        Task<bool> ResetPasswordAsync(ResetPasswordDto model);

        Task LogoutAsync(string refreshToken);
    }
}
