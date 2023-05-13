﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using task.Data;

namespace task.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230512182258_updateUserName")]
    partial class updateUserName
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024");

            modelBuilder.Entity("task.Models.Person", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("address");

                    b.Property<string>("birthdate");

                    b.Property<string>("familyName");

                    b.Property<string>("fatherName");

                    b.Property<string>("firstName");

                    b.Property<string>("password");

                    b.Property<string>("userName");

                    b.HasKey("id");

                    b.HasIndex("userName")
                        .IsUnique();

                    b.ToTable("Persons");
                });
#pragma warning restore 612, 618
        }
    }
}
