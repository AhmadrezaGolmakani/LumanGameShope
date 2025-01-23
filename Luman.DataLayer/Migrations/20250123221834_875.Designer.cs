﻿// <auto-generated />
using System;
using Luman.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Luman.DataLayer.Migrations
{
    [DbContext(typeof(LumanContext))]
    [Migration("20250123221834_875")]
    partial class _875
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Luman.DataLayer.EntityModel.Permitions.Permition", b =>
                {
                    b.Property<int>("PermissionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PermissionID"));

                    b.Property<int?>("ParentID")
                        .HasColumnType("int");

                    b.Property<string>("PermissionTitel")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("PermissionID");

                    b.HasIndex("ParentID");

                    b.ToTable("permitions");
                });

            modelBuilder.Entity("Luman.DataLayer.EntityModel.Permitions.RolePermission", b =>
                {
                    b.Property<int>("RP_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RP_id"));

                    b.Property<int>("PermissionID")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("RP_id");

                    b.HasIndex("PermissionID");

                    b.HasIndex("RoleId");

                    b.ToTable("rolePermissions");
                });

            modelBuilder.Entity("Luman.DataLayer.EntityModel.Product.CategoryProduct", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("pId")
                        .HasColumnType("int");

                    b.HasKey("CategoryId");

                    b.HasIndex("pId");

                    b.ToTable("categories");
                });

            modelBuilder.Entity("Luman.DataLayer.EntityModel.Product.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<long>("Price")
                        .HasColumnType("bigint");

                    b.Property<string>("imagename")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductId");

                    b.ToTable("products");
                });

            modelBuilder.Entity("Luman.DataLayer.EntityModel.User.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("roles");
                });

            modelBuilder.Entity("Luman.DataLayer.EntityModel.User.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Family")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.HasKey("UserId");

                    b.ToTable("users");
                });

            modelBuilder.Entity("Luman.DataLayer.EntityModel.User.UserRole", b =>
                {
                    b.Property<int>("RU_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RU_Id"));

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("RU_Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("userRoles");
                });

            modelBuilder.Entity("Luman.DataLayer.EntityModel.Permitions.Permition", b =>
                {
                    b.HasOne("Luman.DataLayer.EntityModel.Permitions.Permition", null)
                        .WithMany("permission")
                        .HasForeignKey("ParentID");
                });

            modelBuilder.Entity("Luman.DataLayer.EntityModel.Permitions.RolePermission", b =>
                {
                    b.HasOne("Luman.DataLayer.EntityModel.Permitions.Permition", "permition")
                        .WithMany("rolePermissions")
                        .HasForeignKey("PermissionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Luman.DataLayer.EntityModel.User.Role", "role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("permition");

                    b.Navigation("role");
                });

            modelBuilder.Entity("Luman.DataLayer.EntityModel.Product.CategoryProduct", b =>
                {
                    b.HasOne("Luman.DataLayer.EntityModel.Product.Product", "product")
                        .WithMany("categories")
                        .HasForeignKey("pId");

                    b.Navigation("product");
                });

            modelBuilder.Entity("Luman.DataLayer.EntityModel.User.UserRole", b =>
                {
                    b.HasOne("Luman.DataLayer.EntityModel.User.Role", "role")
                        .WithMany("userRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Luman.DataLayer.EntityModel.User.User", "user")
                        .WithMany("userRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("role");

                    b.Navigation("user");
                });

            modelBuilder.Entity("Luman.DataLayer.EntityModel.Permitions.Permition", b =>
                {
                    b.Navigation("permission");

                    b.Navigation("rolePermissions");
                });

            modelBuilder.Entity("Luman.DataLayer.EntityModel.Product.Product", b =>
                {
                    b.Navigation("categories");
                });

            modelBuilder.Entity("Luman.DataLayer.EntityModel.User.Role", b =>
                {
                    b.Navigation("userRoles");
                });

            modelBuilder.Entity("Luman.DataLayer.EntityModel.User.User", b =>
                {
                    b.Navigation("userRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
