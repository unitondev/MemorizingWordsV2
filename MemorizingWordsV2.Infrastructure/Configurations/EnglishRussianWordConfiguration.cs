using MemorizingWordsV2.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MemorizingWordsV2.Infrastructure.Configurations
{
    public class EnglishRussianWordConfiguration : IEntityTypeConfiguration<EnglishRussianWord>
    {
        public void Configure(EntityTypeBuilder<EnglishRussianWord> entity)
        {
            entity.HasKey(e => new { e.EnglishId, e.RussianId })
                .HasName("PK__English___599173F6506BDEBE");

            entity.ToTable("English_Russian_Words");

            entity.Property(e => e.EnglishId).HasColumnName("english_id");

            entity.Property(e => e.RussianId).HasColumnName("russian_id");

            entity.HasOne(d => d.English)
                .WithMany(p => p.EnglishRussianWords)
                .HasForeignKey(d => d.EnglishId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__English_R__engli__4AB81AF0");

            entity.HasOne(d => d.Russian)
                .WithMany(p => p.EnglishRussianWords)
                .HasForeignKey(d => d.RussianId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__English_R__russi__4BAC3F29");
        }
    }
}