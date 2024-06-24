using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Libary.Migrations.Dblibary
{
    /// <inheritdoc />
    public partial class Identity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Epoches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EpochName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Epoches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenreName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Readers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NickName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Readers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegionName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenreId = table.Column<int>(type: "int", nullable: false),
                    EpochId = table.Column<int>(type: "int", nullable: false),
                    BookName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Annotation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PageCout = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Publications_Epoches",
                        column: x => x.EpochId,
                        principalTable: "Epoches",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Publications_Genres",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Deliveries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostOfficeNumber = table.Column<int>(type: "int", nullable: true),
                    PostOfficeAdress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ReaderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliveries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deliveries_Readers",
                        column: x => x.ReaderId,
                        principalTable: "Readers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Autors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    AutorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Pseudonym = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Autors_Regions",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LibaryChecks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicationId = table.Column<int>(type: "int", nullable: false),
                    DeliveryId = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibaryChecks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LibaryChecks_Deliveries",
                        column: x => x.DeliveryId,
                        principalTable: "Deliveries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LibaryChecks_Publications",
                        column: x => x.PublicationId,
                        principalTable: "Publications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PublicationAutors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    PublicationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicationAutors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PublicationAutors_Autors",
                        column: x => x.Id,
                        principalTable: "Autors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PublicationAutors_Publications",
                        column: x => x.PublicationId,
                        principalTable: "Publications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Autors_RegionId",
                table: "Autors",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_ReaderId",
                table: "Deliveries",
                column: "ReaderId");

            migrationBuilder.CreateIndex(
                name: "IX_LibaryChecks_DeliveryId",
                table: "LibaryChecks",
                column: "DeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_LibaryChecks_PublicationId",
                table: "LibaryChecks",
                column: "PublicationId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationAutors_PublicationId",
                table: "PublicationAutors",
                column: "PublicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Publications_EpochId",
                table: "Publications",
                column: "EpochId");

            migrationBuilder.CreateIndex(
                name: "IX_Publications_GenreId",
                table: "Publications",
                column: "GenreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LibaryChecks");

            migrationBuilder.DropTable(
                name: "PublicationAutors");

            migrationBuilder.DropTable(
                name: "Deliveries");

            migrationBuilder.DropTable(
                name: "Autors");

            migrationBuilder.DropTable(
                name: "Publications");

            migrationBuilder.DropTable(
                name: "Readers");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "Epoches");

            migrationBuilder.DropTable(
                name: "Genres");
        }
    }
}
