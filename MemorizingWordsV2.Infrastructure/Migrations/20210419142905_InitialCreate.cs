using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MemorizingWordsV2.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PartOfSpeech",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    part_name = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartOfSpeech", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "RussianWords",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    word = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RussianWords", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "EnglishWords",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    word = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    part_of_speech_id = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnglishWords", x => x.id);
                    table.ForeignKey(
                        name: "FK__EnglishWo__part___45F365D3",
                        column: x => x.part_of_speech_id,
                        principalTable: "PartOfSpeech",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "English_Russian_Words",
                columns: table => new
                {
                    english_id = table.Column<int>(type: "int", nullable: false),
                    russian_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__English___599173F6506BDEBE", x => new { x.english_id, x.russian_id });
                    table.ForeignKey(
                        name: "FK__English_R__engli__4AB81AF0",
                        column: x => x.english_id,
                        principalTable: "EnglishWords",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__English_R__russi__4BAC3F29",
                        column: x => x.russian_id,
                        principalTable: "RussianWords",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_English_Russian_Words_russian_id",
                table: "English_Russian_Words",
                column: "russian_id");

            migrationBuilder.CreateIndex(
                name: "IX_EnglishWords_part_of_speech_id",
                table: "EnglishWords",
                column: "part_of_speech_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "English_Russian_Words");

            migrationBuilder.DropTable(
                name: "EnglishWords");

            migrationBuilder.DropTable(
                name: "RussianWords");

            migrationBuilder.DropTable(
                name: "PartOfSpeech");
        }
    }
}
