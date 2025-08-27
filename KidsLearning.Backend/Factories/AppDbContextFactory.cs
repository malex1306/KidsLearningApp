using KidsLearning.Backend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        optionsBuilder.UseSqlServer(
            "Server=(localdb)\\mssqllocaldb;Database=KidsLearningDb;Trusted_Connection=True;MultipleActiveResultSets=true");

        return new AppDbContext(optionsBuilder.Options);
    }
}