using hackweek_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace hackweek_backend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<CriterionModel> Criteria { get; set; }
        public DbSet<GroupModel> Groups { get; set; }
        public DbSet<GroupRatingModel> GroupRatings { get; set; }
        public DbSet<PropositionCriterionModel> PropositionsCriteria { get; set; }
        public DbSet<PropositionModel> Propositions { get; set; }
        public DbSet<RatingCriterionModel> RatingCriteria { get; set; }
        public DbSet<RatingModel> Ratings { get; set; }
        public DbSet<UserModel> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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

            modelBuilder.Entity<GroupRatingModel>()
                .HasOne(gr => gr.Group)
                .WithMany(g => g.GroupRatings)
                .HasForeignKey(gr => gr.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GroupRatingModel>()
                .HasOne(gr => gr.PropositionCriterion)
                .WithMany()
                .HasForeignKey(gr => gr.PropositionCriterionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PropositionCriterionModel>()
                .HasOne(pc => pc.Proposition)
                .WithMany(p => p.PropositionCriteria)
                .HasForeignKey(pc => pc.PropositionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PropositionCriterionModel>()
                .HasOne(pc => pc.Criterion)
                .WithMany()
                .HasForeignKey(pc => pc.CriterionId)
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
                .HasIndex(u => u.Username)
                .IsUnique();
        }
    }
}

