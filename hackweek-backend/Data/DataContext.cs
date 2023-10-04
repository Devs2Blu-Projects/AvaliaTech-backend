using hackweek_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace hackweek_backend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        DbSet<UserModel> Users { get; set; }
        DbSet<GroupModel> Groups { get; set; }
        DbSet<RatingModel> Ratings { get; set; }
        DbSet<CriterionModel> Criteria { get; set; }
        DbSet<PropositionModel> Propositions { get; set; }
        DbSet<RatingCriterionModel> RatingCriteria { get; set; }
        DbSet<PropositionCriterionModel> PropositionsCriteria { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            base.OnModelCreating (modelBuilder);

            modelBuilder.Entity<GroupModel>()
                .HasOne(g => g.User)
                .WithOne()
                .HasForeignKey<GroupModel>(g => g.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GroupModel>()
                .HasOne(g => g.Proposition)
                .WithOne()
                .HasForeignKey<GroupModel>(g => g.PropositionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RatingModel>()
                .HasOne(r => r.User)
                .WithOne()
                .HasForeignKey<RatingModel>(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RatingModel>()
                .HasOne(r => r.Group)
                .WithOne()
                .HasForeignKey<RatingModel>(r => r.GroupId)
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

            modelBuilder.Entity<PropositionCriterionModel>()
                .HasOne(pc => pc.Proposition)
                .WithMany(p => p.PropositionCriteria)
                .HasForeignKey(pc => pc.PropositionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

