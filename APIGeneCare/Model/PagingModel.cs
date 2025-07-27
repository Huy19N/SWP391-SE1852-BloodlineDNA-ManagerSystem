namespace APIGeneCare.Model
{
    public class PagingModel
    {
        public int CurrentPage { get; set; }
        public int MaxPage { get; set; }
        public IEnumerable<Object>? Data { get; set; }
    }
}
