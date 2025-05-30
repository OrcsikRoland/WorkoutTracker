using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNewFeatureMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutSessions_UserProfiles_UserProfileId",
                table: "WorkoutSessions");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutSessions_UserProfileId",
                table: "WorkoutSessions");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "WorkoutSessions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserProfileId",
                table: "WorkoutSessions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WeightKg = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutSessions_UserProfileId",
                table: "WorkoutSessions",
                column: "UserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutSessions_UserProfiles_UserProfileId",
                table: "WorkoutSessions",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
