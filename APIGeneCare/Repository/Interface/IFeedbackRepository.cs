using APIGeneCare.Data;

namespace APIGeneCare.Repository.Interface
{
    public interface IFeedbackRepository
    {
        IEnumerable<Feedback> GetAllFeedbacks(string? typeSearch, string? search, string? sortBy, int? page);
        Feedback? GetFeedbackById(int id);
        bool CreateFeedback(Feedback feedback);
        bool UpdateFeedback(Feedback feedback);
        bool DeleteFeedback(int id);
    }
}
