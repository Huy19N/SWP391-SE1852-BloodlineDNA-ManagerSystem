using APIGeneCare.Data;

namespace APIGeneCare.Repository.Interface
{
    public interface ICollectionMethodRepository
    {
        IEnumerable<CollectionMethod> GetAllCollectionMethodsPaging(string? typeSearch, string? search, string? sortBy, int? page);
        CollectionMethod? GetCollectionMethodById(int id);
        bool CreateCollectionMethod(CollectionMethod collectionMethod);
        bool UpdateBlog(CollectionMethod collectionMethod);
        bool DeleteCollectionMethodById(int id);
    }
}
