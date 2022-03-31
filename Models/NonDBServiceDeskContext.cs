using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using PZLServiceDesk.Utility;

#nullable disable

namespace PZLServiceDesk.Models
{
    public partial class NonDBServiceDeskContext : DbContext
    {
        public NonDBServiceDeskContext()
        {
        }

        public NonDBServiceDeskContext(DbContextOptions<NonDBServiceDeskContext> options)
            : base(options)
        {
        }

        public virtual DbSet<IssueCategory> IssueCategory { get; set; }

        public virtual DbSet<GetAllIssues> GetAllIssues { get; set; }
        public virtual DbSet<Priority> Priority { get; set; }

        public virtual DbSet<GetSingleIssue> GetSingleIssue { get; set; }

        public virtual DbSet<GetUserforModification> GetUserforModifications { get; set; }

        public virtual DbSet<UserQuery> UserQuery { get; set; }

        public virtual DbSet<CountObj> CountObj { get; set; }


        public virtual DbSet<GetAllTechnicians> GetAllTechnicians { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");



            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
