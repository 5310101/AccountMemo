using AccountMemo_Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountMemo_EFCore.ModelMap_CreateTable
{
    public class AccountMap : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            //builder.HasKey(e => e.Id).HasName("PK__Account__3213E83F5CE46CF7");

            //builder.ToTable("Account");

            //builder.Property(e => e.Id).HasColumnName("id");
            //builder.Property(e => e.Password)
            //    .HasMaxLength(200)
            //    .IsUnicode(false);
            //builder.Property(e => e.Username)
            //    .HasMaxLength(200)
            //    .IsUnicode(false);

            //builder.HasOne(d => d.AccountHolder).WithMany(p => p.Accounts)
            //    .HasForeignKey(d => d.Id)
            //    .HasConstraintName("FK__Account__UserId__29572725");
        }
    }
}
