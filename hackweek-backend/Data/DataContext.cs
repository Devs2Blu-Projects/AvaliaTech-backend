﻿using hackweek_backend.Models;

namespace hackweek_backend.Data
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration _config;
        public DataContext(DbContextOptions<DataContext> options, IConfiguration config) : base(options)
        {
            _config = config;
        }

        public DbSet<CriterionModel> Criteria { get; set; }
        public DbSet<EventModel> Events { get; set; }
        public DbSet<GlobalModel> Global { get; set; }
        public DbSet<GroupModel> Groups { get; set; }
        public DbSet<GroupRatingModel> GroupRatings { get; set; }
        public DbSet<PropositionModel> Propositions { get; set; }
        public DbSet<RatingCriterionModel> RatingCriteria { get; set; }
        public DbSet<RatingModel> Ratings { get; set; }
        public DbSet<UserModel> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GlobalModel>()
                .HasOne(gl => gl.CurrentEvent)
                .WithOne()
                .HasForeignKey<GlobalModel>(gl => gl.CurrentEventId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GroupModel>()
                .HasOne(g => g.User)
                .WithOne()
                .HasForeignKey<GroupModel>(g => g.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GroupModel>()
                .HasOne(g => g.Proposition)
                .WithMany()
                .HasForeignKey(g => g.PropositionId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<GroupModel>()
                .HasOne(g => g.Event)
                .WithMany()
                .HasForeignKey(g => g.EventId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<GroupRatingModel>()
                .HasOne(gr => gr.Group)
                .WithMany(g => g.GroupRatings)
                .HasForeignKey(gr => gr.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GroupRatingModel>()
                .HasOne(gr => gr.Criterion)
                .WithMany()
                .HasForeignKey(gr => gr.CriterionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RatingCriterionModel>()
                .HasOne(rc => rc.Rating)
                .WithMany(r => r.RatingCriteria)
                .HasForeignKey(rc => rc.RatingId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RatingCriterionModel>()
                .HasOne(rc => rc.Criterion)
                .WithMany()
                .HasForeignKey(rc => rc.CriterionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RatingModel>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RatingModel>()
                .HasOne(r => r.Group)
                .WithMany()
                .HasForeignKey(r => r.GroupId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserModel>()
                .HasOne(u => u.Event)
                .WithMany()
                .HasForeignKey(u => u.EventId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserModel>()
                .HasIndex(u => new { u.EventId, u.Username })
                .IsUnique();

            modelBuilder.Entity<GroupModel>()
                .HasIndex(g => new { g.EventId, g.ProjectName })
                .IsUnique();

            modelBuilder.Entity<CriterionModel>()
                 .HasOne(c => c.Event)
                 .WithMany()
                 .HasForeignKey(c => c.EventId)
                 .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserModel>().HasData(
                new UserModel
                {
                    Id = 1,
                    Name = "Admin",
                    Username = _config["Seed:Admin:Username"] ?? "admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(_config["Seed:Admin:Password"] ?? "13579"),
                    Role = UserRoles.Admin,
                    EventId = null
                });

            modelBuilder.Entity<EventModel>().HasData(
                new EventModel
                {
                    Id = 1,
                    Name = _config["Seed:Event:Name"] ?? "HackWeek 2023",
                    StartDate = (DateTime.TryParse(_config["Seed:Event:StartDate"], out DateTime eventStart) ? eventStart : DateTime.Parse("2023-10-31")),
                    EndDate = (DateTime.TryParse(_config["Seed:Event:EndDate"], out DateTime eventEnd) ? eventEnd : DateTime.Parse("2023-10-31")),
                });

            modelBuilder.Entity<GlobalModel>().HasData(
                new GlobalModel
                {
                    Id = 1,
                    CurrentEventId = 1
                });
        }
    }
}

