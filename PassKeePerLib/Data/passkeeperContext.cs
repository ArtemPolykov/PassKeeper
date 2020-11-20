using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PassKeePerLib.Models;

namespace PassKeePerLib.Data
{
    public partial class passkeeperContext : IdentityDbContext<Users, IdentityRole<int>, int>
    {
        public passkeeperContext()
        {
        }

        public passkeeperContext(DbContextOptions<passkeeperContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Accounts> Accounts { get; set; }
        public virtual DbSet<BrowsingHistory> BrowsingHistory { get; set; }
        public virtual DbSet<EnterpriseWallets> EnterpriseWallets { get; set; }
        public virtual DbSet<EnterpriseWalletsAdministrators> EnterpriseWalletsAdministrators { get; set; }
        public virtual DbSet<EnterpriseWalletsApprovedWorkers> EnterpriseWalletsApprovedWorkers { get; set; }
        public virtual DbSet<Enterprises> Enterprises { get; set; }
        public virtual DbSet<PersonalWallets> PersonalWallets { get; set; }
        public virtual DbSet<PersonalWalletsApprovedUsers> PersonalWalletsApprovedUsers { get; set; }
        //public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Wallets> Wallets { get; set; }
        public virtual DbSet<Workers> Workers { get; set; }

//         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//         {
//             if (!optionsBuilder.IsConfigured)
//             {
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                 optionsBuilder.UseMySql("server=localhost;database=passkeeper;user=root;password=112358", x => x.ServerVersion("8.0.20-mysql"));
//             }
//         }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accounts>(entity =>
            {
                entity.ToTable("accounts");

                entity.HasIndex(e => e.WalletId)
                    .HasName("wallet_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountLogin)
                    .HasColumnName("account_login")
                    .HasColumnType("varchar(256)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.AccountName)
                    .HasColumnName("account_name")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.AccountPassword)
                    .HasColumnName("account_password")
                    .HasColumnType("varchar(256)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.AccountaDdress)
                    .HasColumnName("accounta_ddress")
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.WalletId).HasColumnName("wallet_id");

                entity.HasOne(d => d.Wallet)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.WalletId)
                    .HasConstraintName("accounts_ibfk_1");
            });

