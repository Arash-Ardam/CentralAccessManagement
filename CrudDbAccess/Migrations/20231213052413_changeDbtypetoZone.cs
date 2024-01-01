using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrudDbAccess.Migrations
{
    /// <inheritdoc />
    public partial class changeDbtypetoZone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DbType",
                table: "InfraDatabases",
                newName: "Zone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Zone",
                table: "InfraDatabases",
                newName: "DbType");
        }
    }
}
