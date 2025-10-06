using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TaskManagement.Data.Context;

public partial class TaskManagementDbContext : DbContext
{
    public TaskManagementDbContext()
    {
    }

    public TaskManagementDbContext(DbContextOptions<TaskManagementDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<GroupMember> GroupMembers { get; set; }

    public virtual DbSet<GroupTask> GroupTasks { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=taskmanagement;Username=taskuser;Password=taskpass");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Groups_pkey");

            entity.ToTable("Groups", "taskmanagement");

            entity.Property(e => e.GroupName).HasMaxLength(100);

            entity.HasOne(d => d.CreatedUser).WithMany(p => p.Groups)
                .HasForeignKey(d => d.CreatedUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Groups_CreatedUserId_fkey");
        });

        modelBuilder.Entity<GroupMember>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("GroupMembers_pkey");

            entity.ToTable("GroupMembers", "taskmanagement");

            entity.HasOne(d => d.Group).WithMany(p => p.GroupMembers)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("GroupMembers_GroupId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.GroupMembers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("GroupMembers_UserId_fkey");
        });

        modelBuilder.Entity<GroupTask>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("GroupTask_pkey");

            entity.ToTable("GroupTask", "taskmanagement");

            entity.HasOne(d => d.Group).WithMany(p => p.GroupTasks)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("GroupTask_GroupId_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.GroupTasks)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("GroupTask_StatusId_fkey");

            entity.HasOne(d => d.Task).WithMany(p => p.GroupTasks)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("GroupTask_TaskId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.GroupTasks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("GroupTask_UserId_fkey");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Statuses_pkey");

            entity.ToTable("Statuses", "taskmanagement");

            entity.Property(e => e.Status1)
                .HasMaxLength(100)
                .HasColumnName("Status");

            entity.HasOne(d => d.Group).WithMany(p => p.Statuses)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Statuses_GroupId_fkey");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Tasks_pkey");

            entity.ToTable("Tasks", "taskmanagement");

            entity.Property(e => e.Title).HasMaxLength(200);

            entity.HasOne(d => d.Group).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Tasks_GroupId_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Users_pkey");

            entity.ToTable("Users", "taskmanagement");

            entity.HasIndex(e => e.Email, "Users_Email_key").IsUnique();

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasDefaultValueSql("'User'::character varying");
            entity.Property(e => e.Username).HasMaxLength(150);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
