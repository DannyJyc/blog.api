using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Blog.API.Entity.Models;

namespace Blog.API.Entity.Fluents
{
    public class TagFluenter
    {
        public TagFluenter(EntityTypeBuilder<Tag> modelBuilder)
        {
            modelBuilder.ToTable("Tags");
            modelBuilder
           .Property(b => b.T_Name)
           .HasMaxLength(50)
           .HasComment("标签名称");
            modelBuilder
            .Property(b => b.Creatdate)
            .HasColumnType("datetime")
            .HasComment("创建时间");
        }
    }
}
