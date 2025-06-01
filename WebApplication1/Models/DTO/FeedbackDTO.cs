namespace GeneCare.Models.DTO
{
    public class FeedbackDAO
    {
        private int feedbackId;
        private UserDTO user;
        private ServiceDAO service;
        private DateTime createAt;
        private String comment;
        private int rating;

        public FeedbackDAO() { }
        public FeedbackDAO(int feedbackId, UserDTO user, ServiceDAO service, DateTime createAt, String comment, int rating)
        {
            this.feedbackId = feedbackId;
            this.user = user;
            this.service = service;
            this.createAt = createAt;
            this.comment = comment;
            this.rating = rating;
        }
        public int FeedbackId
        {
            get { return feedbackId; }
            set { feedbackId = value; }
        }
        public UserDTO User
        {
            get { return user; }
            set { user = value; }
        }
        public ServiceDAO Service
        {
            get { return service; }
            set { service = value; }
        }
        public DateTime CreateAt
        {
            get { return createAt; }
            set { createAt = value; }
        }
        public String Comment
        {
            get { return comment; }
            set { comment = value; }
        }
        public int Rating
        {
            get { return rating; }
            set { rating = value; }
        }
    }
}
