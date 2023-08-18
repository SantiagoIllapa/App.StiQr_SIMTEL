using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using StiQr_SIMTEL.Shared;

namespace StiQr_SIMTEL.Server.Models;

public partial class StiqrDbContext : DbContext
{
    public StiqrDbContext()
    {
    }

    public StiqrDbContext(DbContextOptions<StiqrDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Label> Labels { get; set; }

    public virtual DbSet<MarkTime> MarkTimes { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolesPermission> RolesPermissions { get; set; }

    public virtual DbSet<RolesUser> RolesUsers { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    public virtual DbSet<Wallet> Wallets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Label>(entity =>
        {
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.IdVehicle).HasColumnName("Id_Vehicle");

            entity.HasOne(d => d.IdVehicleNavigation).WithMany(p => p.Labels)
                .HasForeignKey(d => d.IdVehicle)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Labels_Vehicles");
        });

        modelBuilder.Entity<MarkTime>(entity =>
        {
            entity.Property(e => e.IdLabel).HasColumnName("Id_Label");
            entity.Property(e => e.IdTransaction).HasColumnName("Id_Transaction");
            entity.Property(e => e.TimeAdmission)
                .HasColumnType("datetime")
                .HasColumnName("Time_Admission");
            entity.Property(e => e.TimeDeparture)
                .HasColumnType("datetime")
                .HasColumnName("Time_Departure");

            entity.HasOne(d => d.IdLabelNavigation).WithMany(p => p.MarkTimes)
                .HasForeignKey(d => d.IdLabel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MarkTimes_Labels");

            entity.HasOne(d => d.IdTransactionNavigation).WithMany(p => p.MarkTimes)
                .HasForeignKey(d => d.IdTransaction)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MarkTimes_Transactions");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RolesPermission>(entity =>
        {
            entity.ToTable("Roles_Permissions");

            entity.Property(e => e.IdPermission).HasColumnName("Id_Permission");
            entity.Property(e => e.IdRole).HasColumnName("Id_Role");

            entity.HasOne(d => d.IdPermissionNavigation).WithMany(p => p.RolesPermissions)
                .HasForeignKey(d => d.IdPermission)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Roles_Permissions_Permissions");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.RolesPermissions)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Roles_Permissions_Roles");
        });

        modelBuilder.Entity<RolesUser>(entity =>
        {
            entity.ToTable("Roles_Users");

            entity.Property(e => e.IdRole).HasColumnName("Id_Role");
            entity.Property(e => e.IdUser).HasColumnName("Id_User");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.RolesUsers)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Roles_Users_Roles");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.RolesUsers)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Roles_Users_Users");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.Property(e => e.DateTime).HasColumnType("datetime");
            entity.Property(e => e.IdUserTransmitter).HasColumnName("Id_User_Transmitter");
            entity.Property(e => e.IdWallet).HasColumnName("Id_Wallet");
            entity.Property(e => e.Observations)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.IdUserTransmitterNavigation).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.IdUserTransmitter)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transactions_Users");

            entity.HasOne(d => d.IdWalletNavigation).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.IdWallet)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transactions_Wallets");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Cidentity)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("CIdentity");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Alias)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IdUser).HasColumnName("Id_User");
            entity.Property(e => e.Plate)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vehicles_Users");
        });

        modelBuilder.Entity<Wallet>(entity =>
        {
            entity.Property(e => e.IdUserDriver).HasColumnName("Id_User_Driver");

            entity.HasOne(d => d.IdUserDriverNavigation).WithMany(p => p.Wallets)
                .HasForeignKey(d => d.IdUserDriver)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Wallets_Users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public DbSet<StiQr_SIMTEL.Shared.VehicleDTO> VehicleDTO { get; set; } = default!;
}
