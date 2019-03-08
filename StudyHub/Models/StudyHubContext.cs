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

        public virtual DbSet<AdminRole> AdminRoles { get; set; }
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

            modelBuilder.Entity<AdminRole>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("AdminRole_pkey");

                entity.ToTable("AdminRole");

                entity.HasIndex(e => e.Name)
                    .HasName("AdminRole_Name_key")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

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

                entity.Property(e => e.RoleId).HasDefaultValueSql("2");

                entity.Property(e => e.UserName).HasMaxLength(100);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AdminUsers)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("AdminUser_Role_fkey");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.HasIndex(e => e.Name)
                    .HasName("Course_Name_key")
                    .IsUnique();

                entity.Property(e => e.CourseId).HasDefaultValueSql("nextval('\"Course_Id_seq\"'::regclass)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .ForNpgsqlHasComment("Course Name");

                entity.HasOne(d => d.Publisher)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.PublisherId)
                    .HasConstraintName("Course_PublisherId_fkey");
            });

            modelBuilder.Entity<Enroll>(entity =>
            {
                entity.ToTable("Enroll");

                entity.Property(e => e.EnrollId).HasDefaultValueSql("nextval('\"Enroll_id_seq\"'::regclass)");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Enrolls)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("Enroll_CourseId_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Enrolls)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("Enroll_UserId_fkey");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.Email)
                    .HasName("User_Email_key")
                    .IsUnique();

                entity.HasIndex(e => e.Mobile)
                    .HasName("User_Mobile_key")
                    .IsUnique();

                entity.HasIndex(e => e.NickName)
                    .HasName("User_NickName_key")
                    .IsUnique();

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Mobile).HasMaxLength(30);

                entity.Property(e => e.NickName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.RoleId).HasDefaultValueSql("2");

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
                    .ForNpgsqlHasComment("such as email, mobile, facebook");

                entity.Property(e => e.InSite).ForNpgsqlHasComment("in site or 3rd party");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserLoginAuths)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("UserAuth_UserId_fkey");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("UserRole_pkey");

                entity.ToTable("UserRole");

                entity.HasIndex(e => e.Name)
                    .HasName("UserRole_Role_key")
                    .IsUnique();

                entity.Property(e => e.RoleId).HasDefaultValueSql("nextval('\"UserRole_id_seq\"'::regclass)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.HasSequence<int>("Course_Id_seq");

            modelBuilder.HasSequence<int>("Enroll_id_seq");

            modelBuilder.HasSequence<int>("UserAuth_Id_seq");

            modelBuilder.HasSequence<int>("UserRole_id_seq");
        }
    }
}
