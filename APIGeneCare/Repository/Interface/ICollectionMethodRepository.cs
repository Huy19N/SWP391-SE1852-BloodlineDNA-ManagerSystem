using APIGeneCare.Model.DTO;

namespace APIGeneCare.Repository.Interface
{
    public interface ICollectionMethodRepository
    {
        IEnumerable<CollectionMethodDTO> GetAllCollectionMethodsPaging(string? typeSearch, string? search, string? sortBy, int? page);
        IEnumerable<CollectionMethodDTO> GetAllCollectionMethods();
        CollectionMethodDTO? GetCollectionMethodById(int id);
        bool CreateCollectionMethod(CollectionMethodDTO collectionMethod);
        bool UpdateCollectionMethod(CollectionMethodDTO collectionMethod);
        bool DeleteCollectionMethodById(int id);
    }
}
