using APIGeneCare.Entities;
using APIGeneCare.Model;
using APIGeneCare.Model.DTO;

namespace APIGeneCare.Repository.Interface
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> IsRefreshTokenValid(string token);
        Task<TokenModel> GenerateTokenModel(UserDTO user);
        Task<string> GenerateAccessTokenByRefToken(string token);
        Task<string> GenerateRefreshToken(UserDTO user, string? oldRefreshToken, string jwtId);
        Task<bool> DeleteRefreshTokenByUser(UserDTO user);
    }
}
