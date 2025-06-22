// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using APIGeneCare.Entities;

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
