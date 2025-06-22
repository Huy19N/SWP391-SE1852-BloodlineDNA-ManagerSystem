// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using APIGeneCare.Entities;

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
