using APIGeneCare.Entities;
using APIGeneCare.Model.DTO;

namespace APIGeneCare.Repository.Interface
{
    public interface IRefreshTokenRepository
    {
        IEnumerable<RefreshTokenDTO> GetAllRefreshTokensPaging(string? typeSearch, string? search, string? sortBy, int? page);
        RefreshTokenDTO? GetRefreshTokenById(int id);
        bool CreateRefreshToken(RefreshTokenDTO refreshToken);
        bool UpdateRefreshToken(RefreshTokenDTO refreshToken);
        bool DeleteRefreshTokenById(int id);
    }
}
