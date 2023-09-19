using AccountMemo_Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace AccountMemo_EFCore.ModelMap_CreateTable
{
    public class UserStoreMap : IEntityTypeConfiguration<UserStore>
    {
        public void Configure(EntityTypeBuilder<UserStore> modelBuilder)
        {

            //modelBuilder.HasKey(e => e.Id).HasName("PK__UserStro__3213E83F5847BD50");

            //modelBuilder.ToTable("UserStore");

            //modelBuilder.Property(e => e.Id).HasColumnName("id");
            //modelBuilder.Property(e => e.AppPassword)
            //        .HasMaxLength(200)
            //        .IsUnicode(false)
            //        .HasColumnName("App_Password");
            //modelBuilder.Property(e => e.Name).HasMaxLength(200);
        }
    }
}
