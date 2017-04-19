using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using UrlShortener.DataAccess.DbContext;

namespace UrlShortener.Migrations
{
    [DbContext(typeof(UrlShortenerContext))]
    [Migration("20170418081640_UrlShortenerInitialMigration")]
    partial class UrlShortenerInitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("UrlShortener.Entities.ShortenedUrl", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ShortUrl")
                        .IsRequired();

                    b.Property<string>("Url")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("ShortenedUrls");
                });
        }
    }
}
