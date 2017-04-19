using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using UrlShortener.DataAccess.DbContext;

namespace UrlShortener.Migrations
{
    [DbContext(typeof(UrlShortenerContext))]
    partial class UrlShortenerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("UrlShortener.Entities.ShortenedUrlEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("ShortenedUrl")
                        .IsRequired();

                    b.Property<string>("Url")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("ShortenedUrls");
                });
        }
    }
}
