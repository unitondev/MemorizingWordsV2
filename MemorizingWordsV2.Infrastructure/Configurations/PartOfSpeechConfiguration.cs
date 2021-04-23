using MemorizingWordsV2.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MemorizingWordsV2.Infrastructure.Configurations
{
    public class PartOfSpeechConfiguration : IEntityTypeConfiguration<PartOfSpeech>
    {
        public void Configure(EntityTypeBuilder<PartOfSpeech> entity)
        {
            entity.ToTable("PartOfSpeech");
            
            entity.Property(e => e.Id).HasColumnName("id");
            
            entity.Property(e => e.PartName)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("part_name");
        }
    }
}