using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UrlShortener.Migrations
{
    public partial class ShortenUrlAsCalculatedLazzyLoading : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortenedUrl",
                table: "ShortenedUrls");

            migrationBuilder.AddColumn<int>(
                name: "Clicks",
                table: "ShortenedUrls",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Clicks",
                table: "ShortenedUrls");

            migrationBuilder.AddColumn<string>(
                name: "ShortenedUrl",
                table: "ShortenedUrls",
                nullable: false,
                defaultValue: "");
        }
    }
}
