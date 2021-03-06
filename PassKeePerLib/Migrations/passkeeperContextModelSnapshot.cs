﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PassKeePerLib.Data;

namespace PassKeePerLib.Migrations
{
    [DbContext(typeof(passkeeperContext))]
    partial class passkeeperContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Value")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("PassKeePerLib.Models.Accounts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int");

                    b.Property<string>("AccountLogin")
                        .HasColumnName("account_login")
                        .HasColumnType("varchar(256)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

                    b.Property<string>("AccountName")
                        .HasColumnName("account_name")
                        .HasColumnType("varchar(100)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

                    b.Property<string>("AccountPassword")
                        .HasColumnName("account_password")
                        .HasColumnType("varchar(256)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

                    b.Property<string>("AccountaDdress")
                        .HasColumnName("accounta_ddress")
                        .HasColumnType("varchar(500)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

                    b.Property<int>("WalletId")
                        .HasColumnName("wallet_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WalletId")
                        .HasName("wallet_id");

                    b.ToTable("accounts");
                });

            modelBuilder.Entity("PassKeePerLib.Models.BrowsingHistory", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("int");

                    b.Property<int>("AccountId")
                        .HasColumnName("account_id")
                        .HasColumnType("int");

                    b.HasKey("UserId", "AccountId")
                        .HasName("PRIMARY");

                    b.HasIndex("AccountId")
                        .HasName("account_id");

                    b.ToTable("browsing_history");
                });

            modelBuilder.Entity("PassKeePerLib.Models.EnterpriseWallets", b =>
                {
                    b.Property<int>("WalletId")
                        .HasColumnName("wallet_id")
                        .HasColumnType("int");

                    b.Property<int>("EnterpriseId")
                        .HasColumnName("enterprise_id")
                        .HasColumnType("int");

                    b.HasKey("WalletId", "EnterpriseId")
                        .HasName("PRIMARY");

                    b.HasIndex("EnterpriseId")
                        .HasName("enterprise_id");

                    b.HasIndex("WalletId")
                        .IsUnique()
                        .HasName("wallet_id");

                    b.ToTable("enterprise_wallets");
                });

            modelBuilder.Entity("PassKeePerLib.Models.EnterpriseWalletsAdministrators", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("int");

                    b.Property<int>("EnterpriseId")
                        .HasColumnName("enterprise_id")
                        .HasColumnType("int");

                    b.Property<int>("WalletId")
                        .HasColumnName("wallet_id")
                        .HasColumnType("int");

                    b.HasKey("UserId", "EnterpriseId", "WalletId")
                        .HasName("PRIMARY");

                    b.HasIndex("WalletId", "EnterpriseId")
                        .HasName("wallet_id");

                    b.ToTable("enterprise_wallets_administrators");
                });

