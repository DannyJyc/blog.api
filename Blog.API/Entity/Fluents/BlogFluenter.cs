using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Blog.API.Entity.Models;

namespace Blog.API.Entity.Fluents
{
    public class BlogFluenter
    {
        public BlogFluenter(EntityTypeBuilder<BlogModel> modelBuilder)
        {
            modelBuilder.ToTable("Blogs");
            modelBuilder
           .Property(b => b.B_Title)
           .HasMaxLength(255)
           .HasComment("标题");
            modelBuilder
            .Property(b => b.B_Images)
            .HasMaxLength(255)
            .HasComment("图片路径");
            modelBuilder
            .Property(b => b.B_Content)
            .HasColumnType("ntext")
            .HasComment("内容");
            modelBuilder
            .Property(b => b.B_Comment)
            .HasColumnType("ntext")
            .HasComment("回复内容（JSON）");
            modelBuilder
            .Property(b => b.B_Watched)
            .HasComment("查看数量");
            modelBuilder
            .Property(b => b.B_Replied)
            .HasComment("回复数量");
            modelBuilder
            .Property(b => b.Creatdate)
            .HasColumnType("datetime")
            .HasComment("创建时间");
            modelBuilder
           .Property(b => b.Modifydate)
           .HasColumnType("datetime")
           .HasComment("最后一次修改时间");
        }
    }
}
