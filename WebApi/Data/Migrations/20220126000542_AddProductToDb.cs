using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class AddProductToDb : Migration
    {
        /// <summary>
        /// The up method is called when migrating up the database
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sellerId = table.Column<int>(nullable: false),
                    cost = table.Column<int>(nullable: false),
                    productName = table.Column<string>(nullable: true),
                    amountAvailable = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.id);
                });
        }

        /// <summary>
        /// The down method is called when migrating down the database
        /// </summary>
        /// <param name="migrationBuilder">To perform migration operations</param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "products");
        }
    }
}
