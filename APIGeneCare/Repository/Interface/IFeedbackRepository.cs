// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using APIGeneCare.Entities;
using APIGeneCare.Model.DTO;

namespace APIGeneCare.Repository.Interface
{
    public interface IFeedbackRepository
    {
        IEnumerable<FeedbackDTO> GetAllFeedbacksPaging(string? typeSearch, string? search, string? sortBy, int? page);
        IEnumerable<FeedbackDTO> GetAllFeedbacks();
        FeedbackDTO? GetFeedbackById(int id);
        bool CreateFeedback(FeedbackDTO feedback);
        bool UpdateFeedback(FeedbackDTO feedback);
        bool DeleteFeedbackById(int id);
    }
}
