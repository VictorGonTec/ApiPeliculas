﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;
using peliculasAPI;

namespace peliculasAPI.Migrations
{
    [DbContext(typeof(AplicationDbContex))]
    [Migration("20220123232556_Peliculas")]
    partial class Peliculas
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("peliculasAPI.Entidades.Actor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Biiografia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Foto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Actores");
                });

            modelBuilder.Entity("peliculasAPI.Entidades.Cine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.Property<Point>("Ubicacion")
                        .HasColumnType("geography");

                    b.HasKey("Id");

                    b.ToTable("Cine");
                });

            modelBuilder.Entity("peliculasAPI.Entidades.Generos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Generos");
                });

            modelBuilder.Entity("peliculasAPI.Entidades.Peliculas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("EnCines")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaLanzamiento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Poster")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Resumen")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Triler")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Peliculas");
                });

            modelBuilder.Entity("peliculasAPI.Entidades.PeliculasActores", b =>
                {
                    b.Property<int>("ActorId")
                        .HasColumnType("int");

                    b.Property<int>("PeliculaId")
                        .HasColumnType("int");

                    b.Property<int>("Orden")
                        .HasColumnType("int");

                    b.Property<int?>("PeliculasId")
                        .HasColumnType("int");

                    b.Property<string>("Personaje")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ActorId", "PeliculaId");

                    b.HasIndex("PeliculasId");

                    b.ToTable("PeliculasActores");
                });

            modelBuilder.Entity("peliculasAPI.Entidades.PeliculasCines", b =>
                {
                    b.Property<int>("PeliculaId")
                        .HasColumnType("int");

                    b.Property<int>("CineId")
                        .HasColumnType("int");

                    b.Property<int?>("PeliculasId")
                        .HasColumnType("int");

                    b.HasKey("PeliculaId", "CineId");

                    b.HasIndex("CineId");

                    b.HasIndex("PeliculasId");

                    b.ToTable("PeliculasCines");
                });

            modelBuilder.Entity("peliculasAPI.Entidades.PeliculasGeneros", b =>
                {
                    b.Property<int>("PeliculaId")
                        .HasColumnType("int");

                    b.Property<int>("GeneroId")
                        .HasColumnType("int");

                    b.Property<int?>("PeliculasId")
                        .HasColumnType("int");

                    b.HasKey("PeliculaId", "GeneroId");

                    b.HasIndex("GeneroId");

                    b.HasIndex("PeliculasId");

                    b.ToTable("PeliculasGeneros");
                });

            modelBuilder.Entity("peliculasAPI.Entidades.PeliculasActores", b =>
                {
                    b.HasOne("peliculasAPI.Entidades.Actor", "Actor")
                        .WithMany("PeliculasActores")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("peliculasAPI.Entidades.Peliculas", "Peliculas")
                        .WithMany("PeliculasActores")
                        .HasForeignKey("PeliculasId");

                    b.Navigation("Actor");

                    b.Navigation("Peliculas");
                });

            modelBuilder.Entity("peliculasAPI.Entidades.PeliculasCines", b =>
                {
                    b.HasOne("peliculasAPI.Entidades.Cine", "Cine")
                        .WithMany("PeliculasCines")
                        .HasForeignKey("CineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("peliculasAPI.Entidades.Peliculas", "Peliculas")
                        .WithMany("PeliculasCines")
                        .HasForeignKey("PeliculasId");

                    b.Navigation("Cine");

                    b.Navigation("Peliculas");
                });

            modelBuilder.Entity("peliculasAPI.Entidades.PeliculasGeneros", b =>
                {
                    b.HasOne("peliculasAPI.Entidades.Generos", "Genero")
                        .WithMany("PeliculasGeneros")
                        .HasForeignKey("GeneroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("peliculasAPI.Entidades.Peliculas", "Peliculas")
                        .WithMany("PeliculasGeneros")
                        .HasForeignKey("PeliculasId");

                    b.Navigation("Genero");

                    b.Navigation("Peliculas");
                });

            modelBuilder.Entity("peliculasAPI.Entidades.Actor", b =>
                {
                    b.Navigation("PeliculasActores");
                });

            modelBuilder.Entity("peliculasAPI.Entidades.Cine", b =>
                {
                    b.Navigation("PeliculasCines");
                });

            modelBuilder.Entity("peliculasAPI.Entidades.Generos", b =>
                {
                    b.Navigation("PeliculasGeneros");
                });

            modelBuilder.Entity("peliculasAPI.Entidades.Peliculas", b =>
                {
                    b.Navigation("PeliculasActores");

                    b.Navigation("PeliculasCines");

                    b.Navigation("PeliculasGeneros");
                });
#pragma warning restore 612, 618
        }
    }
}
