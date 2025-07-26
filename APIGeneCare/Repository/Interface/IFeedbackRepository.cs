using APIGeneCare.Model.DTO;

namespace APIGeneCare.Repository.Interface
{
    public interface IFeedbackRepository
    {
        IEnumerable<FeedbackDTO> GetAllFeedbacksPaging(string? typeSearch, string? search, string? sortBy, int? page);
        IEnumerable<FeedbackDTO> GetAllFeedbacks();
        Task<IEnumerable<FeedbackDTO>> GetAllFeedbacksByUserId(int UserId);
        FeedbackDTO? GetFeedbackByBookingId(int id);
        bool CreateFeedback(FeedbackDTO feedback);
        bool UpdateFeedback(FeedbackDTO feedback);
        bool DeleteFeedbackByBookingId(int id);
    }
}
