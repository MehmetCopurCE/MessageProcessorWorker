
using Microsoft.EntityFrameworkCore;

namespace MessageAPI.Data
{
    public class MessageDbContext : DbContext
    {
        public MessageDbContext(DbContextOptions<MessageDbContext> options) : base(options)
        {
        }

        public DbSet<Models.Message> Messages { get; set; }
    }
}
