using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WorkFlow.Models;

public partial class WorkFlow2Context : DbContext
{
    public WorkFlow2Context()
    {
    }

    public WorkFlow2Context(DbContextOptions<WorkFlow2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Application> Applications { get; set; }
    public virtual DbSet<Department> Departments { get; set; }
    public virtual DbSet<Section> Sections { get; set; }
    public virtual DbSet<UserInfo> UserInfos { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<RoleUser> RoleUsers { get; set; }
    public virtual DbSet<Tool> Tools { get; set; }
    public virtual DbSet<SharedTable> SharedTables { get; set; }
    public virtual DbSet<ApplicationLevel> ApplicationLevels { get; set; }
    public virtual DbSet<ApplicationLink> ApplicationLinks { get; set; }
    public virtual DbSet<ApplicationLable> ApplicationLables { get; set; }
    public virtual DbSet<ApplicationRequirement> ApplicationRequirements { get; set; }
    public virtual DbSet<Request> Requests { get; set; }
    public virtual DbSet<RequestLevel> RequestLevels { get; set; }
    public virtual DbSet<LinkCondation> LinkCondations { get; set; }
    public virtual DbSet<ApplicationLevelAssigned> ApplicationLevelAssigneds { get; set; }
    public virtual DbSet<SystemInfo> SystemInfos { get; set; }
    public virtual DbSet<Action> Actions { get; set; }
    public virtual DbSet<ManagerType> ManagerTypes { get; set; }
    public virtual DbSet<FnEmp> FnEmps { get; set; }
    public virtual DbSet<FnApplicationLevelAssigned> FnApplicationLevelAssigneds { get; set; }
    public virtual DbSet<SharedTableData> SharedTableData { get; set; }
    public virtual DbSet<RequestDetail> RequestDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Connection string configured in Program.cs
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tool>(entity =>
        {
            entity.HasKey(e => e.ToolsId);
        });

        modelBuilder.Entity<ApplicationLink>(entity =>
        {
            entity.HasKey(e => e.ApplictionLinkId);
        });

        modelBuilder.Entity<FnEmp>(entity =>
        {
            entity.HasKey(e => e.EmployeeId);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId);
        });

        modelBuilder.Entity<RoleUser>(entity =>
        {
            entity.HasKey(e => e.RoleUserId);
            entity.HasOne(d => d.Role)
                .WithMany(p => p.RoleUsers)
                .HasForeignKey(d => d.RoleId);
            entity.HasOne(d => d.User)
                .WithMany(p => p.RoleUsers)
                .HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<LinkCondation>(entity =>
        {
            entity.HasOne(d => d.Action)
                .WithMany(p => p.LinkCondationActions)
                .HasForeignKey(d => d.ActionId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            entity.HasOne(d => d.ChangeAction)
                .WithMany(p => p.LinkCondationChangeActions)
                .HasForeignKey(d => d.ChangeActionId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });
        
        modelBuilder.Entity<ApplicationLable>(entity =>
        {
            entity.HasOne(d => d.FromApplicationLevel)
                .WithMany(p => p.ApplicationLableFromApplicationLevels)
                .HasForeignKey(d => d.FromApplicationLevelId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            entity.HasOne(d => d.ToApplicationLevel)
                .WithMany(p => p.ApplicationLableToApplicationLevels)
                .HasForeignKey(d => d.ToApplicationLevelId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });
        
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
