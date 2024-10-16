using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RepasoAhoraSi.Data;

public partial class EventosDbContext : DbContext
{
    public EventosDbContext()
    {
    }

    public EventosDbContext(DbContextOptions<EventosDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attendees> Attendees { get; set; }

    public virtual DbSet<EventAttendance> EventAttendance { get; set; }

    public virtual DbSet<Events> Events { get; set; }

    public virtual DbSet<Organizers> Organizers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-68O5AFS\\SQLEXPRESS;Database=EventosDB;Integrated Security=true;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attendees>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Attendee__3214EC078568C627");

            entity.Property(e => e.AttendeeName).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.RegisteredAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<EventAttendance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EventAtt__3214EC0764861D27");

            entity.Property(e => e.CheckedInAt).HasColumnType("datetime");

            entity.HasOne(d => d.Attendee).WithMany(p => p.EventAttendance)
                .HasForeignKey(d => d.AttendeeId)
                .HasConstraintName("FK__EventAtte__Atten__52593CB8");

            entity.HasOne(d => d.Event).WithMany(p => p.EventAttendance)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK__EventAtte__Event__534D60F1");
        });

        modelBuilder.Entity<Events>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Events__3214EC0728E795E0");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EventDate).HasColumnType("datetime");
            entity.Property(e => e.EventName).HasMaxLength(100);
            entity.Property(e => e.Location).HasMaxLength(200);

            entity.HasOne(d => d.Organizer).WithMany(p => p.Events)
                .HasForeignKey(d => d.OrganizerId)
                .HasConstraintName("FK__Events__Organize__5441852A");
        });

        modelBuilder.Entity<Organizers>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Organize__3214EC0782767F60");

            entity.Property(e => e.ContactEmail).HasMaxLength(100);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.OrganizerName).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
