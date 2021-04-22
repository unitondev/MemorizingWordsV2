using MemorizingWordsV2.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MemorizingWordsV2.Infrastructure.Configurations
{
    public class RussianWordConfiguration : IEntityTypeConfiguration<RussianWord>
    {
        public void Configure(EntityTypeBuilder<RussianWord> entity)
        {
            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.Word)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("word");
        }
    }
}