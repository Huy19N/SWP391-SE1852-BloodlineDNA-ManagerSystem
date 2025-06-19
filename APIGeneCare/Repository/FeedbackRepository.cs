// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using APIGeneCare.Data;
using APIGeneCare.Model;
using APIGeneCare.Repository.Interface;

namespace APIGeneCare.Repository
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly GeneCareContext _context;
        public static int PAGE_SIZE { get; set; } = 10;
        public FeedbackRepository(GeneCareContext context) => _context = context;
        public bool CreateFeedback(Feedback feedback)
        {
            if (feedback == null)
            {
                return false;
            }
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Feedbacks.Add(feedback);

                _context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }

        }
        public bool DeleteFeedbackById(int id)
        {
            var feedback = _context.Feedbacks.Find(id);
            if (feedback == null) return false;

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Feedbacks.Remove(feedback);

                _context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
        }
        public IEnumerable<Feedback> GetAllFeedbacksPaging(string? typeSearch, string? search, string? sortBy, int? page)
        {
            var allFeedbacks = _context.Feedbacks.AsQueryable();
            #region Search by type
            if (!String.IsNullOrWhiteSpace(typeSearch) && !String.IsNullOrWhiteSpace(search))
            {
                if (typeSearch.Equals("feedbackid", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (int.TryParse(search, out int feedbackid))
                    {
                        allFeedbacks = _context.Feedbacks.Where(f => f.FeedbackId == feedbackid);
                    }
                }

                if (typeSearch.Equals("userid", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (int.TryParse(search, out int userid))
                    {
                        allFeedbacks = _context.Feedbacks.Where(f => f.UserId == userid);
                    }
                }

                if (typeSearch.Equals("serviceid", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (int.TryParse(search, out int serviceid))
                    {
                        allFeedbacks = _context.Feedbacks.Where(f => f.ServiceId == serviceid);
                    }
                }

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
                if (sortBy.Equals("feedbackid_asc", StringComparison.CurrentCultureIgnoreCase))
                    allFeedbacks = allFeedbacks.OrderBy(f => f.FeedbackId);

                if (sortBy.Equals("feedbackid_desc", StringComparison.CurrentCultureIgnoreCase))
                    allFeedbacks = allFeedbacks.OrderByDescending(f => f.FeedbackId);

                if (sortBy.Equals("userid_asc", StringComparison.CurrentCultureIgnoreCase))
                    allFeedbacks = allFeedbacks.OrderBy(f => f.UserId);

                if (sortBy.Equals("userid_desc", StringComparison.CurrentCultureIgnoreCase))
                    allFeedbacks = allFeedbacks.OrderByDescending(f => f.UserId);

                if (sortBy.Equals("serviceid_asc", StringComparison.CurrentCultureIgnoreCase))
                    allFeedbacks = allFeedbacks.OrderBy(f => f.ServiceId);

                if (sortBy.Equals("serviceid_desc", StringComparison.CurrentCultureIgnoreCase))
                    allFeedbacks = allFeedbacks.OrderByDescending(f => f.ServiceId);

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
            return result.Select(f => new Feedback
            {
                FeedbackId = f.FeedbackId,
                UserId = f.UserId,
                ServiceId = f.ServiceId,
                CreatedAt = f.CreatedAt,
                Comment = f.Comment,
                Rating = f.Rating
            });
        }

        public Feedback? GetFeedbackById(int id)
            => _context.Feedbacks.Find(id);

        public bool UpdateFeedback(Feedback feedback)
        {
            if (feedback == null)
            {
                return false;
            }

            var existingFeedback = _context.Feedbacks.Find(feedback.FeedbackId);
            if (existingFeedback == null)
            {
                return false;
            }

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                existingFeedback.UserId = feedback.UserId;
                existingFeedback.ServiceId = feedback.ServiceId;
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
                return false;
            }
        }
    }
}
