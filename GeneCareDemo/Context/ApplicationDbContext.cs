using GeneCare.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace GeneCareDemo.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>contextOptions) : base(contextOptions)
        {
            
        }

        //Code - Approach
        public DbSet<UserDTO> Users { get; set; }

    }

}

