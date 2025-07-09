using APIGeneCare.Model.DTO;

namespace APIGeneCare.Repository.Interface
{
    public interface IRefreshTokenRepository
    {
        Task<bool> IsRefreshTokenValid(string token);
        Task<string?> CreateRefreshToken(UserDTO user);
        Task<string> UpdateRefreshToken(UserDTO user);
        Task<bool> DeleteRefreshTokenByUser(UserDTO user);
    }
}
