using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.API.Migrations
{
    /// <inheritdoc />
    public partial class insertotherentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blog_Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bid = table.Column<int>(type: "int", nullable: false),
                    Tid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BTitle = table.Column<string>(name: "B_Title", type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "标题"),
                    BImages = table.Column<string>(name: "B_Images", type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "图片路径"),
                    BContent = table.Column<string>(name: "B_Content", type: "ntext", nullable: false, comment: "内容"),
                    BComment = table.Column<string>(name: "B_Comment", type: "ntext", nullable: false, comment: "回复内容（JSON）"),
                    BWatched = table.Column<int>(name: "B_Watched", type: "int", nullable: false, comment: "查看数量"),
                    BReplied = table.Column<int>(name: "B_Replied", type: "int", nullable: false, comment: "回复数量"),
                    Creatdate = table.Column<DateTime>(type: "datetime", nullable: false, comment: "创建时间"),
                    Modifydate = table.Column<DateTime>(type: "datetime", nullable: false, comment: "最后一次修改时间"),
                    Uid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TName = table.Column<string>(name: "T_Name", type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "标签名称"),
                    Creatdate = table.Column<DateTime>(type: "datetime", nullable: false, comment: "创建时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blog_Tags");

            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "Tags");
        }
    }
}
