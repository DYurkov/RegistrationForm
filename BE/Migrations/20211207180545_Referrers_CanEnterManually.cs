using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class Referrers_CanEnterManually : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomUserRegistrationReferrer",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CanEnterManually",
                table: "RegistrationReferrers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.Sql("INSERT INTO RegistrationReferrers (Name, CanEnterManually) VALUES ('From my friend', 0), ('Advertisement on web site', 0), ('Other', 1)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomUserRegistrationReferrer",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CanEnterManually",
                table: "RegistrationReferrers");
        }
    }
}
