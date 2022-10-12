using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForumAPI.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Threads_ThreadId",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "ThreadId",
                table: "Posts",
                newName: "ThreadsId");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_ThreadId",
                table: "Posts",
                newName: "IX_Posts_ThreadsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Threads_ThreadsId",
                table: "Posts",
                column: "ThreadsId",
                principalTable: "Threads",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Threads_ThreadsId",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "ThreadsId",
                table: "Posts",
                newName: "ThreadId");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_ThreadsId",
                table: "Posts",
                newName: "IX_Posts_ThreadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Threads_ThreadId",
                table: "Posts",
                column: "ThreadId",
                principalTable: "Threads",
                principalColumn: "Id");
        }
    }
}
