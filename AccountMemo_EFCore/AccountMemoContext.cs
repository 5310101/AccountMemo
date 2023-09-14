using System;
using System.Collections.Generic;
using AccountMemo_EFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountMemo_EFCore;

public partial class AccountMemoContext : DbContext
{
    public AccountMemoContext()
    {
    }

    public AccountMemoContext(DbContextOptions options) : base(options)
    {

    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<UserStore> UserStores { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseSqlServer("Data Source=NGUYENQUAN;Initial Catalog=AccountMemo;Persist Security Info=True;TrustServerCertificate=true;User ID=sa;Password=181091");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Account__3213E83F5CE46CF7");

            entity.ToTable("Account");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Account__UserId__29572725");
        });

        modelBuilder.Entity<UserStore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserStro__3213E83F5847BD50");

            entity.ToTable("UserStore");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AppPassword)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("App_Password");
            entity.Property(e => e.Name).HasMaxLength(200);

            entity.HasOne(d => d.Account).WithMany(p => p.UserStores)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("UserStore_Account_FK");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
