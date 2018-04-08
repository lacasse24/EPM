using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using SqueletteImplantation.DbEntities;

namespace squeletteimplantation.Migrations
{
    [DbContext(typeof(BD_EPM))]
    [Migration("20170901183948_Youhou")]
    partial class Youhou
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("SqueletteImplantation.DbEntities.Models.Categorie", b =>
                {
                    b.Property<int>("CatId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CatNom")
                        .IsRequired();

                    b.Property<int>("DomId");

                    b.HasKey("CatId");

                    b.HasIndex("DomId");

                    b.ToTable("Categorie");
                });

            modelBuilder.Entity("SqueletteImplantation.DbEntities.Models.Critere", b =>
                {
                    b.Property<int>("CritId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CatId");

                    b.Property<string>("CritNom")
                        .IsRequired();

                    b.HasKey("CritId");

                    b.HasIndex("CatId");

                    b.ToTable("Critere");
                });

            modelBuilder.Entity("SqueletteImplantation.DbEntities.Models.Domaine", b =>
                {
                    b.Property<int>("DomId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DomNom")
                        .IsRequired();

                    b.HasKey("DomId");

                    b.ToTable("Domaine");
                });

            modelBuilder.Entity("SqueletteImplantation.DbEntities.Models.Machin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("NombreMagique");

                    b.Property<string>("Truc")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Machin");
                });

            modelBuilder.Entity("SqueletteImplantation.DbEntities.Models.RelTracCrit", b =>
                {
                    b.Property<int>("CritId");

                    b.Property<int>("TracId");

                    b.HasKey("CritId", "TracId");

                    b.HasIndex("TracId");

                    b.ToTable("RelTracCrit");
                });

            modelBuilder.Entity("SqueletteImplantation.DbEntities.Models.Trace", b =>
                {
                    b.Property<int>("TracId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("TracUrl")
                        .IsRequired();

                    b.Property<string>("TraceNom")
                        .IsRequired();

                    b.HasKey("TracId");

                    b.ToTable("Trace");
                });

            modelBuilder.Entity("SqueletteImplantation.DbEntities.Models.Categorie", b =>
                {
                    b.HasOne("SqueletteImplantation.DbEntities.Models.Domaine", "domaine")
                        .WithMany("categories")
                        .HasForeignKey("DomId")
                        .HasConstraintName("fk_cat_dom")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SqueletteImplantation.DbEntities.Models.Critere", b =>
                {
                    b.HasOne("SqueletteImplantation.DbEntities.Models.Categorie", "categorie")
                        .WithMany("criteres")
                        .HasForeignKey("CatId")
                        .HasConstraintName("fk_crit_cat")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SqueletteImplantation.DbEntities.Models.RelTracCrit", b =>
                {
                    b.HasOne("SqueletteImplantation.DbEntities.Models.Critere", "criteres")
                        .WithMany("reltraccrit")
                        .HasForeignKey("CritId")
                        .HasConstraintName("fk_Critere_relTracCrit")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SqueletteImplantation.DbEntities.Models.Trace", "trace")
                        .WithMany("reltraccrit")
                        .HasForeignKey("TracId")
                        .HasConstraintName("fk_Trace_relTracCrit")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
