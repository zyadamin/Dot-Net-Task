﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using task.Data;

namespace task.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230525225047_deletecolumn")]
    partial class deletecolumn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("task.Models.Person", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("address");

                    b.Property<string>("birthdate");

                    b.Property<string>("familyName");

                    b.Property<string>("fatherName");

                    b.Property<string>("firstName");

                    b.Property<string>("password");

                    b.Property<string>("userName")
                        .HasMaxLength(250);

                    b.HasKey("id");

                    b.HasIndex("userName")
                        .IsUnique()
                        .HasFilter("[userName] IS NOT NULL");

                    b.ToTable("Persons");
                });
#pragma warning restore 612, 618
        }
    }
}
