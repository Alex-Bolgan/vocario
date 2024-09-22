    using Microsoft.EntityFrameworkCore;

    namespace ReCallVocabulary.Data_Access
    {
        public class StatsContext : DbContext
        {
            public DbSet<StatsRecord> StatsRecords { get; set; }
            public string MyPath { get; }

            public StatsContext(string path)
            {
                MyPath = PathDB.GetPath(path);
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlite($"Data Source={MyPath}");
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<StatsRecord>().ToTable("StatsRecords");
            }
        }
    }

