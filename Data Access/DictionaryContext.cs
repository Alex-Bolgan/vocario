using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ReCallVocabulary.Data_Access
{
    public class DictionaryContext : DbContext
    {
        public DbSet<Phrase> Phrases { get; set; }
        public string MyPath { get; }
        public DictionaryContext(string path)
        {
            MyPath = PathDB.GetPath(path);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={MyPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var splitStringConverter = new ValueConverter<string[]?, string>(
                v => (v != null && v.Length > 0) ? string.Join(',', v) : string.Empty,
                v => (string.IsNullOrWhiteSpace(v)) ? new string[] { string.Empty} : v.Split(',', StringSplitOptions.RemoveEmptyEntries));

            modelBuilder.Entity<Phrase>()
                .Property(e => e.Tags)
                .HasConversion(splitStringConverter);

            modelBuilder.Entity<Phrase>()
                .Property(e => e.Synonyms)
                .HasConversion(splitStringConverter);
        }
    }
}
