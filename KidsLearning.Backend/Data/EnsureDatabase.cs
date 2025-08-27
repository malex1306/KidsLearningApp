using KidsLearning.Backend.Models;
using Microsoft.AspNetCore.Identity;

namespace KidsLearning.Backend.Data;

public class EnsureDatabase
{
    public static void Seed(AppDbContext context, UserManager<IdentityUser> userManager)
    {
        context.Database.EnsureCreated();

        IdentityUser parentUser;
        var existingParent = userManager.FindByEmailAsync("parent1@test.com").GetAwaiter().GetResult();

        if (existingParent == null)
        {
            parentUser = new IdentityUser
            {
                UserName = "parent1",
                Email = "parent1@test.com",
                EmailConfirmed = true
            };
            userManager.CreateAsync(parentUser, "Passwort123!").GetAwaiter().GetResult();
        }
        else
        {
            parentUser = existingParent;
        }

        if (!context.Children.Any())
        {
            var child = new Child
            {
                Name = "Max",
                ParentId = parentUser.Id,
                AvatarUrl = "assets/images/teddy-bear.png",
                DateOfBirth = DateTime.Now.AddYears(-7),
                Difficulty = "Vorschule"
            };
            context.Children.Add(child);
            context.SaveChanges();
        }

        // --- LearningTasks ---
        if (!context.LearningTasks.Any())
        {
            var tasks = new List<LearningTask>
            {
                // Task 1
                new()
                {
                    Title = "Zahlen finden",
                    Description = "Finde die fehlenden Zahlen in der Reihe.",
                    Subject = "Mathe-Abenteuer"
                },
                // Task 2
                new()
                {
                    Title = "Addition bis 10",
                    Description = "Addiere zwei einstellige Zahlen.",
                    Subject = "Mathe-Abenteuer"
                },
                // Task 3
                new()
                {
                    Title = "Formen erkennen",
                    Description = "Erkenne verschiedene geometrische Formen.",
                    Subject = "Mathe-Abenteuer"
                },
                // Task 4
                new()
                {
                    Title = "Alphabet lernen",
                    Description = "Nenne alle Buchstaben des Alphabets in der richtigen Reihenfolge.",
                    Subject = "Buchstaben-land"
                },
                // Task 5
                new()
                {
                    Title = "Buchstaben verbinden",
                    Description = "Verbinde die Großbuchstaben mit den Kleinbuchstaben.",
                    Subject = "Buchstaben-land"
                },
                // Task 6
                new()
                {
                    Title = "Wörter buchstabieren",
                    Description = "Buchstabiere einfache Wörter wie 'Hund' und 'Katze'.",
                    Subject = "Buchstaben-land"
                },
                // Task 7
                new()
                {
                    Title = "Englische Bilder",
                    Description = "Wähle das korrekte Englische Wort zum angezeigten Bild",
                    Subject = "Englisch"
                },
                // Task 8
                new()
                {
                    Title = "Deutsch/Englisch verbinden",
                    Description = "Finde zu jedem deutschen Wort das richtige Englische Wort",
                    Subject = "Englisch"
                },
                // Task 9
                new()
                {
                    Title = "Finde den Imposter",
                    Description = "Wähle das Englische Wort, das nicht zu den anderen passt",
                    Subject = "Englisch"
                },
                // Task 10
                new()
                {
                    Title = "Fülle die Lücken",
                    Description = "Schreibe die richtigen Wörter in die Lücken",
                    Subject = "Buchstaben-land"
                },
                // Task 11
                new()
                {
                    Title = "Zahlenkombinationen",
                    Description = "Bist du bereit dir Zahlenkombinationen zu merken?",
                    Subject = "Logik-Dschungel"
                },
                // Task 12
                new()
                {
                    Title = "Fülle die Form",
                    Description = "Finde die Richtige Form die in das Muster passt!",
                    Subject = "Logik-Dschungel"
                },
                // Task 13
                new()
                {
                    Title = "Quiz",
                    Description = "Teste dein aktuelles Wissen in einem Quiz",
                    Subject = "Quiz-Ozean"
                },
                // Task 14
                new()
                {
                    Title = "Puzzle",
                    Description = "Hier kannst du Puzzeln",
                    Subject = "Spaß-Paradise"
                }
            };
            context.LearningTasks.AddRange(tasks);
            context.SaveChanges();
        }

        if (!context.Questions.Any())
        {
            var questions = new List<Questions>
            {
                // Fragen für "Zahlen finden" (LearningTask 1)
                //Vorschule
                new()
                {
                    Text = "Welche Zahl fehlt: 1, 2, ?, 4", CorrectAnswer = "3",
                    Options = { "2", "3", "4", "5" }, LearningTaskId = 1, Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Welche Zahl fehlt: ?, 2, 3", CorrectAnswer = "1",
                    Options = { "0", "1", "2", "3" }, LearningTaskId = 1, Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Welche Zahl fehlt: 3, ?, 5", CorrectAnswer = "4",
                    Options = { "2", "3", "4", "5" }, LearningTaskId = 1, Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Welche Zahl fehlt: 4, ?, 6", CorrectAnswer = "5",
                    Options = { "3", "4", "5", "7" }, LearningTaskId = 1, Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Welche Zahl fehlt: 6, 7, ?, 9", CorrectAnswer = "8",
                    Options = { "6", "7", "8", "10" }, LearningTaskId = 1, Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Welche Zahl fehlt: ?, 1, 2", CorrectAnswer = "0",
                    Options = { "0", "1", "2", "3" }, LearningTaskId = 1, Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Welche Zahl fehlt: 7, 8, ?, 10", CorrectAnswer = "9",
                    Options = { "7", "8", "9", "11" }, LearningTaskId = 1, Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Welche Zahl fehlt: 2, 3, ?, 5", CorrectAnswer = "4",
                    Options = { "3", "4", "5", "6" }, LearningTaskId = 1, Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Welche Zahl fehlt: 9, ?, 11", CorrectAnswer = "10",
                    Options = { "9", "10", "11", "12" }, LearningTaskId = 1, Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Welche Zahl fehlt: ?, 5, 6", CorrectAnswer = "4",
                    Options = { "3", "4", "5", "6" }, LearningTaskId = 1, Difficulty = "Vorschule"
                },

                //1Klasse
                new()
                {
                    Text = "Welche Zahl fehlt: 2, 4, ?, 8", CorrectAnswer = "6",
                    Options = { "5", "6", "7", "8" }, LearningTaskId = 1, Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Welche Zahl fehlt: 10, ?, 12, 13", CorrectAnswer = "11",
                    Options = { "10", "11", "12", "14" }, LearningTaskId = 1, Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Welche Zahl fehlt: 1, 3, ?, 7", CorrectAnswer = "5",
                    Options = { "4", "5", "6", "7" }, LearningTaskId = 1, Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Welche Zahl fehlt: ?, 15, 16", CorrectAnswer = "14",
                    Options = { "13", "14", "15", "16" }, LearningTaskId = 1, Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Welche Zahl fehlt: 20, 21, ?, 23", CorrectAnswer = "22",
                    Options = { "21", "22", "23", "24" }, LearningTaskId = 1, Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Welche Zahl fehlt: 5, ?, 15, 20", CorrectAnswer = "10",
                    Options = { "8", "9", "10", "12" }, LearningTaskId = 1, Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Welche Zahl fehlt: ?, 30, 40", CorrectAnswer = "20",
                    Options = { "10", "20", "25", "30" }, LearningTaskId = 1, Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Welche Zahl fehlt: 12, 13, ?, 15", CorrectAnswer = "14",
                    Options = { "12", "13", "14", "15" }, LearningTaskId = 1, Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Welche Zahl fehlt: 50, ?, 52", CorrectAnswer = "51",
                    Options = { "50", "51", "52", "53" }, LearningTaskId = 1, Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Welche Zahl fehlt: 9, 12, ?, 18", CorrectAnswer = "15",
                    Options = { "14", "15", "16", "17" }, LearningTaskId = 1, Difficulty = "1 Klasse"
                },

                //2Klasse
                new()
                {
                    Text = "Welche Zahl fehlt: 100, 200, ?, 400", CorrectAnswer = "300",
                    Options = { "100", "200", "300", "400" }, LearningTaskId = 1,
                    Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Welche Zahl fehlt: 2, 4, 6, ?", CorrectAnswer = "8",
                    Options = { "6", "7", "8", "9" }, LearningTaskId = 1, Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Welche Zahl fehlt: 5, 10, ?, 20", CorrectAnswer = "15",
                    Options = { "12", "15", "18", "20" }, LearningTaskId = 1, Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Welche Zahl fehlt: 25, 30, ?, 40", CorrectAnswer = "35",
                    Options = { "32", "33", "34", "35" }, LearningTaskId = 1, Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Welche Zahl fehlt: ?, 70, 80", CorrectAnswer = "60",
                    Options = { "50", "55", "60", "65" }, LearningTaskId = 1, Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Welche Zahl fehlt: 90, ?, 110", CorrectAnswer = "100",
                    Options = { "95", "98", "100", "105" }, LearningTaskId = 1, Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Welche Zahl fehlt: 7, 14, ?, 28", CorrectAnswer = "21",
                    Options = { "20", "21", "22", "23" }, LearningTaskId = 1, Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Welche Zahl fehlt: 11, ?, 33", CorrectAnswer = "22",
                    Options = { "20", "21", "22", "23" }, LearningTaskId = 1, Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Welche Zahl fehlt: 40, 50, ?, 70", CorrectAnswer = "60",
                    Options = { "55", "58", "60", "65" }, LearningTaskId = 1, Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Welche Zahl fehlt: 13, ?, 39", CorrectAnswer = "26",
                    Options = { "24", "25", "26", "27" }, LearningTaskId = 1, Difficulty = "2 Klasse"
                },

                //3Klasse
                new()
                {
                    Text = "Welche Zahl fehlt: 100, 105, ?, 115", CorrectAnswer = "110",
                    Options = { "108", "109", "110", "111" }, LearningTaskId = 1,
                    Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Welche Zahl fehlt: 200, 220, ?, 260", CorrectAnswer = "240",
                    Options = { "230", "235", "240", "245" }, LearningTaskId = 1,
                    Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Welche Zahl fehlt: 300, ?, 500", CorrectAnswer = "400",
                    Options = { "350", "375", "400", "450" }, LearningTaskId = 1,
                    Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Welche Zahl fehlt: 50, ?, 80", CorrectAnswer = "65",
                    Options = { "60", "62", "65", "70" }, LearningTaskId = 1, Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Welche Zahl fehlt: 1000, 900, ?, 700", CorrectAnswer = "800",
                    Options = { "750", "800", "850", "900" }, LearningTaskId = 1,
                    Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Welche Zahl fehlt: ?, 3000, 4000", CorrectAnswer = "2000",
                    Options = { "1000", "2000", "2500", "3000" }, LearningTaskId = 1,
                    Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Welche Zahl fehlt: 15, 30, ?, 60", CorrectAnswer = "45",
                    Options = { "40", "42", "45", "48" }, LearningTaskId = 1, Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Welche Zahl fehlt: 21, ?, 63", CorrectAnswer = "42",
                    Options = { "40", "41", "42", "43" }, LearningTaskId = 1, Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Welche Zahl fehlt: 120, 130, ?, 150", CorrectAnswer = "140",
                    Options = { "135", "138", "140", "142" }, LearningTaskId = 1,
                    Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Welche Zahl fehlt: 500, ?, 700", CorrectAnswer = "600",
                    Options = { "550", "580", "600", "620" }, LearningTaskId = 1,
                    Difficulty = "3 Klasse"
                },

                //4Klasse
                new()
                {
                    Text = "Welche Zahl fehlt: 5, 10, ?, 20, 25", CorrectAnswer = "15",
                    Options = { "12", "13", "14", "15" }, LearningTaskId = 1, Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Welche Zahl fehlt: 100, 200, ?, 400, 500", CorrectAnswer = "300",
                    Options = { "250", "275", "300", "325" }, LearningTaskId = 1,
                    Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Welche Zahl fehlt: 2, 4, 8, ?, 32", CorrectAnswer = "16",
                    Options = { "12", "14", "16", "18" }, LearningTaskId = 1, Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Welche Zahl fehlt: 3, 9, ?, 81", CorrectAnswer = "27",
                    Options = { "18", "21", "27", "36" }, LearningTaskId = 1, Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Welche Zahl fehlt: 1, 4, 9, ?, 25", CorrectAnswer = "16",
                    Options = { "12", "14", "16", "18" }, LearningTaskId = 1, Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Welche Zahl fehlt: ?, 64, 81", CorrectAnswer = "49",
                    Options = { "36", "42", "49", "56" }, LearningTaskId = 1, Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Welche Zahl fehlt: 12, 24, ?, 96", CorrectAnswer = "48",
                    Options = { "36", "42", "48", "54" }, LearningTaskId = 1, Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Welche Zahl fehlt: 144, ?, 576", CorrectAnswer = "288",
                    Options = { "200", "240", "288", "320" }, LearningTaskId = 1,
                    Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Welche Zahl fehlt: 7, 14, ?, 56", CorrectAnswer = "28",
                    Options = { "21", "25", "28", "30" }, LearningTaskId = 1, Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Welche Zahl fehlt: 1000, ?, 4000", CorrectAnswer = "2000",
                    Options = { "1500", "2000", "2500", "3000" }, LearningTaskId = 1,
                    Difficulty = "4 Klasse"
                },

                // Vorschule - Addition bis 10 (LearningTaskId = 2)
                new()
                {
                    Text = "Was ist 1 + 1?", CorrectAnswer = "2", Options = { "1", "2", "3", "4" },
                    LearningTaskId = 2, Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Was ist 2 + 3?", CorrectAnswer = "5", Options = { "4", "5", "6", "7" },
                    LearningTaskId = 2, Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Was ist 0 + 4?", CorrectAnswer = "4", Options = { "3", "4", "5", "6" },
                    LearningTaskId = 2, Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Was ist 5 + 1?", CorrectAnswer = "6", Options = { "5", "6", "7", "8" },
                    LearningTaskId = 2, Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Was ist 3 + 2?", CorrectAnswer = "5", Options = { "4", "5", "6", "7" },
                    LearningTaskId = 2, Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Was ist 4 + 4?", CorrectAnswer = "8", Options = { "7", "8", "9", "10" },
                    LearningTaskId = 2, Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Was ist 6 + 2?", CorrectAnswer = "8", Options = { "7", "8", "9", "10" },
                    LearningTaskId = 2, Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Was ist 7 + 1?", CorrectAnswer = "8", Options = { "6", "7", "8", "9" },
                    LearningTaskId = 2, Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Was ist 8 + 1?", CorrectAnswer = "9", Options = { "7", "8", "9", "10" },
                    LearningTaskId = 2, Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Was ist 9 + 0?", CorrectAnswer = "9", Options = { "8", "9", "10", "11" },
                    LearningTaskId = 2, Difficulty = "Vorschule"
                },

                // 1. Klasse (LearningTaskId = 2)
                new()
                {
                    Text = "Was ist 12 + 3?", CorrectAnswer = "15",
                    Options = { "14", "15", "16", "17" }, LearningTaskId = 2, Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Was ist 8 + 7?", CorrectAnswer = "15",
                    Options = { "13", "14", "15", "16" }, LearningTaskId = 2, Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Was ist 10 - 4?", CorrectAnswer = "6", Options = { "5", "6", "7", "8" },
                    LearningTaskId = 2, Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Was ist 15 - 9?", CorrectAnswer = "6", Options = { "5", "6", "7", "8" },
                    LearningTaskId = 2, Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Was ist 11 + 8?", CorrectAnswer = "19",
                    Options = { "18", "19", "20", "21" }, LearningTaskId = 2, Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Was ist 7 + 6?", CorrectAnswer = "13",
                    Options = { "12", "13", "14", "15" }, LearningTaskId = 2, Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Was ist 20 - 5?", CorrectAnswer = "15",
                    Options = { "14", "15", "16", "17" }, LearningTaskId = 2, Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Was ist 9 + 9?", CorrectAnswer = "18",
                    Options = { "16", "17", "18", "19" }, LearningTaskId = 2, Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Was ist 6 + 12?", CorrectAnswer = "18",
                    Options = { "17", "18", "19", "20" }, LearningTaskId = 2, Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Was ist 14 - 7?", CorrectAnswer = "7", Options = { "6", "7", "8", "9" },
                    LearningTaskId = 2, Difficulty = "1 Klasse"
                },

                // 2. Klasse (LearningTaskId = 2)
                new()
                {
                    Text = "Was ist 45 + 32?", CorrectAnswer = "77",
                    Options = { "76", "77", "78", "79" }, LearningTaskId = 2, Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Was ist 80 - 25?", CorrectAnswer = "55",
                    Options = { "54", "55", "56", "57" }, LearningTaskId = 2, Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Was ist 7 × 3?", CorrectAnswer = "21",
                    Options = { "20", "21", "22", "23" }, LearningTaskId = 2, Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Was ist 6 × 5?", CorrectAnswer = "30",
                    Options = { "28", "29", "30", "31" }, LearningTaskId = 2, Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Was ist 50 + 27?", CorrectAnswer = "77",
                    Options = { "76", "77", "78", "79" }, LearningTaskId = 2, Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Was ist 9 × 4?", CorrectAnswer = "36",
                    Options = { "34", "35", "36", "37" }, LearningTaskId = 2, Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Was ist 100 - 45?", CorrectAnswer = "55",
                    Options = { "54", "55", "56", "57" }, LearningTaskId = 2, Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Was ist 25 + 19?", CorrectAnswer = "44",
                    Options = { "43", "44", "45", "46" }, LearningTaskId = 2, Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Was ist 12 × 2?", CorrectAnswer = "24",
                    Options = { "22", "23", "24", "25" }, LearningTaskId = 2, Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Was ist 40 - 18?", CorrectAnswer = "22",
                    Options = { "21", "22", "23", "24" }, LearningTaskId = 2, Difficulty = "2 Klasse"
                },

                // 3. Klasse (LearningTaskId = 2)
                new()
                {
                    Text = "Was ist 7 × 8?", CorrectAnswer = "56",
                    Options = { "54", "55", "56", "57" }, LearningTaskId = 2, Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Was ist 64 ÷ 8?", CorrectAnswer = "8", Options = { "7", "8", "9", "10" },
                    LearningTaskId = 2, Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Was ist 9 × 9?", CorrectAnswer = "81",
                    Options = { "80", "81", "82", "83" }, LearningTaskId = 2, Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Was ist 72 ÷ 9?", CorrectAnswer = "8", Options = { "7", "8", "9", "10" },
                    LearningTaskId = 2, Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Was ist 6 × 12?", CorrectAnswer = "72",
                    Options = { "70", "71", "72", "73" }, LearningTaskId = 2, Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Was ist 90 ÷ 10?", CorrectAnswer = "9", Options = { "8", "9", "10", "11" },
                    LearningTaskId = 2, Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Was ist 11 × 7?", CorrectAnswer = "77",
                    Options = { "76", "77", "78", "79" }, LearningTaskId = 2, Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Was ist 8 × 8?", CorrectAnswer = "64",
                    Options = { "63", "64", "65", "66" }, LearningTaskId = 2, Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Was ist 100 ÷ 4?", CorrectAnswer = "25",
                    Options = { "24", "25", "26", "27" }, LearningTaskId = 2, Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Was ist 12 × 12?", CorrectAnswer = "144",
                    Options = { "142", "143", "144", "145" }, LearningTaskId = 2,
                    Difficulty = "3 Klasse"
                },

                // 4. Klasse (LearningTaskId = 2)
                new()
                {
                    Text = "Was ist 125 + 378?", CorrectAnswer = "503",
                    Options = { "502", "503", "504", "505" }, LearningTaskId = 2,
                    Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Was ist 800 - 275?", CorrectAnswer = "525",
                    Options = { "524", "525", "526", "527" }, LearningTaskId = 2,
                    Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Was ist 45 × 6?", CorrectAnswer = "270",
                    Options = { "268", "269", "270", "271" }, LearningTaskId = 2,
                    Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Was ist 900 ÷ 30?", CorrectAnswer = "30",
                    Options = { "28", "29", "30", "31" }, LearningTaskId = 2, Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Was ist 123 + 456?", CorrectAnswer = "579",
                    Options = { "578", "579", "580", "581" }, LearningTaskId = 2,
                    Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Was ist 750 - 425?", CorrectAnswer = "325",
                    Options = { "324", "325", "326", "327" }, LearningTaskId = 2,
                    Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Was ist 25 × 12?", CorrectAnswer = "300",
                    Options = { "298", "299", "300", "301" }, LearningTaskId = 2,
                    Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Was ist 640 ÷ 16?", CorrectAnswer = "40",
                    Options = { "39", "40", "41", "42" }, LearningTaskId = 2, Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Was ist 999 - 123?", CorrectAnswer = "876",
                    Options = { "875", "876", "877", "878" }, LearningTaskId = 2,
                    Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Was ist 48 × 9?", CorrectAnswer = "432",
                    Options = { "430", "431", "432", "433" }, LearningTaskId = 2,
                    Difficulty = "4 Klasse"
                },

                // Vorschule - Formen erkennen (LearningTask 3)
                new()
                {
                    Text = "Welche Form ist rund?", CorrectAnswer = "Kreis",
                    Options = { "Quadrat", "Kreis", "Dreieck", "Rechteck" }, LearningTaskId = 3,
                    Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Welche Form hat vier gleich lange Seiten?", CorrectAnswer = "Quadrat",
                    Options = { "Dreieck", "Kreis", "Quadrat", "Rechteck" }, LearningTaskId = 3,
                    Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Welche Form hat drei Ecken?", CorrectAnswer = "Dreieck",
                    Options = { "Quadrat", "Dreieck", "Kreis", "Stern" }, LearningTaskId = 3,
                    Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Welche Form hat keine Ecken?", CorrectAnswer = "Kreis",
                    Options = { "Dreieck", "Kreis", "Quadrat", "Rechteck" }, LearningTaskId = 3,
                    Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Welche Form sieht aus wie ein Ei?", CorrectAnswer = "Oval",
                    Options = { "Kreis", "Quadrat", "Oval", "Dreieck" }, LearningTaskId = 3,
                    Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Welche Form hat fünf Ecken?", CorrectAnswer = "Fünfeck",
                    Options = { "Dreieck", "Fünfeck", "Quadrat", "Sechseck" }, LearningTaskId = 3,
                    Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Welche Form hat die meisten Ecken?", CorrectAnswer = "Stern",
                    Options = { "Dreieck", "Quadrat", "Stern", "Kreis" }, LearningTaskId = 3,
                    Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Welche Form hat zwei lange und zwei kurze Seiten?", CorrectAnswer = "Rechteck",
                    Options = { "Quadrat", "Rechteck", "Kreis", "Oval" }, LearningTaskId = 3,
                    Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Welche Form ist wie ein Herz?", CorrectAnswer = "Herz",
                    Options = { "Kreis", "Herz", "Dreieck", "Rechteck" }, LearningTaskId = 3
                },
                new()
                {
                    Text = "Welche Form sieht man oft am Himmel in der Nacht?", CorrectAnswer = "Stern",
                    Options = { "Dreieck", "Stern", "Kreis", "Quadrat" }, LearningTaskId = 3,
                    Difficulty = "Vorschule"
                },

                // 1. Klasse - Formen erkennen (LearningTask 3)
                new()
                {
                    Text = "Welche Form hat vier rechte Winkel?", CorrectAnswer = "Rechteck",
                    Options = { "Quadrat", "Rechteck", "Dreieck", "Kreis" }, LearningTaskId = 3,
                    Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Welche Form hat sechs Ecken?", CorrectAnswer = "Sechseck",
                    Options = { "Fünfeck", "Sechseck", "Achteck", "Quadrat" }, LearningTaskId = 3,
                    Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Welche Form ist wie ein Stoppschild?", CorrectAnswer = "Achteck",
                    Options = { "Sechseck", "Achteck", "Quadrat", "Dreieck" }, LearningTaskId = 3,
                    Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Welche Form kann rollen?", CorrectAnswer = "Kreis",
                    Options = { "Quadrat", "Rechteck", "Kreis", "Dreieck" }, LearningTaskId = 3,
                    Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Welche Form hat gleich lange Seiten und vier Ecken?", CorrectAnswer = "Quadrat",
                    Options = { "Rechteck", "Kreis", "Quadrat", "Dreieck" }, LearningTaskId = 3,
                    Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Welche Form ist wie ein Dach?", CorrectAnswer = "Dreieck",
                    Options = { "Kreis", "Quadrat", "Dreieck", "Oval" }, LearningTaskId = 3,
                    Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Welche Form ist wie ein Fenster?", CorrectAnswer = "Quadrat",
                    Options = { "Quadrat", "Kreis", "Dreieck", "Herz" }, LearningTaskId = 3,
                    Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Welche Form ist wie ein Ei?", CorrectAnswer = "Oval",
                    Options = { "Oval", "Kreis", "Quadrat", "Rechteck" }, LearningTaskId = 3,
                    Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Welche Form hat acht Ecken?", CorrectAnswer = "Achteck",
                    Options = { "Sechseck", "Achteck", "Dreieck", "Fünfeck" }, LearningTaskId = 3,
                    Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Welche Form hat vier Seiten, aber nicht alle gleich lang?", CorrectAnswer = "Rechteck",
                    Options = { "Quadrat", "Rechteck", "Trapez", "Kreis" }, LearningTaskId = 3,
                    Difficulty = "1 Klasse"
                },

                // 2. Klasse - Formen erkennen (LearningTask 3)
                new()
                {
                    Text = "Welche Form hat sieben Ecken?", CorrectAnswer = "Siebeneck",
                    Options = { "Sechseck", "Siebeneck", "Achteck", "Neuneck" }, LearningTaskId = 3,
                    Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Welche Form sieht wie eine Pizza-Stück aus?", CorrectAnswer = "Kreissektor",
                    Options = { "Dreieck", "Kreissektor", "Rechteck", "Oval" }, LearningTaskId = 3,
                    Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Welche Form ist wie eine Raute?", CorrectAnswer = "Rhombus",
                    Options = { "Rhombus", "Quadrat", "Rechteck", "Kreis" }, LearningTaskId = 3,
                    Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Welche Form hat gleich lange Seiten, aber keine rechten Winkel?", CorrectAnswer = "Raute",
                    Options = { "Raute", "Quadrat", "Rechteck", "Dreieck" }, LearningTaskId = 3,
                    Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Welche Form ist wie eine Scheibe?", CorrectAnswer = "Kreis",
                    Options = { "Kreis", "Quadrat", "Rechteck", "Oval" }, LearningTaskId = 3,
                    Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Welche Form hat zehn Ecken?", CorrectAnswer = "Zehneck",
                    Options = { "Achteck", "Zehneck", "Siebeneck", "Fünfeck" }, LearningTaskId = 3,
                    Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Welche Form ist wie ein Diamant?", CorrectAnswer = "Raute",
                    Options = { "Kreis", "Raute", "Dreieck", "Quadrat" }, LearningTaskId = 3,
                    Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Welche Form hat 12 Ecken?", CorrectAnswer = "Zwölfeck",
                    Options = { "Zehneck", "Elfeck", "Zwölfeck", "Achteck" }, LearningTaskId = 3,
                    Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Welche Form hat eine gekrümmte Linie und zwei Punkte?", CorrectAnswer = "Halbkreis",
                    Options = { "Halbkreis", "Kreis", "Dreieck", "Trapez" }, LearningTaskId = 3,
                    Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Welche Form hat vier ungleiche Seiten?", CorrectAnswer = "Trapez",
                    Options = { "Quadrat", "Rechteck", "Trapez", "Raute" }, LearningTaskId = 3,
                    Difficulty = "2 Klasse"
                },

                // 3. Klasse - Formen erkennen (LearningTask 3)
                new()
                {
                    Text = "Wie viele Ecken hat ein Fünfeck?", CorrectAnswer = "5",
                    Options = { "4", "5", "6", "7" }, LearningTaskId = 3, Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Wie viele Seiten hat ein Sechseck?", CorrectAnswer = "6",
                    Options = { "5", "6", "7", "8" }, LearningTaskId = 3, Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Wie viele Ecken hat ein Achteck?", CorrectAnswer = "8",
                    Options = { "6", "7", "8", "9" }, LearningTaskId = 3, Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Wie viele Diagonalen hat ein Quadrat?", CorrectAnswer = "2",
                    Options = { "1", "2", "3", "4" }, LearningTaskId = 3, Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Wie nennt man ein Vieleck mit 10 Seiten?", CorrectAnswer = "Zehneck",
                    Options = { "Fünfeck", "Sechseck", "Zehneck", "Achteck" }, LearningTaskId = 3,
                    Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Welche Form hat keine Ecken?", CorrectAnswer = "Kreis",
                    Options = { "Dreieck", "Quadrat", "Kreis", "Fünfeck" }, LearningTaskId = 3
                },
                new()
                {
                    Text = "Welche Form hat 6 gleich lange Seiten?", CorrectAnswer = "Sechseck",
                    Options = { "Fünfeck", "Sechseck", "Achteck", "Zehneck" }, LearningTaskId = 3,
                    Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Welche Form hat 12 Ecken?", CorrectAnswer = "Zwölfeck",
                    Options = { "Achteck", "Zehneck", "Zwölfeck", "Neuneck" }, LearningTaskId = 3
                },
                new()
                {
                    Text = "Wie nennt man ein Viereck mit zwei parallelen Seiten?", CorrectAnswer = "Trapez",
                    Options = { "Quadrat", "Trapez", "Rechteck", "Parallelogramm" },
                    LearningTaskId = 3, Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Wie nennt man ein Viereck mit zwei Paaren paralleler Seiten?",
                    CorrectAnswer = "Parallelogramm",
                    Options = { "Trapez", "Rhombus", "Quadrat", "Parallelogramm" }, LearningTaskId = 3,
                    Difficulty = "3 Klasse"
                },

                // 4. Klasse - Formen erkennen (LearningTask 3)
                new()
                {
                    Text = "Wie viele Flächen hat ein Würfel?", CorrectAnswer = "6",
                    Options = { "4", "6", "8", "12" }, LearningTaskId = 3, Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Wie viele Kanten hat ein Würfel?", CorrectAnswer = "12",
                    Options = { "6", "8", "10", "12" }, LearningTaskId = 3, Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Wie viele Ecken hat ein Würfel?", CorrectAnswer = "8",
                    Options = { "6", "8", "10", "12" }, LearningTaskId = 3, Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Wie viele Flächen hat eine Pyramide mit quadratischer Grundfläche?", CorrectAnswer = "5",
                    Options = { "4", "5", "6", "8" }, LearningTaskId = 3, Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Wie viele Flächen hat ein Quader?", CorrectAnswer = "6",
                    Options = { "4", "6", "8", "12" }, LearningTaskId = 3, Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Wie viele Kanten hat ein Quader?", CorrectAnswer = "12",
                    Options = { "8", "10", "12", "14" }, LearningTaskId = 3, Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Wie viele Flächen hat ein Zylinder?", CorrectAnswer = "3",
                    Options = { "2", "3", "4", "5" }, LearningTaskId = 3, Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Wie viele Flächen hat ein Kegel?", CorrectAnswer = "2",
                    Options = { "1", "2", "3", "4" }, LearningTaskId = 3, Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Wie viele Flächen hat eine Kugel?", CorrectAnswer = "1",
                    Options = { "0", "1", "2", "3" }, LearningTaskId = 3, Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Wie viele Flächen hat ein Prisma mit einer sechseckigen Grundfläche?", CorrectAnswer = "8",
                    Options = { "6", "7", "8", "9" }, LearningTaskId = 3, Difficulty = "4 Klasse"
                },

                // Vorschule – Alphabet lernen (LearningTask4)
                new()
                {
                    Text = "Welcher Buchstabe kommt nach A?", CorrectAnswer = "B",
                    Options = { "C", "B", "D", "Z" }, LearningTaskId = 4, Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Welcher Buchstabe kommt nach B?", CorrectAnswer = "C",
                    Options = { "A", "C", "D", "E" }, LearningTaskId = 4, Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Welcher Buchstabe kommt nach C?", CorrectAnswer = "D",
                    Options = { "B", "E", "D", "A" }, LearningTaskId = 4, Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Welcher Buchstabe kommt vor D?", CorrectAnswer = "C",
                    Options = { "B", "E", "C", "F" }, LearningTaskId = 4, Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Welcher Buchstabe ist der erste im Alphabet?", CorrectAnswer = "A",
                    Options = { "Z", "B", "C", "A" }, LearningTaskId = 4, Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Welcher Buchstabe ist der letzte im Alphabet?", CorrectAnswer = "Z",
                    Options = { "X", "Y", "Z", "A" }, LearningTaskId = 4, Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Welcher Buchstabe kommt vor B?", CorrectAnswer = "A",
                    Options = { "C", "Z", "D", "A" }, LearningTaskId = 4, Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Welcher Buchstabe kommt nach E?", CorrectAnswer = "F",
                    Options = { "D", "F", "G", "H" }, LearningTaskId = 4, Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Welcher Buchstabe kommt nach F?", CorrectAnswer = "G",
                    Options = { "E", "H", "G", "I" }, LearningTaskId = 4, Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Welcher Buchstabe kommt vor C?", CorrectAnswer = "B",
                    Options = { "D", "B", "E", "A" }, LearningTaskId = 4, Difficulty = "Vorschule"
                },

                // 1. Klasse – Alphabet lernen (LearningTask4)
                new()
                {
                    Text = "Welcher Buchstabe kommt nach H?", CorrectAnswer = "I",
                    Options = { "G", "I", "J", "K" }, LearningTaskId = 4, Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Welcher Buchstabe kommt vor J?", CorrectAnswer = "I",
                    Options = { "K", "H", "I", "L" }, LearningTaskId = 4, Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Welcher Buchstabe steht zwischen K und M?", CorrectAnswer = "L",
                    Options = { "K", "L", "N", "O" }, LearningTaskId = 4, Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Welcher Buchstabe kommt nach L?", CorrectAnswer = "M",
                    Options = { "N", "M", "K", "O" }, LearningTaskId = 4, Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Welcher Buchstabe kommt nach O?", CorrectAnswer = "P",
                    Options = { "Q", "P", "R", "N" }, LearningTaskId = 4, Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Welcher Buchstabe kommt vor P?", CorrectAnswer = "O",
                    Options = { "N", "O", "M", "Q" }, LearningTaskId = 4, Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Welcher Buchstabe steht zwischen Q und S?", CorrectAnswer = "R",
                    Options = { "P", "R", "T", "S" }, LearningTaskId = 4, Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Welcher Buchstabe kommt nach S?", CorrectAnswer = "T",
                    Options = { "R", "T", "U", "V" }, LearningTaskId = 4, Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Welcher Buchstabe kommt vor T?", CorrectAnswer = "S",
                    Options = { "Q", "S", "R", "U" }, LearningTaskId = 4, Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Welcher Buchstabe kommt nach U?", CorrectAnswer = "V",
                    Options = { "W", "V", "X", "T" }, LearningTaskId = 4, Difficulty = "1 Klasse"
                },

                // 2. Klasse – Alphabet lernen (LearningTask4)
                new()
                {
                    Text = "Welcher Buchstabe kommt nach V?", CorrectAnswer = "W",
                    Options = { "U", "W", "X", "Y" }, LearningTaskId = 4, Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Welcher Buchstabe kommt vor W?", CorrectAnswer = "V",
                    Options = { "U", "T", "V", "X" }, LearningTaskId = 4, Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Welcher Buchstabe steht zwischen X und Z?", CorrectAnswer = "Y",
                    Options = { "Z", "X", "Y", "W" }, LearningTaskId = 4, Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Welcher Buchstabe kommt nach M?", CorrectAnswer = "N",
                    Options = { "L", "O", "N", "P" }, LearningTaskId = 4, Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Welcher Buchstabe kommt nach Q?", CorrectAnswer = "R",
                    Options = { "P", "R", "S", "T" }, LearningTaskId = 4, Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Welcher Buchstabe kommt vor L?", CorrectAnswer = "K",
                    Options = { "J", "M", "K", "N" }, LearningTaskId = 4, Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Welcher Buchstabe kommt vor F?", CorrectAnswer = "E",
                    Options = { "G", "D", "E", "H" }, LearningTaskId = 4, Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Welcher Buchstabe kommt nach Z?", CorrectAnswer = "Keiner",
                    Options = { "A", "B", "Keiner", "Y" }, LearningTaskId = 4, Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Welcher Buchstabe kommt vor A?", CorrectAnswer = "Keiner",
                    Options = { "Z", "Keiner", "B", "Y" }, LearningTaskId = 4, Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Welcher Buchstabe kommt zwischen D und F?", CorrectAnswer = "E",
                    Options = { "G", "F", "E", "H" }, LearningTaskId = 4, Difficulty = "2 Klasse"
                },

                // 3. Klasse – Alphabet lernen (LearningTask4)
                new()
                {
                    Text = "Welcher Buchstabe kommt an 5. Stelle im Alphabet?", CorrectAnswer = "E",
                    Options = { "C", "D", "E", "F" }, LearningTaskId = 4, Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Welcher Buchstabe steht an 10. Stelle?", CorrectAnswer = "J",
                    Options = { "I", "J", "K", "L" }, LearningTaskId = 4, Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Welcher Buchstabe steht an 15. Stelle?", CorrectAnswer = "O",
                    Options = { "N", "O", "P", "Q" }, LearningTaskId = 4, Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Welcher Buchstabe steht an 20. Stelle?", CorrectAnswer = "T",
                    Options = { "S", "T", "U", "V" }, LearningTaskId = 4, Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Welcher Buchstabe steht an 26. Stelle?", CorrectAnswer = "Z",
                    Options = { "X", "Y", "Z", "A" }, LearningTaskId = 4, Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Welcher Buchstabe steht an 3. Stelle?", CorrectAnswer = "C",
                    Options = { "B", "C", "D", "E" }, LearningTaskId = 4, Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Welcher Buchstabe steht an 12. Stelle?", CorrectAnswer = "L",
                    Options = { "K", "L", "M", "N" }, LearningTaskId = 4, Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Welcher Buchstabe steht an 8. Stelle?", CorrectAnswer = "H",
                    Options = { "G", "H", "I", "J" }, LearningTaskId = 4, Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Welcher Buchstabe steht an 18. Stelle?", CorrectAnswer = "R",
                    Options = { "Q", "R", "S", "T" }, LearningTaskId = 4, Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Welcher Buchstabe steht an 22. Stelle?", CorrectAnswer = "V",
                    Options = { "U", "V", "W", "X" }, LearningTaskId = 4, Difficulty = "3 Klasse"
                },

                // 4. Klasse – Alphabet lernen
                new()
                {
                    Text = "Welcher Buchstabe steht 2 Stellen nach M?", CorrectAnswer = "O",
                    Options = { "N", "O", "P", "Q" }, LearningTaskId = 4, Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Welcher Buchstabe steht 3 Stellen vor P?", CorrectAnswer = "M",
                    Options = { "L", "M", "N", "O" }, LearningTaskId = 4, Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Welcher Buchstabe steht 4 Stellen nach J?", CorrectAnswer = "N",
                    Options = { "L", "M", "N", "O" }, LearningTaskId = 4, Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Welcher Buchstabe steht 5 Stellen vor Z?", CorrectAnswer = "U",
                    Options = { "V", "U", "T", "W" }, LearningTaskId = 4, Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Welcher Buchstabe steht 2 Stellen nach F?", CorrectAnswer = "H",
                    Options = { "G", "H", "I", "J" }, LearningTaskId = 4, Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Welcher Buchstabe steht 7 Stellen nach A?", CorrectAnswer = "H",
                    Options = { "F", "G", "H", "I" }, LearningTaskId = 4, Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Welcher Buchstabe steht 6 Stellen nach Q?", CorrectAnswer = "W",
                    Options = { "T", "U", "V", "W" }, LearningTaskId = 4, Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Welcher Buchstabe steht 4 Stellen vor K?", CorrectAnswer = "G",
                    Options = { "H", "F", "G", "E" }, LearningTaskId = 4, Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Welcher Buchstabe steht 5 Stellen nach L?", CorrectAnswer = "Q",
                    Options = { "N", "O", "P", "Q" }, LearningTaskId = 4, Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Welcher Buchstabe steht 8 Stellen nach D?", CorrectAnswer = "L",
                    Options = { "J", "K", "L", "M" }, LearningTaskId = 4, Difficulty = "4 Klasse"
                },


                // Vorschule – Buchstaben verbinden (LearningTask 5)
                new()
                {
                    Text = "Welche Kombination ist richtig?", CorrectAnswer = "A-a",
                    Options = { "A-b", "A-c", "A-a", "A-d" }, LearningTaskId = 5,
                    Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Welche Kombination ist richtig?", CorrectAnswer = "B-b",
                    Options = { "B-c", "B-a", "B-b", "B-d" }, LearningTaskId = 5,
                    Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Welche Kombination ist richtig?", CorrectAnswer = "C-c",
                    Options = { "C-d", "C-b", "C-c", "C-a" }, LearningTaskId = 5,
                    Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Welche Kombination ist richtig?", CorrectAnswer = "D-d",
                    Options = { "D-d", "D-c", "D-b", "D-a" }, LearningTaskId = 5,
                    Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Welche Kombination ist richtig?", CorrectAnswer = "E-e",
                    Options = { "E-f", "E-g", "E-h", "E-e" }, LearningTaskId = 5,
                    Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Welche Kombination ist richtig?", CorrectAnswer = "F-f",
                    Options = { "F-f", "F-g", "F-e", "F-h" }, LearningTaskId = 5,
                    Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Welche Kombination ist richtig?", CorrectAnswer = "G-g",
                    Options = { "G-f", "G-h", "G-g", "G-i" }, LearningTaskId = 5,
                    Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Welche Kombination ist richtig?", CorrectAnswer = "H-h",
                    Options = { "H-h", "H-i", "H-g", "H-j" }, LearningTaskId = 5,
                    Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Welche Kombination ist richtig?", CorrectAnswer = "I-i",
                    Options = { "I-j", "I-i", "I-h", "I-k" }, LearningTaskId = 5,
                    Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Welche Kombination ist richtig?", CorrectAnswer = "J-j",
                    Options = { "J-i", "J-k", "J-j", "J-l" }, LearningTaskId = 5,
                    Difficulty = "Vorschule"
                },

                // 1. Klasse – Buchstaben verbinden (LearningTask 5)
                new()
                {
                    Text = "Welche Kombination passt zu 'Katze'?", CorrectAnswer = "K-k",
                    Options = { "K-c", "K-k", "K-t", "K-a" }, LearningTaskId = 5,
                    Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Welche Kombination passt zu 'Hund'?", CorrectAnswer = "H-h",
                    Options = { "H-u", "H-n", "H-d", "H-h" }, LearningTaskId = 5,
                    Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Welche Kombination passt zu 'Maus'?", CorrectAnswer = "M-m",
                    Options = { "M-a", "M-m", "M-u", "M-s" }, LearningTaskId = 5,
                    Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Welche Kombination passt zu 'Buch'?", CorrectAnswer = "B-b",
                    Options = { "B-b", "B-u", "B-c", "B-h" }, LearningTaskId = 5,
                    Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Welche Kombination passt zu 'Fisch'?", CorrectAnswer = "F-f",
                    Options = { "F-i", "F-s", "F-f", "F-c" }, LearningTaskId = 5,
                    Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Welche Kombination passt zu 'Apfel'?", CorrectAnswer = "A-a",
                    Options = { "A-p", "A-a", "A-f", "A-l" }, LearningTaskId = 5,
                    Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Welche Kombination passt zu 'Vogel'?", CorrectAnswer = "V-v",
                    Options = { "V-o", "V-v", "V-g", "V-l" }, LearningTaskId = 5,
                    Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Welche Kombination passt zu 'Sonne'?", CorrectAnswer = "S-s",
                    Options = { "S-o", "S-s", "S-n", "S-e" }, LearningTaskId = 5,
                    Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Welche Kombination passt zu 'Haus'?", CorrectAnswer = "H-h",
                    Options = { "H-h", "H-a", "H-u", "H-s" }, LearningTaskId = 5,
                    Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Welche Kombination passt zu 'Blume'?", CorrectAnswer = "B-b",
                    Options = { "B-l", "B-b", "B-u", "B-e" }, LearningTaskId = 5,
                    Difficulty = "1 Klasse"
                },

                // 2. Klasse – Buchstaben verbinden (LearningTask 5)
                new()
                {
                    Text = "Welche Kombination passt zu 'Tiger'?", CorrectAnswer = "T-t",
                    Options = { "T-t", "T-i", "T-g", "T-e" }, LearningTaskId = 5,
                    Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Welche Kombination passt zu 'Elefant'?", CorrectAnswer = "E-e",
                    Options = { "E-l", "E-e", "E-f", "E-a" }, LearningTaskId = 5,
                    Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Welche Kombination passt zu 'Papagei'?", CorrectAnswer = "P-p",
                    Options = { "P-p", "P-a", "P-g", "P-e" }, LearningTaskId = 5,
                    Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Welche Kombination passt zu 'Schule'?", CorrectAnswer = "S-s",
                    Options = { "S-s", "S-c", "S-h", "S-u" }, LearningTaskId = 5,
                    Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Welche Kombination passt zu 'Zebra'?", CorrectAnswer = "Z-z",
                    Options = { "Z-e", "Z-b", "Z-z", "Z-r" }, LearningTaskId = 5,
                    Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Welche Kombination passt zu 'König'?", CorrectAnswer = "K-k",
                    Options = { "K-k", "K-ö", "K-n", "K-i" }, LearningTaskId = 5,
                    Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Welche Kombination passt zu 'Fuchs'?", CorrectAnswer = "F-f",
                    Options = { "F-u", "F-f", "F-c", "F-h" }, LearningTaskId = 5,
                    Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Welche Kombination passt zu 'Biene'?", CorrectAnswer = "B-b",
                    Options = { "B-i", "B-b", "B-e", "B-n" }, LearningTaskId = 5,
                    Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Welche Kombination passt zu 'Hase'?", CorrectAnswer = "H-h",
                    Options = { "H-a", "H-h", "H-s", "H-e" }, LearningTaskId = 5,
                    Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Welche Kombination passt zu 'Apfelbaum'?", CorrectAnswer = "A-a",
                    Options = { "A-p", "A-a", "A-f", "A-b" }, LearningTaskId = 5,
                    Difficulty = "2 Klasse"
                },

                // 3. Klasse – Buchstaben verbinden (LearningTask 5)
                new()
                {
                    Text = "Welche Kombination passt zu 'Drachen'?", CorrectAnswer = "D-d",
                    Options = { "D-r", "D-d", "D-a", "D-c" }, LearningTaskId = 5,
                    Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Welche Kombination passt zu 'Schmetterling'?", CorrectAnswer = "S-s",
                    Options = { "S-m", "S-s", "S-h", "S-e" }, LearningTaskId = 5,
                    Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Welche Kombination passt zu 'Känguru'?", CorrectAnswer = "K-k",
                    Options = { "K-k", "K-ä", "K-n", "K-u" }, LearningTaskId = 5,
                    Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Welche Kombination passt zu 'Burg'?", CorrectAnswer = "B-b",
                    Options = { "B-u", "B-b", "B-r", "B-g" }, LearningTaskId = 5,
                    Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Welche Kombination passt zu 'Fahrrad'?", CorrectAnswer = "F-f",
                    Options = { "F-a", "F-f", "F-r", "F-h" }, LearningTaskId = 5,
                    Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Welche Kombination passt zu 'Zoo'?", CorrectAnswer = "Z-z",
                    Options = { "Z-o", "Z-z", "Z-a", "Z-u" }, LearningTaskId = 5,
                    Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Welche Kombination passt zu 'Apfelsaft'?", CorrectAnswer = "A-a",
                    Options = { "A-p", "A-a", "A-f", "A-s" }, LearningTaskId = 5,
                    Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Welche Kombination passt zu 'Vogelhaus'?", CorrectAnswer = "V-v",
                    Options = { "V-o", "V-v", "V-h", "V-u" }, LearningTaskId = 5,
                    Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Welche Kombination passt zu 'Tisch'?", CorrectAnswer = "T-t",
                    Options = { "T-i", "T-s", "T-t", "T-c" }, LearningTaskId = 5,
                    Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Welche Kombination passt zu 'Lampe'?", CorrectAnswer = "L-l",
                    Options = { "L-a", "L-l", "L-m", "L-p" }, LearningTaskId = 5,
                    Difficulty = "3 Klasse"
                },

                // 4. Klasse – Buchstaben verbinden (LearningTask 5)
                new()
                {
                    Text = "Welche Kombination passt zu 'Computer'?", CorrectAnswer = "C-c",
                    Options = { "C-o", "C-c", "C-m", "C-p" }, LearningTaskId = 5,
                    Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Welche Kombination passt zu 'Telefon'?", CorrectAnswer = "T-t",
                    Options = { "T-t", "T-e", "T-l", "T-o" }, LearningTaskId = 5,
                    Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Welche Kombination passt zu 'Bibliothek'?", CorrectAnswer = "B-b",
                    Options = { "B-b", "B-i", "B-l", "B-t" }, LearningTaskId = 5,
                    Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Welche Kombination passt zu 'Universität'?", CorrectAnswer = "U-u",
                    Options = { "U-u", "U-n", "U-i", "U-v" }, LearningTaskId = 5,
                    Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Welche Kombination passt zu 'Schokolade'?", CorrectAnswer = "S-s",
                    Options = { "S-s", "S-c", "S-h", "S-o" }, LearningTaskId = 5,
                    Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Welche Kombination passt zu 'Mathematik'?", CorrectAnswer = "M-m",
                    Options = { "M-m", "M-a", "M-t", "M-h" }, LearningTaskId = 5,
                    Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Welche Kombination passt zu 'Physik'?", CorrectAnswer = "P-p",
                    Options = { "P-p", "P-h", "P-y", "P-s" }, LearningTaskId = 5,
                    Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Welche Kombination passt zu 'Geographie'?", CorrectAnswer = "G-g",
                    Options = { "G-g", "G-e", "G-o", "G-r" }, LearningTaskId = 5,
                    Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Welche Kombination passt zu 'Chemie'?", CorrectAnswer = "C-c",
                    Options = { "C-h", "C-e", "C-m", "C-c" }, LearningTaskId = 5,
                    Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Welche Kombination passt zu 'Biologie'?", CorrectAnswer = "B-b",
                    Options = { "B-i", "B-o", "B-l", "B-b" }, LearningTaskId = 5,
                    Difficulty = "4 Klasse"
                },

                // Neue Fragen für "Wörter buchstabieren" (LearningTask 6)
                //Vorschule
                new()
                {
                    Text = "Buchstabiere das Wort 'Ball'.", CorrectAnswer = "BALL",
                    ImageUrl = "assets/questImg/soccer.png",
                    Options = { "B", "O", "M", "L", "M", "K", "L", "A" }, LearningTaskId = 6,
                    Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Buchstabiere das Wort 'Auto'.", CorrectAnswer = "AUTO",
                    ImageUrl = "assets/questImg/toy-car.png",
                    Options = { "A", "S", "T", "O", "E", "M", "U", "R" }, LearningTaskId = 6,
                    Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Buchstabiere das Wort 'Haus'.", CorrectAnswer = "HAUS",
                    ImageUrl = "assets/questImg/house.png",
                    Options = { "K", "R", "U", "S", "E", "A", "M", "H" }, LearningTaskId = 6,
                    Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Buchstabiere das Wort 'Oma'.", CorrectAnswer = "OMA",
                    ImageUrl = "assets/questImg/old-woman.png",
                    Options = { "O", "E", "A", "E", "S", "M", "K", "L" }, LearningTaskId = 6,
                    Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Buchstabiere das Wort 'Bär'.", CorrectAnswer = "BÄR",
                    ImageUrl = "assets/questImg/bears.png",
                    Options = { "B", "N", "L", "A", "R", "E", "T", "Ä" }, LearningTaskId = 6,
                    Difficulty = "Vorschule"
                },

                // Neue Fragen für "Wörter buchstabieren" (LearningTask 6)
                //1Klasse
                new()
                {
                    Text = "Buchstabiere das Wort 'Katze'.", CorrectAnswer = "KATZE",
                    ImageUrl = "assets/questImg/exotic-shorthair.png",
                    Options = { "P", "A", "E", "Z", "T", "S", "N", "K" }, LearningTaskId = 6,
                    Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Buchstabiere das Wort 'Hund'.", CorrectAnswer = "HUND",
                    ImageUrl = "assets/questImg/dog.png",
                    Options = { "H", "B", "N", "L", "T", "P", "U", "D" }, LearningTaskId = 6,
                    Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Buchstabiere das Wort 'Apfel'.", CorrectAnswer = "APFEL",
                    ImageUrl = "assets/questImg/apple.png",
                    Options = { "P", "A", "F", "O", "L", "S", "E", "M" }, LearningTaskId = 6,
                    Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Buchstabiere das Wort 'Sonne'.", CorrectAnswer = "SONNE",
                    ImageUrl = "assets/questImg/sun.png",
                    Options = { "N", "O", "S", "N", "U", "E", "R", "L" }, LearningTaskId = 6,
                    Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Buchstabiere das Wort 'Fisch'.", CorrectAnswer = "FISCH",
                    ImageUrl = "assets/questImg/clown-fish.png",
                    Options = { "S", "I", "F", "C", "H", "E", "T", "A" }, LearningTaskId = 6,
                    Difficulty = "1 Klasse"
                },


                // Neue Fragen für "Wörter buchstabieren" (LearningTask 6)
                //2Klasse
                new()
                {
                    Text = "Buchstabiere das Wort 'Blume'.", CorrectAnswer = "BLUME",
                    ImageUrl = "assets/questImg/flower.png",
                    Options = { "S", "N", "U", "M", "E", "A", "B", "L" }, LearningTaskId = 6,
                    Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Buchstabiere das Wort 'Tiger'.", CorrectAnswer = "TIGER",
                    ImageUrl = "assets/questImg/tiger.png",
                    Options = { "T", "P", "A", "E", "R", "G", "L", "I" }, LearningTaskId = 6,
                    Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Buchstabiere das Wort 'Brot'.", CorrectAnswer = "BROT",
                    ImageUrl = "assets/questImg/baguette.png",
                    Options = { "N", "L", "O", "T", "A", "S", "B", "R" }, LearningTaskId = 6,
                    Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Buchstabiere das Wort 'Pferd'.", CorrectAnswer = "PFERD",
                    ImageUrl = "assets/questImg/horse.png",
                    Options = { "F", "P", "E", "N", "D", "T", "S", "R" }, LearningTaskId = 6,
                    Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Buchstabiere das Wort 'Banane'.", CorrectAnswer = "BANANE",
                    ImageUrl = "assets/questImg/banana.png",
                    Options = { "N", "A", "B", "A", "N", "E", "S", "T" }, LearningTaskId = 6,
                    Difficulty = "2 Klasse"
                },


                // Neue Fragen für "Wörter buchstabieren" (LearningTask 6)
                //3Klasse
                new()
                {
                    Text = "Buchstabiere das Wort 'Schule'.", CorrectAnswer = "SCHULE",
                    ImageUrl = "assets/questImg/elementary-school.png",
                    Options = { "M", "L", "H", "U", "C", "E", "R", "S" }, LearningTaskId = 6,
                    Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Buchstabiere das Wort 'Wasser'.", CorrectAnswer = "WASSER",
                    ImageUrl = "assets/questImg/drainage.png",
                    Options = { "S", "A", "S", "W", "E", "T", "R", "N" }, LearningTaskId = 6,
                    Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Buchstabiere das Wort 'Zebra'.", CorrectAnswer = "ZEBRA",
                    ImageUrl = "assets/questImg/zebra.png",
                    Options = { "Z", "L", "O", "R", "A", "B", "N", "E" }, LearningTaskId = 6,
                    Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Buchstabiere das Wort 'Tisch'.", CorrectAnswer = "TISCH",
                    ImageUrl = "assets/questImg/dining-table.png",
                    Options = { "I", "T", "H", "C", "S", "R", "E", "N" }, LearningTaskId = 6,
                    Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Buchstabiere das Wort 'Wolke'.", CorrectAnswer = "WOLKE",
                    ImageUrl = "assets/questImg/cloud.png",
                    Options = { "T", "O", "T", "K", "E", "S", "W", "L" }, LearningTaskId = 6,
                    Difficulty = "3 Klasse"
                },

                // Neue Fragen für "Wörter buchstabieren" (LearningTask 6)
                //4Klasse
                new()
                {
                    Text = "Buchstabiere das Wort 'Planet'.", CorrectAnswer = "PLANET",
                    ImageUrl = "assets/questImg/saturn.png",
                    Options = { "T", "R", "A", "N", "E", "P", "S", "L" }, LearningTaskId = 6,
                    Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Buchstabiere das Wort 'Computer'.", CorrectAnswer = "COMPUTER",
                    ImageUrl = "assets/questImg/computers.png",
                    Options = { "T", "O", "M", "U", "R", "C", "E", "P" }, LearningTaskId = 6,
                    Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Buchstabiere das Wort 'Brücke'.", CorrectAnswer = "BRÜCKE",
                    ImageUrl = "assets/questImg/bridge.png",
                    Options = { "B", "Ü", "R", "K", "C", "E", "S", "T" }, LearningTaskId = 6,
                    Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Buchstabiere das Wort 'Freund'.", CorrectAnswer = "FREUND",
                    ImageUrl = "assets/questImg/friends.png",
                    Options = { "A", "S", "E", "U", "N", "D", "F", "R" }, LearningTaskId = 6,
                    Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Buchstabiere das Wort 'Wald'.", CorrectAnswer = "WALD",
                    ImageUrl = "assets/questImg/forest.png",
                    Options = { "O", "T", "L", "D", "S", "A", "R", "W" }, LearningTaskId = 6,
                    Difficulty = "4 Klasse"
                },

                // Englisch Bilder (Task 7)
                //Vorschule
                new()
                {
                    Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "Ball",
                    ImageUrl = "assets/questImg/soccer.png", Options = { "Ball", "Car", "Cat", "Sun" },
                    LearningTaskId = 7, Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "Apple",
                    ImageUrl = "assets/questImg/apple.png",
                    Options = { "Apple", "Banana", "Dog", "Hat" }, LearningTaskId = 7,
                    Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "Car",
                    ImageUrl = "assets/questImg/toy-car.png",
                    Options = { "Car", "Bus", "Plane", "Tree" }, LearningTaskId = 7,
                    Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "Dog",
                    ImageUrl = "assets/questImg/dog.png", Options = { "Dog", "Cat", "Bird", "Fish" },
                    LearningTaskId = 7, Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "Sun",
                    ImageUrl = "assets/questImg/sun.png", Options = { "Sun", "Moon", "Star", "Cloud" },
                    LearningTaskId = 7, Difficulty = "Vorschule"
                },

                //1Klasse
                new()
                {
                    Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "Cat",
                    ImageUrl = "assets/questImg/exotic-shorthair.png",
                    Options = { "Dog", "Cat", "Fox", "Pig" }, LearningTaskId = 7,
                    Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "Fox",
                    ImageUrl = "assets/questImg/fox.png",
                    Options = { "Fox", "Penguin", "Horse", "Cow" }, LearningTaskId = 7,
                    Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "Bird",
                    ImageUrl = "assets/questImg/bird.png", Options = { "Bird", "Dog", "Cat", "Fish" },
                    LearningTaskId = 7, Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "Milk",
                    ImageUrl = "assets/questImg/milk.png",
                    Options = { "Milk", "Juice", "Water", "Tea" }, LearningTaskId = 7,
                    Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "Tree",
                    ImageUrl = "assets/questImg/tree.png",
                    Options = { "Tree", "Flower", "Grass", "Rock" }, LearningTaskId = 7,
                    Difficulty = "1 Klasse"
                },


                //2Klasse
                new()
                {
                    Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "Horse",
                    ImageUrl = "assets/questImg/horse.png", Options = { "Horse", "Dog", "Cat", "Cow" },
                    LearningTaskId = 7, Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "Fish",
                    ImageUrl = "assets/questImg/fish.png", Options = { "Fish", "Bird", "Dog", "Fox" },
                    LearningTaskId = 7, Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "Banana",
                    ImageUrl = "assets/questImg/banana.png",
                    Options = { "Banana", "Apple", "Grapes", "Orange" }, LearningTaskId = 7,
                    Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "Duck",
                    ImageUrl = "assets/questImg/ducks.png",
                    Options = { "Duck", "Chicken", "Goose", "Turkey" }, LearningTaskId = 7,
                    Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "House",
                    ImageUrl = "assets/questImg/house.png",
                    Options = { "House", "Car", "Tree", "Boat" }, LearningTaskId = 7,
                    Difficulty = "2 Klasse"
                },

                //3Klasse
                new()
                {
                    Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "Penguin",
                    ImageUrl = "assets/questImg/penguin.png",
                    Options = { "Penguin", "Fox", "Dog", "Cat" }, LearningTaskId = 7,
                    Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "Dolphin",
                    ImageUrl = "assets/questImg/dolphin.png",
                    Options = { "Dolphin", "Shark", "Whale", "Fish" }, LearningTaskId = 7,
                    Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "Carrot",
                    ImageUrl = "assets/questImg/carrot.png",
                    Options = { "Carrot", "Potato", "Apple", "Tomato" }, LearningTaskId = 7,
                    Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "Lion",
                    ImageUrl = "assets/questImg/lion.png",
                    Options = { "Lion", "Tiger", "Bear", "Wolf" }, LearningTaskId = 7,
                    Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "Plane",
                    ImageUrl = "assets/questImg/airplane.png",
                    Options = { "Plane", "Car", "Train", "Boat" }, LearningTaskId = 7,
                    Difficulty = "3 Klasse"
                },

                //4Klasse
                new()
                {
                    Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "Elephant",
                    ImageUrl = "assets/questImg/elephant.png",
                    Options = { "Elephant", "Lion", "Tiger", "Bear" }, LearningTaskId = 7,
                    Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "Giraffe",
                    ImageUrl = "assets/questImg/giraffe.png",
                    Options = { "Giraffe", "Zebra", "Horse", "Cow" }, LearningTaskId = 7,
                    Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "Shark",
                    ImageUrl = "assets/questImg/shark.png",
                    Options = { "Shark", "Dolphin", "Whale", "Fish" }, LearningTaskId = 7,
                    Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "Tiger",
                    ImageUrl = "assets/questImg/tiger.png",
                    Options = { "Tiger", "Lion", "Leopard", "Cat" }, LearningTaskId = 7,
                    Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "Frog",
                    ImageUrl = "assets/questImg/frog.png",
                    Options = { "Frog", "Toad", "Lizard", "Snake" }, LearningTaskId = 7,
                    Difficulty = "4 Klasse"
                },


                new()
                {
                    Text = "Wähle das korrekte Englische Wort",
                    CorrectAnswer = "Fox",
                    ImageUrl = "assets/images/fox.png",
                    Options = { "Fox", "Dolphin", "Penguin", "Lizard" },
                    LearningTaskId = 7
                },

                // Vorschule – Englisch Vokabeln (LearningTask 8)

                // Category: Family
                new()
                {
                    Text = "Onkel", CorrectAnswer = "uncle", Options = { "uncle" }, LearningTaskId = 8,
                    Category = "Family", Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Vater", CorrectAnswer = "father", Options = { "father" },
                    LearningTaskId = 8, Category = "Family", Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Mutter", CorrectAnswer = "mother", Options = { "mother" },
                    LearningTaskId = 8, Category = "Family", Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Bruder", CorrectAnswer = "brother", Options = { "brother" },
                    LearningTaskId = 8, Category = "Family", Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Schwester", CorrectAnswer = "sister", Options = { "sister" },
                    LearningTaskId = 8, Category = "Family", Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Tante", CorrectAnswer = "aunt", Options = { "aunt" }, LearningTaskId = 8,
                    Category = "Family", Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Großvater", CorrectAnswer = "grandpa", Options = { "grandpa" },
                    LearningTaskId = 8, Category = "Family", Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Großmutter", CorrectAnswer = "grandma", Options = { "grandma" },
                    LearningTaskId = 8, Category = "Family", Difficulty = "Vorschule"
                },

                // Category: Animals
                new()
                {
                    Text = "Hund", CorrectAnswer = "dog", Options = { "dog" }, LearningTaskId = 8,
                    Category = "Animal", Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Katze", CorrectAnswer = "cat", Options = { "cat" }, LearningTaskId = 8,
                    Category = "Animal", Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Hase", CorrectAnswer = "rabbit", Options = { "rabbit" },
                    LearningTaskId = 8, Category = "Animal", Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Maus", CorrectAnswer = "mouse", Options = { "mouse" }, LearningTaskId = 8,
                    Category = "Animal", Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Vogel", CorrectAnswer = "bird", Options = { "bird" }, LearningTaskId = 8,
                    Category = "Animal", Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Fisch", CorrectAnswer = "fish", Options = { "fish" }, LearningTaskId = 8,
                    Category = "Animal", Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Schwein", CorrectAnswer = "pig", Options = { "pig" }, LearningTaskId = 8,
                    Category = "Animal", Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Pferd", CorrectAnswer = "horse", Options = { "horse" }, LearningTaskId = 8,
                    Category = "Animal", Difficulty = "Vorschule"
                },

                // Category: Other
                new()
                {
                    Text = "Haus", CorrectAnswer = "house", Options = { "house" }, LearningTaskId = 8,
                    Category = "Other", Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Baum", CorrectAnswer = "tree", Options = { "tree" }, LearningTaskId = 8,
                    Category = "Other", Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Auto", CorrectAnswer = "car", Options = { "car" }, LearningTaskId = 8,
                    Category = "Other", Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Schiff", CorrectAnswer = "ship", Options = { "ship" }, LearningTaskId = 8,
                    Category = "Other", Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Blume", CorrectAnswer = "flower", Options = { "flower" },
                    LearningTaskId = 8, Category = "Other", Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Tisch", CorrectAnswer = "table", Options = { "table" }, LearningTaskId = 8,
                    Category = "Other", Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Stuhl", CorrectAnswer = "chair", Options = { "chair" }, LearningTaskId = 8,
                    Category = "Other", Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Tür", CorrectAnswer = "door", Options = { "door" }, LearningTaskId = 8,
                    Category = "Other", Difficulty = "Vorschule"
                },

                // Category: Cloth
                new()
                {
                    Text = "Hut", CorrectAnswer = "hat", Options = { "hat" }, LearningTaskId = 8,
                    Category = "Cloth", Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Schuh", CorrectAnswer = "shoe", Options = { "shoe" }, LearningTaskId = 8,
                    Category = "Cloth", Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Jacke", CorrectAnswer = "jacket", Options = { "jacket" },
                    LearningTaskId = 8, Category = "Cloth", Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Handschuhe", CorrectAnswer = "gloves", Options = { "gloves" },
                    LearningTaskId = 8, Category = "Cloth", Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Stiefel", CorrectAnswer = "boot", Options = { "boot" }, LearningTaskId = 8,
                    Category = "Cloth", Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Brille", CorrectAnswer = "glasses", Options = { "glasses" },
                    LearningTaskId = 8, Category = "Cloth", Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Hose", CorrectAnswer = "pants", Options = { "pants" }, LearningTaskId = 8,
                    Category = "Cloth", Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Pullover", CorrectAnswer = "sweater", Options = { "sweater" },
                    LearningTaskId = 8, Category = "Cloth", Difficulty = "Vorschule"
                },

                // 1Klasse – Englisch Vokabeln (LearningTask 8)
                // Category: Family
                new()
                {
                    Text = "Cousin", CorrectAnswer = "cousin", Options = { "cousin" },
                    LearningTaskId = 8, Category = "Family", Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Großmutter", CorrectAnswer = "grandma", Options = { "grandma" },
                    LearningTaskId = 8, Category = "Family", Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Großvater", CorrectAnswer = "grandpa", Options = { "grandpa" },
                    LearningTaskId = 8, Category = "Family", Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Vater", CorrectAnswer = "father", Options = { "father" },
                    LearningTaskId = 8, Category = "Family", Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Mutter", CorrectAnswer = "mother", Options = { "mother" },
                    LearningTaskId = 8, Category = "Family", Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Onkel", CorrectAnswer = "uncle", Options = { "uncle" }, LearningTaskId = 8,
                    Category = "Family", Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Tante", CorrectAnswer = "aunt", Options = { "aunt" }, LearningTaskId = 8,
                    Category = "Family", Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Bruder", CorrectAnswer = "brother", Options = { "brother" },
                    LearningTaskId = 8, Category = "Family", Difficulty = "1 Klasse"
                },

                // Category: Animals
                new()
                {
                    Text = "Hund", CorrectAnswer = "dog", Options = { "dog" }, LearningTaskId = 8,
                    Category = "Animal", Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Katze", CorrectAnswer = "cat", Options = { "cat" }, LearningTaskId = 8,
                    Category = "Animal", Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Vogel", CorrectAnswer = "bird", Options = { "bird" }, LearningTaskId = 8,
                    Category = "Animal", Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Fisch", CorrectAnswer = "fish", Options = { "fish" }, LearningTaskId = 8,
                    Category = "Animal", Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Pferd", CorrectAnswer = "horse", Options = { "horse" }, LearningTaskId = 8,
                    Category = "Animal", Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Schwein", CorrectAnswer = "pig", Options = { "pig" }, LearningTaskId = 8,
                    Category = "Animal", Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Huhn", CorrectAnswer = "chicken", Options = { "chicken" },
                    LearningTaskId = 8, Category = "Animal", Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Schaf", CorrectAnswer = "sheep", Options = { "sheep" }, LearningTaskId = 8,
                    Category = "Animal", Difficulty = "1 Klasse"
                },

                // Category: Other
                new()
                {
                    Text = "Haus", CorrectAnswer = "house", Options = { "house" }, LearningTaskId = 8,
                    Category = "Other", Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Baum", CorrectAnswer = "tree", Options = { "tree" }, LearningTaskId = 8,
                    Category = "Other", Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Auto", CorrectAnswer = "car", Options = { "car" }, LearningTaskId = 8,
                    Category = "Other", Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Blume", CorrectAnswer = "flower", Options = { "flower" },
                    LearningTaskId = 8, Category = "Other", Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Tisch", CorrectAnswer = "table", Options = { "table" }, LearningTaskId = 8,
                    Category = "Other", Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Stuhl", CorrectAnswer = "chair", Options = { "chair" }, LearningTaskId = 8,
                    Category = "Other", Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Tür", CorrectAnswer = "door", Options = { "door" }, LearningTaskId = 8,
                    Category = "Other", Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Fenster", CorrectAnswer = "window", Options = { "window" },
                    LearningTaskId = 8, Category = "Other", Difficulty = "1 Klasse"
                },

                // Category: Cloth
                new()
                {
                    Text = "Hut", CorrectAnswer = "hat", Options = { "hat" }, LearningTaskId = 8,
                    Category = "Cloth", Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Schuh", CorrectAnswer = "shoe", Options = { "shoe" }, LearningTaskId = 8,
                    Category = "Cloth", Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Jacke", CorrectAnswer = "jacket", Options = { "jacket" },
                    LearningTaskId = 8, Category = "Cloth", Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Handschuhe", CorrectAnswer = "gloves", Options = { "gloves" },
                    LearningTaskId = 8, Category = "Cloth", Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Stiefel", CorrectAnswer = "boot", Options = { "boot" }, LearningTaskId = 8,
                    Category = "Cloth", Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Brille", CorrectAnswer = "glasses", Options = { "glasses" },
                    LearningTaskId = 8, Category = "Cloth", Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Hose", CorrectAnswer = "pants", Options = { "pants" }, LearningTaskId = 8,
                    Category = "Cloth", Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Pullover", CorrectAnswer = "sweater", Options = { "sweater" },
                    LearningTaskId = 8, Category = "Cloth", Difficulty = "1 Klasse"
                },

                // 2Klasse – Englisch Vokabeln (LearningTask 8)
                // Category: Family
                new()
                {
                    Text = "Onkel", CorrectAnswer = "uncle", Options = { "uncle" }, LearningTaskId = 8,
                    Category = "Family", Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Tante", CorrectAnswer = "aunt", Options = { "aunt" }, LearningTaskId = 8,
                    Category = "Family", Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Bruder", CorrectAnswer = "brother", Options = { "brother" },
                    LearningTaskId = 8, Category = "Family", Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Schwester", CorrectAnswer = "sister", Options = { "sister" },
                    LearningTaskId = 8, Category = "Family", Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Vater", CorrectAnswer = "father", Options = { "father" },
                    LearningTaskId = 8, Category = "Family", Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Mutter", CorrectAnswer = "mother", Options = { "mother" },
                    LearningTaskId = 8, Category = "Family", Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Großvater", CorrectAnswer = "grandpa", Options = { "grandpa" },
                    LearningTaskId = 8, Category = "Family", Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Großmutter", CorrectAnswer = "grandma", Options = { "grandma" },
                    LearningTaskId = 8, Category = "Family", Difficulty = "2 Klasse"
                },

                // Category: Animals
                new()
                {
                    Text = "Hund", CorrectAnswer = "dog", Options = { "dog" }, LearningTaskId = 8,
                    Category = "Animal", Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Katze", CorrectAnswer = "cat", Options = { "cat" }, LearningTaskId = 8,
                    Category = "Animal", Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Hase", CorrectAnswer = "rabbit", Options = { "rabbit" },
                    LearningTaskId = 8, Category = "Animal", Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Maus", CorrectAnswer = "mouse", Options = { "mouse" }, LearningTaskId = 8,
                    Category = "Animal", Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Vogel", CorrectAnswer = "bird", Options = { "bird" }, LearningTaskId = 8,
                    Category = "Animal", Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Fuchs", CorrectAnswer = "fox", Options = { "fox" }, LearningTaskId = 8,
                    Category = "Animal", Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Delfin", CorrectAnswer = "dolphin", Options = { "dolphin" },
                    LearningTaskId = 8, Category = "Animal", Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Pinguin", CorrectAnswer = "penguin", Options = { "penguin" },
                    LearningTaskId = 8, Category = "Animal", Difficulty = "2 Klasse"
                },

                // Category: Other
                new()
                {
                    Text = "Haus", CorrectAnswer = "house", Options = { "house" }, LearningTaskId = 8,
                    Category = "Other", Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Baum", CorrectAnswer = "tree", Options = { "tree" }, LearningTaskId = 8,
                    Category = "Other", Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Auto", CorrectAnswer = "car", Options = { "car" }, LearningTaskId = 8,
                    Category = "Other", Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Tisch", CorrectAnswer = "table", Options = { "table" }, LearningTaskId = 8,
                    Category = "Other", Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Stuhl", CorrectAnswer = "chair", Options = { "chair" }, LearningTaskId = 8,
                    Category = "Other", Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Rakete", CorrectAnswer = "rocket", Options = { "rocket" },
                    LearningTaskId = 8, Category = "Other", Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Vulkan", CorrectAnswer = "volcano", Options = { "volcano" },
                    LearningTaskId = 8, Category = "Other", Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Blume", CorrectAnswer = "flower", Options = { "flower" },
                    LearningTaskId = 8, Category = "Other", Difficulty = "2 Klasse"
                },

                // Category: Cloth
                new()
                {
                    Text = "Hut", CorrectAnswer = "hat", Options = { "hat" }, LearningTaskId = 8,
                    Category = "Cloth", Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Schuh", CorrectAnswer = "shoe", Options = { "shoe" }, LearningTaskId = 8,
                    Category = "Cloth", Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Jacke", CorrectAnswer = "jacket", Options = { "jacket" },
                    LearningTaskId = 8, Category = "Cloth", Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Handschuhe", CorrectAnswer = "gloves", Options = { "gloves" },
                    LearningTaskId = 8, Category = "Cloth", Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Stiefel", CorrectAnswer = "boot", Options = { "boot" }, LearningTaskId = 8,
                    Category = "Cloth", Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Brille", CorrectAnswer = "glasses", Options = { "glasses" },
                    LearningTaskId = 8, Category = "Cloth", Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Pullover", CorrectAnswer = "sweater", Options = { "sweater" },
                    LearningTaskId = 8, Category = "Cloth", Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Hose", CorrectAnswer = "pants", Options = { "pants" }, LearningTaskId = 8,
                    Category = "Cloth", Difficulty = "2 Klasse"
                },

                // 3Klasse – Englisch Vokabeln (LearningTask 8)
                // Category: Family
                new()
                {
                    Text = "Cousin", CorrectAnswer = "cousin", Options = { "cousin" },
                    LearningTaskId = 8, Category = "Family", Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Schwiegermutter", CorrectAnswer = "mother-in-law",
                    Options = { "mother-in-law" }, LearningTaskId = 8, Category = "Family",
                    Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Schwiegervater", CorrectAnswer = "father-in-law",
                    Options = { "father-in-law" }, LearningTaskId = 8, Category = "Family",
                    Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Neffe", CorrectAnswer = "nephew", Options = { "nephew" },
                    LearningTaskId = 8, Category = "Family", Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Nichte", CorrectAnswer = "niece", Options = { "niece" },
                    LearningTaskId = 8, Category = "Family", Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Geschwister", CorrectAnswer = "siblings", Options = { "siblings" },
                    LearningTaskId = 8, Category = "Family", Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Ehepartner", CorrectAnswer = "spouse", Options = { "spouse" },
                    LearningTaskId = 8, Category = "Family", Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Elternteil", CorrectAnswer = "parent", Options = { "parent" },
                    LearningTaskId = 8, Category = "Family", Difficulty = "3 Klasse"
                },

                // Category: Animals
                new()
                {
                    Text = "Fisch", CorrectAnswer = "fish", Options = { "fish" }, LearningTaskId = 8,
                    Category = "Animal", Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Känguru", CorrectAnswer = "kangaroo", Options = { "kangaroo" },
                    LearningTaskId = 8, Category = "Animal", Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Elefant", CorrectAnswer = "elephant", Options = { "elephant" },
                    LearningTaskId = 8, Category = "Animal", Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Pferd", CorrectAnswer = "horse", Options = { "horse" }, LearningTaskId = 8,
                    Category = "Animal", Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Frosch", CorrectAnswer = "frog", Options = { "frog" }, LearningTaskId = 8,
                    Category = "Animal", Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Schaf", CorrectAnswer = "sheep", Options = { "sheep" }, LearningTaskId = 8,
                    Category = "Animal", Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Ziege", CorrectAnswer = "goat", Options = { "goat" }, LearningTaskId = 8,
                    Category = "Animal", Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Schwein", CorrectAnswer = "pig", Options = { "pig" }, LearningTaskId = 8,
                    Category = "Animal", Difficulty = "3 Klasse"
                },

                // Category: Other
                new()
                {
                    Text = "Brücke", CorrectAnswer = "bridge", Options = { "bridge" },
                    LearningTaskId = 8, Category = "Other", Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Straße", CorrectAnswer = "street", Options = { "street" },
                    LearningTaskId = 8, Category = "Other", Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Berg", CorrectAnswer = "mountain", Options = { "mountain" },
                    LearningTaskId = 8, Category = "Other", Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Fluss", CorrectAnswer = "river", Options = { "river" }, LearningTaskId = 8,
                    Category = "Other", Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "See", CorrectAnswer = "lake", Options = { "lake" }, LearningTaskId = 8,
                    Category = "Other", Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Insel", CorrectAnswer = "island", Options = { "island" },
                    LearningTaskId = 8, Category = "Other", Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Burg", CorrectAnswer = "castle", Options = { "castle" },
                    LearningTaskId = 8, Category = "Other", Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Leuchtturm", CorrectAnswer = "lighthouse", Options = { "lighthouse" },
                    LearningTaskId = 8, Category = "Other", Difficulty = "3 Klasse"
                },

                // Category: Cloth
                new()
                {
                    Text = "Schal", CorrectAnswer = "scarf", Options = { "scarf" }, LearningTaskId = 8,
                    Category = "Cloth", Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Kleid", CorrectAnswer = "dress", Options = { "dress" }, LearningTaskId = 8,
                    Category = "Cloth", Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Hemd", CorrectAnswer = "shirt", Options = { "shirt" }, LearningTaskId = 8,
                    Category = "Cloth", Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Socken", CorrectAnswer = "socks", Options = { "socks" },
                    LearningTaskId = 8, Category = "Cloth", Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Schuhe", CorrectAnswer = "shoes", Options = { "shoes" },
                    LearningTaskId = 8, Category = "Cloth", Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Mütze", CorrectAnswer = "cap", Options = { "cap" }, LearningTaskId = 8,
                    Category = "Cloth", Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Hose", CorrectAnswer = "pants", Options = { "pants" }, LearningTaskId = 8,
                    Category = "Cloth", Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Jacke", CorrectAnswer = "jacket", Options = { "jacket" },
                    LearningTaskId = 8, Category = "Cloth", Difficulty = "3 Klasse"
                },

                // 4Klasse – Englisch Vokabeln (LearningTask 8)
                // Category: Family
                new()
                {
                    Text = "Schwiegersohn", CorrectAnswer = "son-in-law", Options = { "son-in-law" },
                    LearningTaskId = 8, Category = "Family", Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Schwiegertochter", CorrectAnswer = "daughter-in-law",
                    Options = { "daughter-in-law" }, LearningTaskId = 8, Category = "Family",
                    Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Großcousin", CorrectAnswer = "first cousin once removed",
                    Options = { "first cousin once removed" }, LearningTaskId = 8, Category = "Family",
                    Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Großnichte", CorrectAnswer = "grandniece", Options = { "grandniece" },
                    LearningTaskId = 8, Category = "Family", Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Großneffe", CorrectAnswer = "grandnephew", Options = { "grandnephew" },
                    LearningTaskId = 8, Category = "Family", Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Schwager", CorrectAnswer = "brother-in-law",
                    Options = { "brother-in-law" }, LearningTaskId = 8, Category = "Family",
                    Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Schwägerin", CorrectAnswer = "sister-in-law",
                    Options = { "sister-in-law" }, LearningTaskId = 8, Category = "Family",
                    Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Verlobter", CorrectAnswer = "fiancé", Options = { "fiancé" },
                    LearningTaskId = 8, Category = "Family", Difficulty = "4 Klasse"
                },

                // Category: Animals
                new()
                {
                    Text = "Eule", CorrectAnswer = "owl", Options = { "owl" }, LearningTaskId = 8,
                    Category = "Animal", Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Wal", CorrectAnswer = "whale", Options = { "whale" }, LearningTaskId = 8,
                    Category = "Animal", Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Pferd", CorrectAnswer = "horse", Options = { "horse" }, LearningTaskId = 8,
                    Category = "Animal", Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Bär", CorrectAnswer = "bear", Options = { "bear" }, LearningTaskId = 8,
                    Category = "Animal", Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Adler", CorrectAnswer = "eagle", Options = { "eagle" }, LearningTaskId = 8,
                    Category = "Animal", Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Krokodil", CorrectAnswer = "crocodile", Options = { "crocodile" },
                    LearningTaskId = 8, Category = "Animal", Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Pferd", CorrectAnswer = "horse", Options = { "horse" }, LearningTaskId = 8,
                    Category = "Animal", Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Papagei", CorrectAnswer = "parrot", Options = { "parrot" },
                    LearningTaskId = 8, Category = "Animal", Difficulty = "4 Klasse"
                },

                // Category: Other
                new()
                {
                    Text = "Leiter", CorrectAnswer = "ladder", Options = { "ladder" },
                    LearningTaskId = 8, Category = "Other", Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Tunnel", CorrectAnswer = "tunnel", Options = { "tunnel" },
                    LearningTaskId = 8, Category = "Other", Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Brunnen", CorrectAnswer = "fountain", Options = { "fountain" },
                    LearningTaskId = 8, Category = "Other", Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Wolke", CorrectAnswer = "cloud", Options = { "cloud" }, LearningTaskId = 8,
                    Category = "Other", Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Sonne", CorrectAnswer = "sun", Options = { "sun" }, LearningTaskId = 8,
                    Category = "Other", Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Mond", CorrectAnswer = "moon", Options = { "moon" }, LearningTaskId = 8,
                    Category = "Other", Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Stern", CorrectAnswer = "star", Options = { "star" }, LearningTaskId = 8,
                    Category = "Other", Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Straßenlaterne", CorrectAnswer = "streetlight",
                    Options = { "streetlight" }, LearningTaskId = 8, Category = "Other",
                    Difficulty = "4 Klasse"
                },

                // Category: Cloth
                new()
                {
                    Text = "Pullover", CorrectAnswer = "sweater", Options = { "sweater" },
                    LearningTaskId = 8, Category = "Cloth", Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Bluse", CorrectAnswer = "blouse", Options = { "blouse" },
                    LearningTaskId = 8, Category = "Cloth", Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Shorts", CorrectAnswer = "shorts", Options = { "shorts" },
                    LearningTaskId = 8, Category = "Cloth", Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Schal", CorrectAnswer = "scarf", Options = { "scarf" }, LearningTaskId = 8,
                    Category = "Cloth", Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Handschuhe", CorrectAnswer = "gloves", Options = { "gloves" },
                    LearningTaskId = 8, Category = "Cloth", Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Mantel", CorrectAnswer = "coat", Options = { "coat" }, LearningTaskId = 8,
                    Category = "Cloth", Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Sandalen", CorrectAnswer = "sandals", Options = { "sandals" },
                    LearningTaskId = 8, Category = "Cloth", Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Hut", CorrectAnswer = "hat", Options = { "hat" }, LearningTaskId = 8,
                    Category = "Cloth", Difficulty = "4 Klasse"
                },


                // Vorschule – Finde das falsche Wort (LearningTask 9)
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "tree",
                    Options = { "rocket", "tree", "ship", "car" }, LearningTaskId = 9,
                    Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "lava",
                    Options = { "lava", "green", "blue", "red" }, LearningTaskId = 9,
                    Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "ship",
                    Options = { "hat", "boot", "ship", "shirt" }, LearningTaskId = 9,
                    Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "glass",
                    Options = { "lion", "dog", "glass", "penguin" }, LearningTaskId = 9,
                    Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "snake",
                    Options = { "door", "window", "wall", "snake" }, LearningTaskId = 9,
                    Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "toy",
                    Options = { "mother", "sister", "uncle", "toy" }, LearningTaskId = 9,
                    Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "food",
                    Options = { "golf", "football", "basketball", "food" }, LearningTaskId = 9,
                    Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "cat",
                    Options = { "apple", "banana", "carrot", "cat" }, LearningTaskId = 9,
                    Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "shoe",
                    Options = { "hat", "shirt", "shoe", "jacket" }, LearningTaskId = 9,
                    Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "car",
                    Options = { "plane", "bike", "boat", "car" }, LearningTaskId = 9,
                    Difficulty = "Vorschule"
                },

                // 1. Klasse – Finde das falsche Wort (LearningTask 9)
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "banana",
                    Options = { "apple", "orange", "banana", "grape" }, LearningTaskId = 9,
                    Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "car",
                    Options = { "bus", "bike", "train", "car" }, LearningTaskId = 9,
                    Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "dog",
                    Options = { "cat", "rabbit", "dog", "parrot" }, LearningTaskId = 9,
                    Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "hat",
                    Options = { "shirt", "pants", "hat", "jacket" }, LearningTaskId = 9,
                    Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "moon",
                    Options = { "sun", "star", "cloud", "moon" }, LearningTaskId = 9,
                    Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "shoe",
                    Options = { "sock", "glove", "shoe", "hat" }, LearningTaskId = 9,
                    Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "fish",
                    Options = { "dog", "cat", "fish", "bird" }, LearningTaskId = 9,
                    Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "tree",
                    Options = { "flower", "bush", "grass", "tree" }, LearningTaskId = 9,
                    Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "table",
                    Options = { "chair", "sofa", "table", "bed" }, LearningTaskId = 9,
                    Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "rabbit",
                    Options = { "lion", "tiger", "bear", "rabbit" }, LearningTaskId = 9,
                    Difficulty = "1 Klasse"
                },

                // 2. Klasse – Finde das falsche Wort (LearningTask 9)
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "carrot",
                    Options = { "apple", "banana", "orange", "carrot" }, LearningTaskId = 9,
                    Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "train",
                    Options = { "dog", "cat", "rabbit", "train" }, LearningTaskId = 9,
                    Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "shirt",
                    Options = { "pants", "shoes", "shirt", "jacket" }, LearningTaskId = 9,
                    Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "whale",
                    Options = { "fish", "shark", "dolphin", "whale" }, LearningTaskId = 9,
                    Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "rose",
                    Options = { "oak", "pine", "rose", "maple" }, LearningTaskId = 9,
                    Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "plane",
                    Options = { "bus", "car", "bike", "plane" }, LearningTaskId = 9,
                    Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "milk",
                    Options = { "water", "juice", "milk", "tea" }, LearningTaskId = 9,
                    Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "spoon",
                    Options = { "knife", "fork", "plate", "spoon" }, LearningTaskId = 9,
                    Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "sun",
                    Options = { "Monday", "Tuesday", "Friday", "sun" }, LearningTaskId = 9,
                    Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "triangle",
                    Options = { "circle", "square", "triangle", "red" }, LearningTaskId = 9,
                    Difficulty = "2 Klasse"
                },