            modelBuilder.Entity<BrowsingHistory>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.AccountId })
                    .HasName("PRIMARY");

                entity.ToTable("browsing_history");

                entity.HasIndex(e => e.AccountId)
                    .HasName("account_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.BrowsingHistory)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("browsing_history_ibfk_2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BrowsingHistory)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("browsing_history_ibfk_1");
            });

            modelBuilder.Entity<EnterpriseWallets>(entity =>
            {
                entity.HasKey(e => new { e.WalletId, e.EnterpriseId })
                    .HasName("PRIMARY");

                entity.ToTable("enterprise_wallets");

                entity.HasIndex(e => e.EnterpriseId)
                    .HasName("enterprise_id");

                entity.HasIndex(e => e.WalletId)
                    .HasName("wallet_id")
                    .IsUnique();

                entity.Property(e => e.WalletId).HasColumnName("wallet_id");

                entity.Property(e => e.EnterpriseId).HasColumnName("enterprise_id");

                entity.HasOne(d => d.Enterprise)
                    .WithMany(p => p.EnterpriseWallets)
                    .HasForeignKey(d => d.EnterpriseId)
                    .HasConstraintName("enterprise_wallets_ibfk_2");

                entity.HasOne(d => d.Wallet)
                    .WithOne(p => p.EnterpriseWallets)
                    .HasForeignKey<EnterpriseWallets>(d => d.WalletId)
                    .HasConstraintName("enterprise_wallets_ibfk_1");
            });

            modelBuilder.Entity<EnterpriseWalletsAdministrators>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.EnterpriseId, e.WalletId })
                    .HasName("PRIMARY");

                entity.ToTable("enterprise_wallets_administrators");

                entity.HasIndex(e => new { e.WalletId, e.EnterpriseId })
                    .HasName("wallet_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.EnterpriseId).HasColumnName("enterprise_id");

                entity.Property(e => e.WalletId).HasColumnName("wallet_id");

                entity.HasOne(d => d.Workers)
                    .WithMany(p => p.EnterpriseWalletsAdministrators)
                    .HasForeignKey(d => new { d.UserId, d.EnterpriseId })
                    .HasConstraintName("enterprise_wallets_administrators_ibfk_1");

                entity.HasOne(d => d.EnterpriseWallets)
                    .WithMany(p => p.EnterpriseWalletsAdministrators)
                    .HasForeignKey(d => new { d.WalletId, d.EnterpriseId })
                    .HasConstraintName("enterprise_wallets_administrators_ibfk_2");
            });

            modelBuilder.Entity<EnterpriseWalletsApprovedWorkers>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.EnterpriseId, e.WalletId })
                    .HasName("PRIMARY");

                entity.ToTable("enterprise_wallets_approved_workers");

                entity.HasIndex(e => new { e.WalletId, e.EnterpriseId })
                    .HasName("wallet_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.EnterpriseId).HasColumnName("enterprise_id");

                entity.Property(e => e.WalletId).HasColumnName("wallet_id");

                entity.HasOne(d => d.Workers)
                    .WithMany(p => p.EnterpriseWalletsApprovedWorkers)
                    .HasForeignKey(d => new { d.UserId, d.EnterpriseId })
                    .HasConstraintName("enterprise_wallets_approved_workers_ibfk_1");

                entity.HasOne(d => d.EnterpriseWallets)
                    .WithMany(p => p.EnterpriseWalletsApprovedWorkers)
                    .HasForeignKey(d => new { d.WalletId, d.EnterpriseId })
                    .HasConstraintName("enterprise_wallets_approved_workers_ibfk_2");
            });

            modelBuilder.Entity<Enterprises>(entity =>
            {
                entity.ToTable("enterprises");

                entity.HasIndex(e => e.EnterpriseAddress)
                    .HasName("enterprise_address")
                    .IsUnique();

                entity.HasIndex(e => e.EnterpriseEmail)
                    .HasName("enterprise_email")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EnterpriseAddress)
                    .HasColumnName("enterprise_address")
                    .HasColumnType("varchar(75)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.EnterpriseEmail)
                    .HasColumnName("enterprise_email")
                    .HasColumnType("varchar(75)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.EnterpriseName)
                    .IsRequired()
                    .HasColumnName("enterprise_name")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<PersonalWallets>(entity =>
            {
                entity.HasKey(e => new { e.WalletId, e.UserId })
                    .HasName("PRIMARY");

                entity.ToTable("personal_wallets");

                entity.HasIndex(e => e.UserId)
                    .HasName("user_id");

                entity.HasIndex(e => e.WalletId)
                    .HasName("wallet_id")
                    .IsUnique();

                entity.Property(e => e.WalletId).HasColumnName("wallet_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PersonalWallets)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("personal_wallets_ibfk_2");

                entity.HasOne(d => d.Wallet)
                    .WithOne(p => p.PersonalWallets)
                    .HasForeignKey<PersonalWallets>(d => d.WalletId)
                    .HasConstraintName("personal_wallets_ibfk_1");
            });

            modelBuilder.Entity<PersonalWalletsApprovedUsers>(entity =>
            {
                entity.HasKey(e => new { e.WalletId, e.UserId })
                    .HasName("PRIMARY");

                entity.ToTable("personal_wallets_approved_users");

                entity.HasIndex(e => e.UserId)
                    .HasName("user_id");

                entity.HasIndex(e => new { e.WalletId, e.UserId })
                    .HasName("wallet_id")
                    .IsUnique();

                entity.Property(e => e.WalletId).HasColumnName("wallet_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PersonalWalletsApprovedUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("personal_wallets_approved_users_ibfk_2");

                entity.HasOne(d => d.Wallet)
                    .WithMany(p => p.PersonalWalletsApprovedUsers)
                    .HasForeignKey(d => d.WalletId)
                    .HasConstraintName("personal_wallets_approved_users_ibfk_1");
            });

            // modelBuilder.Entity<Users>(entity =>
            // {
            //     entity.ToTable("users");

            //     entity.HasIndex(e => e.Email)
            //         .HasName("email")
            //         .IsUnique();

            //     entity.HasIndex(e => e.Login)
            //         .HasName("login")
            //         .IsUnique();

            //     entity.Property(e => e.Id).HasColumnName("id");

            //     entity.Property(e => e.Email)
            //         .HasColumnName("email")
            //         .HasColumnType("varchar(70)")
            //         .HasCharSet("utf8mb4")
            //         .HasCollation("utf8mb4_0900_ai_ci");

            //     entity.Property(e => e.Login)
            //         .IsRequired()
            //         .HasColumnName("login")
            //         .HasColumnType("varchar(50)")
            //         .HasCharSet("utf8mb4")
            //         .HasCollation("utf8mb4_0900_ai_ci");

            //     entity.Property(e => e.UserName)
            //         .IsRequired()
            //         .HasColumnName("user_name")
            //         .HasColumnType("varchar(50)")
            //         .HasCharSet("utf8mb4")
            //         .HasCollation("utf8mb4_0900_ai_ci");

            //     entity.Property(e => e.UserlastName)
            //         .HasColumnName("userlast_name")
            //         .HasColumnType("varchar(50)")
            //         .HasCharSet("utf8mb4")
            //         .HasCollation("utf8mb4_0900_ai_ci");
            // });

            modelBuilder.Entity<Wallets>(entity =>
            {
                entity.ToTable("wallets");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.WalletName)
                    .IsRequired()
                    .HasColumnName("wallet_name")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Workers>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.EnterpriseId })
                    .HasName("PRIMARY");

                entity.ToTable("workers");

                entity.HasIndex(e => e.EnterpriseId)
                    .HasName("enterprise_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.EnterpriseId).HasColumnName("enterprise_id");

                entity.HasOne(d => d.Enterprise)
                    .WithMany(p => p.Workers)
                    .HasForeignKey(d => d.EnterpriseId)
                    .HasConstraintName("workers_ibfk_2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Workers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("workers_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
