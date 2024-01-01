using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrudDbAccess.Migrations
{
    /// <inheritdoc />
    public partial class addFieldNameInfraDatabases : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "InfraDatabases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "InfraDatabases");
        }
    }
}