                // 3. Klasse – Finde das falsche Wort (LearningTask 9)
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "computer",
                    Options = { "lion", "tiger", "bear", "computer" }, LearningTaskId = 9,
                    Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "giraffe",
                    Options = { "apple", "banana", "orange", "giraffe" }, LearningTaskId = 9,
                    Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "boots",
                    Options = { "shirt", "pants", "jacket", "boots" }, LearningTaskId = 9,
                    Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "rainbow",
                    Options = { "sun", "moon", "star", "rainbow" }, LearningTaskId = 9,
                    Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "hospital",
                    Options = { "school", "museum", "hospital", "library" }, LearningTaskId = 9,
                    Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "swimming",
                    Options = { "football", "basketball", "tennis", "swimming" }, LearningTaskId = 9,
                    Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "train",
                    Options = { "car", "bike", "bus", "train" }, LearningTaskId = 9,
                    Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "tree",
                    Options = { "dog", "cat", "rabbit", "tree" }, LearningTaskId = 9,
                    Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "chair",
                    Options = { "bed", "sofa", "table", "chair" }, LearningTaskId = 9,
                    Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "pencil",
                    Options = { "red", "blue", "green", "pencil" }, LearningTaskId = 9,
                    Difficulty = "3 Klasse"
                },

                // 4. Klasse – Finde das falsche Wort (LearningTask 9)
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "cinema",
                    Options = { "mother", "father", "sister", "cinema" }, LearningTaskId = 9,
                    Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "parrot",
                    Options = { "shirt", "pants", "jacket", "parrot" }, LearningTaskId = 9,
                    Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "giraffe",
                    Options = { "car", "bus", "train", "giraffe" }, LearningTaskId = 9,
                    Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "electricity",
                    Options = { "Monday", "Tuesday", "Friday", "electricity" }, LearningTaskId = 9,
                    Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "raincoat",
                    Options = { "hat", "scarf", "gloves", "raincoat" }, LearningTaskId = 9,
                    Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "squirrel",
                    Options = { "rose", "tulip", "sunflower", "squirrel" }, LearningTaskId = 9,
                    Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "museum",
                    Options = { "mountain", "river", "forest", "museum" }, LearningTaskId = 9,
                    Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "football",
                    Options = { "math", "english", "science", "football" }, LearningTaskId = 9,
                    Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "whale",
                    Options = { "dog", "cat", "rabbit", "whale" }, LearningTaskId = 9,
                    Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "smartphone",
                    Options = { "book", "newspaper", "magazine", "smartphone" }, LearningTaskId = 9,
                    Difficulty = "4 Klasse"
                },


                // Fragen für "Fülle die Lücken" (LearningTask 10 - Vorschule)
                new()
                {
                    Text = "Die Kuh macht ___.", CorrectAnswer = "Muh",
                    Options = { "Wuff", "Miau", "Muh" }, LearningTaskId = 10, Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Der Himmel ist ___.", CorrectAnswer = "blau",
                    Options = { "rot", "blau", "grün" }, LearningTaskId = 10, Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Die Banane ist ___.", CorrectAnswer = "gelb",
                    Options = { "gelb", "blau", "lila" }, LearningTaskId = 10, Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Ein Auto fährt auf der ___.", CorrectAnswer = "Straße",
                    Options = { "Straße", "Wiese", "Wald" }, LearningTaskId = 10,
                    Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Der Ball ist ___.", CorrectAnswer = "rund",
                    Options = { "eckig", "rund", "flach" }, LearningTaskId = 10,
                    Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Ein Fisch schwimmt im ___.", CorrectAnswer = "Wasser",
                    Options = { "Sand", "Wasser", "Baum" }, LearningTaskId = 10,
                    Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Die Sonne ist ___.", CorrectAnswer = "warm",
                    Options = { "warm", "kalt", "nass" }, LearningTaskId = 10, Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Die Katze macht ___.", CorrectAnswer = "Miau",
                    Options = { "Miau", "Wuff", "Muh" }, LearningTaskId = 10, Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Wir essen ein ___.", CorrectAnswer = "Brot",
                    Options = { "Brot", "Auto", "Haus" }, LearningTaskId = 10, Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Das Baby liegt im ___.", CorrectAnswer = "Bett",
                    Options = { "Bett", "Auto", "Baum" }, LearningTaskId = 10, Difficulty = "Vorschule"
                },

                // Fragen für "Fülle die Lücken" (LearningTask 10 - 1. Klasse)
                new()
                {
                    Text = "Die Blume ist ___.", CorrectAnswer = "schön",
                    Options = { "schnell", "schön", "laut" }, LearningTaskId = 10,
                    Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Der Vogel fliegt in den ___.", CorrectAnswer = "Himmel",
                    Options = { "Himmel", "Boden", "Baum" }, LearningTaskId = 10,
                    Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Der Junge isst ein ___.", CorrectAnswer = "Eis",
                    Options = { "Auto", "Eis", "Ball" }, LearningTaskId = 10, Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Die Oma liest ein ___.", CorrectAnswer = "Buch",
                    Options = { "Buch", "Auto", "Apfel" }, LearningTaskId = 10, Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Das Haus hat ein ___.", CorrectAnswer = "Dach",
                    Options = { "Dach", "Bein", "Rad" }, LearningTaskId = 10, Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Der Hund frisst einen ___.", CorrectAnswer = "Knochen",
                    Options = { "Knochen", "Ball", "Schuh" }, LearningTaskId = 10,
                    Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Die Ente schwimmt im ___.", CorrectAnswer = "Teich",
                    Options = { "Teich", "Haus", "Wald" }, LearningTaskId = 10, Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Das Feuer ist ___.", CorrectAnswer = "heiß",
                    Options = { "heiß", "kalt", "nass" }, LearningTaskId = 10, Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Der Lehrer schreibt an die ___.", CorrectAnswer = "Tafel",
                    Options = { "Tafel", "Wand", "Tür" }, LearningTaskId = 10, Difficulty = "1 Klasse"
                },
                new()
                {
                    Text = "Die Uhr zeigt die ___.", CorrectAnswer = "Zeit",
                    Options = { "Zeit", "Hand", "Nase" }, LearningTaskId = 10, Difficulty = "1 Klasse"
                },

                // Fragen für "Fülle die Lücken" (LearningTask 10 - 2. Klasse)
                new()
                {
                    Text = "Der Regen fällt vom ___.", CorrectAnswer = "Himmel",
                    Options = { "Himmel", "Boden", "Baum" }, LearningTaskId = 10,
                    Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Die Maus läuft ins ___.", CorrectAnswer = "Loch",
                    Options = { "Loch", "Haus", "Glas" }, LearningTaskId = 10, Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Im Winter fällt der ___.", CorrectAnswer = "Schnee",
                    Options = { "Regen", "Schnee", "Wind" }, LearningTaskId = 10,
                    Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Der Schüler schreibt mit einem ___.", CorrectAnswer = "Stift",
                    Options = { "Stift", "Rad", "Glas" }, LearningTaskId = 10, Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Am Abend wird es ___.", CorrectAnswer = "dunkel",
                    Options = { "hell", "dunkel", "laut" }, LearningTaskId = 10,
                    Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Der Tisch hat vier ___.", CorrectAnswer = "Beine",
                    Options = { "Beine", "Arme", "Türen" }, LearningTaskId = 10,
                    Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Ein Jahr hat zwölf ___.", CorrectAnswer = "Monate",
                    Options = { "Monate", "Tage", "Stunden" }, LearningTaskId = 10,
                    Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Ein Apfelbaum hat viele ___.", CorrectAnswer = "Äpfel",
                    Options = { "Äpfel", "Autos", "Tiere" }, LearningTaskId = 10,
                    Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Die Kerze gibt ___.", CorrectAnswer = "Licht",
                    Options = { "Licht", "Luft", "Lärm" }, LearningTaskId = 10, Difficulty = "2 Klasse"
                },
                new()
                {
                    Text = "Der König trägt eine ___.", CorrectAnswer = "Krone",
                    Options = { "Krone", "Jacke", "Uhr" }, LearningTaskId = 10, Difficulty = "2 Klasse"
                },

                // Fragen für "Fülle die Lücken" (LearningTask 10 - 3. Klasse)
                new()
                {
                    Text = "Die Erde dreht sich um die ___.", CorrectAnswer = "Sonne",
                    Options = { "Sonne", "Mond", "Sterne" }, LearningTaskId = 10,
                    Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Ein Rechteck hat vier ___.", CorrectAnswer = "Ecken",
                    Options = { "Ecken", "Seiten", "Räder" }, LearningTaskId = 10,
                    Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Der Fluss fließt ins ___.", CorrectAnswer = "Meer",
                    Options = { "Meer", "Gebirge", "Haus" }, LearningTaskId = 10,
                    Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Ein Jahr hat 365 ___.", CorrectAnswer = "Tage",
                    Options = { "Tage", "Stunden", "Wochen" }, LearningTaskId = 10,
                    Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Das Gegenteil von heiß ist ___.", CorrectAnswer = "kalt",
                    Options = { "warm", "kalt", "schön" }, LearningTaskId = 10, Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Die Hauptstadt von Deutschland ist ___.", CorrectAnswer = "Berlin",
                    Options = { "Berlin", "Hamburg", "Köln" }, LearningTaskId = 10,
                    Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Die Ampel zeigt ___.", CorrectAnswer = "Farben",
                    Options = { "Farben", "Lichter", "Töne" }, LearningTaskId = 10,
                    Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Das Gegenteil von laut ist ___.", CorrectAnswer = "leise",
                    Options = { "leise", "ruhig", "schnell" }, LearningTaskId = 10,
                    Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Der Bäcker backt ___.", CorrectAnswer = "Brot",
                    Options = { "Brot", "Milch", "Tische" }, LearningTaskId = 10,
                    Difficulty = "3 Klasse"
                },
                new()
                {
                    Text = "Das Thermometer misst die ___.", CorrectAnswer = "Temperatur",
                    Options = { "Temperatur", "Zeit", "Höhe" }, LearningTaskId = 10,
                    Difficulty = "3 Klasse"
                },

                // Fragen für "Fülle die Lücken" (LearningTask 10 - 4. Klasse)
                new()
                {
                    Text = "Die Hauptstadt von Frankreich ist ___.", CorrectAnswer = "Paris",
                    Options = { "Paris", "Rom", "Berlin" }, LearningTaskId = 10,
                    Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Wasser gefriert bei ___.", CorrectAnswer = "0 Grad",
                    Options = { "0 Grad", "10 Grad", "100 Grad" }, LearningTaskId = 10,
                    Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Das größte Land der Erde ist ___.", CorrectAnswer = "Russland",
                    Options = { "Russland", "China", "Kanada" }, LearningTaskId = 10,
                    Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Das Gegenteil von schwer ist ___.", CorrectAnswer = "leicht",
                    Options = { "leicht", "dick", "stark" }, LearningTaskId = 10,
                    Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Die Donau ist ein ___.", CorrectAnswer = "Fluss",
                    Options = { "Fluss", "Gebirge", "See" }, LearningTaskId = 10,
                    Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Die Erde ist ein ___.", CorrectAnswer = "Planet",
                    Options = { "Planet", "Stern", "Mond" }, LearningTaskId = 10,
                    Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Das Gegenteil von früh ist ___.", CorrectAnswer = "spät",
                    Options = { "spät", "schnell", "klein" }, LearningTaskId = 10,
                    Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Die Alpen sind ein ___.", CorrectAnswer = "Gebirge",
                    Options = { "Gebirge", "Fluss", "Meer" }, LearningTaskId = 10,
                    Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Das Blut ist ___.", CorrectAnswer = "rot",
                    Options = { "rot", "blau", "grün" }, LearningTaskId = 10, Difficulty = "4 Klasse"
                },
                new()
                {
                    Text = "Der höchste Berg der Erde ist der ___.", CorrectAnswer = "Mount Everest",
                    Options = { "Mount Everest", "Zugspitze", "Matterhorn" }, LearningTaskId = 10,
                    Difficulty = "4 Klasse"
                },


                // Fragen für "Fülle die Form" (Learning Task 12)
                new()
                {
                    Text = "Wähle die passende Form für das Muster",
                    CorrectAnswer = "assets/questImg/form.heart.png",
                    ImageUrl = "assets/questImg/form.heartQuestion.png",
                    Options =
                    {
                        "assets/questImg/form.label.png", "assets/questImg/form.heart.png",
                        "assets/questImg/form.tear.png", "assets/questImg/form.new-moon.png"
                    },
                    LearningTaskId = 12,
                    Difficulty = "Vorschule"
                },
                new()
                {
                    Text = "Wähle die passende Form für das Muster",
                    CorrectAnswer = "assets/questImg/form.diamond.png",
                    ImageUrl = "assets/questImg/form.diamondQuestion.png",
                    Options =
                    {
                        "assets/questImg/form.hexagon.png", "assets/questImg/form.bleach.png",
                        "assets/questImg/form.diamond.png", "assets/questImg/form.black-square.png"
                    },
                    LearningTaskId = 12,
                    Difficulty = "Vorschule"
                }
            };

            context.Questions.AddRange(questions);
            context.SaveChanges();
        }

        if (!context.Avatars.Any())
        {
            var avatars = new List<Avatar>
            {
                new()
                {
                    Name = "Glückliche Ente",
                    ImageUrl = "assets/images/duck.png",
                    Description = "Eine fröhliche Ente, die gerne schwimmt.",
                    UnlockStarRequirement = 2
                },
                new()
                {
                    Name = "Abenteuerlicher Bär",
                    ImageUrl = "assets/images/bear.png",
                    Description = "Ein mutiger Bär, der gerne im Wald spielt.",
                    UnlockStarRequirement = 4
                },
                new()
                {
                    Name = "Clevere Eule",
                    ImageUrl = "assets/images/owl.png",
                    Description = "Eine weise Eule, die alle Antworten kennt.",
                    UnlockStarRequirement = 6
                }
            };
            context.Avatars.AddRange(avatars);
            context.SaveChanges();
        }
    }
}