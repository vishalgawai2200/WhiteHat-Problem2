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
        public DbSet<Minutes>? Minutes { get; set; }
        public DbSet<Participant>? Participants { get; set; }
        public DbSet<Note>? Notes { get; set; }
    }
}