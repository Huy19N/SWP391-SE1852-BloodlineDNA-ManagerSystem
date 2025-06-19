// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using APIGeneCare.Data;

namespace APIGeneCare.Repository.Interface
{
    public interface IFeedbackRepository
    {
        IEnumerable<Feedback> GetAllFeedbacksPaging(string? typeSearch, string? search, string? sortBy, int? page);
        Feedback? GetFeedbackById(int id);
        bool CreateFeedback(Feedback feedback);
        bool UpdateFeedback(Feedback feedback);
        bool DeleteFeedbackById(int id);
    }
}
