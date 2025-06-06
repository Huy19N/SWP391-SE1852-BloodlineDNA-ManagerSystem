using Microsoft.EntityFrameworkCore;

namespace APIGeneCare.Data
{
    public class GeneCareContext : DbContext
    {
        public GeneCareContext(DbContextOptions<GeneCareContext> opt) : base(opt)
        {

        }
    }
}
