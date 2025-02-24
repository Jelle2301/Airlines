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

    public virtual DbSet<Bestemming> Bestemmings { get; set; }

    public virtual DbSet<Boeking> Boekings { get; set; }

    public virtual DbSet<Maaltijd> Maaltijds { get; set; }

    public virtual DbSet<Overstap> Overstaps { get; set; }

    public virtual DbSet<Plaat> Plaats { get; set; }

    public virtual DbSet<Reisklasse> Reisklasses { get; set; }

    public virtual DbSet<Seizoen> Seizoens { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<Vertrekplaat> Vertrekplaats { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQL22_VIVES; Database=flightDatabase ;Trusted_Connection=True; TrustServerCertificate=True;MultipleActiveResultSets=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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

            entity.Property(e => e.AchternaamBoeking)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.VoornaamBoeking)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Ticket).WithMany(p => p.Boekings)
                .HasForeignKey(d => d.TicketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Boeking_Ticket");
        });

        modelBuilder.Entity<Maaltijd>(entity =>
        {
            entity.ToTable("Maaltijd");

            entity.Property(e => e.Naam)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Soort)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Overstap>(entity =>
        {
            entity.ToTable("Overstap");

            entity.HasOne(d => d.Plaats).WithMany(p => p.Overstaps)
                .HasForeignKey(d => d.PlaatsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Overstap_Plaats");
        });

        modelBuilder.Entity<Plaat>(entity =>
        {
            entity.HasKey(e => e.PlaatsId);

            entity.Property(e => e.Naam)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Reisklasse>(entity =>
        {
            entity.ToTable("Reisklasse");

            entity.Property(e => e.SoortReisklasse)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Seizoen>(entity =>
        {
            entity.ToTable("Seizoen");

            entity.Property(e => e.SoortPeriode)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PK_Tickett");

            entity.ToTable("Ticket");

            entity.Property(e => e.Achternaam)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Voornaam)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Bestemming).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.BestemmingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ticket_Bestemming");

            entity.HasOne(d => d.Maaltijd).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.MaaltijdId)
                .HasConstraintName("FK_Ticket_Maaltijd");

            entity.HasOne(d => d.Overstap).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.OverstapId)
                .HasConstraintName("FK_Ticket_Overstap");

            entity.HasOne(d => d.Reisklasse).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.ReisklasseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ticket_Reisklasse");

            entity.HasOne(d => d.Seizoen).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.SeizoenId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ticket_Seizoen");

            entity.HasOne(d => d.Vertrekplaats).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.VertrekplaatsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ticket_Vertrekplaats");
        });

        modelBuilder.Entity<Vertrekplaat>(entity =>
        {
            entity.HasKey(e => e.VertrekplaatsId);

            entity.HasOne(d => d.Plaats).WithMany(p => p.Vertrekplaats)
                .HasForeignKey(d => d.PlaatsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vertrekplaats_Plaats");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
