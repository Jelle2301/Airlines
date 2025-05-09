﻿using System;
using System.Collections.Generic;
using Domains.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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

    public virtual DbSet<Plaat> Plaats { get; set; }

    public virtual DbSet<Reisklasse> Reisklasses { get; set; }

    public virtual DbSet<Seizoen> Seizoens { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<Vertrekplaat> Vertrekplaats { get; set; }

    public virtual DbSet<Vliegtuig> Vliegtuigs { get; set; }

    public virtual DbSet<Vlucht> Vluchts { get; set; }

    public virtual DbSet<Zitplaat> Zitplaats { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // install this packages: - Microsoft.Extensions.Configuration.Json
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
            // add connectionstring to appsettings.json file (see appsettings.json)
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }
    }


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
            entity.Property(e => e.Status).IsUnicode(false);
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

            entity.Property(e => e.ExtraOmschrijving).IsUnicode(false);
            entity.Property(e => e.Naam).IsUnicode(false);
            entity.Property(e => e.PlaatsMaaltijd).IsUnicode(false);
            entity.Property(e => e.Soort).IsUnicode(false);
        });

        modelBuilder.Entity<Plaat>(entity =>
        {
            entity.HasKey(e => e.PlaatsId);

            entity.Property(e => e.Code).IsUnicode(false);
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
                .HasConstraintName("FK_Ticket_Seizoen");

            entity.HasOne(d => d.Vlucht).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.VluchtId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ticket_Vlucht");

            entity.HasOne(d => d.Zitplaats).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.ZitplaatsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ticket_Zitplaats");
        });

        modelBuilder.Entity<Vertrekplaat>(entity =>
        {
            entity.HasKey(e => e.VertrekplaatsId);

            entity.HasOne(d => d.Plaats).WithMany(p => p.Vertrekplaats)
                .HasForeignKey(d => d.PlaatsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vertrekplaats_Plaats");
        });

        modelBuilder.Entity<Vliegtuig>(entity =>
        {
            entity.HasKey(e => e.VliegtuigId).HasName("PK_Vliegtuig_1");

            entity.ToTable("Vliegtuig");
        });

        modelBuilder.Entity<Vlucht>(entity =>
        {
            entity.HasKey(e => e.VluchtId).HasName("PK_Vliegtuig");

            entity.ToTable("Vlucht");

            entity.Property(e => e.EventueleVolgendeOverstapId).HasColumnName("eventueleVolgendeOverstapId");
            entity.Property(e => e.IsOverstap).HasColumnName("isOverstap");
            entity.Property(e => e.TijdAankomst).HasColumnType("datetime");
            entity.Property(e => e.TijdVertrek).HasColumnType("datetime");

            entity.HasOne(d => d.Bestemming).WithMany(p => p.Vluchts)
                .HasForeignKey(d => d.BestemmingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vliegtuig_Bestemming");

            entity.HasOne(d => d.EventueleVolgendeOverstap).WithMany(p => p.InverseEventueleVolgendeOverstap)
                .HasForeignKey(d => d.EventueleVolgendeOverstapId)
                .HasConstraintName("FK_Vlucht_Vlucht");

            entity.HasOne(d => d.Vertrekplaats).WithMany(p => p.Vluchts)
                .HasForeignKey(d => d.VertrekplaatsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vliegtuig_Vertrekplaats");

            entity.HasOne(d => d.Vliegtuig).WithMany(p => p.Vluchts)
                .HasForeignKey(d => d.VliegtuigId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vlucht_Vliegtuig");
        });

        modelBuilder.Entity<Zitplaat>(entity =>
        {
            entity.HasKey(e => e.ZitplaatsId);

            entity.Property(e => e.IsBezet).HasColumnName("isBezet");

            entity.HasOne(d => d.Vlucht).WithMany(p => p.Zitplaats)
                .HasForeignKey(d => d.VluchtId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Zitplaats_Vlucht");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
