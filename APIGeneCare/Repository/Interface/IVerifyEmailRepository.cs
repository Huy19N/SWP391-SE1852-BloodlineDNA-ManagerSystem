using APIGeneCare.Data;

namespace APIGeneCare.Repository.Interface
{
    public interface IVerifyEmailRepository
    {
        IEnumerable<VerifyEmail> GetAllVerifyEmailPaging(string? typeSearch, string? search, string? sortBy, int? page);
        VerifyEmail? GetVerifyEmailById(int id);
        bool CreateVerifyEmail(VerifyEmail verifyEmail);
        bool UpdateVerifyEmail(VerifyEmail verifyEmail);
        bool DeleteVerifyEmailById(int id);
    }
}
