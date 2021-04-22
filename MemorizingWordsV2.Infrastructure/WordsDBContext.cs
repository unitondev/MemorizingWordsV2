using MemorizingWordsV2.Domain;
using MemorizingWordsV2.Domain.Models;
using MemorizingWordsV2.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MemorizingWordsV2.Infrastructure
{
    public partial class WordsDBContext : DbContext
    {
        public WordsDBContext()
        {
        }

        public WordsDBContext(DbContextOptions<WordsDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EnglishRussianWord> EnglishRussianWords { get; set; }
        public virtual DbSet<EnglishWord> EnglishWords { get; set; }
        public virtual DbSet<PartOfSpeech> PartOfSpeeches { get; set; }
        public virtual DbSet<RussianWord> RussianWords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=WordsDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");
            modelBuilder.ApplyConfiguration(new EnglishRussianWordConfiguration());
            modelBuilder.ApplyConfiguration(new EnglishWordConfiguration());
            modelBuilder.ApplyConfiguration(new PartOfSpeechConfiguration());
            modelBuilder.ApplyConfiguration(new RussianWordConfiguration());

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
