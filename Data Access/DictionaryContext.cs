using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

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
    }
}
