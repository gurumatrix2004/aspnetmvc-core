﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PatientRepository;

namespace PatientRepository.Migrations
{
    [DbContext(typeof(PatientDBContext))]
    [Migration("20200901130035_v2")]
    partial class v2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PatientLibrary.PatientAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("tblPatient");
                });

            modelBuilder.Entity("PatientLibrary.PatientProblem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("PatientAddressId")
                        .HasColumnType("int");

                    b.Property<string>("problem")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PatientAddressId");

                    b.ToTable("tblPatientProblem");
                });

            modelBuilder.Entity("PatientLibrary.Treatment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("PatientProblemId")
                        .HasColumnType("int");

                    b.Property<string>("treatmentDose")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("treatmentName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PatientProblemId");

                    b.ToTable("tblPatientTreatment");
                });

            modelBuilder.Entity("PatientLibrary.PatientProblem", b =>
                {
                    b.HasOne("PatientLibrary.PatientAddress", null)
                        .WithMany("lstproblems")
                        .HasForeignKey("PatientAddressId");
                });

            modelBuilder.Entity("PatientLibrary.Treatment", b =>
                {
                    b.HasOne("PatientLibrary.PatientProblem", null)
                        .WithMany("treatments")
                        .HasForeignKey("PatientProblemId");
                });
#pragma warning restore 612, 618
        }
    }
}
