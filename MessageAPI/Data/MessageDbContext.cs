﻿
using MessageAPI.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace MessageAPI.Data
{
    public class MessageDbContext : DbContext
    {
        public MessageDbContext(DbContextOptions<MessageDbContext> options) : base(options)
        {
        }

        public DbSet<Models.Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // MsgStatus enum'ını string olarak saklamak
            modelBuilder.Entity<Models.Message>()
                .Property(m => m.Status)
                .HasConversion(
                    v => v.ToString(), // Enum'ı string'e dönüştür
                    v => (MsgStatus)Enum.Parse(typeof(MsgStatus), v)); // String'i enum'a dönüştür

            // MsgType enum'ını string olarak saklamak
            modelBuilder.Entity<Models.Message>()
                .Property(m => m.MessageType)
                .HasConversion(
                    v => v.ToString(), // Enum'ı string'e dönüştür
                    v => (MsgType)Enum.Parse(typeof(MsgType), v)); // String'i enum'a dönüştür
        }
    }
}
