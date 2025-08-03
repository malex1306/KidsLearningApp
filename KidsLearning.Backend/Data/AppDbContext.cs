using Microsoft.EntityFrameworkCore;
using KidsLearning.Backend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace KidsLearning.Backend.Data;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<Child>()
        .HasOne<IdentityUser>(c => c.Parent)
        .WithMany()
        .HasForeignKey(c => c.ParentId)
        .IsRequired()
        .OnDelete(DeleteBehavior.Cascade);
}

    public DbSet<LearningQuest> LearningQuests { get; set; } = null!;
    public DbSet<Child> Children { get; set; } = null!;
    public DbSet<SubjectProgress> SubjectProgresses { get; set; } = null!;
}
