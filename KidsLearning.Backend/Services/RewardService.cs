using KidsLearning.Backend.Data;
using KidsLearning.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace KidsLearning.Backend.Services;

public class RewardService
{
    private readonly AppDbContext _context;

    public RewardService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> RewardChildForCompletedTask(int childId)
    {
        var child = await _context.Children
            .Include(c => c.Badges)
            .FirstOrDefaultAsync(c => c.Id == childId);

        if (child == null) return false;

        child.StarCount += 1;

        var today = DateTime.UtcNow.Date;
        if (child.LastLearningDay?.Date != today)
        {
            if (child.LastLearningDay?.Date == today.AddDays(-1))
                child.ConsecutiveLearningDays++;
            else
                child.ConsecutiveLearningDays = 1;

            child.LastLearningDay = today;
            child.DailyLearningMinutes = 0;
        }

        child.DailyLearningMinutes += 10;

        if (child.ConsecutiveLearningDays % 5 == 0 && child.ConsecutiveLearningDays > 0)
        {
            var newBadge = new Badge
            {
                Name = "5-Tage-Lernserie Abzeichen",
                Description = "Du hast 5 Tage in Folge gelernt! Fantastisch!",
                IconUrl = "assets/images/badge.png",
                ChildId = child.Id
            };
            child.Badges.Add(newBadge);
        }

        await _context.SaveChangesAsync();
        return true;
    }
}