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

        public virtual DbSet<AdminUser> AdminUsers { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Enroll> Enrolls { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserLoginAuth> UserLoginAuths { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

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

            modelBuilder.Entity<AdminUser>(entity =>
            {
                entity.HasKey(e => e.AdminId)
                    .HasName("AdminUser_pkey");

                entity.ToTable("AdminUser");

                entity.HasIndex(e => e.UserName)
                    .HasName("AdminUser_UserName_key")
                    .IsUnique();

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Role).HasDefaultValueSql("2");

                entity.Property(e => e.UserName).HasMaxLength(100);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.HasIndex(e => e.Name)
                    .HasName("Course_Name_key")
                    .IsUnique();

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

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("User_RoleId_fkey");
            });

            modelBuilder.Entity<UserLoginAuth>(entity =>
            {
                entity.ToTable("UserLoginAuth");

                entity.HasIndex(e => e.Identifier)
                    .HasName("UserAuth_Identifier_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"UserAuth_Id_seq\"'::regclass)");

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
                    .WithMany(p => p.UserLoginAuths)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("UserAuth_UserId_fkey");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRole");

                entity.HasIndex(e => e.Role)
                    .HasName("UserRole_Role_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.HasSequence<int>("UserAuth_Id_seq");
        }
    }
}
