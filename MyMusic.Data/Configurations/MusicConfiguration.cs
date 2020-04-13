using MyMusic.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyMusic.Data.Configurations
{
  public class MusicConfiguration : IEntityTypeConfiguration<Music>
  {
    public void Configure(EntityTypeBuilder<Music> builder)
    {
      builder.HasKey(m => m.Id);
      builder.Property(m => m.Id).UseIdentityColumn().HasColumnName("id");
      builder.Property(m => m.Name).IsRequired().HasMaxLength(50).HasColumnName("name");
      builder.Property(m => m.ArtistId).HasColumnName("artistid");
      builder.HasOne(m => m.Artist).WithMany(a => a.Musics).HasForeignKey(m => m.ArtistId);
      builder.ToTable("musics");
    }
  }
}