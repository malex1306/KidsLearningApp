using Microsoft.AspNetCore.Identity;
using KidsLearning.Backend.Models;

namespace KidsLearning.Backend.Data;

public class EnsureDatabase
{
    public static void Seed(AppDbContext context, UserManager<IdentityUser> userManager)
    {
        context.Database.EnsureCreated();

        // --- Parents ---
        if (!userManager.Users.Any())
        {
            var parent = new IdentityUser
            {
                UserName = "parent1",
                Email = "parent1@test.com",
                EmailConfirmed = true
            };
            userManager.CreateAsync(parent, "Passwort123!").GetAwaiter().GetResult();
        }

        // --- Children ---
        if (!context.Children.Any())
        {
            var parent = userManager.Users.First();
            var child = new Child
            {
                Name = "Max",
                ParentId = parent.Id
            };
            context.Children.Add(child);
            context.SaveChanges();
        }

        // --- LearningTasks ---
        if (!context.LearningTasks.Any())
        {
            var tasks = new List<LearningTask>
            {
                new LearningTask { Title = "Zahlen finden", Description = "Finde die fehlenden Zahlen in der Reihe.", Subject = "Mathe-Abenteuer" },
                new LearningTask { Title = "Addition bis 10", Description = "Addiere zwei einstellige Zahlen.", Subject = "Mathe-Abenteuer" },
                new LearningTask { Title = "Formen erkennen", Description = "Erkenne verschiedene geometrische Formen.", Subject = "Mathe-Abenteuer" },
                new LearningTask { Title = "Alphabet lernen", Description = "Nenne alle Buchstaben des Alphabets in der richtigen Reihenfolge.", Subject = "Buchstaben-land" },
                new LearningTask { Title = "Buchstaben verbinden", Description = "Verbinde die Großbuchstaben mit den Kleinbuchstaben.", Subject = "Buchstaben-land" },
                new LearningTask { Title = "Wörter buchstabieren", Description = "Buchstabiere einfache Wörter wie 'Hund' und 'Katze'.", Subject = "Buchstaben-land" },
                new LearningTask { Title = "Englisch", Description = "Test", Subject = "Englisch" }
            };
            context.LearningTasks.AddRange(tasks);
            context.SaveChanges();
        }

        // --- Questions ---
        if (!context.Questions.Any())
        {
            var questions = new List<Questions>
            {
                // Zahlen finden (TaskId = 1)
                new Questions { Text = "Welche Zahl fehlt in der Reihe: 1, 2, ?, 4, 5", CorrectAnswer = "3", Options = new List<string>{"1","2","3","4"}, LearningTaskId = 1 },
                new Questions { Text = "Welche Zahl fehlt in der Reihe: 5, ?, 7, 8, 9", CorrectAnswer = "6", Options = new List<string>{"5","6","7","8"}, LearningTaskId = 1 },

                // Addition bis 10 (TaskId = 2)
                new Questions { Text = "Was ist 3 + 4?", CorrectAnswer = "7", Options = new List<string>{"5","6","7","8"}, LearningTaskId = 2 },
                new Questions { Text = "Was ist 5 + 2?", CorrectAnswer = "7", Options = new List<string>{"6","7","8","9"}, LearningTaskId = 2 },

                // Formen erkennen (TaskId = 3)
                new Questions { Text = "Welche Form hat vier gleich lange Seiten?", CorrectAnswer = "Quadrat", Options = new List<string>{"Dreieck","Kreis","Quadrat","Rechteck"}, LearningTaskId = 3 },
                new Questions { Text = "Welche Form hat drei Ecken?", CorrectAnswer = "Dreieck", Options = new List<string>{"Quadrat","Dreieck","Kreis","Stern"}, LearningTaskId = 3 },

                // Alphabet lernen (TaskId = 4)
                new Questions { Text = "Welcher Buchstabe kommt nach dem B?", CorrectAnswer = "C", Options = new List<string>{"A","B","C","D"}, LearningTaskId = 4 },
                new Questions { Text = "Welcher Buchstabe kommt vor dem G?", CorrectAnswer = "F", Options = new List<string>{"D","E","F","H"}, LearningTaskId = 4 },

                // Buchstaben verbinden (TaskId = 5)
                new Questions { Text = "Welche Kombination ist richtig?", CorrectAnswer = "A-a", Options = new List<string>{"A-b","A-c","A-a","A-d"}, LearningTaskId = 5 },
                new Questions { Text = "Welche Kombination ist richtig?", CorrectAnswer = "E-e", Options = new List<string>{"E-f","E-g","E-h","E-e"}, LearningTaskId = 5 },

                // Wörter buchstabieren (TaskId = 6)
                new Questions { Text = "Buchstabiere das Wort 'Katze'.", CorrectAnswer = "KATZE", ImageUrl = "assets/images/bengal.png", Options = new List<string>{"K","A","T","Z","E","S","N","P"}, LearningTaskId = 6 },
                new Questions { Text = "Buchstabiere das Wort 'Hund'.", CorrectAnswer = "HUND", ImageUrl = "assets/images/golden-retriever.png", Options = new List<string>{"H","U","N","D","T","P","B","L"}, LearningTaskId = 6 }
            };
            context.Questions.AddRange(questions);
            context.SaveChanges();
        }

        // --- Avatare ---
        if (!context.Avatars.Any())
        {
            var avatars = new List<Avatar>
            {
                new Avatar { Name = "Glückliche Ente", ImageUrl = "assets/images/duck.png", Description = "Eine fröhliche Ente, die gerne schwimmt.", UnlockStarRequirement = 2 },
                new Avatar { Name = "Abenteuerlicher Bär", ImageUrl = "assets/images/bear.png", Description = "Ein mutiger Bär, der gerne im Wald spielt.", UnlockStarRequirement = 4 },
                new Avatar { Name = "Clevere Eule", ImageUrl = "assets/images/owl.png", Description = "Eine weise Eule, die alle Antworten kennt.", UnlockStarRequirement = 6 }
            };
            context.Avatars.AddRange(avatars);
            context.SaveChanges();
        }
    }
}
