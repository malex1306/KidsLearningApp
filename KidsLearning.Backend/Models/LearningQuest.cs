using KidsLearning.Backend.Enums;

namespace KidsLearning.Backend.Models;

public class LearningQuest
{
    public int Id { get; set; }
    public string Question { get; set; } = string.Empty;
    public string RightAnswer { get; set; } = string.Empty;
    public string[] WrongAnswers { get; set; } = Array.Empty<string>();
    public Difficulty Difficulty { get; set; }
}