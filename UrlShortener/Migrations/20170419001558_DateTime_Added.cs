using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UrlShortener.Migrations
{
    public partial class DateTime_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShortUrl",
                table: "ShortenedUrls",
                newName: "ShortenedUrl");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "ShortenedUrls",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "ShortenedUrls");

            migrationBuilder.RenameColumn(
                name: "ShortenedUrl",
                table: "ShortenedUrls",
                newName: "ShortUrl");
        }
    }
}
