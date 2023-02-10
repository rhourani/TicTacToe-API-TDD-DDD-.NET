using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SSHTicTacToe.Models;

public partial class SshContext : DbContext
{
    public SshContext()
    {
    }

    public SshContext(DbContextOptions<SshContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AuthorizedKey> AuthorizedKeys { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-AC9D4VB;Initial Catalog=SSH;Integrated Security=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthorizedKey>(entity =>
        {
            entity.HasKey(e => e.KeyId);

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.HostId).HasMaxLength(100);
            entity.Property(e => e.Ipadresses).HasColumnName("IPAdresses");
            entity.Property(e => e.KeyType).HasMaxLength(500);
            entity.Property(e => e.LastUsed).HasColumnType("datetime");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasMaxLength(100);
            entity.Property(e => e.ValidFrom).HasColumnType("datetime");
            entity.Property(e => e.ValidTo).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
