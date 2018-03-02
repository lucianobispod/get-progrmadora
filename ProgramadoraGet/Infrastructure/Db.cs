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

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Enterprise> Enterprises { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }

        public DbSet<LikeTag> LikeTags { get; set; }

        public DbSet<Match> Matchs { get; set; }

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

            m.Entity<Comment>().Property(d => d.CreatedAt).ValueGeneratedOnAdd();
            m.Entity<Comment>().Property(d => d.UpdatedAt).ValueGeneratedOnAddOrUpdate();
            m.Entity<Comment>().HasOne(h => h.Question).WithMany(w => w.Comment).OnDelete(DeleteBehavior.Restrict);
            m.Entity<Comment>().HasOne(h => h.User).WithMany(w => w.Comment);


            m.Entity<Enterprise>().Property(p => p.CreatedAt).ValueGeneratedOnAdd();
            m.Entity<Enterprise>().Property(p => p.UpdatedAt).ValueGeneratedOnUpdate();

            m.Entity<Feedback>().Property(d => d.CreatedAt).ValueGeneratedOnAdd();
            m.Entity<Feedback>().HasOne(h => h.User).WithMany(w => w.Feedback);

            m.Entity<Match>().HasOne(h => h.User).WithMany(w => w.Match);
            m.Entity<Match>().HasOne(h => h.Enterprise).WithMany(w => w.Match);
            m.Entity<Match>().HasKey(s => new { s.UserId, s.EnterpriseId}).ForSqlServerIsClustered(true);

            m.Entity<Skills>().HasKey(s => new { s.UserId, s.TagId }).ForSqlServerIsClustered(true);
            m.Entity<Skills>().HasOne(h => h.User).WithMany(w => w.Skills);
            m.Entity<Skills>().HasOne(h => h.Tag).WithMany(w => w.Skills);

            m.Entity<LikeTag>().HasKey(s => new { s.UserId, s.TagId }).ForSqlServerIsClustered(true);
            m.Entity<LikeTag>().HasOne(h => h.User).WithMany(w => w.LikeTag);
            m.Entity<LikeTag>().HasOne(h => h.Tag).WithMany(w => w.LikeTag);

            m.Entity<QuestionTag>().HasKey(s => new { s.QuestionId, s.TagId }).ForSqlServerIsClustered(true);
            m.Entity<QuestionTag>().HasOne(h => h.Tag).WithMany(w => w.QuestionTag);
            m.Entity<QuestionTag>().HasOne(h => h.Question).WithMany(w => w.QuestionTag);

            m.Entity<Notification>().Property(d => d.CreatedAt).ValueGeneratedOnAdd();
            m.Entity<Notification>().Property(d => d.UpdatedAt).ValueGeneratedOnAddOrUpdate();
            m.Entity<Notification>().HasOne(d => d.User).WithMany(w => w.Notification);

            m.Entity<Question>().Property(d => d.CreatedAt).ValueGeneratedOnAdd();
            m.Entity<Question>().Property(d => d.UpdatedAt).ValueGeneratedOnAddOrUpdate();
            m.Entity<Question>().HasOne(h => h.User).WithMany(w => w.Question);
            m.Entity<Question>().HasMany(h => h.Comment).WithOne(w => w.Question);


            m.Entity<User>().Property(d => d.CreatedAt).ValueGeneratedOnAdd();
            m.Entity<User>().Property(d => d.UpdatedAt).ValueGeneratedOnAddOrUpdate();

            m.Entity<RecoveryPassword>().HasOne(d => d.User).WithMany(w => w.RecoveryPassword);

        }

    }
}
