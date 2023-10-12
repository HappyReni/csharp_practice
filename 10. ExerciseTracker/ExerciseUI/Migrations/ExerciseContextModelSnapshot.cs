﻿// <auto-generated />
using System;
using ExerciseUI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ExerciseUI.Migrations
{
    [DbContext(typeof(ExerciseContext))]
    partial class ExerciseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ExerciseUI.Model.ExerciseModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "comment");

                    b.Property<DateTime>("DateEnd")
                        .HasColumnType("datetime2")
                        .HasAnnotation("Relational:JsonPropertyName", "dateEnd");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("datetime2")
                        .HasAnnotation("Relational:JsonPropertyName", "dateStart");

                    b.HasKey("Id");

                    b.ToTable("Exercises");
                });
#pragma warning restore 612, 618
        }
    }
}
