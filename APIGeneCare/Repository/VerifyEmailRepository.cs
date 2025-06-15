using APIGeneCare.Data;
using APIGeneCare.Repository.Interface;

namespace APIGeneCare.Repository
{
    public class VerifyEmailRepository : IVerifyEmailRepository
    {
        public bool CreateVerifyEmail(VerifyEmail verifyEmail)
        {
            throw new NotImplementedException();
        }

        public bool DeleteVerifyEmailById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VerifyEmail> GetAllVerifyEmailPaging(string? typeSearch, string? search, string? sortBy, int? page)
        {
            throw new NotImplementedException();
        }

        public VerifyEmail? GetVerifyEmailById(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateVerifyEmail(VerifyEmail verifyEmail)
        {
            throw new NotImplementedException();
        }
    }
}
