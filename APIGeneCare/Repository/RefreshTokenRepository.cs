﻿using APIGeneCare.Data;
using APIGeneCare.Repository.Interface;

namespace APIGeneCare.Repository
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        public bool CreateRefreshToken(RefreshToken refreshToken)
        {
            throw new NotImplementedException();
        }

        public bool DeleteRefreshTokenById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RefreshToken> GetAllRefreshTokensPaging(string? typeSearch, string? search, string? sortBy, int? page)
        {
            throw new NotImplementedException();
        }

        public RefreshToken? GetRefreshTokenById(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateRefreshToken(RefreshToken refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
