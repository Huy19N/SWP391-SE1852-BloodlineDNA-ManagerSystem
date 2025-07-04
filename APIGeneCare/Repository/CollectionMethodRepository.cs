using APIGeneCare.Entities;
using APIGeneCare.Model.DTO;
using APIGeneCare.Repository.Interface;
using System.Reflection.Metadata;
using System.Transactions;

namespace APIGeneCare.Repository
{
    public class CollectionMethodRepository : ICollectionMethodRepository
    {
        private readonly GeneCareContext _context;
        public static int PAGE_SIZE { get; set; } = 10;
        public CollectionMethodRepository(GeneCareContext context) => _context = context;

        public IEnumerable<CollectionMethodDTO> GetAllCollectionMethodsPaging(string? typeSearch, string? search, string? sortBy, int? page)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<CollectionMethodDTO> GetAllCollectionMethods()
            => _context.CollectionMethods.Select(cm => new CollectionMethodDTO
            {
                MethodId = cm.MethodId,
                MethodName = cm.MethodName
            });
        public CollectionMethodDTO? GetCollectionMethodById(int id)
            => _context.CollectionMethods.Select(cm => new CollectionMethodDTO
            {
                MethodId = cm.MethodId,
                MethodName = cm.MethodName
            }).SingleOrDefault(cm => cm.MethodId == id);
        public bool CreateCollectionMethod(CollectionMethodDTO collectionMethod)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                if(collectionMethod == null ||
                    String.IsNullOrWhiteSpace(collectionMethod.MethodName) )
                    return false;

                _context.CollectionMethods.Add(new CollectionMethod
                {
                    MethodName = collectionMethod.MethodName
                });
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
        public bool UpdateCollectionMethod(CollectionMethodDTO collectionMethod)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                if (collectionMethod == null ||
                    String.IsNullOrWhiteSpace(collectionMethod.MethodName))
                    return false;

                var existCollectionMethod = _context.CollectionMethods.Find(collectionMethod.MethodId);
                if (existCollectionMethod == null)
                    return false;

                existCollectionMethod.MethodId = collectionMethod.MethodId;
                existCollectionMethod.MethodName = collectionMethod.MethodName;
                
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
        public bool DeleteCollectionMethodById(int id)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var collectionMethod = _context.CollectionMethods.Find(id);
                if (collectionMethod == null) return false;

                _context.CollectionMethods.Remove(collectionMethod);
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
