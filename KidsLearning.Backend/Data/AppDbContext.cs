using KidsLearning.Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KidsLearning.Backend.Data;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Child> Children { get; set; } = null!;
    public DbSet<SubjectProgress> SubjectProgresses { get; set; } = null!;
    public DbSet<LearningTask> LearningTasks { get; set; } = null!;
    public DbSet<Questions> Questions { get; set; } = null!;
    public DbSet<ChildCompletedTask> ChildCompletedTasks { get; set; } = null!;
    public DbSet<Avatar> Avatars { get; set; } = null!;
    public DbSet<Badge> Badges { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Questions>()
            .Property(q => q.Options)
            .HasConversion(
                v => string.Join(';', v),
                v => v.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList()
            );

        modelBuilder.Entity<LearningTask>()
            .HasMany(lt => lt.Questions)
            .WithOne(q => q.LearningTask)
            .HasForeignKey(q => q.LearningTaskId);

        modelBuilder.Entity<Child>()
            .HasOne<IdentityUser>(c => c.Parent)
            .WithMany()
            .HasForeignKey(c => c.ParentId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}