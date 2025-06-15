using APIGeneCare.Data;

namespace APIGeneCare.Repository.Interface
{
    public interface IRefreshTokenRepository
    {
        IEnumerable<RefreshToken> GetAllRefreshTokensPaging(string? typeSearch, string? search, string? sortBy, int? page);
        RefreshToken? GetRefreshTokenById(int id);
        bool CreateRefreshToken(RefreshToken refreshToken);
        bool UpdateRefreshToken(RefreshToken refreshToken);
        bool DeleteRefreshTokenById(int id);
    }
}
