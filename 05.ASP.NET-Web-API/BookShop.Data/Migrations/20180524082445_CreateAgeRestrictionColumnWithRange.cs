namespace BookShop.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class CreateAgeRestrictionColumnWithRange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AgeRestriction",
                table: "Books",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgeRestriction",
                table: "Books");
        }
    }
}
