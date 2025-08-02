using Microsoft.EntityFrameworkCore;
using KidsLearning.Backend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace KidsLearning.Backend.Data;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    
    public DbSet<LearningQuest> LearningQuests { get; set; } = null!;
}
