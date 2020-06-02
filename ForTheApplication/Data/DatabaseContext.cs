using ForTheApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace ForTheApplication.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public virtual DbSet<UserModel> Users { get; set; }
    }

}
