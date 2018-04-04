using Microsoft.EntityFrameworkCore;
using ProgramadoraGet.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramadoraGet.Infrastructure
{
    public class Db : DbContext
    {
        public Db(DbContextOptions<Db> options) : base(options) { }

        #region Tables

        public DbSet<AcademicQualification> AcademicQualifications { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Enterprise> Enterprises { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }

        public DbSet<LikeTag> LikeTags { get; set; }

        public DbSet<Match> Matches { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<QuestionTag> QuestionTags { get; set; }

        public DbSet<RecoveryPassword> RecoveryPasswords { get; set; }

        public DbSet<Skills> Skills { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<User> Users { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder m)
        {
            base.OnModelCreating(m);

            m.Entity<AcademicQualification>().ToTable(nameof(AcademicQualification));
            m.Entity<Comment>().ToTable(nameof(Comment));
            m.Entity<Enterprise>().ToTable(nameof(Enterprise));
            m.Entity<Feedback>().ToTable(nameof(Feedback));
            m.Entity<LikeTag>().ToTable(nameof(LikeTag));
            m.Entity<Match>().ToTable(nameof(Match));
            m.Entity<Notification>().ToTable(nameof(Notification));
            m.Entity<Question>().ToTable(nameof(Question));
            m.Entity<QuestionTag>().ToTable(nameof(QuestionTag));
            m.Entity<RecoveryPassword>().ToTable(nameof(RecoveryPassword));
            m.Entity<Skills>().ToTable(nameof(Skills));
            m.Entity<Tag>().ToTable(nameof(Tag));
            m.Entity<User>().ToTable(nameof(User));

            m.Entity<Comment>().Property(d => d.CommentText).HasMaxLength(200);
            m.Entity<Comment>().Property(d => d.UpdatedAt).ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("getdate()");
            m.Entity<Comment>().Property(d => d.CreatedAt).ValueGeneratedOnAdd().HasDefaultValueSql("getdate()");
            m.Entity<Comment>().Property(d => d.QuestionId).IsRequired();
            m.Entity<Comment>().Property(d => d.CommentText).IsRequired();
            m.Entity<Comment>().HasOne(h => h.Question).WithMany(w => w.Comment).OnDelete(DeleteBehavior.Restrict).HasForeignKey(f => f.QuestionId);
            m.Entity<Comment>().HasOne(h => h.User).WithMany(w => w.Comment).HasForeignKey(f => f.UserId);

            m.Entity<Enterprise>().Property(d => d.Name).HasMaxLength(200);
            m.Entity<Enterprise>().Property(d => d.Email).HasMaxLength(100);
            m.Entity<Enterprise>().Property(d => d.PhoneNumber).HasMaxLength(14);
            m.Entity<Enterprise>().Property(d => d.State).HasMaxLength(2);
            m.Entity<Enterprise>().Property(d => d.Location).HasMaxLength(50);
            m.Entity<Enterprise>().Property(p => p.UpdatedAt).ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("getdate()");
            m.Entity<Enterprise>().Property(p => p.CreatedAt).ValueGeneratedOnAdd().HasDefaultValueSql("getdate()");
            m.Entity<Enterprise>().HasIndex(p => p.Email).IsUnique();
            m.Entity<Enterprise>().HasIndex(p => p.Name).IsUnique();
            m.Entity<Enterprise>().Property(d => d.Email).IsRequired();
            m.Entity<Enterprise>().Property(d => d.Name).IsRequired();
            m.Entity<Enterprise>().HasMany(h => h.Match).WithOne(w => w.Enterprise);

            m.Entity<Feedback>().Property(d => d.Content).IsRequired();
            m.Entity<Feedback>().Property(d => d.Title).IsRequired();
            m.Entity<Feedback>().Property(d => d.CreatedAt).ValueGeneratedOnAdd().HasDefaultValueSql("getdate()");
            m.Entity<Feedback>().HasOne(h => h.User).WithMany(w => w.Feedback);
            m.Entity<Feedback>().Property(d => d.Content).HasMaxLength(200);
            m.Entity<Feedback>().Property(d => d.Title).HasMaxLength(100);

            m.Entity<AcademicQualification>().HasOne(h => h.User).WithMany(w => w.Qualifications).HasForeignKey(f => f.UserId);
            m.Entity<AcademicQualification>().Property(d => d.Institution).HasMaxLength(100).IsRequired();
            m.Entity<AcademicQualification>().Property(d => d.Course).HasMaxLength(100).IsRequired();
            m.Entity<AcademicQualification>().Property(d => d.CreatedAt).ValueGeneratedOnAdd().HasDefaultValueSql("getdate()");
            m.Entity<AcademicQualification>().Property(d => d.Period).HasMaxLength(50).IsRequired();
            m.Entity<AcademicQualification>().Property(d => d.UpdatedAt).ValueGeneratedOnAdd().HasDefaultValueSql("getdate()");
            
            m.Entity<LikeTag>().HasKey(s => new { s.UserId, s.TagId }).ForSqlServerIsClustered(true);
            m.Entity<LikeTag>().HasOne(h => h.User).WithMany(w => w.LikeTag).HasForeignKey(f => f.UserId);
            m.Entity<LikeTag>().HasOne(h => h.Tag).WithMany(w => w.LikeTag).HasForeignKey(f => f.TagId);

            m.Entity<Match>().HasOne(h => h.User).WithMany(w => w.Match).HasForeignKey(f => f.UserId);
            m.Entity<Match>().HasOne(h => h.Enterprise).WithMany(w => w.Match).HasForeignKey(f => f.EnterpriseId);
            m.Entity<Match>().HasKey(s => new { s.UserId, s.EnterpriseId }).ForSqlServerIsClustered(true);

            m.Entity<Notification>().Property(d => d.CreatedAt).ValueGeneratedOnAdd().HasDefaultValueSql("getdate()");
            m.Entity<Notification>().Property(d => d.UpdatedAt).ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("getdate()");
            m.Entity<Notification>().Property(d => d.Title).IsRequired();
            m.Entity<Notification>().Property(d => d.Link).IsRequired();

            m.Entity<Notification>().HasOne(d => d.User).WithMany(w => w.Notification);
            m.Entity<Notification>().Property(d => d.Title).HasMaxLength(150);

            m.Entity<Question>().Property(d => d.UpdatedAt).ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("getdate()");
            m.Entity<Question>().Property(d => d.CreatedAt).ValueGeneratedOnAdd().HasDefaultValueSql("getdate()");
            m.Entity<Question>().HasOne(h => h.User).WithMany(w => w.Question).HasForeignKey(f => f.UserId);
            m.Entity<Question>().HasMany(h => h.Comment).WithOne(w => w.Question);
            m.Entity<Question>().Property(d => d.Content).IsRequired();
            m.Entity<Question>().Property(d => d.Content).HasMaxLength(500);
            m.Entity<Question>().Property(d => d.Title).IsRequired();
            m.Entity<Question>().Property(d => d.Title).HasMaxLength(150);

            m.Entity<QuestionTag>().HasKey(s => new { s.QuestionId, s.TagId }).ForSqlServerIsClustered(true);
            m.Entity<QuestionTag>().HasOne(h => h.Tag).WithMany(w => w.QuestionTag).HasForeignKey(f => f.TagId);
            m.Entity<QuestionTag>().HasOne(h => h.Question).WithMany(w => w.QuestionTag).HasForeignKey(f => f.QuestionId);

            m.Entity<RecoveryPassword>().HasOne(d => d.User).WithMany(w => w.RecoveryPassword);
            m.Entity<RecoveryPassword>().Property(p => p.CreatedAt).ValueGeneratedOnAdd().HasDefaultValueSql("getdate()");

            m.Entity<Skills>().HasKey(s => new { s.UserId, s.TagId }).ForSqlServerIsClustered(true);
            m.Entity<Skills>().HasOne(h => h.User).WithMany(w => w.Skills).HasForeignKey(f => f.UserId);
            m.Entity<Skills>().HasOne(h => h.Tag).WithMany(w => w.Skills).HasForeignKey(f => f.TagId);

            m.Entity<Tag>().Property(p => p.CreatedAt).ValueGeneratedOnAdd().HasDefaultValueSql("getdate()");
            m.Entity<Tag>().Property(p => p.Name).IsRequired();
            m.Entity<Tag>().Property(d => d.Name).HasMaxLength(100);

            m.Entity<User>().Property(d => d.CreatedAt).ValueGeneratedOnAdd().HasDefaultValueSql("getdate()");
            m.Entity<User>().Property(d => d.UpdatedAt).ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("getdate()");
            m.Entity<User>().HasIndex(e => e.Email).IsUnique();
            m.Entity<User>().Property(e => e.Email).IsRequired();
            m.Entity<User>().Property(e => e.Name).IsRequired();
            m.Entity<User>().Property(e => e.LastName).IsRequired();
            m.Entity<User>().Property(e => e.PasswordHash).IsRequired();
            m.Entity<User>().Property(e => e.PasswordSalt).IsRequired();
            m.Entity<User>().Property(e => e.State).HasMaxLength(2);


            m.Entity<User>().HasMany(h => h.LikeTag).WithOne(w => w.User);
            m.Entity<User>().HasMany(h => h.Match).WithOne(w => w.User);
            m.Entity<User>().HasMany(h => h.Feedback).WithOne(w => w.User);
            m.Entity<User>().HasMany(h => h.Comment).WithOne(w => w.User);
            m.Entity<User>().HasMany(h => h.Skills).WithOne(w => w.User);
            m.Entity<User>().HasMany(h => h.RecoveryPassword).WithOne(w => w.User);
            m.Entity<User>().HasMany(h => h.Question).WithOne(w => w.User);
            m.Entity<User>().HasMany(h => h.Notification).WithOne(w => w.User);
            m.Entity<User>().HasMany(h => h.Qualifications).WithOne(w => w.User);
            m.Entity<User>().Property(d => d.Name).HasMaxLength(50);
            m.Entity<User>().Property(d => d.LastName).HasMaxLength(50);
            m.Entity<User>().Property(d => d.Location).HasMaxLength(100);
            m.Entity<User>().Property(d => d.PhoneNumber).HasMaxLength(14);
            m.Entity<User>().Property(d => d.Email).HasMaxLength(70);
            m.Entity<User>().Property(d => d.Description).HasMaxLength(100);
            m.Entity<User>().Property(d => d.RFID).HasMaxLength(50);


        }

    }
}
