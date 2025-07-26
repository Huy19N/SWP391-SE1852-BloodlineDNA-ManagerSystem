using APIGeneCare.Entities;
using APIGeneCare.Model;
using APIGeneCare.Model.DTO;
using APIGeneCare.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace APIGeneCare.Repository
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly GeneCareContext _context;
        public static int PAGE_SIZE { get; set; } = 10;
        public FeedbackRepository(GeneCareContext context) => _context = context;

        public IEnumerable<FeedbackDTO> GetAllFeedbacksPaging(string? typeSearch, string? search, string? sortBy, int? page)
        {
            var allFeedbacks = _context.Feedbacks.AsQueryable();
            #region Search by type
            if (!String.IsNullOrWhiteSpace(typeSearch) && !String.IsNullOrWhiteSpace(search))
            {

                if (typeSearch.Equals("comment", StringComparison.CurrentCultureIgnoreCase))
                    allFeedbacks = _context.Feedbacks.Where(f => !String.IsNullOrWhiteSpace(f.Comment) &&
                    f.Comment.Equals(f.Comment, StringComparison.CurrentCultureIgnoreCase));

                if (typeSearch.Equals("rating", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (int.TryParse(search, out int rating))
                    {
                        allFeedbacks = _context.Feedbacks.Where(f => f.Rating == rating);
                    }
                }
            }
            #endregion
            #region Sort by
            if (!String.IsNullOrWhiteSpace(sortBy))
            {

                if (sortBy.Equals("createdat_asc", StringComparison.CurrentCultureIgnoreCase))
                    allFeedbacks = allFeedbacks.OrderBy(f => f.CreatedAt);

                if (sortBy.Equals("createdat_desc", StringComparison.CurrentCultureIgnoreCase))
                    allFeedbacks = allFeedbacks.OrderByDescending(f => f.CreatedAt);

                if (sortBy.Equals("comment_asc", StringComparison.CurrentCultureIgnoreCase))
                    allFeedbacks = allFeedbacks.OrderBy(f => f.Comment);

                if (sortBy.Equals("comment_desc", StringComparison.CurrentCultureIgnoreCase))
                    allFeedbacks = allFeedbacks.OrderByDescending(f => f.Comment);

                if (sortBy.Equals("rating_asc", StringComparison.CurrentCultureIgnoreCase))
                    allFeedbacks = allFeedbacks.OrderBy(f => f.Rating);

                if (sortBy.Equals("rating_desc", StringComparison.CurrentCultureIgnoreCase))
                    allFeedbacks = allFeedbacks.OrderByDescending(f => f.Rating);

            }
            #endregion

            var result = PaginatedList<Feedback>.Create(allFeedbacks, page ?? 1, PAGE_SIZE);
            return result.Select(f => new FeedbackDTO
            {
                BookingId = f.BookingId,
                CreatedAt = f.CreatedAt,
                Comment = f.Comment,
                Rating = f.Rating
            });
        }
        public IEnumerable<FeedbackDTO> GetAllFeedbacks()
            => _context.Feedbacks.Select(f => new FeedbackDTO
            {
                BookingId = f.BookingId,
                CreatedAt = f.CreatedAt,
                Comment = f.Comment,
                Rating = f.Rating
            }).ToList();
        public async Task<IEnumerable<FeedbackDTO>> GetAllFeedbacksByUserId(int UserId)
            => await _context.Feedbacks.Where(f => f.Booking.UserId == UserId).Select(f => new FeedbackDTO
            {
                BookingId = f.BookingId,
                CreatedAt = f.CreatedAt,
                Comment = f.Comment,
                Rating = f.Rating
            }).ToListAsync();
        public FeedbackDTO? GetFeedbackByBookingId(int id)
            => _context.Feedbacks.Select(f => new FeedbackDTO
            {
                BookingId = f.BookingId,
                CreatedAt = f.CreatedAt,
                Comment = f.Comment,
                Rating = f.Rating
            }).FirstOrDefault(f => f.BookingId == id);
        public bool CreateFeedback(FeedbackDTO feedback)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                if (feedback == null)
                {
                    return false;
                }
                _context.Feedbacks.Add(new Feedback
                {
                    BookingId = feedback.BookingId,
                    CreatedAt = feedback.CreatedAt,
                    Comment = feedback.Comment,
                    Rating = feedback.Rating
                });

                _context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }

        }
        public bool UpdateFeedback(FeedbackDTO feedback)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                if (feedback == null)
                {
                    return false;
                }

                var existingFeedback = _context.Feedbacks.Find(feedback.BookingId);
                if (existingFeedback == null)
                {
                    return false;
                }
                existingFeedback.BookingId = feedback.BookingId;
                existingFeedback.CreatedAt = feedback.CreatedAt;
                existingFeedback.Comment = feedback.Comment;
                existingFeedback.Rating = feedback.Rating;

                _context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public bool DeleteFeedbackByBookingId(int id)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var feedback = _context.Feedbacks.Find(id);
                if (feedback == null) return false;
                _context.Feedbacks.Remove(feedback);

                _context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
