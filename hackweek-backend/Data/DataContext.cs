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

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            base.OnModelCreating (modelBuilder);

            // modelBuilder.Entity<GroupModel>()
            //     .Property(g => g.FinalGrade)
            //     .HasDefaultValueSql("")
            //     .ValueGeneratedOnAddOrUpdate();

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

            // modelBuilder.Entity<GroupRatingModel>()
            //     .Property(gr => gr.Grade)
            //     .HasDefaultValueSql("")
            //     .ValueGeneratedOnAddOrUpdate();

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
                .WithOne()
                .HasForeignKey<RatingModel>(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RatingModel>()
                .HasOne(r => r.Group)
                .WithOne()
                .HasForeignKey<RatingModel>(r => r.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserModel>()
                .HasIndex(u => u.Username)
                .IsUnique();
        }
    }
}

