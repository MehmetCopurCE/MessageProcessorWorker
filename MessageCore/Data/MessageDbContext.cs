using MessageCore.Models;
using Microsoft.EntityFrameworkCore;


namespace MessageCore.Data
{
    public class MessageDbContext : DbContext
    {
        public MessageDbContext(DbContextOptions<MessageDbContext> options) : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }
    }
}
