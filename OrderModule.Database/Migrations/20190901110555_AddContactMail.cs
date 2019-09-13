using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderModule.Database.Migrations
{
    public partial class AddContactMail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContactMail",
                schema: "dbo",
                table: "orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactMail",
                schema: "dbo",
                table: "orders");
        }
    }
}
