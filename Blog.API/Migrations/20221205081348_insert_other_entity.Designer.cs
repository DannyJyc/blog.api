﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Blog.API.Entity;

#nullable disable

namespace Blog.API.Migrations
{
    [DbContext(typeof(EFCoreContext))]
    [Migration("20221205081348_insert_other_entity")]
    partial class insertotherentity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Blog.API.Entity.Models.BlogModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("B_Comment")
                        .IsRequired()
                        .HasColumnType("ntext")
                        .HasComment("回复内容（JSON）");

                    b.Property<string>("B_Content")
                        .IsRequired()
                        .HasColumnType("ntext")
                        .HasComment("内容");

                    b.Property<string>("B_Images")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasComment("图片路径");

                    b.Property<int>("B_Replied")
                        .HasColumnType("int")
                        .HasComment("回复数量");

                    b.Property<string>("B_Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasComment("标题");

                    b.Property<int>("B_Watched")
                        .HasColumnType("int")
                        .HasComment("查看数量");

                    b.Property<DateTime>("Creatdate")
                        .HasColumnType("datetime")
                        .HasComment("创建时间");

                    b.Property<DateTime>("Modifydate")
                        .HasColumnType("datetime")
                        .HasComment("最后一次修改时间");

                    b.Property<int>("Uid")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Blogs", (string)null);
                });

            modelBuilder.Entity("Blog.API.Entity.Models.Blog_Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Bid")
                        .HasColumnType("int");

                    b.Property<int>("Tid")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Blog_Tags");
                });

            modelBuilder.Entity("Blog.API.Entity.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Creatdate")
                        .HasColumnType("datetime")
                        .HasComment("创建时间");

                    b.Property<string>("T_Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasComment("标签名称");

                    b.HasKey("Id");

                    b.ToTable("Tags", (string)null);
                });

            modelBuilder.Entity("Blog.API.Entity.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Creatdate")
                        .HasColumnType("datetime");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasComment("密码");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasComment("用户名");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
