using MeetingNotesProcessor.Model;
using Microsoft.EntityFrameworkCore;

namespace MeetingNotesProcessor.DataContext
{
    public class ApiContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "MinutesDB");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Minute>()
                        //.HasNoKey()                        
                        .Property(e => e.Notes)                        
                        .HasConversion(
                            v => string.Join(',', v),
                            v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());

               // .HasNoKey();
        }

        public DbSet<Minute> Minutes { get; set; }
    }
}