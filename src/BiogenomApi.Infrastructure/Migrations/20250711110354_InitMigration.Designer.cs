﻿// <auto-generated />
using System;
using BiogenomApi.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BiogenomApi.Infrastructure.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20250711110354_InitMigration")]
    partial class InitMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BiogenomApi.Domain.Entities.DietarySupplement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Application")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("DietarySupplements");
                });

            modelBuilder.Entity("BiogenomApi.Domain.Entities.DietarySupplementImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("Data")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<int>("DietarySupplementId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DietarySupplementId")
                        .HasDatabaseName("IX_DietarySupplementImages_DietarySupplementId");

                    b.ToTable("DietarySupplementImages");
                });

            modelBuilder.Entity("BiogenomApi.Domain.Entities.Food", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("IX_Foods_Name");

                    b.ToTable("Foods");
                });

            modelBuilder.Entity("BiogenomApi.Domain.Entities.FoodVitamin", b =>
                {
                    b.Property<int>("FoodId")
                        .HasColumnType("integer");

                    b.Property<int>("VitaminId")
                        .HasColumnType("integer");

                    b.HasKey("FoodId", "VitaminId");

                    b.HasIndex("VitaminId");

                    b.ToTable("FoodVitamin");
                });

            modelBuilder.Entity("BiogenomApi.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("IX_Users_Email");

                    b.HasIndex("Gender")
                        .HasDatabaseName("IX_Users_Gender");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BiogenomApi.Domain.Entities.UserVitaminSurvey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("SurveyAtUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserVitaminSurvey");
                });

            modelBuilder.Entity("BiogenomApi.Domain.Entities.UserVitaminSurveyResult", b =>
                {
                    b.Property<int>("UserVitaminSurveyId")
                        .HasColumnType("integer");

                    b.Property<int>("VitaminId")
                        .HasColumnType("integer");

                    b.HasKey("UserVitaminSurveyId", "VitaminId");

                    b.HasIndex("VitaminId");

                    b.ToTable("UserVitaminSurveyResult");
                });

            modelBuilder.Entity("BiogenomApi.Domain.Entities.Vitamin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ImportanceForHealth")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<double>("LowerLimit")
                        .HasColumnType("double precision");

                    b.Property<int>("MeasurementUnit")
                        .HasColumnType("integer");

                    b.Property<string>("Prevention")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("ScarcityManifestation")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<double?>("UpperLimit")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("Vitamins");
                });

            modelBuilder.Entity("BiogenomApi.Domain.Entities.VitaminDietarySupplement", b =>
                {
                    b.Property<int>("VitaminId")
                        .HasColumnType("integer");

                    b.Property<int>("DietarySupplementId")
                        .HasColumnType("integer");

                    b.HasKey("VitaminId", "DietarySupplementId");

                    b.HasIndex("DietarySupplementId");

                    b.ToTable("VitaminDietarySupplements");
                });

            modelBuilder.Entity("BiogenomApi.Domain.Entities.DietarySupplementImage", b =>
                {
                    b.HasOne("BiogenomApi.Domain.Entities.DietarySupplement", "DietarySupplement")
                        .WithMany("Images")
                        .HasForeignKey("DietarySupplementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DietarySupplement");
                });

            modelBuilder.Entity("BiogenomApi.Domain.Entities.FoodVitamin", b =>
                {
                    b.HasOne("BiogenomApi.Domain.Entities.Food", "Food")
                        .WithMany("Vitamins")
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BiogenomApi.Domain.Entities.Vitamin", "Vitamin")
                        .WithMany()
                        .HasForeignKey("VitaminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("BiogenomApi.Domain.Entities.NutrientAmount", "Amount", b1 =>
                        {
                            b1.Property<int>("FoodVitaminFoodId")
                                .HasColumnType("integer");

                            b1.Property<int>("FoodVitaminVitaminId")
                                .HasColumnType("integer");

                            b1.Property<int>("Unit")
                                .HasColumnType("integer")
                                .HasColumnName("AmountUnit");

                            b1.Property<double>("Value")
                                .HasColumnType("double precision")
                                .HasColumnName("Amount");

                            b1.HasKey("FoodVitaminFoodId", "FoodVitaminVitaminId");

                            b1.ToTable("FoodVitamin");

                            b1.WithOwner()
                                .HasForeignKey("FoodVitaminFoodId", "FoodVitaminVitaminId");
                        });

                    b.Navigation("Amount")
                        .IsRequired();

                    b.Navigation("Food");

                    b.Navigation("Vitamin");
                });

            modelBuilder.Entity("BiogenomApi.Domain.Entities.UserVitaminSurvey", b =>
                {
                    b.HasOne("BiogenomApi.Domain.Entities.User", "User")
                        .WithMany("VitaminSurveys")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BiogenomApi.Domain.Entities.UserVitaminSurveyResult", b =>
                {
                    b.HasOne("BiogenomApi.Domain.Entities.UserVitaminSurvey", "UserVitaminSurvey")
                        .WithMany("Results")
                        .HasForeignKey("UserVitaminSurveyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BiogenomApi.Domain.Entities.Vitamin", "Vitamin")
                        .WithMany()
                        .HasForeignKey("VitaminId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.OwnsOne("BiogenomApi.Domain.Entities.NutrientAmount", "Amount", b1 =>
                        {
                            b1.Property<int>("UserVitaminSurveyResultUserVitaminSurveyId")
                                .HasColumnType("integer");

                            b1.Property<int>("UserVitaminSurveyResultVitaminId")
                                .HasColumnType("integer");

                            b1.Property<int>("Unit")
                                .HasColumnType("integer")
                                .HasColumnName("AmountUnit");

                            b1.Property<double>("Value")
                                .HasColumnType("double precision")
                                .HasColumnName("Amount");

                            b1.HasKey("UserVitaminSurveyResultUserVitaminSurveyId", "UserVitaminSurveyResultVitaminId");

                            b1.ToTable("UserVitaminSurveyResult");

                            b1.WithOwner()
                                .HasForeignKey("UserVitaminSurveyResultUserVitaminSurveyId", "UserVitaminSurveyResultVitaminId");
                        });

                    b.Navigation("Amount")
                        .IsRequired();

                    b.Navigation("UserVitaminSurvey");

                    b.Navigation("Vitamin");
                });

            modelBuilder.Entity("BiogenomApi.Domain.Entities.VitaminDietarySupplement", b =>
                {
                    b.HasOne("BiogenomApi.Domain.Entities.DietarySupplement", "DietarySupplement")
                        .WithMany("RelatedVitamins")
                        .HasForeignKey("DietarySupplementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BiogenomApi.Domain.Entities.Vitamin", "Vitamin")
                        .WithMany("RelatedSupplements")
                        .HasForeignKey("VitaminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("BiogenomApi.Domain.Entities.NutrientAmount", "Amount", b1 =>
                        {
                            b1.Property<int>("VitaminDietarySupplementVitaminId")
                                .HasColumnType("integer");

                            b1.Property<int>("VitaminDietarySupplementDietarySupplementId")
                                .HasColumnType("integer");

                            b1.Property<int>("Unit")
                                .HasColumnType("integer")
                                .HasColumnName("AmountUnit");

                            b1.Property<double>("Value")
                                .HasColumnType("double precision")
                                .HasColumnName("Amount");

                            b1.HasKey("VitaminDietarySupplementVitaminId", "VitaminDietarySupplementDietarySupplementId");

                            b1.ToTable("VitaminDietarySupplements");

                            b1.WithOwner()
                                .HasForeignKey("VitaminDietarySupplementVitaminId", "VitaminDietarySupplementDietarySupplementId");
                        });

                    b.Navigation("Amount")
                        .IsRequired();

                    b.Navigation("DietarySupplement");

                    b.Navigation("Vitamin");
                });

            modelBuilder.Entity("BiogenomApi.Domain.Entities.DietarySupplement", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("RelatedVitamins");
                });

            modelBuilder.Entity("BiogenomApi.Domain.Entities.Food", b =>
                {
                    b.Navigation("Vitamins");
                });

            modelBuilder.Entity("BiogenomApi.Domain.Entities.User", b =>
                {
                    b.Navigation("VitaminSurveys");
                });

            modelBuilder.Entity("BiogenomApi.Domain.Entities.UserVitaminSurvey", b =>
                {
                    b.Navigation("Results");
                });

            modelBuilder.Entity("BiogenomApi.Domain.Entities.Vitamin", b =>
                {
                    b.Navigation("RelatedSupplements");
                });
#pragma warning restore 612, 618
        }
    }
}
