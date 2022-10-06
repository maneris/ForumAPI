using System;
using Microsoft.EntityFrameworkCore.Migrations;
namespace ForumAPI.Migrations
{
    public class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThreadId = table.Column<int>(type: "int", nullable: false),
                    TopicId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_Topic_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Thread_ThreadId",
                        column: x => x.ThreadId,
                        principalTable: "Threads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.Id);
                });
            migrationBuilder.CreateTable(
                   name: "Threads",
                   columns: table => new
                   {
                       Id = table.Column<int>(type: "int", nullable: false)
                           .Annotation("SqlServer:Identity", "1, 1"),
                       Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                       Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                       CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                       TopicId = table.Column<int>(type: "int", nullable: false)
                   },
                   constraints: table =>
                   {
                       table.ForeignKey(
                          name: "FK_Topic_TopicId",
                          column: x => x.TopicId,
                          principalTable: "Topics",
                          principalColumn: "Id",
                          onDelete: ReferentialAction.Cascade);
                       table.PrimaryKey("PK_Threads", x => x.Id);
                   });

            migrationBuilder.CreateIndex(
                name: "IX_Threads_TopicsId",
                table: "Threads",
                column: "TopicId");
            migrationBuilder.CreateIndex(
                name: "IX_Posts_TopicsId",
                table: "Posts",
                column: "TopicId");
            migrationBuilder.CreateIndex(
                name: "IX_Threads_ThreadsId",
                table: "Posts",
                column: "ThreadID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Topics");
            migrationBuilder.DropTable(
                name: "Threads");

        }

    }
}
