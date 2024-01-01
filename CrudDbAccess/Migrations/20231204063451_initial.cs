using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrudDbAccess.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InfraDatabases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DbType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfraDatabases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccessInformation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Port = table.Column<int>(type: "int", nullable: false),
                    ToId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Direction = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessInformation_InfraDatabases_FromId",
                        column: x => x.FromId,
                        principalTable: "InfraDatabases",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccessInformation_InfraDatabases_ToId",
                        column: x => x.ToId,
                        principalTable: "InfraDatabases",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessInformation_FromId",
                table: "AccessInformation",
                column: "FromId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessInformation_ToId",
                table: "AccessInformation",
                column: "ToId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessInformation");

            migrationBuilder.DropTable(
                name: "InfraDatabases");
        }
    }
}
