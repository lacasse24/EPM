using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using SqueletteImplantation.DbEntities;

namespace squeletteimplantation.Migrations
{
    [DbContext(typeof(BD_EPM))]
    [Migration("20171103163832_HistoriqueP2")]
    partial class HistoriqueP2
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

            modelBuilder.Entity("SqueletteImplantation.DbEntities.Models.RelTracCrit", b =>
                {
                    b.Property<int>("CritId");

                    b.Property<int>("TracId");

                    b.HasKey("CritId", "TracId");

                    b.HasIndex("TracId");

                    b.ToTable("RelTracCrit");
                });

            modelBuilder.Entity("SqueletteImplantation.DbEntities.Models.RelTracUsag", b =>
                {
                    b.Property<int>("TracId");

                    b.Property<int>("UtilId");

                    b.Property<DateTime>("DateTelechargement")
                        .ValueGeneratedOnAdd();

                    b.HasKey("TracId", "UtilId");

                    b.HasIndex("UtilId");

                    b.ToTable("RelTracUsager");
                });

            modelBuilder.Entity("SqueletteImplantation.DbEntities.Models.Trace", b =>
                {
                    b.Property<int>("TracId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("TracLogi");

                    b.Property<string>("TracUrl")
                        .IsRequired();

                    b.Property<string>("TraceNom")
                        .IsRequired();

                    b.HasKey("TracId");

                    b.ToTable("Trace");
                });

            modelBuilder.Entity("SqueletteImplantation.DbEntities.Models.Utilisateur", b =>
                {
                    b.Property<int>("UtilId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("UtilEmail")
                        .IsRequired();

                    b.Property<string>("UtilNom")
                        .IsRequired();

                    b.Property<string>("UtilPWD")
                        .IsRequired();

                    b.Property<string>("UtilPren")
                        .IsRequired();

                    b.Property<int>("UtilType");

                    b.Property<string>("UtilUserName")
                        .IsRequired();

                    b.HasKey("UtilId");

                    b.HasIndex("UtilEmail")
                        .IsUnique();

                    b.HasIndex("UtilUserName")
                        .IsUnique();

                    b.ToTable("Utilisateur");
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

            modelBuilder.Entity("SqueletteImplantation.DbEntities.Models.RelTracUsag", b =>
                {
                    b.HasOne("SqueletteImplantation.DbEntities.Models.Trace", "trace")
                        .WithMany("reltracusag")
                        .HasForeignKey("TracId")
                        .HasConstraintName("fk_Trace_relTracUsag")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SqueletteImplantation.DbEntities.Models.Utilisateur", "utilisateur")
                        .WithMany("reltracusag")
                        .HasForeignKey("UtilId")
                        .HasConstraintName("fk_Utilisateur_relTracUsag")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
