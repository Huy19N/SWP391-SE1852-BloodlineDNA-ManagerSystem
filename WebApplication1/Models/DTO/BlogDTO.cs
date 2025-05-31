namespace GeneCare.Models.DTO
{
    public class BlogDTO
    {
        private int blogId;
        private int userId;
        private String title;
        private String content;
        private DateTime createdAt;

        public BlogDTO() { }
        public BlogDTO(int blogId, int userId, String title, String content, DateTime createdAt)
        {
            this.blogId = blogId;
            this.userId = userId;
            this.title = title;
            this.content = content;
            this.createdAt = createdAt;
        }
        public int BlogId
        {
            get { return blogId; }
            set { blogId = value; }
        }
        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }
        public String Title
        {
            get { return title; }
            set { title = value; }
        }
        public String Content
        {
            get { return content; }
            set { content = value; }
        }
        public DateTime CreatedAt
        {
            get { return createdAt; }
            set { createdAt = value; }
        }
    }
}