            modelBuilder.Entity("PassKeePerLib.Models.EnterpriseWalletsApprovedWorkers", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("int");

                    b.Property<int>("EnterpriseId")
                        .HasColumnName("enterprise_id")
                        .HasColumnType("int");

                    b.Property<int>("WalletId")
                        .HasColumnName("wallet_id")
                        .HasColumnType("int");

                    b.HasKey("UserId", "EnterpriseId", "WalletId")
                        .HasName("PRIMARY");

                    b.HasIndex("WalletId", "EnterpriseId")
                        .HasName("wallet_id");

                    b.ToTable("enterprise_wallets_approved_workers");
                });

            modelBuilder.Entity("PassKeePerLib.Models.Enterprises", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int");

                    b.Property<string>("EnterpriseAddress")
                        .HasColumnName("enterprise_address")
                        .HasColumnType("varchar(75)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

                    b.Property<string>("EnterpriseEmail")
                        .HasColumnName("enterprise_email")
                        .HasColumnType("varchar(75)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

                    b.Property<string>("EnterpriseName")
                        .IsRequired()
                        .HasColumnName("enterprise_name")
                        .HasColumnType("varchar(200)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

                    b.HasKey("Id");

                    b.HasIndex("EnterpriseAddress")
                        .IsUnique()
                        .HasName("enterprise_address");

                    b.HasIndex("EnterpriseEmail")
                        .IsUnique()
                        .HasName("enterprise_email");

                    b.ToTable("enterprises");
                });

            modelBuilder.Entity("PassKeePerLib.Models.PersonalWallets", b =>
                {
                    b.Property<int>("WalletId")
                        .HasColumnName("wallet_id")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("int");

                    b.HasKey("WalletId", "UserId")
                        .HasName("PRIMARY");

                    b.HasIndex("UserId")
                        .HasName("user_id");

                    b.HasIndex("WalletId")
                        .IsUnique()
                        .HasName("wallet_id");

                    b.ToTable("personal_wallets");
                });

            modelBuilder.Entity("PassKeePerLib.Models.PersonalWalletsApprovedUsers", b =>
                {
                    b.Property<int>("WalletId")
                        .HasColumnName("wallet_id")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("int");

                    b.HasKey("WalletId", "UserId")
                        .HasName("PRIMARY");

                    b.HasIndex("UserId")
                        .HasName("user_id");

                    b.HasIndex("WalletId", "UserId")
                        .IsUnique()
                        .HasName("wallet_id");

                    b.ToTable("personal_wallets_approved_users");
                });

            modelBuilder.Entity("PassKeePerLib.Models.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("PassKeePerLib.Models.Wallets", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int");

                    b.Property<string>("WalletName")
                        .IsRequired()
                        .HasColumnName("wallet_name")
                        .HasColumnType("varchar(50)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

                    b.HasKey("Id");

                    b.ToTable("wallets");
                });

            modelBuilder.Entity("PassKeePerLib.Models.Workers", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("int");

                    b.Property<int>("EnterpriseId")
                        .HasColumnName("enterprise_id")
                        .HasColumnType("int");

                    b.HasKey("UserId", "EnterpriseId")
                        .HasName("PRIMARY");

                    b.HasIndex("EnterpriseId")
                        .HasName("enterprise_id");

                    b.ToTable("workers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("PassKeePerLib.Models.Users", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("PassKeePerLib.Models.Users", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PassKeePerLib.Models.Users", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("PassKeePerLib.Models.Users", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PassKeePerLib.Models.Accounts", b =>
                {
                    b.HasOne("PassKeePerLib.Models.Wallets", "Wallet")
                        .WithMany("Accounts")
                        .HasForeignKey("WalletId")
                        .HasConstraintName("accounts_ibfk_1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PassKeePerLib.Models.BrowsingHistory", b =>
                {
                    b.HasOne("PassKeePerLib.Models.Accounts", "Account")
                        .WithMany("BrowsingHistory")
                        .HasForeignKey("AccountId")
                        .HasConstraintName("browsing_history_ibfk_2")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PassKeePerLib.Models.Users", "User")
                        .WithMany("BrowsingHistory")
                        .HasForeignKey("UserId")
                        .HasConstraintName("browsing_history_ibfk_1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PassKeePerLib.Models.EnterpriseWallets", b =>
                {
                    b.HasOne("PassKeePerLib.Models.Enterprises", "Enterprise")
                        .WithMany("EnterpriseWallets")
                        .HasForeignKey("EnterpriseId")
                        .HasConstraintName("enterprise_wallets_ibfk_2")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PassKeePerLib.Models.Wallets", "Wallet")
                        .WithOne("EnterpriseWallets")
                        .HasForeignKey("PassKeePerLib.Models.EnterpriseWallets", "WalletId")
                        .HasConstraintName("enterprise_wallets_ibfk_1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PassKeePerLib.Models.EnterpriseWalletsAdministrators", b =>
                {
                    b.HasOne("PassKeePerLib.Models.Workers", "Workers")
                        .WithMany("EnterpriseWalletsAdministrators")
                        .HasForeignKey("UserId", "EnterpriseId")
                        .HasConstraintName("enterprise_wallets_administrators_ibfk_1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PassKeePerLib.Models.EnterpriseWallets", "EnterpriseWallets")
                        .WithMany("EnterpriseWalletsAdministrators")
                        .HasForeignKey("WalletId", "EnterpriseId")
                        .HasConstraintName("enterprise_wallets_administrators_ibfk_2")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PassKeePerLib.Models.EnterpriseWalletsApprovedWorkers", b =>
                {
                    b.HasOne("PassKeePerLib.Models.Workers", "Workers")
                        .WithMany("EnterpriseWalletsApprovedWorkers")
                        .HasForeignKey("UserId", "EnterpriseId")
                        .HasConstraintName("enterprise_wallets_approved_workers_ibfk_1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PassKeePerLib.Models.EnterpriseWallets", "EnterpriseWallets")
                        .WithMany("EnterpriseWalletsApprovedWorkers")
                        .HasForeignKey("WalletId", "EnterpriseId")
                        .HasConstraintName("enterprise_wallets_approved_workers_ibfk_2")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PassKeePerLib.Models.PersonalWallets", b =>
                {
                    b.HasOne("PassKeePerLib.Models.Users", "User")
                        .WithMany("PersonalWallets")
                        .HasForeignKey("UserId")
                        .HasConstraintName("personal_wallets_ibfk_2")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PassKeePerLib.Models.Wallets", "Wallet")
                        .WithOne("PersonalWallets")
                        .HasForeignKey("PassKeePerLib.Models.PersonalWallets", "WalletId")
                        .HasConstraintName("personal_wallets_ibfk_1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PassKeePerLib.Models.PersonalWalletsApprovedUsers", b =>
                {
                    b.HasOne("PassKeePerLib.Models.Users", "User")
                        .WithMany("PersonalWalletsApprovedUsers")
                        .HasForeignKey("UserId")
                        .HasConstraintName("personal_wallets_approved_users_ibfk_2")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PassKeePerLib.Models.Wallets", "Wallet")
                        .WithMany("PersonalWalletsApprovedUsers")
                        .HasForeignKey("WalletId")
                        .HasConstraintName("personal_wallets_approved_users_ibfk_1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PassKeePerLib.Models.Workers", b =>
                {
                    b.HasOne("PassKeePerLib.Models.Enterprises", "Enterprise")
                        .WithMany("Workers")
                        .HasForeignKey("EnterpriseId")
                        .HasConstraintName("workers_ibfk_2")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PassKeePerLib.Models.Users", "User")
                        .WithMany("Workers")
                        .HasForeignKey("UserId")
                        .HasConstraintName("workers_ibfk_1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
