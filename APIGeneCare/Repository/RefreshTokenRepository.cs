// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using APIGeneCare.Entities;
using APIGeneCare.Repository.Interface;

namespace APIGeneCare.Repository
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly GeneCareContext _context;
        public static int PAGE_SIZE { get; set; } = 10;
        public RefreshTokenRepository(GeneCareContext context) => _context = context;
        

        public bool CreateRefreshToken(RefreshTokenDTO refreshToken)
        {
            throw new NotImplementedException();
        }

        public bool DeleteRefreshTokenById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RefreshTokenDTO> GetAllRefreshTokensPaging(string? typeSearch, string? search, string? sortBy, int? page)
        {
            throw new NotImplementedException();
        }

        public RefreshTokenDTO? GetRefreshTokenById(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateRefreshToken(RefreshTokenDTO refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
