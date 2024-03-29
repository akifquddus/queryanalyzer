﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApiR;

namespace WebApiR.Migrations
{
    [DbContext(typeof(WebApiRDbContext))]
    [Migration("20191111192613_update")]
    partial class update
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApiR.Models.Answers", b =>
                {
                    b.Property<int>("AnswerId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Data")
                        .IsRequired();

                    b.Property<int>("QuestionId");

                    b.Property<int>("UserId");

                    b.HasKey("AnswerId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("WebApiR.Models.Article", b =>
                {
                    b.Property<int>("ArticleId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryId")
                        .IsRequired();

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<string>("Image");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("ArticleId");

                    b.ToTable("Article");
                });

            modelBuilder.Entity("WebApiR.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryName")
                        .IsRequired();

                    b.Property<string>("Image")
                        .IsRequired();

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("WebApiR.Models.Growth", b =>
                {
                    b.Property<int>("GrowthId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Date")
                        .IsRequired();

                    b.Property<double>("Head");

                    b.Property<double>("Height");

                    b.Property<int>("UserId");

                    b.Property<double>("Weight");

                    b.HasKey("GrowthId");

                    b.ToTable("Growth");
                });

            modelBuilder.Entity("WebApiR.Models.Question", b =>
                {
                    b.Property<int>("QuestionId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Data")
                        .IsRequired();

                    b.HasKey("QuestionId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("WebApiR.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FullName")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<int>("UserType");

                    b.Property<string>("Username");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
