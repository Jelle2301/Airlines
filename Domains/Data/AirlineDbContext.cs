using System;
using System.Collections.Generic;
using Domains.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domains.Data;

public partial class AirlineDbContext : DbContext
{
    public AirlineDbContext()
    {
    }

    public AirlineDbContext(DbContextOptions<AirlineDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Bestemming> Bestemmings { get; set; }

    public virtual DbSet<Boeking> Boekings { get; set; }

    public virtual DbSet<Maaltijd> Maaltijds { get; set; }

    public virtual DbSet<Overstap> Overstaps { get; set; }

    public virtual DbSet<Plaats> Plaats { get; set; }

    public virtual DbSet<Reisklasse> Reisklasses { get; set; }

    public virtual DbSet<Seizoen> Seizoens { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<Vertrekplaats> Vertrekplaats { get; set; }

    public virtual DbSet<Vlucht> Vluchts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server = tcp:flight-project-vives.database.windows.net; Initial Catalog = flightDatabase; User ID = beheerder; Password = Flight1Ww.; MultipleActiveResultSets = True; Encrypt = True; TrustServerCertificate = True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.ProviderKey).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.Name).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Bestemming>(entity =>
        {
            entity.ToTable("Bestemming");

            entity.HasOne(d => d.Plaats).WithMany(p => p.Bestemmings)
                .HasForeignKey(d => d.PlaatsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bestemming_Plaats");
        });

        modelBuilder.Entity<Boeking>(entity =>
        {
            entity.ToTable("Boeking");

            entity.Property(e => e.AchternaamBoeking).IsUnicode(false);
            entity.Property(e => e.UserId).HasMaxLength(50);
            entity.Property(e => e.VoornaamBoeking).IsUnicode(false);

            entity.HasOne(d => d.Ticket).WithMany(p => p.Boekings)
                .HasForeignKey(d => d.TicketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Boeking_Ticket");
        });

        modelBuilder.Entity<Maaltijd>(entity =>
        {
            entity.ToTable("Maaltijd");

            entity.Property(e => e.Naam).IsUnicode(false);
            entity.Property(e => e.Soort).IsUnicode(false);
        });

        modelBuilder.Entity<Overstap>(entity =>
        {
            entity.ToTable("Overstap");

            entity.HasOne(d => d.Plaats).WithMany(p => p.Overstaps)
                .HasForeignKey(d => d.PlaatsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Overstap_Plaats");
        });

        modelBuilder.Entity<Plaats>(entity =>
        {
            entity.HasKey(e => e.PlaatsId);

            entity.Property(e => e.Naam).IsUnicode(false);
        });

        modelBuilder.Entity<Reisklasse>(entity =>
        {
            entity.ToTable("Reisklasse");

            entity.Property(e => e.SoortReisklasse).IsUnicode(false);
        });

        modelBuilder.Entity<Seizoen>(entity =>
        {
            entity.ToTable("Seizoen");

            entity.Property(e => e.SoortPeriode).IsUnicode(false);
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PK_Tickett");

            entity.ToTable("Ticket");

            entity.Property(e => e.Achternaam).IsUnicode(false);
            entity.Property(e => e.Voornaam).IsUnicode(false);

            entity.HasOne(d => d.Maaltijd).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.MaaltijdId)
                .HasConstraintName("FK_Ticket_Maaltijd");

            entity.HasOne(d => d.Reisklasse).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.ReisklasseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ticket_Reisklasse");

            entity.HasOne(d => d.Seizoen).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.SeizoenId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ticket_Seizoen");

            entity.HasOne(d => d.Vliegtuig).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.VliegtuigId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ticket_Vliegtuig");
        });

        modelBuilder.Entity<Vertrekplaats>(entity =>
        {
            entity.HasKey(e => e.VertrekplaatsId);

            entity.HasOne(d => d.Plaats).WithMany(p => p.Vertrekplaats)
                .HasForeignKey(d => d.PlaatsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vertrekplaats_Plaats");
        });

        modelBuilder.Entity<Vlucht>(entity =>
        {
            entity.HasKey(e => e.VliegtuigId).HasName("PK_Vliegtuig");

            entity.ToTable("Vlucht");

            entity.HasOne(d => d.Bestemming).WithMany(p => p.Vluchts)
                .HasForeignKey(d => d.BestemmingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vliegtuig_Bestemming");

            entity.HasOne(d => d.Overstap).WithMany(p => p.Vluchts)
                .HasForeignKey(d => d.OverstapId)
                .HasConstraintName("FK_Vliegtuig_Overstap");

            entity.HasOne(d => d.Vertrekplaats).WithMany(p => p.Vluchts)
                .HasForeignKey(d => d.VertrekplaatsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vliegtuig_Vertrekplaats");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
