using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TM.Domain.Entities.CardAssigns;
using TM.Domain.Entities.CardHistories;
using TM.Domain.Entities.CardMovements;
using TM.Domain.Entities.Cards;
using TM.Domain.Entities.CardTags;
using TM.Domain.Entities.Phases;
using TM.Domain.Entities.Priorities;
using TM.Domain.Entities.ProjectMembers;
using TM.Domain.Entities.ProjectPhases;
using TM.Domain.Entities.Projects;
using TM.Domain.Entities.Tags;
using TM.Domain.Entities.ToDos;
using TM.Domain.Entities.Users;

#nullable disable

namespace TM.Infrastructure.Data
{
    public partial class TaskManagerContext : DbContext
    {
        public TaskManagerContext()
        {
        }

        public TaskManagerContext(DbContextOptions<TaskManagerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Card> Cards { get; set; }
        public virtual DbSet<CardAssign> CardAssigns { get; set; }
        public virtual DbSet<CardHistory> CardHistories { get; set; }
        public virtual DbSet<CardMovement> CardMovements { get; set; }
        public virtual DbSet<CardTag> CardTags { get; set; }
        public virtual DbSet<Phase> Phases { get; set; }
        public virtual DbSet<Priority> Priorities { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectMember> ProjectMembers { get; set; }
        public virtual DbSet<ProjectPhase> ProjectPhases { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Todo> Todos { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Card>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Priority)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.PriorityId)
                    .HasConstraintName("fk_Card_Priority");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("fk_Card_Project");
            });

            modelBuilder.Entity<CardAssign>(entity =>
            {
                entity.Property(e => e.IsAssigned).HasDefaultValueSql("('True')");

                entity.HasOne(d => d.Card)
                    .WithMany(p => p.CardAssigns)
                    .HasForeignKey(d => d.CardId)
                    .HasConstraintName("fk_CardAssign");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CardAssigns)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_assign_User");
            });

            modelBuilder.Entity<CardHistory>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Card)
                    .WithMany(p => p.CardHistories)
                    .HasForeignKey(d => d.CardId)
                    .HasConstraintName("fk_CardHistory_Card");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CardHistories)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_CardHistory_User");
            });

            modelBuilder.Entity<CardMovement>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsCurrent).HasDefaultValueSql("('True')");

                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Card)
                    .WithMany(p => p.CardMovements)
                    .HasForeignKey(d => d.CardId)
                    .HasConstraintName("fk_CardMovement_Card");

                entity.HasOne(d => d.Phase)
                    .WithMany(p => p.CardMovements)
                    .HasForeignKey(d => d.PhaseId)
                    .HasConstraintName("fk_CardMovement_Phase");
            });

            modelBuilder.Entity<CardTag>(entity =>
            {
                entity.HasOne(d => d.Card)
                    .WithMany(p => p.CardTags)
                    .HasForeignKey(d => d.CardId)
                    .HasConstraintName("fk_CardTag_Card");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.CardTags)
                    .HasForeignKey(d => d.TagId)
                    .HasConstraintName("fk_CardTag_Tag");
            });

            modelBuilder.Entity<Priority>(entity =>
            {
                entity.Property(e => e.Color).IsUnicode(false);
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<ProjectMember>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("('True')");

                entity.Property(e => e.IsOwner).HasDefaultValueSql("('False')");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectMembers)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("fk_ProjectMember");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ProjectMembers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_member_User");
            });

            modelBuilder.Entity<ProjectPhase>(entity =>
            {
                entity.HasOne(d => d.Phase)
                    .WithMany(p => p.ProjectPhases)
                    .HasForeignKey(d => d.PhaseId)
                    .HasConstraintName("fk_ProjectPhase_Phase");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectPhases)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("fk_ProjectPhase_Project");
            });

            modelBuilder.Entity<Todo>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsCheck).HasDefaultValueSql("('False')");

                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Card)
                    .WithMany(p => p.Todos)
                    .HasForeignKey(d => d.CardId)
                    .HasConstraintName("fk_Todo_Card");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("fk_Todo_Todo");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("('True')");

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.Phone).IsUnicode(false);

                entity.Property(e => e.UserName).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
