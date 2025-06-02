namespace GeneCare.Models.DTO
{
    public class BlogDAO
    {
        private int blogId;
        private UserDTO user;
        private String title;
        private String content;
        private DateTime createdAt;

        public BlogDAO() { }
        public BlogDAO(int blogId, UserDTO user, String title, String content, DateTime createdAt)
        {
            this.blogId = blogId;
            this.user = user;
            this.title = title;
            this.content = content;
            this.createdAt = createdAt;
        }
        public int BlogId
        {
            get { return blogId; }
            set { blogId = value; }
        }
        public UserDTO User
        {
            get { return user; }
            set { user = value; }
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
