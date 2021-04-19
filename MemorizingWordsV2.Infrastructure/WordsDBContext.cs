using MemorizingWordsV2.Domain;
using MemorizingWordsV2.Domain.Models;
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

            modelBuilder.Entity<EnglishRussianWord>(entity =>
            {
                entity.HasKey(e => new { e.EnglishId, e.RussianId })
                    .HasName("PK__English___599173F6BFE35253");

                entity.ToTable("English_Russian_Words");

                entity.Property(e => e.EnglishId).HasColumnName("english_id");

                entity.Property(e => e.RussianId).HasColumnName("russian_id");

                entity.HasOne(d => d.English)
                    .WithMany(p => p.EnglishRussianWords)
                    .HasForeignKey(d => d.EnglishId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__English_R__engli__403A8C7D");

                entity.HasOne(d => d.Russian)
                    .WithMany(p => p.EnglishRussianWords)
                    .HasForeignKey(d => d.RussianId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__English_R__russi__412EB0B6");
            });

            modelBuilder.Entity<EnglishWord>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PartOfSpeechId).HasColumnName("part_of_speech_id");

                entity.Property(e => e.Word)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("word");

                entity.HasOne(d => d.PartOfSpeech)
                    .WithMany(p => p.EnglishWords)
                    .HasForeignKey(d => d.PartOfSpeechId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__EnglishWo__part___3B75D760");
            });

            modelBuilder.Entity<PartOfSpeech>(entity =>
            {
                entity.ToTable("PartOfSpeech");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PartName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("part_name");
            });

            modelBuilder.Entity<RussianWord>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Word)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("word");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
