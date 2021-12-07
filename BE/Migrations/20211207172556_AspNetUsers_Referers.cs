using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class AspNetUsers_Referers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RegistrationReferrerId",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RegistrationReferrers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrationReferrers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RegistrationReferrerId",
                table: "Users",
                column: "RegistrationReferrerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_RegistrationReferrers_RegistrationReferrerId",
                table: "Users",
                column: "RegistrationReferrerId",
                principalTable: "RegistrationReferrers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_RegistrationReferrers_RegistrationReferrerId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "RegistrationReferrers");

            migrationBuilder.DropIndex(
                name: "IX_Users_RegistrationReferrerId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RegistrationReferrerId",
                table: "Users");
        }
    }
}
