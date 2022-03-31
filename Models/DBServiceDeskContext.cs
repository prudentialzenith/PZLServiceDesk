using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace PZLServiceDesk.Models
{
    public partial class DBServiceDeskContext : DbContext
    {
        public DBServiceDeskContext()
        {
        }

        public DBServiceDeskContext(DbContextOptions<DBServiceDeskContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApplicationTable> ApplicationTables { get; set; }
        public virtual DbSet<Dept> Depts { get; set; }
        public virtual DbSet<IssueCategoryTbl> IssueCategoryTbls { get; set; }
        public virtual DbSet<IssueReOpenedTbl> IssueReOpenedTbls { get; set; }
        public virtual DbSet<IssueRole> IssueRoles { get; set; }
        public virtual DbSet<IssueTbl> IssueTbls { get; set; }
        public virtual DbSet<PriorityTbl> PriorityTbls { get; set; }
        public virtual DbSet<ReAssigneIssueTbl> ReAssigneIssueTbls { get; set; }
        public virtual DbSet<RequisitionRole> RequisitionRoles { get; set; }
        public virtual DbSet<ResolutionTbl> ResolutionTbls { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserApplicationTable> UserApplicationTables { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-SIANQ2V;initial catalog= PZLITservicedesk; Integrated Security=True;ConnectRetryCount=0");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<ApplicationTable>(entity =>
            {
                entity.Property(e => e.ApplicationDescription).IsUnicode(false);

                entity.Property(e => e.ApplicationName).IsUnicode(false);
            });

            modelBuilder.Entity<Dept>(entity =>
            {
                entity.Property(e => e.Dept1).IsUnicode(false);

                entity.Property(e => e.DeptAlias).IsUnicode(false);
            });

            modelBuilder.Entity<IssueCategoryTbl>(entity =>
            {
                entity.Property(e => e.AsigneeEmail).IsUnicode(false);

                entity.Property(e => e.Category).IsUnicode(false);

                entity.Property(e => e.CategoryAssignee).IsUnicode(false);

                entity.Property(e => e.Datecreated).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<IssueReOpenedTbl>(entity =>
            {
                entity.Property(e => e.Datecreated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IssueDesc).IsUnicode(false);
            });

            modelBuilder.Entity<IssueRole>(entity =>
            {
                entity.Property(e => e.Role).IsUnicode(false);

                entity.Property(e => e.RoleAlias).IsUnicode(false);
            });

            modelBuilder.Entity<IssueTbl>(entity =>
            {
                entity.Property(e => e.AssinedTo).IsUnicode(false);

                entity.Property(e => e.CreatedBy).IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.ResolvedBy).IsUnicode(false);

                entity.Property(e => e.Status).IsUnicode(false);

                entity.Property(e => e.Subject).IsUnicode(false);

                entity.HasOne(d => d.CategoryNavigation)
                    .WithMany(p => p.IssueTbls)
                    .HasForeignKey(d => d.Category)
                    .HasConstraintName("FK_IssueTbl_IssueTypeTbl");

                entity.HasOne(d => d.PriorityNavigation)
                    .WithMany(p => p.IssueTbls)
                    .HasForeignKey(d => d.Priority)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IssueTbl_PriorityTbl");
            });

            modelBuilder.Entity<PriorityTbl>(entity =>
            {
                entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PriorityDesc).IsUnicode(false);

                entity.Property(e => e.PriorityName).IsUnicode(false);
            });

            modelBuilder.Entity<ReAssigneIssueTbl>(entity =>
            {
                entity.Property(e => e.DateAssigned).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NewAssignee).IsUnicode(false);

                entity.Property(e => e.PreviousAssignee).IsUnicode(false);
            });

            modelBuilder.Entity<RequisitionRole>(entity =>
            {
                entity.Property(e => e.Role).IsUnicode(false);

                entity.Property(e => e.RoleAlias).IsUnicode(false);
            });

            modelBuilder.Entity<ResolutionTbl>(entity =>
            {
                entity.Property(e => e.DateResolved).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ResolutionDesc).IsUnicode(false);

                entity.Property(e => e.Resolvedby).IsUnicode(false);

                entity.Property(e => e.Status).IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Username).IsUnicode(false);

                entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Otp).IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.SessionId).IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('TRUE')");

                entity.HasOne(d => d.DeptDNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.DeptD)
                    .HasConstraintName("FK_User_Dept");

                entity.HasOne(d => d.IssueRole)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IssueRoleId)
                    .HasConstraintName("FK_User_Role");
            });

            modelBuilder.Entity<UserApplicationTable>(entity =>
            {
                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.SessionId).IsFixedLength(true);

                entity.Property(e => e.Status).IsUnicode(false);

                entity.Property(e => e.Token).IsUnicode(false);

                entity.Property(e => e.Username).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
