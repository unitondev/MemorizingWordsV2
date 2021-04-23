using MemorizingWordsV2.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MemorizingWordsV2.Infrastructure.Configurations
{
    public class EnglishWordConfiguration : IEntityTypeConfiguration<EnglishWord>
    {
        public void Configure(EntityTypeBuilder<EnglishWord> entity)
        {
            entity.HasIndex(e => e.Word, "UQ__EnglishW__839740545E8AC8FF")
                .IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasColumnName("created_at");

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
                .HasConstraintName("FK__EnglishWo__part___276EDEB3");

        }
    }
}