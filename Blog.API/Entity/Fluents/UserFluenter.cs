using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Blog.API.Entity.Models;

namespace Blog.API.Entity.Fluents
{
    public class UserFluenter
    {
        public UserFluenter(EntityTypeBuilder<User> modelBuilder)
        {
            modelBuilder.ToTable("Users");
            modelBuilder
           .Property(b => b.Username)
           .HasMaxLength(255)
           .HasComment("用户名");
            modelBuilder
            .Property(b => b.Password)
            .HasMaxLength(20)
            .HasComment("密码");
            modelBuilder
            .Property(b => b.Creatdate)
            .HasColumnType("datetime");
        }
    }
}
