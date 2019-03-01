using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace StudyHub.Models
{
    public partial class StudyHubContext : DbContext
    {
        public StudyHubContext()
        {
        }

        public StudyHubContext(DbContextOptions<StudyHubContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Enroll> Enrolls { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserAuth> UserAuths { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=localhost;Database=StudyHub;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .ForNpgsqlHasComment("Course Name");

                entity.HasOne(d => d.Publisher)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.PublisherId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Course_PublisherId_fkey");
            });

            modelBuilder.Entity<Enroll>(entity =>
            {
                entity.ToTable("Enroll");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Enrolls)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Enroll_CourseId_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Enrolls)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Enroll_UserId_fkey");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.NickName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Role).HasDefaultValueSql("'0'::smallint");
            });

            modelBuilder.Entity<UserAuth>(entity =>
            {
                entity.ToTable("UserAuth");

                entity.Property(e => e.Credential)
                    .IsRequired()
                    .HasMaxLength(500)
                    .ForNpgsqlHasComment("password or token");

                entity.Property(e => e.Identifier)
                    .IsRequired()
                    .HasMaxLength(200)
                    .ForNpgsqlHasComment("uid from 3rd party or email, phone");

                entity.Property(e => e.IdentityType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .ForNpgsqlHasComment("such as email, phone, facebook");

                entity.Property(e => e.InSite).ForNpgsqlHasComment("in site or 3rd party");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserAuths)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("UserAuth_UserId_fkey");
            });
        }
    }
}
