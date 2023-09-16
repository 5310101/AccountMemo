using System;
using System.Collections.Generic;
using AccountMemo_Domain.Models;
using AccountMemo_EFCore.ModelMap_CreateTable;
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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=QUANNA\\QUANNA;Initial Catalog=AccountMemo;Persist Security Info=True;TrustServerCertificate=true;User ID=sa;Password=Admin@123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AccountMap());
        modelBuilder.ApplyConfiguration(new UserStoreMap());

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
