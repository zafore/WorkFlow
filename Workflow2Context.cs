using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WorkFlow.Models;

public partial class Workflow2Context : DbContext
{
    public Workflow2Context()
    {
    }

    public Workflow2Context(DbContextOptions<Workflow2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Application> Applications { get; set; } = null!;
    public virtual DbSet<Department> Departments { get; set; } = null!;
    public virtual DbSet<Section> Sections { get; set; } = null!;
    public virtual DbSet<UserInfo> UserInfos { get; set; } = null!;
    public virtual DbSet<Role> Roles { get; set; } = null!;
    public virtual DbSet<RoleUser> RoleUsers { get; set; } = null!;
    public virtual DbSet<Tool> Tools { get; set; } = null!;
    public virtual DbSet<SharedTable> SharedTables { get; set; } = null!;
    public virtual DbSet<ApplicationLevel> ApplicationLevels { get; set; } = null!;
    public virtual DbSet<ApplicationLink> ApplicationLinks { get; set; } = null!;
    public virtual DbSet<ApplicationLable> ApplicationLables { get; set; } = null!;
    public virtual DbSet<ApplicationRequirement> ApplicationRequirements { get; set; } = null!;
    public virtual DbSet<Request> Requests { get; set; } = null!;
    public virtual DbSet<RequestLevel> RequestLevels { get; set; } = null!;
    public virtual DbSet<LinkCondation> LinkCondations { get; set; } = null!;
    public virtual DbSet<ApplicationLevelAssigned> ApplicationLevelAssigneds { get; set; } = null!;
    public virtual DbSet<SystemInfo> SystemInfos { get; set; } = null!;
    public virtual DbSet<Action> Actions { get; set; } = null!;
    public virtual DbSet<ManagerType> ManagerTypes { get; set; } = null!;
    public virtual DbSet<FnEmp> FnEmps { get; set; } = null!;
    public virtual DbSet<SharedTableData> SharedTableData { get; set; } = null!;
    public virtual DbSet<ApplicationLevelAssigned> FnApplicationLevelAssigneds { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local);Database=workflow2;Integrated Security=True;Trusted_Connection=True;Persist Security Info=True;User ID=test;Password=123;Trusted_Connection=SSPI;Encrypt=true;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Application>(entity =>
        {
            entity.ToTable("Applications");
            entity.HasKey(e => e.ApplicationId);
            entity.Property(e => e.ApplicationRequirements).HasMaxLength(100);
            entity.Property(e => e.ApplicationNameAr).HasMaxLength(100);
            entity.Property(e => e.ApplicationNameEng).HasMaxLength(100);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.ToTable("Departments");
            entity.HasKey(e => e.DepartmentId);
  
            entity.Property(e => e.DepartmentNameEng).HasMaxLength(100);
        });

        modelBuilder.Entity<Section>(entity =>
        {
            entity.ToTable("Sections");
            entity.HasKey(e => e.SectionId);
     
            entity.Property(e => e.SectionNameEng).HasMaxLength(100);
        });

        modelBuilder.Entity<Tool>(entity =>
        {
            entity.ToTable("Tools");
            entity.HasKey(e => e.ToolsId);
            entity.Property(e => e.ToolName).HasMaxLength(100);
         
        });

        modelBuilder.Entity<SharedTable>(entity =>
        {
            entity.ToTable("SharedTables");
            entity.HasKey(e => e.SharedTableId);
            entity.Property(e => e.SharedTableName).HasMaxLength(100);
  
        });

        modelBuilder.Entity<ApplicationLevel>(entity =>
        {
            entity.ToTable("ApplicationLevels");
            entity.HasKey(e => e.ApplicationLevelId);
            entity.Property(e => e.ApplicationLevelName).HasMaxLength(100);
           
        });

        modelBuilder.Entity<ApplicationLink>(entity =>
        {
            entity.ToTable("ApplicationLinks");
          
        });

        modelBuilder.Entity<ApplicationLable>(entity =>
        {
            entity.ToTable("ApplicationLables");
            entity.HasKey(e => e.ApplicationLableId);
            
        });

        modelBuilder.Entity<ApplicationRequirement>(entity =>
        {
            entity.ToTable("ApplicationRequirements");
            entity.HasKey(e => e.ApplicationRequirementId);
      
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.ToTable("Requests");
            entity.HasKey(e => e.RequestId);
         
        });

        modelBuilder.Entity<RequestLevel>(entity =>
        {
            entity.ToTable("RequestLevels");
            entity.HasKey(e => e.RequestLevelId);
       
        });

        modelBuilder.Entity<LinkCondation>(entity =>
        {
            entity.ToTable("LinkCondations");
            entity.HasKey(e => e.LinkCondationId);
          
        });

        modelBuilder.Entity<ApplicationLevelAssigned>(entity =>
        {
            entity.ToTable("ApplicationLevelAssigneds");
            entity.HasKey(e => e.ApplicationLevelAssignedId);
        });

        modelBuilder.Entity<SystemInfo>(entity =>
        {
            entity.ToTable("SystemInfo");
            entity.HasKey(e => e.SystemInfoId);
            entity.Property(e => e.SystemInfoName).HasMaxLength(100);
       
        });

        modelBuilder.Entity<Action>(entity =>
        {
            entity.ToTable("Actions");
            entity.HasKey(e => e.ActionId);
            entity.Property(e => e.ActionName).HasMaxLength(100);
       
        });

        modelBuilder.Entity<ManagerType>(entity =>
        {
            entity.ToTable("ManagerTypes");
            entity.HasKey(e => e.ManagerTypeId);
            entity.Property(e => e.ManagerTypeName).HasMaxLength(100);
          
        });

        modelBuilder.Entity<FnEmp>(entity =>
        {
            entity.ToTable("FnEmps");
 
        });

        modelBuilder.Entity<SharedTableData>(entity =>
        {
            entity.ToTable("SharedTableData");
            entity.HasKey(e => e.SharedTableDataId);
            entity.Property(e => e.DataValue).HasMaxLength(100);
            entity.Property(e => e.DataValueAr).HasMaxLength(100);
            entity.Property(e => e.DataValueEng).HasMaxLength(100);
        });

        modelBuilder.Entity<UserInfo>(entity =>
        {
            entity.ToTable("UserInfo");
            entity.HasKey(e => e.UserId);
            entity.Property(e => e.UserName).HasMaxLength(50);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.UserIdentity).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(255);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Roles");
    
        });

        modelBuilder.Entity<RoleUser>(entity =>
        {
            entity.ToTable("RoleUser");
            entity.HasKey(e => e.RoleUserId);
            entity.HasOne(d => d.Role)
                .WithMany(p => p.RoleUsers)
         
                .OnDelete(DeleteBehavior.ClientSetNull)
           
               
            
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
