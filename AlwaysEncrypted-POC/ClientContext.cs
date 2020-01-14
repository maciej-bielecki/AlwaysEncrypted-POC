using Microsoft.EntityFrameworkCore;

namespace AlwaysEncrypted_POC
{
    public class ClientContext : DbContext
    {
        public ClientContext(DbContextOptions options) : base(options)
        {

        }
        public virtual DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new ClientMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}
