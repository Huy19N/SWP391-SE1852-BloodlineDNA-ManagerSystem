using APIGeneCare.Entities;
using APIGeneCare.Model;
using APIGeneCare.Model.DTO;

namespace APIGeneCare.Repository.Interface
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> IsRefreshTokenValid(string token);
        Task<TokenModel> GenerateTokenModel(UserRefeshToken user);
        Task<string> GenerateAccessTokenByRefToken(string token);
        Task<string> GenerateRefreshToken(UserRefeshToken user, string jwtId);
        Task<bool> DeleteRefreshTokenByUser(UserDTO user);
    }
}
