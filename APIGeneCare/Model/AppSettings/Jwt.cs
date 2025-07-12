namespace APIGeneCare.Model.AppSettings
{
    public class Jwt
    {
        public string SecretKey { get; set; } = string.Empty;
        public double MinAccessExpirationTime { get; set; } = 0;
        public double MinRefreshExpirationTime { get; set; } = 0; //muốn sử dụng lâu không cần login thì tăng lên
        public int LockoutMaxCount { get; set; } = 0; //số lần đăng nhập sai trước khi khóa ip
        public double LockoutTimeEachFaildCountInMinutes { get; set; } = 0; //thời gian khóa ip sau mỗi lần đăng nhập sai
        public int MaxCountOfLogin { get; set; } = 0; //số lần đăng nhập tối đa trong ngày
    }
}
