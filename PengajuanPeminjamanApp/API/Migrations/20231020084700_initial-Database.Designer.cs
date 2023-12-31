﻿// <auto-generated />
using System;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(RequestFasilityDbContext))]
    [Migration("20231020084700_initial-Database")]
    partial class initialDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.23")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("API.Model.Account", b =>
                {
                    b.Property<Guid>("Guid")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("guid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("create_date");

                    b.Property<DateTime>("ModifiedeDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("modified_date");

                    b.Property<int>("Otp")
                        .HasColumnType("int")
                        .HasColumnName("otp");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(25)")
                        .HasColumnName("password");

                    b.HasKey("Guid");

                    b.ToTable("tb_m_account");
                });

            modelBuilder.Entity("API.Model.AccountRole", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("guid");

                    b.Property<Guid>("AccountGuid")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("account_guid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("create_date");

                    b.Property<DateTime>("ModifiedeDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("modified_date");

                    b.Property<Guid>("RoleGuid")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("role_guid");

                    b.HasKey("Guid");

                    b.HasIndex("AccountGuid");

                    b.HasIndex("RoleGuid");

                    b.ToTable("tb_m_account_role");
                });

            modelBuilder.Entity("API.Model.Employee", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("guid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("create_date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("first_name");

                    b.Property<int>("Gender")
                        .HasColumnType("int")
                        .HasColumnName("gender");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("last_name");

                    b.Property<DateTime>("ModifiedeDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("modified_date");

                    b.Property<string>("Nik")
                        .IsRequired()
                        .HasColumnType("nchar(10)")
                        .HasColumnName("nik");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("phone_number");

                    b.HasKey("Guid");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Nik")
                        .IsUnique();

                    b.HasIndex("PhoneNumber")
                        .IsUnique();

                    b.ToTable("tb_m_employee");
                });

            modelBuilder.Entity("API.Model.Fasility", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("guid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("create_date");

                    b.Property<DateTime>("ModifiedeDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("name");

                    b.Property<int>("Stock")
                        .HasColumnType("int")
                        .HasColumnName("stock");

                    b.HasKey("Guid");

                    b.ToTable("tb_m_fasility");
                });

            modelBuilder.Entity("API.Model.ListFasility", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("guid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("create_date");

                    b.Property<Guid>("FasilityGuid")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("fasility_guid");

                    b.Property<DateTime>("ModifiedeDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("modified_date");

                    b.Property<Guid>("RequestGuid")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("request_guid");

                    b.Property<int>("TotalFasility")
                        .HasColumnType("int")
                        .HasColumnName("total_fasility");

                    b.HasKey("Guid");

                    b.HasIndex("FasilityGuid");

                    b.HasIndex("RequestGuid");

                    b.ToTable("tb_m_list_fasility");
                });

            modelBuilder.Entity("API.Model.Notification", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("guid");

                    b.Property<Guid>("AccountGuid")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("account_guid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("create_date");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsSeen")
                        .HasColumnType("bit")
                        .HasColumnName("is_seen");

                    b.Property<string>("Massage")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("massage");

                    b.Property<DateTime>("ModifiedeDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("modified_date");

                    b.HasKey("Guid");

                    b.HasIndex("AccountGuid");

                    b.ToTable("tb_m_notification");
                });

            modelBuilder.Entity("API.Model.Request", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("guid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("create_date");

                    b.Property<Guid>("EmployeeGuid")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("employee_guid");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("end_date");

                    b.Property<DateTime>("ModifiedeDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("modified_date");

                    b.Property<Guid>("RoomGuid")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("room_guid");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("start_date");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("status");

                    b.HasKey("Guid");

                    b.HasIndex("EmployeeGuid");

                    b.HasIndex("RoomGuid")
                        .IsUnique();

                    b.ToTable("tr_m_request");
                });

            modelBuilder.Entity("API.Model.Role", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("guid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("create_date");

                    b.Property<DateTime>("ModifiedeDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(25)")
                        .HasColumnName("name");

                    b.HasKey("Guid");

                    b.ToTable("tb_m_role");
                });

            modelBuilder.Entity("API.Model.Room", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("guid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("create_date");

                    b.Property<int>("Floor")
                        .HasColumnType("int")
                        .HasColumnName("floor");

                    b.Property<DateTime>("ModifiedeDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("name");

                    b.HasKey("Guid");

                    b.ToTable("tb_m_rooms");
                });

            modelBuilder.Entity("API.Model.Account", b =>
                {
                    b.HasOne("API.Model.Employee", "Employee")
                        .WithOne("Account")
                        .HasForeignKey("API.Model.Account", "Guid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("API.Model.AccountRole", b =>
                {
                    b.HasOne("API.Model.Account", "Account")
                        .WithMany("AccountRoles")
                        .HasForeignKey("AccountGuid")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("API.Model.Role", "Role")
                        .WithMany("AccountRoles")
                        .HasForeignKey("RoleGuid")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("API.Model.ListFasility", b =>
                {
                    b.HasOne("API.Model.Fasility", "Fasility")
                        .WithMany("ListFasilities")
                        .HasForeignKey("FasilityGuid")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("API.Model.Request", "Request")
                        .WithMany("ListFasilities")
                        .HasForeignKey("RequestGuid")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Fasility");

                    b.Navigation("Request");
                });

            modelBuilder.Entity("API.Model.Notification", b =>
                {
                    b.HasOne("API.Model.Account", "Account")
                        .WithMany("Notifications")
                        .HasForeignKey("AccountGuid")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("API.Model.Request", b =>
                {
                    b.HasOne("API.Model.Employee", "Employee")
                        .WithMany("Request")
                        .HasForeignKey("EmployeeGuid")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("API.Model.Room", "Room")
                        .WithOne("Request")
                        .HasForeignKey("API.Model.Request", "RoomGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("API.Model.Account", b =>
                {
                    b.Navigation("AccountRoles");

                    b.Navigation("Notifications");
                });

            modelBuilder.Entity("API.Model.Employee", b =>
                {
                    b.Navigation("Account");

                    b.Navigation("Request");
                });

            modelBuilder.Entity("API.Model.Fasility", b =>
                {
                    b.Navigation("ListFasilities");
                });

            modelBuilder.Entity("API.Model.Request", b =>
                {
                    b.Navigation("ListFasilities");
                });

            modelBuilder.Entity("API.Model.Role", b =>
                {
                    b.Navigation("AccountRoles");
                });

            modelBuilder.Entity("API.Model.Room", b =>
                {
                    b.Navigation("Request");
                });
#pragma warning restore 612, 618
        }
    }
}
