using APIGeneCare.Data;
using APIGeneCare.Repository.Interface;

namespace APIGeneCare.Repository
{
    public class CollectionMethodRepository : ICollectionMethodRepository
    {
        public bool CreateCollectionMethod(CollectionMethod collectionMethod)
        {
            throw new NotImplementedException();
        }

        public bool DeleteCollectionMethodById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CollectionMethod> GetAllCollectionMethodsPaging(string? typeSearch, string? search, string? sortBy, int? page)
        {
            throw new NotImplementedException();
        }

        public CollectionMethod? GetCollectionMethodById(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateBlog(CollectionMethod collectionMethod)
        {
            throw new NotImplementedException();
        }
    }
}
