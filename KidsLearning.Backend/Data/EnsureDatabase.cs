using Microsoft.AspNetCore.Identity;
using KidsLearning.Backend.Models;

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
                new LearningTask
                {
                    Title = "Zahlen finden",
                    Description = "Finde die fehlenden Zahlen in der Reihe.",
                    Subject = "Mathe-Abenteuer"
                },
                // Task 2
                new LearningTask
                {
                    Title = "Addition bis 10",
                    Description = "Addiere zwei einstellige Zahlen.",
                    Subject = "Mathe-Abenteuer"
                },
                // Task 3
                new LearningTask
                {
                    Title = "Formen erkennen",
                    Description = "Erkenne verschiedene geometrische Formen.",
                    Subject = "Mathe-Abenteuer"
                },
                // Task 4
                new LearningTask
                {
                    Title = "Alphabet lernen",
                    Description = "Nenne alle Buchstaben des Alphabets in der richtigen Reihenfolge.",
                    Subject = "Buchstaben-land"
                },
                // Task 5
                new LearningTask
                {
                    Title = "Buchstaben verbinden",
                    Description = "Verbinde die Großbuchstaben mit den Kleinbuchstaben.",
                    Subject = "Buchstaben-land"
                },
                // Task 6
                new LearningTask
                {
                    Title = "Wörter buchstabieren",
                    Description = "Buchstabiere einfache Wörter wie 'Hund' und 'Katze'.",
                    Subject = "Buchstaben-land"
                },
                // Task 7
                new LearningTask {
                    Title = "Englische Bilder",
                    Description = "Wähle das korrekte Englische Wort zum angezeigten Bild",
                    Subject = "Englisch"

                },
                // Task 8
                new LearningTask {
                    Title = "Deutsch/Englisch verbinden",
                    Description = "Finde zu jedem deutschen Wort das richtige Englische Wort",
                    Subject = "Englisch"

                },
                // Task 9
                new LearningTask {
                    Title = "Finde den Imposter",
                    Description = "Wähle das Englische Wort, das nicht zu den anderen passt",
                    Subject = "Englisch"

                },
                // Task 10
                new LearningTask
                {
                    Title = "Fülle die Lücken",
                    Description = "Schreibe die richtigen Wörter in die Lücken",
                    Subject = "Buchstaben-land"
                },
                // Task 11
                new LearningTask
                {
                    Title = "Zahlenkombinationen",
                    Description = "Bist du bereit dir Zahlenkombinationen zu merken?",
                    Subject = "Logik-Dschungel"

                },
                // Task 12
                new LearningTask
                {
                    Title = "Fülle die Form",
                    Description = "Finde die Richtige Form die in das Muster passt!",
                    Subject = "Logik-Dschungel"
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
                new Questions { Text = "Welche Zahl fehlt: 1, 2, ?, 4", CorrectAnswer = "3", Options = new List<string>{ "2", "3", "4", "5" }, LearningTaskId = 1, Difficulty = "Vorschule" },
                new Questions { Text = "Welche Zahl fehlt: ?, 2, 3", CorrectAnswer = "1", Options = new List<string>{ "0", "1", "2", "3" }, LearningTaskId = 1, Difficulty = "Vorschule"  },
                new Questions { Text = "Welche Zahl fehlt: 3, ?, 5", CorrectAnswer = "4", Options = new List<string>{ "2", "3", "4", "5" }, LearningTaskId = 1, Difficulty = "Vorschule"  },
                new Questions { Text = "Welche Zahl fehlt: 4, ?, 6", CorrectAnswer = "5", Options = new List<string>{ "3", "4", "5", "7" }, LearningTaskId = 1, Difficulty = "Vorschule"  },
                new Questions { Text = "Welche Zahl fehlt: 6, 7, ?, 9", CorrectAnswer = "8", Options = new List<string>{ "6", "7", "8", "10" }, LearningTaskId = 1, Difficulty = "Vorschule"  },
                new Questions { Text = "Welche Zahl fehlt: ?, 1, 2", CorrectAnswer = "0", Options = new List<string>{ "0", "1", "2", "3" }, LearningTaskId = 1, Difficulty = "Vorschule"  },
                new Questions { Text = "Welche Zahl fehlt: 7, 8, ?, 10", CorrectAnswer = "9", Options = new List<string>{ "7", "8", "9", "11" }, LearningTaskId = 1, Difficulty = "Vorschule"  },
                new Questions { Text = "Welche Zahl fehlt: 2, 3, ?, 5", CorrectAnswer = "4", Options = new List<string>{ "3", "4", "5", "6" }, LearningTaskId = 1, Difficulty = "Vorschule"  },
                new Questions { Text = "Welche Zahl fehlt: 9, ?, 11", CorrectAnswer = "10", Options = new List<string>{ "9", "10", "11", "12" }, LearningTaskId = 1, Difficulty = "Vorschule"  },
                new Questions { Text = "Welche Zahl fehlt: ?, 5, 6", CorrectAnswer = "4", Options = new List<string>{ "3", "4", "5", "6" }, LearningTaskId = 1, Difficulty = "Vorschule"  },

                //1Klasse
                new Questions { Text = "Welche Zahl fehlt: 2, 4, ?, 8", CorrectAnswer = "6", Options = new List<string>{ "5", "6", "7", "8" }, LearningTaskId = 1, Difficulty = "1 Klasse"  },
                new Questions { Text = "Welche Zahl fehlt: 10, ?, 12, 13", CorrectAnswer = "11", Options = new List<string>{ "10", "11", "12", "14" }, LearningTaskId = 1, Difficulty = "1 Klasse" },
                new Questions { Text = "Welche Zahl fehlt: 1, 3, ?, 7", CorrectAnswer = "5", Options = new List<string>{ "4", "5", "6", "7" }, LearningTaskId = 1, Difficulty = "1 Klasse" },
                new Questions { Text = "Welche Zahl fehlt: ?, 15, 16", CorrectAnswer = "14", Options = new List<string>{ "13", "14", "15", "16" }, LearningTaskId = 1, Difficulty = "1 Klasse" },
                new Questions { Text = "Welche Zahl fehlt: 20, 21, ?, 23", CorrectAnswer = "22", Options = new List<string>{ "21", "22", "23", "24" }, LearningTaskId = 1, Difficulty = "1 Klasse" },
                new Questions { Text = "Welche Zahl fehlt: 5, ?, 15, 20", CorrectAnswer = "10", Options = new List<string>{ "8", "9", "10", "12" }, LearningTaskId = 1, Difficulty = "1 Klasse" },
                new Questions { Text = "Welche Zahl fehlt: ?, 30, 40", CorrectAnswer = "20", Options = new List<string>{ "10", "20", "25", "30" }, LearningTaskId = 1, Difficulty = "1 Klasse" },
                new Questions { Text = "Welche Zahl fehlt: 12, 13, ?, 15", CorrectAnswer = "14", Options = new List<string>{ "12", "13", "14", "15" }, LearningTaskId = 1, Difficulty = "1 Klasse" },
                new Questions { Text = "Welche Zahl fehlt: 50, ?, 52", CorrectAnswer = "51", Options = new List<string>{ "50", "51", "52", "53" }, LearningTaskId = 1, Difficulty = "1 Klasse" },
                new Questions { Text = "Welche Zahl fehlt: 9, 12, ?, 18", CorrectAnswer = "15", Options = new List<string>{ "14", "15", "16", "17" }, LearningTaskId = 1, Difficulty = "1 Klasse" },

                //2Klasse
                new Questions { Text = "Welche Zahl fehlt: 100, 200, ?, 400", CorrectAnswer = "300", Options = new List<string>{ "100", "200", "300", "400" }, LearningTaskId = 1, Difficulty = "2 Klasse" },
                new Questions { Text = "Welche Zahl fehlt: 2, 4, 6, ?", CorrectAnswer = "8", Options = new List<string>{ "6", "7", "8", "9" }, LearningTaskId = 1, Difficulty = "2 Klasse" },
                new Questions { Text = "Welche Zahl fehlt: 5, 10, ?, 20", CorrectAnswer = "15", Options = new List<string>{ "12", "15", "18", "20" }, LearningTaskId = 1, Difficulty = "2 Klasse" },
                new Questions { Text = "Welche Zahl fehlt: 25, 30, ?, 40", CorrectAnswer = "35", Options = new List<string>{ "32", "33", "34", "35" }, LearningTaskId = 1, Difficulty = "2 Klasse" },
                new Questions { Text = "Welche Zahl fehlt: ?, 70, 80", CorrectAnswer = "60", Options = new List<string>{ "50", "55", "60", "65" }, LearningTaskId = 1, Difficulty = "2 Klasse" },
                new Questions { Text = "Welche Zahl fehlt: 90, ?, 110", CorrectAnswer = "100", Options = new List<string>{ "95", "98", "100", "105" }, LearningTaskId = 1, Difficulty = "2 Klasse" },
                new Questions { Text = "Welche Zahl fehlt: 7, 14, ?, 28", CorrectAnswer = "21", Options = new List<string>{ "20", "21", "22", "23" }, LearningTaskId = 1, Difficulty = "2 Klasse" },
                new Questions { Text = "Welche Zahl fehlt: 11, ?, 33", CorrectAnswer = "22", Options = new List<string>{ "20", "21", "22", "23" }, LearningTaskId = 1, Difficulty = "2 Klasse" },
                new Questions { Text = "Welche Zahl fehlt: 40, 50, ?, 70", CorrectAnswer = "60", Options = new List<string>{ "55", "58", "60", "65" }, LearningTaskId = 1, Difficulty = "2 Klasse" },
                new Questions { Text = "Welche Zahl fehlt: 13, ?, 39", CorrectAnswer = "26", Options = new List<string>{ "24", "25", "26", "27" }, LearningTaskId = 1, Difficulty = "2 Klasse" },

                //3Klasse
                new Questions { Text = "Welche Zahl fehlt: 100, 105, ?, 115", CorrectAnswer = "110", Options = new List<string>{ "108", "109", "110", "111" }, LearningTaskId = 1, Difficulty = "3 Klasse" },
                new Questions { Text = "Welche Zahl fehlt: 200, 220, ?, 260", CorrectAnswer = "240", Options = new List<string>{ "230", "235", "240", "245" }, LearningTaskId = 1, Difficulty = "3 Klasse" },
                new Questions { Text = "Welche Zahl fehlt: 300, ?, 500", CorrectAnswer = "400", Options = new List<string>{ "350", "375", "400", "450" }, LearningTaskId = 1, Difficulty = "3 Klasse" },
                new Questions { Text = "Welche Zahl fehlt: 50, ?, 80", CorrectAnswer = "65", Options = new List<string>{ "60", "62", "65", "70" }, LearningTaskId = 1, Difficulty = "3 Klasse" },
                new Questions { Text = "Welche Zahl fehlt: 1000, 900, ?, 700", CorrectAnswer = "800", Options = new List<string>{ "750", "800", "850", "900" }, LearningTaskId = 1, Difficulty = "3 Klasse" },
                new Questions { Text = "Welche Zahl fehlt: ?, 3000, 4000", CorrectAnswer = "2000", Options = new List<string>{ "1000", "2000", "2500", "3000" }, LearningTaskId = 1, Difficulty = "3 Klasse" },
                new Questions { Text = "Welche Zahl fehlt: 15, 30, ?, 60", CorrectAnswer = "45", Options = new List<string>{ "40", "42", "45", "48" }, LearningTaskId = 1, Difficulty = "3 Klasse" },
                new Questions { Text = "Welche Zahl fehlt: 21, ?, 63", CorrectAnswer = "42", Options = new List<string>{ "40", "41", "42", "43" }, LearningTaskId = 1, Difficulty = "3 Klasse" },
                new Questions { Text = "Welche Zahl fehlt: 120, 130, ?, 150", CorrectAnswer = "140", Options = new List<string>{ "135", "138", "140", "142" }, LearningTaskId = 1, Difficulty = "3 Klasse" },
                new Questions { Text = "Welche Zahl fehlt: 500, ?, 700", CorrectAnswer = "600", Options = new List<string>{ "550", "580", "600", "620" }, LearningTaskId = 1, Difficulty = "3 Klasse" },

                //4Klasse
                new Questions { Text = "Welche Zahl fehlt: 5, 10, ?, 20, 25", CorrectAnswer = "15", Options = new List<string>{ "12", "13", "14", "15" }, LearningTaskId = 1, Difficulty = "4 Klasse" },
                new Questions { Text = "Welche Zahl fehlt: 100, 200, ?, 400, 500", CorrectAnswer = "300", Options = new List<string>{ "250", "275", "300", "325" }, LearningTaskId = 1, Difficulty = "4 Klasse" },
                new Questions { Text = "Welche Zahl fehlt: 2, 4, 8, ?, 32", CorrectAnswer = "16", Options = new List<string>{ "12", "14", "16", "18" }, LearningTaskId = 1, Difficulty = "4 Klasse" },
                new Questions { Text = "Welche Zahl fehlt: 3, 9, ?, 81", CorrectAnswer = "27", Options = new List<string>{ "18", "21", "27", "36" }, LearningTaskId = 1, Difficulty = "4 Klasse" },
                new Questions { Text = "Welche Zahl fehlt: 1, 4, 9, ?, 25", CorrectAnswer = "16", Options = new List<string>{ "12", "14", "16", "18" }, LearningTaskId = 1, Difficulty = "4 Klasse" },
                new Questions { Text = "Welche Zahl fehlt: ?, 64, 81", CorrectAnswer = "49", Options = new List<string>{ "36", "42", "49", "56" }, LearningTaskId = 1, Difficulty = "4 Klasse" },
                new Questions { Text = "Welche Zahl fehlt: 12, 24, ?, 96", CorrectAnswer = "48", Options = new List<string>{ "36", "42", "48", "54" }, LearningTaskId = 1, Difficulty = "4 Klasse" },
                new Questions { Text = "Welche Zahl fehlt: 144, ?, 576", CorrectAnswer = "288", Options = new List<string>{ "200", "240", "288", "320" }, LearningTaskId = 1, Difficulty = "4 Klasse" },
                new Questions { Text = "Welche Zahl fehlt: 7, 14, ?, 56", CorrectAnswer = "28", Options = new List<string>{ "21", "25", "28", "30" }, LearningTaskId = 1, Difficulty = "4 Klasse" },
                new Questions { Text = "Welche Zahl fehlt: 1000, ?, 4000", CorrectAnswer = "2000", Options = new List<string>{ "1500", "2000", "2500", "3000" }, LearningTaskId = 1, Difficulty = "4 Klasse" },

                // Vorschule - Addition bis 10 (LearningTaskId = 2)
                new Questions { Text = "Was ist 1 + 1?", CorrectAnswer = "2", Options = new List<string>{ "1","2","3","4" }, LearningTaskId = 2, Difficulty = "Vorschule" },
                new Questions { Text = "Was ist 2 + 3?", CorrectAnswer = "5", Options = new List<string>{ "4","5","6","7" }, LearningTaskId = 2, Difficulty = "Vorschule" },
                new Questions { Text = "Was ist 0 + 4?", CorrectAnswer = "4", Options = new List<string>{ "3","4","5","6" }, LearningTaskId = 2, Difficulty = "Vorschule" },
                new Questions { Text = "Was ist 5 + 1?", CorrectAnswer = "6", Options = new List<string>{ "5","6","7","8" }, LearningTaskId = 2, Difficulty = "Vorschule" },
                new Questions { Text = "Was ist 3 + 2?", CorrectAnswer = "5", Options = new List<string>{ "4","5","6","7" }, LearningTaskId = 2, Difficulty = "Vorschule" },
                new Questions { Text = "Was ist 4 + 4?", CorrectAnswer = "8", Options = new List<string>{ "7","8","9","10" }, LearningTaskId = 2, Difficulty = "Vorschule" },
                new Questions { Text = "Was ist 6 + 2?", CorrectAnswer = "8", Options = new List<string>{ "7","8","9","10" }, LearningTaskId = 2, Difficulty = "Vorschule" },
                new Questions { Text = "Was ist 7 + 1?", CorrectAnswer = "8", Options = new List<string>{ "6","7","8","9" }, LearningTaskId = 2, Difficulty = "Vorschule" },
                new Questions { Text = "Was ist 8 + 1?", CorrectAnswer = "9", Options = new List<string>{ "7","8","9","10" }, LearningTaskId = 2, Difficulty = "Vorschule" },
                new Questions { Text = "Was ist 9 + 0?", CorrectAnswer = "9", Options = new List<string>{ "8","9","10","11" }, LearningTaskId = 2, Difficulty = "Vorschule" },

                // 1. Klasse (LearningTaskId = 2)
                new Questions { Text = "Was ist 12 + 3?", CorrectAnswer = "15", Options = new List<string>{ "14","15","16","17" }, LearningTaskId = 2, Difficulty = "1 Klasse" },
                new Questions { Text = "Was ist 8 + 7?", CorrectAnswer = "15", Options = new List<string>{ "13","14","15","16" }, LearningTaskId = 2, Difficulty = "1 Klasse" },
                new Questions { Text = "Was ist 10 - 4?", CorrectAnswer = "6", Options = new List<string>{ "5","6","7","8" }, LearningTaskId = 2, Difficulty = "1 Klasse" },
                new Questions { Text = "Was ist 15 - 9?", CorrectAnswer = "6", Options = new List<string>{ "5","6","7","8" }, LearningTaskId = 2, Difficulty = "1 Klasse" },
                new Questions { Text = "Was ist 11 + 8?", CorrectAnswer = "19", Options = new List<string>{ "18","19","20","21" }, LearningTaskId = 2, Difficulty = "1 Klasse" },
                new Questions { Text = "Was ist 7 + 6?", CorrectAnswer = "13", Options = new List<string>{ "12","13","14","15" }, LearningTaskId = 2, Difficulty = "1 Klasse" },
                new Questions { Text = "Was ist 20 - 5?", CorrectAnswer = "15", Options = new List<string>{ "14","15","16","17" }, LearningTaskId = 2, Difficulty = "1 Klasse" },
                new Questions { Text = "Was ist 9 + 9?", CorrectAnswer = "18", Options = new List<string>{ "16","17","18","19" }, LearningTaskId = 2, Difficulty = "1 Klasse" },
                new Questions { Text = "Was ist 6 + 12?", CorrectAnswer = "18", Options = new List<string>{ "17","18","19","20" }, LearningTaskId = 2, Difficulty = "1 Klasse" },
                new Questions { Text = "Was ist 14 - 7?", CorrectAnswer = "7", Options = new List<string>{ "6","7","8","9" }, LearningTaskId = 2, Difficulty = "1 Klasse" },

                // 2. Klasse (LearningTaskId = 2)
                new Questions { Text = "Was ist 45 + 32?", CorrectAnswer = "77", Options = new List<string>{ "76","77","78","79" }, LearningTaskId = 2, Difficulty = "2 Klasse" },
                new Questions { Text = "Was ist 80 - 25?", CorrectAnswer = "55", Options = new List<string>{ "54","55","56","57" }, LearningTaskId = 2, Difficulty = "2 Klasse" },
                new Questions { Text = "Was ist 7 × 3?", CorrectAnswer = "21", Options = new List<string>{ "20","21","22","23" }, LearningTaskId = 2, Difficulty = "2 Klasse" },
                new Questions { Text = "Was ist 6 × 5?", CorrectAnswer = "30", Options = new List<string>{ "28","29","30","31" }, LearningTaskId = 2, Difficulty = "2 Klasse" },
                new Questions { Text = "Was ist 50 + 27?", CorrectAnswer = "77", Options = new List<string>{ "76","77","78","79" }, LearningTaskId = 2, Difficulty = "2 Klasse" },
                new Questions { Text = "Was ist 9 × 4?", CorrectAnswer = "36", Options = new List<string>{ "34","35","36","37" }, LearningTaskId = 2, Difficulty = "2 Klasse" },
                new Questions { Text = "Was ist 100 - 45?", CorrectAnswer = "55", Options = new List<string>{ "54","55","56","57" }, LearningTaskId = 2, Difficulty = "2 Klasse" },
                new Questions { Text = "Was ist 25 + 19?", CorrectAnswer = "44", Options = new List<string>{ "43","44","45","46" }, LearningTaskId = 2, Difficulty = "2 Klasse" },
                new Questions { Text = "Was ist 12 × 2?", CorrectAnswer = "24", Options = new List<string>{ "22","23","24","25" }, LearningTaskId = 2, Difficulty = "2 Klasse" },
                new Questions { Text = "Was ist 40 - 18?", CorrectAnswer = "22", Options = new List<string>{ "21","22","23","24" }, LearningTaskId = 2, Difficulty = "2 Klasse" },

                // 3. Klasse (LearningTaskId = 2)
                new Questions { Text = "Was ist 7 × 8?", CorrectAnswer = "56", Options = new List<string>{ "54","55","56","57" }, LearningTaskId = 2, Difficulty = "3 Klasse" },
                new Questions { Text = "Was ist 64 ÷ 8?", CorrectAnswer = "8", Options = new List<string>{ "7","8","9","10" }, LearningTaskId = 2, Difficulty = "3 Klasse" },
                new Questions { Text = "Was ist 9 × 9?", CorrectAnswer = "81", Options = new List<string>{ "80","81","82","83" }, LearningTaskId = 2, Difficulty = "3 Klasse" },
                new Questions { Text = "Was ist 72 ÷ 9?", CorrectAnswer = "8", Options = new List<string>{ "7","8","9","10" }, LearningTaskId = 2, Difficulty = "3 Klasse" },
                new Questions { Text = "Was ist 6 × 12?", CorrectAnswer = "72", Options = new List<string>{ "70","71","72","73" }, LearningTaskId = 2, Difficulty = "3 Klasse" },
                new Questions { Text = "Was ist 90 ÷ 10?", CorrectAnswer = "9", Options = new List<string>{ "8","9","10","11" }, LearningTaskId = 2, Difficulty = "3 Klasse" },
                new Questions { Text = "Was ist 11 × 7?", CorrectAnswer = "77", Options = new List<string>{ "76","77","78","79" }, LearningTaskId = 2, Difficulty = "3 Klasse" },
                new Questions { Text = "Was ist 8 × 8?", CorrectAnswer = "64", Options = new List<string>{ "63","64","65","66" }, LearningTaskId = 2, Difficulty = "3 Klasse" },
                new Questions { Text = "Was ist 100 ÷ 4?", CorrectAnswer = "25", Options = new List<string>{ "24","25","26","27" }, LearningTaskId = 2, Difficulty = "3 Klasse" },
                new Questions { Text = "Was ist 12 × 12?", CorrectAnswer = "144", Options = new List<string>{ "142","143","144","145" }, LearningTaskId = 2, Difficulty = "3 Klasse" },

                // 4. Klasse (LearningTaskId = 2)
                new Questions { Text = "Was ist 125 + 378?", CorrectAnswer = "503", Options = new List<string>{ "502","503","504","505" }, LearningTaskId = 2, Difficulty = "4 Klasse" },
                new Questions { Text = "Was ist 800 - 275?", CorrectAnswer = "525", Options = new List<string>{ "524","525","526","527" }, LearningTaskId = 2, Difficulty = "4 Klasse" },
                new Questions { Text = "Was ist 45 × 6?", CorrectAnswer = "270", Options = new List<string>{ "268","269","270","271" }, LearningTaskId = 2, Difficulty = "4 Klasse" },
                new Questions { Text = "Was ist 900 ÷ 30?", CorrectAnswer = "30", Options = new List<string>{ "28","29","30","31" }, LearningTaskId = 2, Difficulty = "4 Klasse" },
                new Questions { Text = "Was ist 123 + 456?", CorrectAnswer = "579", Options = new List<string>{ "578","579","580","581" }, LearningTaskId = 2, Difficulty = "4 Klasse" },
                new Questions { Text = "Was ist 750 - 425?", CorrectAnswer = "325", Options = new List<string>{ "324","325","326","327" }, LearningTaskId = 2, Difficulty = "4 Klasse" },
                new Questions { Text = "Was ist 25 × 12?", CorrectAnswer = "300", Options = new List<string>{ "298","299","300","301" }, LearningTaskId = 2, Difficulty = "4 Klasse" },
                new Questions { Text = "Was ist 640 ÷ 16?", CorrectAnswer = "40", Options = new List<string>{ "39","40","41","42" }, LearningTaskId = 2, Difficulty = "4 Klasse" },
                new Questions { Text = "Was ist 999 - 123?", CorrectAnswer = "876", Options = new List<string>{ "875","876","877","878" }, LearningTaskId = 2, Difficulty = "4 Klasse" },
                new Questions { Text = "Was ist 48 × 9?", CorrectAnswer = "432", Options = new List<string>{ "430","431","432","433" }, LearningTaskId = 2, Difficulty = "4 Klasse" },

               // Vorschule - Formen erkennen (LearningTask 3)
                new Questions { Text = "Welche Form ist rund?", CorrectAnswer = "Kreis", Options = new List<string> { "Quadrat", "Kreis", "Dreieck", "Rechteck" }, LearningTaskId = 3, Difficulty = "Vorschule" },
                new Questions { Text = "Welche Form hat vier gleich lange Seiten?", CorrectAnswer = "Quadrat", Options = new List<string> { "Dreieck", "Kreis", "Quadrat", "Rechteck" }, LearningTaskId = 3, Difficulty = "Vorschule" },
                new Questions { Text = "Welche Form hat drei Ecken?", CorrectAnswer = "Dreieck", Options = new List<string> { "Quadrat", "Dreieck", "Kreis", "Stern" }, LearningTaskId = 3, Difficulty = "Vorschule" },
                new Questions { Text = "Welche Form hat keine Ecken?", CorrectAnswer = "Kreis", Options = new List<string> { "Dreieck", "Kreis", "Quadrat", "Rechteck" }, LearningTaskId = 3, Difficulty = "Vorschule" },
                new Questions { Text = "Welche Form sieht aus wie ein Ei?", CorrectAnswer = "Oval", Options = new List<string> { "Kreis", "Quadrat", "Oval", "Dreieck" }, LearningTaskId = 3, Difficulty = "Vorschule" },
                new Questions { Text = "Welche Form hat fünf Ecken?", CorrectAnswer = "Fünfeck", Options = new List<string> { "Dreieck", "Fünfeck", "Quadrat", "Sechseck" }, LearningTaskId = 3, Difficulty = "Vorschule" },
                new Questions { Text = "Welche Form hat die meisten Ecken?", CorrectAnswer = "Stern", Options = new List<string> { "Dreieck", "Quadrat", "Stern", "Kreis" }, LearningTaskId = 3, Difficulty = "Vorschule" },
                new Questions { Text = "Welche Form hat zwei lange und zwei kurze Seiten?", CorrectAnswer = "Rechteck", Options = new List<string> { "Quadrat", "Rechteck", "Kreis", "Oval" }, LearningTaskId = 3, Difficulty = "Vorschule" },
                new Questions { Text = "Welche Form ist wie ein Herz?", CorrectAnswer = "Herz", Options = new List<string> { "Kreis", "Herz", "Dreieck", "Rechteck" }, LearningTaskId = 3 },
                new Questions { Text = "Welche Form sieht man oft am Himmel in der Nacht?", CorrectAnswer = "Stern", Options = new List<string> { "Dreieck", "Stern", "Kreis", "Quadrat" }, LearningTaskId = 3, Difficulty = "Vorschule" },

                // 1. Klasse - Formen erkennen (LearningTask 3)
                new Questions { Text = "Welche Form hat vier rechte Winkel?", CorrectAnswer = "Rechteck", Options = new List<string> { "Quadrat", "Rechteck", "Dreieck", "Kreis" }, LearningTaskId = 3, Difficulty = "1 Klasse" },
                new Questions { Text = "Welche Form hat sechs Ecken?", CorrectAnswer = "Sechseck", Options = new List<string> { "Fünfeck", "Sechseck", "Achteck", "Quadrat" }, LearningTaskId = 3, Difficulty = "1 Klasse" },
                new Questions { Text = "Welche Form ist wie ein Stoppschild?", CorrectAnswer = "Achteck", Options = new List<string> { "Sechseck", "Achteck", "Quadrat", "Dreieck" }, LearningTaskId = 3, Difficulty = "1 Klasse" },
                new Questions { Text = "Welche Form kann rollen?", CorrectAnswer = "Kreis", Options = new List<string> { "Quadrat", "Rechteck", "Kreis", "Dreieck" }, LearningTaskId = 3, Difficulty = "1 Klasse" },
                new Questions { Text = "Welche Form hat gleich lange Seiten und vier Ecken?", CorrectAnswer = "Quadrat", Options = new List<string> { "Rechteck", "Kreis", "Quadrat", "Dreieck" }, LearningTaskId = 3, Difficulty = "1 Klasse" },
                new Questions { Text = "Welche Form ist wie ein Dach?", CorrectAnswer = "Dreieck", Options = new List<string> { "Kreis", "Quadrat", "Dreieck", "Oval" }, LearningTaskId = 3, Difficulty = "1 Klasse" },
                new Questions { Text = "Welche Form ist wie ein Fenster?", CorrectAnswer = "Quadrat", Options = new List<string> { "Quadrat", "Kreis", "Dreieck", "Herz" }, LearningTaskId = 3, Difficulty = "1 Klasse" },
                new Questions { Text = "Welche Form ist wie ein Ei?", CorrectAnswer = "Oval", Options = new List<string> { "Oval", "Kreis", "Quadrat", "Rechteck" }, LearningTaskId = 3, Difficulty = "1 Klasse" },
                new Questions { Text = "Welche Form hat acht Ecken?", CorrectAnswer = "Achteck", Options = new List<string> { "Sechseck", "Achteck", "Dreieck", "Fünfeck" }, LearningTaskId = 3, Difficulty = "1 Klasse" },
                new Questions { Text = "Welche Form hat vier Seiten, aber nicht alle gleich lang?", CorrectAnswer = "Rechteck", Options = new List<string> { "Quadrat", "Rechteck", "Trapez", "Kreis" }, LearningTaskId = 3, Difficulty = "1 Klasse" },

                // 2. Klasse - Formen erkennen (LearningTask 3)
                new Questions { Text = "Welche Form hat sieben Ecken?", CorrectAnswer = "Siebeneck", Options = new List<string> { "Sechseck", "Siebeneck", "Achteck", "Neuneck" }, LearningTaskId = 3, Difficulty = "2 Klasse" },
                new Questions { Text = "Welche Form sieht wie eine Pizza-Stück aus?", CorrectAnswer = "Kreissektor", Options = new List<string> { "Dreieck", "Kreissektor", "Rechteck", "Oval" }, LearningTaskId = 3, Difficulty = "2 Klasse" },
                new Questions { Text = "Welche Form ist wie eine Raute?", CorrectAnswer = "Rhombus", Options = new List<string> { "Rhombus", "Quadrat", "Rechteck", "Kreis" }, LearningTaskId = 3, Difficulty = "2 Klasse" },
                new Questions { Text = "Welche Form hat gleich lange Seiten, aber keine rechten Winkel?", CorrectAnswer = "Raute", Options = new List<string> { "Raute", "Quadrat", "Rechteck", "Dreieck" }, LearningTaskId = 3, Difficulty = "2 Klasse" },
                new Questions { Text = "Welche Form ist wie eine Scheibe?", CorrectAnswer = "Kreis", Options = new List<string> { "Kreis", "Quadrat", "Rechteck", "Oval" }, LearningTaskId = 3, Difficulty = "2 Klasse" },
                new Questions { Text = "Welche Form hat zehn Ecken?", CorrectAnswer = "Zehneck", Options = new List<string> { "Achteck", "Zehneck", "Siebeneck", "Fünfeck" }, LearningTaskId = 3, Difficulty = "2 Klasse" },
                new Questions { Text = "Welche Form ist wie ein Diamant?", CorrectAnswer = "Raute", Options = new List<string> { "Kreis", "Raute", "Dreieck", "Quadrat" }, LearningTaskId = 3, Difficulty = "2 Klasse" },
                new Questions { Text = "Welche Form hat 12 Ecken?", CorrectAnswer = "Zwölfeck", Options = new List<string> { "Zehneck", "Elfeck", "Zwölfeck", "Achteck" }, LearningTaskId = 3, Difficulty = "2 Klasse" },
                new Questions { Text = "Welche Form hat eine gekrümmte Linie und zwei Punkte?", CorrectAnswer = "Halbkreis", Options = new List<string> { "Halbkreis", "Kreis", "Dreieck", "Trapez" }, LearningTaskId = 3, Difficulty = "2 Klasse" },
                new Questions { Text = "Welche Form hat vier ungleiche Seiten?", CorrectAnswer = "Trapez", Options = new List<string> { "Quadrat", "Rechteck", "Trapez", "Raute" }, LearningTaskId = 3, Difficulty = "2 Klasse" },

               // 3. Klasse - Formen erkennen (LearningTask 3)
                new Questions { Text = "Wie viele Ecken hat ein Fünfeck?", CorrectAnswer = "5", Options = new List<string> { "4", "5", "6", "7" }, LearningTaskId = 3, Difficulty = "3 Klasse" },
                new Questions { Text = "Wie viele Seiten hat ein Sechseck?", CorrectAnswer = "6", Options = new List<string> { "5", "6", "7", "8" }, LearningTaskId = 3, Difficulty = "3 Klasse" },
                new Questions { Text = "Wie viele Ecken hat ein Achteck?", CorrectAnswer = "8", Options = new List<string> { "6", "7", "8", "9" }, LearningTaskId = 3, Difficulty = "3 Klasse" },
                new Questions { Text = "Wie viele Diagonalen hat ein Quadrat?", CorrectAnswer = "2", Options = new List<string> { "1", "2", "3", "4" }, LearningTaskId = 3, Difficulty = "3 Klasse" },
                new Questions { Text = "Wie nennt man ein Vieleck mit 10 Seiten?", CorrectAnswer = "Zehneck", Options = new List<string> { "Fünfeck", "Sechseck", "Zehneck", "Achteck" }, LearningTaskId = 3, Difficulty = "3 Klasse" },
                new Questions { Text = "Welche Form hat keine Ecken?", CorrectAnswer = "Kreis", Options = new List<string> { "Dreieck", "Quadrat", "Kreis", "Fünfeck" }, LearningTaskId = 3 },
                new Questions { Text = "Welche Form hat 6 gleich lange Seiten?", CorrectAnswer = "Sechseck", Options = new List<string> { "Fünfeck", "Sechseck", "Achteck", "Zehneck" }, LearningTaskId = 3, Difficulty = "3 Klasse" },
                new Questions { Text = "Welche Form hat 12 Ecken?", CorrectAnswer = "Zwölfeck", Options = new List<string> { "Achteck", "Zehneck", "Zwölfeck", "Neuneck" }, LearningTaskId = 3 },
                new Questions { Text = "Wie nennt man ein Viereck mit zwei parallelen Seiten?", CorrectAnswer = "Trapez", Options = new List<string> { "Quadrat", "Trapez", "Rechteck", "Parallelogramm" }, LearningTaskId = 3, Difficulty = "3 Klasse" },
                new Questions { Text = "Wie nennt man ein Viereck mit zwei Paaren paralleler Seiten?", CorrectAnswer = "Parallelogramm", Options = new List<string> { "Trapez", "Rhombus", "Quadrat", "Parallelogramm" }, LearningTaskId = 3, Difficulty = "3 Klasse" },

                // 4. Klasse - Formen erkennen (LearningTask 3)
                new Questions { Text = "Wie viele Flächen hat ein Würfel?", CorrectAnswer = "6", Options = new List<string> { "4", "6", "8", "12" }, LearningTaskId = 3, Difficulty = "4 Klasse" },
                new Questions { Text = "Wie viele Kanten hat ein Würfel?", CorrectAnswer = "12", Options = new List<string> { "6", "8", "10", "12" }, LearningTaskId = 3, Difficulty = "4 Klasse" },
                new Questions { Text = "Wie viele Ecken hat ein Würfel?", CorrectAnswer = "8", Options = new List<string> { "6", "8", "10", "12" }, LearningTaskId = 3, Difficulty = "4 Klasse" },
                new Questions { Text = "Wie viele Flächen hat eine Pyramide mit quadratischer Grundfläche?", CorrectAnswer = "5", Options = new List<string> { "4", "5", "6", "8" }, LearningTaskId = 3, Difficulty = "4 Klasse" },
                new Questions { Text = "Wie viele Flächen hat ein Quader?", CorrectAnswer = "6", Options = new List<string> { "4", "6", "8", "12" }, LearningTaskId = 3, Difficulty = "4 Klasse" },
                new Questions { Text = "Wie viele Kanten hat ein Quader?", CorrectAnswer = "12", Options = new List<string> { "8", "10", "12", "14" }, LearningTaskId = 3, Difficulty = "4 Klasse" },
                new Questions { Text = "Wie viele Flächen hat ein Zylinder?", CorrectAnswer = "3", Options = new List<string> { "2", "3", "4", "5" }, LearningTaskId = 3, Difficulty = "4 Klasse" },
                new Questions { Text = "Wie viele Flächen hat ein Kegel?", CorrectAnswer = "2", Options = new List<string> { "1", "2", "3", "4" }, LearningTaskId = 3, Difficulty = "4 Klasse" },
                new Questions { Text = "Wie viele Flächen hat eine Kugel?", CorrectAnswer = "1", Options = new List<string> { "0", "1", "2", "3" }, LearningTaskId = 3, Difficulty = "4 Klasse" },
                new Questions { Text = "Wie viele Flächen hat ein Prisma mit einer sechseckigen Grundfläche?", CorrectAnswer = "8", Options = new List<string> { "6", "7", "8", "9" }, LearningTaskId = 3, Difficulty = "4 Klasse" },

                // Vorschule – Alphabet lernen (LearningTask4)
                new Questions { Text = "Welcher Buchstabe kommt nach A?", CorrectAnswer = "B", Options = new List<string> { "C", "B", "D", "Z" }, LearningTaskId = 4, Difficulty = "Vorschule" },
                new Questions { Text = "Welcher Buchstabe kommt nach B?", CorrectAnswer = "C", Options = new List<string> { "A", "C", "D", "E" }, LearningTaskId = 4, Difficulty = "Vorschule" },
                new Questions { Text = "Welcher Buchstabe kommt nach C?", CorrectAnswer = "D", Options = new List<string> { "B", "E", "D", "A" }, LearningTaskId = 4, Difficulty = "Vorschule" },
                new Questions { Text = "Welcher Buchstabe kommt vor D?", CorrectAnswer = "C", Options = new List<string> { "B", "E", "C", "F" }, LearningTaskId = 4, Difficulty = "Vorschule" },
                new Questions { Text = "Welcher Buchstabe ist der erste im Alphabet?", CorrectAnswer = "A", Options = new List<string> { "Z", "B", "C", "A" }, LearningTaskId = 4, Difficulty = "Vorschule" },
                new Questions { Text = "Welcher Buchstabe ist der letzte im Alphabet?", CorrectAnswer = "Z", Options = new List<string> { "X", "Y", "Z", "A" }, LearningTaskId = 4, Difficulty = "Vorschule" },
                new Questions { Text = "Welcher Buchstabe kommt vor B?", CorrectAnswer = "A", Options = new List<string> { "C", "Z", "D", "A" }, LearningTaskId = 4, Difficulty = "Vorschule" },
                new Questions { Text = "Welcher Buchstabe kommt nach E?", CorrectAnswer = "F", Options = new List<string> { "D", "F", "G", "H" }, LearningTaskId = 4, Difficulty = "Vorschule" },
                new Questions { Text = "Welcher Buchstabe kommt nach F?", CorrectAnswer = "G", Options = new List<string> { "E", "H", "G", "I" }, LearningTaskId = 4, Difficulty = "Vorschule" },
                new Questions { Text = "Welcher Buchstabe kommt vor C?", CorrectAnswer = "B", Options = new List<string> { "D", "B", "E", "A" }, LearningTaskId = 4, Difficulty = "Vorschule" },

                // 1. Klasse – Alphabet lernen (LearningTask4)
                new Questions { Text = "Welcher Buchstabe kommt nach H?", CorrectAnswer = "I", Options = new List<string> { "G", "I", "J", "K" }, LearningTaskId = 4, Difficulty = "1 Klasse" },
                new Questions { Text = "Welcher Buchstabe kommt vor J?", CorrectAnswer = "I", Options = new List<string> { "K", "H", "I", "L" }, LearningTaskId = 4, Difficulty = "1 Klasse" },
                new Questions { Text = "Welcher Buchstabe steht zwischen K und M?", CorrectAnswer = "L", Options = new List<string> { "K", "L", "N", "O" }, LearningTaskId = 4, Difficulty = "1 Klasse" },
                new Questions { Text = "Welcher Buchstabe kommt nach L?", CorrectAnswer = "M", Options = new List<string> { "N", "M", "K", "O" }, LearningTaskId = 4, Difficulty = "1 Klasse" },
                new Questions { Text = "Welcher Buchstabe kommt nach O?", CorrectAnswer = "P", Options = new List<string> { "Q", "P", "R", "N" }, LearningTaskId = 4, Difficulty = "1 Klasse" },
                new Questions { Text = "Welcher Buchstabe kommt vor P?", CorrectAnswer = "O", Options = new List<string> { "N", "O", "M", "Q" }, LearningTaskId = 4, Difficulty = "1 Klasse" },
                new Questions { Text = "Welcher Buchstabe steht zwischen Q und S?", CorrectAnswer = "R", Options = new List<string> { "P", "R", "T", "S" }, LearningTaskId = 4, Difficulty = "1 Klasse" },
                new Questions { Text = "Welcher Buchstabe kommt nach S?", CorrectAnswer = "T", Options = new List<string> { "R", "T", "U", "V" }, LearningTaskId = 4, Difficulty = "1 Klasse" },
                new Questions { Text = "Welcher Buchstabe kommt vor T?", CorrectAnswer = "S", Options = new List<string> { "Q", "S", "R", "U" }, LearningTaskId = 4, Difficulty = "1 Klasse" },
                new Questions { Text = "Welcher Buchstabe kommt nach U?", CorrectAnswer = "V", Options = new List<string> { "W", "V", "X", "T" }, LearningTaskId = 4, Difficulty = "1 Klasse" },

                // 2. Klasse – Alphabet lernen (LearningTask4)
                new Questions { Text = "Welcher Buchstabe kommt nach V?", CorrectAnswer = "W", Options = new List<string> { "U", "W", "X", "Y" }, LearningTaskId = 4, Difficulty = "2 Klasse" },
                new Questions { Text = "Welcher Buchstabe kommt vor W?", CorrectAnswer = "V", Options = new List<string> { "U", "T", "V", "X" }, LearningTaskId = 4, Difficulty = "2 Klasse" },
                new Questions { Text = "Welcher Buchstabe steht zwischen X und Z?", CorrectAnswer = "Y", Options = new List<string> { "Z", "X", "Y", "W" }, LearningTaskId = 4, Difficulty = "2 Klasse" },
                new Questions { Text = "Welcher Buchstabe kommt nach M?", CorrectAnswer = "N", Options = new List<string> { "L", "O", "N", "P" }, LearningTaskId = 4, Difficulty = "2 Klasse" },
                new Questions { Text = "Welcher Buchstabe kommt nach Q?", CorrectAnswer = "R", Options = new List<string> { "P", "R", "S", "T" }, LearningTaskId = 4, Difficulty = "2 Klasse" },
                new Questions { Text = "Welcher Buchstabe kommt vor L?", CorrectAnswer = "K", Options = new List<string> { "J", "M", "K", "N" }, LearningTaskId = 4, Difficulty = "2 Klasse" },
                new Questions { Text = "Welcher Buchstabe kommt vor F?", CorrectAnswer = "E", Options = new List<string> { "G", "D", "E", "H" }, LearningTaskId = 4, Difficulty = "2 Klasse" },
                new Questions { Text = "Welcher Buchstabe kommt nach Z?", CorrectAnswer = "Keiner", Options = new List<string> { "A", "B", "Keiner", "Y" }, LearningTaskId = 4, Difficulty = "2 Klasse" },
                new Questions { Text = "Welcher Buchstabe kommt vor A?", CorrectAnswer = "Keiner", Options = new List<string> { "Z", "Keiner", "B", "Y" }, LearningTaskId = 4, Difficulty = "2 Klasse" },
                new Questions { Text = "Welcher Buchstabe kommt zwischen D und F?", CorrectAnswer = "E", Options = new List<string> { "G", "F", "E", "H" }, LearningTaskId = 4, Difficulty = "2 Klasse" },

                // 3. Klasse – Alphabet lernen (LearningTask4)
                new Questions { Text = "Welcher Buchstabe kommt an 5. Stelle im Alphabet?", CorrectAnswer = "E", Options = new List<string> { "C", "D", "E", "F" }, LearningTaskId = 4, Difficulty = "3 Klasse" },
                new Questions { Text = "Welcher Buchstabe steht an 10. Stelle?", CorrectAnswer = "J", Options = new List<string> { "I", "J", "K", "L" }, LearningTaskId = 4, Difficulty = "3 Klasse" },
                new Questions { Text = "Welcher Buchstabe steht an 15. Stelle?", CorrectAnswer = "O", Options = new List<string> { "N", "O", "P", "Q" }, LearningTaskId = 4, Difficulty = "3 Klasse" },
                new Questions { Text = "Welcher Buchstabe steht an 20. Stelle?", CorrectAnswer = "T", Options = new List<string> { "S", "T", "U", "V" }, LearningTaskId = 4, Difficulty = "3 Klasse" },
                new Questions { Text = "Welcher Buchstabe steht an 26. Stelle?", CorrectAnswer = "Z", Options = new List<string> { "X", "Y", "Z", "A" }, LearningTaskId = 4, Difficulty = "3 Klasse" },
                new Questions { Text = "Welcher Buchstabe steht an 3. Stelle?", CorrectAnswer = "C", Options = new List<string> { "B", "C", "D", "E" }, LearningTaskId = 4, Difficulty = "3 Klasse" },
                new Questions { Text = "Welcher Buchstabe steht an 12. Stelle?", CorrectAnswer = "L", Options = new List<string> { "K", "L", "M", "N" }, LearningTaskId = 4, Difficulty = "3 Klasse" },
                new Questions { Text = "Welcher Buchstabe steht an 8. Stelle?", CorrectAnswer = "H", Options = new List<string> { "G", "H", "I", "J" }, LearningTaskId = 4, Difficulty = "3 Klasse" },
                new Questions { Text = "Welcher Buchstabe steht an 18. Stelle?", CorrectAnswer = "R", Options = new List<string> { "Q", "R", "S", "T" }, LearningTaskId = 4, Difficulty = "3 Klasse" },
                new Questions { Text = "Welcher Buchstabe steht an 22. Stelle?", CorrectAnswer = "V", Options = new List<string> { "U", "V", "W", "X" }, LearningTaskId = 4, Difficulty = "3 Klasse" },

                // 4. Klasse – Alphabet lernen
                new Questions { Text = "Welcher Buchstabe steht 2 Stellen nach M?", CorrectAnswer = "O", Options = new List<string> { "N", "O", "P", "Q" }, LearningTaskId = 4, Difficulty = "4 Klasse" },
                new Questions { Text = "Welcher Buchstabe steht 3 Stellen vor P?", CorrectAnswer = "M", Options = new List<string> { "L", "M", "N", "O" }, LearningTaskId = 4, Difficulty = "4 Klasse" },
                new Questions { Text = "Welcher Buchstabe steht 4 Stellen nach J?", CorrectAnswer = "N", Options = new List<string> { "L", "M", "N", "O" }, LearningTaskId = 4, Difficulty = "4 Klasse" },
                new Questions { Text = "Welcher Buchstabe steht 5 Stellen vor Z?", CorrectAnswer = "U", Options = new List<string> { "V", "U", "T", "W" }, LearningTaskId = 4, Difficulty = "4 Klasse" },
                new Questions { Text = "Welcher Buchstabe steht 2 Stellen nach F?", CorrectAnswer = "H", Options = new List<string> { "G", "H", "I", "J" }, LearningTaskId = 4, Difficulty = "4 Klasse" },
                new Questions { Text = "Welcher Buchstabe steht 7 Stellen nach A?", CorrectAnswer = "H", Options = new List<string> { "F", "G", "H", "I" }, LearningTaskId = 4, Difficulty = "4 Klasse" },
                new Questions { Text = "Welcher Buchstabe steht 6 Stellen nach Q?", CorrectAnswer = "W", Options = new List<string> { "T", "U", "V", "W" }, LearningTaskId = 4, Difficulty = "4 Klasse" },
                new Questions { Text = "Welcher Buchstabe steht 4 Stellen vor K?", CorrectAnswer = "G", Options = new List<string> { "H", "F", "G", "E" }, LearningTaskId = 4, Difficulty = "4 Klasse" },
                new Questions { Text = "Welcher Buchstabe steht 5 Stellen nach L?", CorrectAnswer = "Q", Options = new List<string> { "N", "O", "P", "Q" }, LearningTaskId = 4, Difficulty = "4 Klasse" },
                new Questions { Text = "Welcher Buchstabe steht 8 Stellen nach D?", CorrectAnswer = "L", Options = new List<string> { "J", "K", "L", "M" }, LearningTaskId = 4, Difficulty = "4 Klasse" },



                // Vorschule – Buchstaben verbinden (LearningTask 5)
                new Questions { Text = "Welche Kombination ist richtig?", CorrectAnswer = "A-a", Options = new List<string> { "A-b", "A-c", "A-a", "A-d" }, LearningTaskId = 5, Difficulty = "Vorschule" },
                new Questions { Text = "Welche Kombination ist richtig?", CorrectAnswer = "B-b", Options = new List<string> { "B-c", "B-a", "B-b", "B-d" }, LearningTaskId = 5, Difficulty = "Vorschule" },
                new Questions { Text = "Welche Kombination ist richtig?", CorrectAnswer = "C-c", Options = new List<string> { "C-d", "C-b", "C-c", "C-a" }, LearningTaskId = 5, Difficulty = "Vorschule" },
                new Questions { Text = "Welche Kombination ist richtig?", CorrectAnswer = "D-d", Options = new List<string> { "D-d", "D-c", "D-b", "D-a" }, LearningTaskId = 5, Difficulty = "Vorschule" },
                new Questions { Text = "Welche Kombination ist richtig?", CorrectAnswer = "E-e", Options = new List<string> { "E-f", "E-g", "E-h", "E-e" }, LearningTaskId = 5, Difficulty = "Vorschule" },
                new Questions { Text = "Welche Kombination ist richtig?", CorrectAnswer = "F-f", Options = new List<string> { "F-f", "F-g", "F-e", "F-h" }, LearningTaskId = 5, Difficulty = "Vorschule" },
                new Questions { Text = "Welche Kombination ist richtig?", CorrectAnswer = "G-g", Options = new List<string> { "G-f", "G-h", "G-g", "G-i" }, LearningTaskId = 5, Difficulty = "Vorschule" },
                new Questions { Text = "Welche Kombination ist richtig?", CorrectAnswer = "H-h", Options = new List<string> { "H-h", "H-i", "H-g", "H-j" }, LearningTaskId = 5, Difficulty = "Vorschule" },
                new Questions { Text = "Welche Kombination ist richtig?", CorrectAnswer = "I-i", Options = new List<string> { "I-j", "I-i", "I-h", "I-k" }, LearningTaskId = 5, Difficulty = "Vorschule" },
                new Questions { Text = "Welche Kombination ist richtig?", CorrectAnswer = "J-j", Options = new List<string> { "J-i", "J-k", "J-j", "J-l" }, LearningTaskId = 5, Difficulty = "Vorschule" },
                
                // 1. Klasse – Buchstaben verbinden (LearningTask 5)
                new Questions { Text = "Welche Kombination passt zu 'Katze'?", CorrectAnswer = "K-k", Options = new List<string> { "K-c", "K-k", "K-t", "K-a" }, LearningTaskId = 5, Difficulty = "1 Klasse" },
                new Questions { Text = "Welche Kombination passt zu 'Hund'?", CorrectAnswer = "H-h", Options = new List<string> { "H-u", "H-n", "H-d", "H-h" }, LearningTaskId = 5, Difficulty = "1 Klasse" },
                new Questions { Text = "Welche Kombination passt zu 'Maus'?", CorrectAnswer = "M-m", Options = new List<string> { "M-a", "M-m", "M-u", "M-s" }, LearningTaskId = 5, Difficulty = "1 Klasse" },
                new Questions { Text = "Welche Kombination passt zu 'Buch'?", CorrectAnswer = "B-b", Options = new List<string> { "B-b", "B-u", "B-c", "B-h" }, LearningTaskId = 5, Difficulty = "1 Klasse" },
                new Questions { Text = "Welche Kombination passt zu 'Fisch'?", CorrectAnswer = "F-f", Options = new List<string> { "F-i", "F-s", "F-f", "F-c" }, LearningTaskId = 5, Difficulty = "1 Klasse" },
                new Questions { Text = "Welche Kombination passt zu 'Apfel'?", CorrectAnswer = "A-a", Options = new List<string> { "A-p", "A-a", "A-f", "A-l" }, LearningTaskId = 5, Difficulty = "1 Klasse" },
                new Questions { Text = "Welche Kombination passt zu 'Vogel'?", CorrectAnswer = "V-v", Options = new List<string> { "V-o", "V-v", "V-g", "V-l" }, LearningTaskId = 5, Difficulty = "1 Klasse" },
                new Questions { Text = "Welche Kombination passt zu 'Sonne'?", CorrectAnswer = "S-s", Options = new List<string> { "S-o", "S-s", "S-n", "S-e" }, LearningTaskId = 5, Difficulty = "1 Klasse" },
                new Questions { Text = "Welche Kombination passt zu 'Haus'?", CorrectAnswer = "H-h", Options = new List<string> { "H-h", "H-a", "H-u", "H-s" }, LearningTaskId = 5, Difficulty = "1 Klasse" },
                new Questions { Text = "Welche Kombination passt zu 'Blume'?", CorrectAnswer = "B-b", Options = new List<string> { "B-l", "B-b", "B-u", "B-e" }, LearningTaskId = 5, Difficulty = "1 Klasse" },

                // 2. Klasse – Buchstaben verbinden (LearningTask 5)
                new Questions { Text = "Welche Kombination passt zu 'Tiger'?", CorrectAnswer = "T-t", Options = new List<string> { "T-t", "T-i", "T-g", "T-e" }, LearningTaskId = 5, Difficulty = "2 Klasse" },
                new Questions { Text = "Welche Kombination passt zu 'Elefant'?", CorrectAnswer = "E-e", Options = new List<string> { "E-l", "E-e", "E-f", "E-a" }, LearningTaskId = 5, Difficulty = "2 Klasse" },
                new Questions { Text = "Welche Kombination passt zu 'Papagei'?", CorrectAnswer = "P-p", Options = new List<string> { "P-p", "P-a", "P-g", "P-e" }, LearningTaskId = 5, Difficulty = "2 Klasse" },
                new Questions { Text = "Welche Kombination passt zu 'Schule'?", CorrectAnswer = "S-s", Options = new List<string> { "S-s", "S-c", "S-h", "S-u" }, LearningTaskId = 5, Difficulty = "2 Klasse" },
                new Questions { Text = "Welche Kombination passt zu 'Zebra'?", CorrectAnswer = "Z-z", Options = new List<string> { "Z-e", "Z-b", "Z-z", "Z-r" }, LearningTaskId = 5, Difficulty = "2 Klasse" },
                new Questions { Text = "Welche Kombination passt zu 'König'?", CorrectAnswer = "K-k", Options = new List<string> { "K-k", "K-ö", "K-n", "K-i" }, LearningTaskId = 5, Difficulty = "2 Klasse" },
                new Questions { Text = "Welche Kombination passt zu 'Fuchs'?", CorrectAnswer = "F-f", Options = new List<string> { "F-u", "F-f", "F-c", "F-h" }, LearningTaskId = 5, Difficulty = "2 Klasse" },
                new Questions { Text = "Welche Kombination passt zu 'Biene'?", CorrectAnswer = "B-b", Options = new List<string> { "B-i", "B-b", "B-e", "B-n" }, LearningTaskId = 5, Difficulty = "2 Klasse" },
                new Questions { Text = "Welche Kombination passt zu 'Hase'?", CorrectAnswer = "H-h", Options = new List<string> { "H-a", "H-h", "H-s", "H-e" }, LearningTaskId = 5, Difficulty = "2 Klasse" },
                new Questions { Text = "Welche Kombination passt zu 'Apfelbaum'?", CorrectAnswer = "A-a", Options = new List<string> { "A-p", "A-a", "A-f", "A-b" }, LearningTaskId = 5, Difficulty = "2 Klasse" },

                // 3. Klasse – Buchstaben verbinden (LearningTask 5)
                new Questions { Text = "Welche Kombination passt zu 'Drachen'?", CorrectAnswer = "D-d", Options = new List<string> { "D-r", "D-d", "D-a", "D-c" }, LearningTaskId = 5, Difficulty = "3 Klasse" },
                new Questions { Text = "Welche Kombination passt zu 'Schmetterling'?", CorrectAnswer = "S-s", Options = new List<string> { "S-m", "S-s", "S-h", "S-e" }, LearningTaskId = 5, Difficulty = "3 Klasse" },
                new Questions { Text = "Welche Kombination passt zu 'Känguru'?", CorrectAnswer = "K-k", Options = new List<string> { "K-k", "K-ä", "K-n", "K-u" }, LearningTaskId = 5, Difficulty = "3 Klasse" },
                new Questions { Text = "Welche Kombination passt zu 'Burg'?", CorrectAnswer = "B-b", Options = new List<string> { "B-u", "B-b", "B-r", "B-g" }, LearningTaskId = 5, Difficulty = "3 Klasse" },
                new Questions { Text = "Welche Kombination passt zu 'Fahrrad'?", CorrectAnswer = "F-f", Options = new List<string> { "F-a", "F-f", "F-r", "F-h" }, LearningTaskId = 5, Difficulty = "3 Klasse" },
                new Questions { Text = "Welche Kombination passt zu 'Zoo'?", CorrectAnswer = "Z-z", Options = new List<string> { "Z-o", "Z-z", "Z-a", "Z-u" }, LearningTaskId = 5, Difficulty = "3 Klasse" },
                new Questions { Text = "Welche Kombination passt zu 'Apfelsaft'?", CorrectAnswer = "A-a", Options = new List<string> { "A-p", "A-a", "A-f", "A-s" }, LearningTaskId = 5, Difficulty = "3 Klasse" },
                new Questions { Text = "Welche Kombination passt zu 'Vogelhaus'?", CorrectAnswer = "V-v", Options = new List<string> { "V-o", "V-v", "V-h", "V-u" }, LearningTaskId = 5, Difficulty = "3 Klasse" },
                new Questions { Text = "Welche Kombination passt zu 'Tisch'?", CorrectAnswer = "T-t", Options = new List<string> { "T-i", "T-s", "T-t", "T-c" }, LearningTaskId = 5, Difficulty = "3 Klasse" },
                new Questions { Text = "Welche Kombination passt zu 'Lampe'?", CorrectAnswer = "L-l", Options = new List<string> { "L-a", "L-l", "L-m", "L-p" }, LearningTaskId = 5, Difficulty = "3 Klasse" },

                // 4. Klasse – Buchstaben verbinden (LearningTask 5)
                new Questions { Text = "Welche Kombination passt zu 'Computer'?", CorrectAnswer = "C-c", Options = new List<string> { "C-o", "C-c", "C-m", "C-p" }, LearningTaskId = 5, Difficulty = "4 Klasse" },
                new Questions { Text = "Welche Kombination passt zu 'Telefon'?", CorrectAnswer = "T-t", Options = new List<string> { "T-t", "T-e", "T-l", "T-o" }, LearningTaskId = 5, Difficulty = "4 Klasse" },
                new Questions { Text = "Welche Kombination passt zu 'Bibliothek'?", CorrectAnswer = "B-b", Options = new List<string> { "B-b", "B-i", "B-l", "B-t" }, LearningTaskId = 5, Difficulty = "4 Klasse" },
                new Questions { Text = "Welche Kombination passt zu 'Universität'?", CorrectAnswer = "U-u", Options = new List<string> { "U-u", "U-n", "U-i", "U-v" }, LearningTaskId = 5, Difficulty = "4 Klasse" },
                new Questions { Text = "Welche Kombination passt zu 'Schokolade'?", CorrectAnswer = "S-s", Options = new List<string> { "S-s", "S-c", "S-h", "S-o" }, LearningTaskId = 5, Difficulty = "4 Klasse" },
                new Questions { Text = "Welche Kombination passt zu 'Mathematik'?", CorrectAnswer = "M-m", Options = new List<string> { "M-m", "M-a", "M-t", "M-h" }, LearningTaskId = 5, Difficulty = "4 Klasse" },
                new Questions { Text = "Welche Kombination passt zu 'Physik'?", CorrectAnswer = "P-p", Options = new List<string> { "P-p", "P-h", "P-y", "P-s" }, LearningTaskId = 5, Difficulty = "4 Klasse" },
                new Questions { Text = "Welche Kombination passt zu 'Geographie'?", CorrectAnswer = "G-g", Options = new List<string> { "G-g", "G-e", "G-o", "G-r" }, LearningTaskId = 5, Difficulty = "4 Klasse" },
                new Questions { Text = "Welche Kombination passt zu 'Chemie'?", CorrectAnswer = "C-c", Options = new List<string> { "C-h", "C-e", "C-m", "C-c" }, LearningTaskId = 5, Difficulty = "4 Klasse" },
                new Questions { Text = "Welche Kombination passt zu 'Biologie'?", CorrectAnswer = "B-b", Options = new List<string> { "B-i", "B-o", "B-l", "B-b" }, LearningTaskId = 5, Difficulty = "4 Klasse" },

                // Neue Fragen für "Wörter buchstabieren" (LearningTask 6)
                //Vorschule
                new Questions { Text = "Buchstabiere das Wort 'Ball'.", CorrectAnswer = "BALL", ImageUrl = "assets/questImg/soccer.png", Options = new List<string> { "B", "O", "M", "L", "M", "K", "L", "A" }, LearningTaskId = 6, Difficulty = "Vorschule" },
                new Questions { Text = "Buchstabiere das Wort 'Auto'.", CorrectAnswer = "AUTO", ImageUrl = "assets/questImg/toy-car.png", Options = new List<string> { "A", "S", "T", "O", "E", "M", "U", "R" }, LearningTaskId = 6, Difficulty = "Vorschule" },
                new Questions { Text = "Buchstabiere das Wort 'Haus'.", CorrectAnswer = "HAUS", ImageUrl = "assets/questImg/house.png", Options = new List<string> { "K", "R", "U", "S", "E", "A", "M", "H" }, LearningTaskId = 6, Difficulty = "Vorschule" },
                new Questions { Text = "Buchstabiere das Wort 'Oma'.", CorrectAnswer = "OMA", ImageUrl = "assets/questImg/old-woman.png", Options = new List<string> { "O", "E", "A", "E", "S", "M", "K", "L" }, LearningTaskId = 6, Difficulty = "Vorschule" },
                new Questions { Text = "Buchstabiere das Wort 'Bär'.", CorrectAnswer = "BÄR", ImageUrl = "assets/questImg/bears.png", Options = new List<string> { "B", "N", "L", "A", "R", "E", "T", "Ä" }, LearningTaskId = 6, Difficulty = "Vorschule" },
                
                // Neue Fragen für "Wörter buchstabieren" (LearningTask 6)
                //1Klasse
               new Questions { Text = "Buchstabiere das Wort 'Katze'.", CorrectAnswer = "KATZE", ImageUrl = "assets/questImg/exotic-shorthair.png", Options = new List<string> { "P", "A", "E", "Z", "T", "S", "N", "K" }, LearningTaskId = 6, Difficulty = "1 Klasse" },
                new Questions { Text = "Buchstabiere das Wort 'Hund'.", CorrectAnswer = "HUND", ImageUrl = "assets/questImg/dog.png", Options = new List<string> { "H", "B", "N", "L", "T", "P", "U", "D" }, LearningTaskId = 6, Difficulty = "1 Klasse" },
                new Questions { Text = "Buchstabiere das Wort 'Apfel'.", CorrectAnswer = "APFEL", ImageUrl = "assets/questImg/apple.png", Options = new List<string> { "P", "A", "F", "O", "L", "S", "E", "M" }, LearningTaskId = 6, Difficulty = "1 Klasse" },
                new Questions { Text = "Buchstabiere das Wort 'Sonne'.", CorrectAnswer = "SONNE", ImageUrl = "assets/questImg/sun.png", Options = new List<string> { "N", "O", "S", "N", "U", "E", "R", "L" }, LearningTaskId = 6, Difficulty = "1 Klasse" },
                new Questions { Text = "Buchstabiere das Wort 'Fisch'.", CorrectAnswer = "FISCH", ImageUrl = "assets/questImg/clown-fish.png", Options = new List<string> { "S", "I", "F", "C", "H", "E", "T", "A" }, LearningTaskId = 6, Difficulty = "1 Klasse" },


                 // Neue Fragen für "Wörter buchstabieren" (LearningTask 6)
                //2Klasse
                new Questions { Text = "Buchstabiere das Wort 'Blume'.", CorrectAnswer = "BLUME", ImageUrl = "assets/questImg/flower.png", Options = new List<string> { "S", "N", "U", "M", "E", "A", "B", "L" }, LearningTaskId = 6, Difficulty = "2 Klasse" },
                new Questions { Text = "Buchstabiere das Wort 'Tiger'.", CorrectAnswer = "TIGER", ImageUrl = "assets/questImg/tiger.png", Options = new List<string> { "T", "P", "A", "E", "R", "G", "L", "I" }, LearningTaskId = 6, Difficulty = "2 Klasse" },
                new Questions { Text = "Buchstabiere das Wort 'Brot'.", CorrectAnswer = "BROT", ImageUrl = "assets/questImg/baguette.png", Options = new List<string> { "N", "L", "O", "T", "A", "S", "B", "R" }, LearningTaskId = 6, Difficulty = "2 Klasse" },
                new Questions { Text = "Buchstabiere das Wort 'Pferd'.", CorrectAnswer = "PFERD", ImageUrl = "assets/questImg/horse.png", Options = new List<string> { "F", "P", "E", "N", "D", "T", "S", "R" }, LearningTaskId = 6, Difficulty = "2 Klasse" },
                new Questions { Text = "Buchstabiere das Wort 'Banane'.", CorrectAnswer = "BANANE", ImageUrl = "assets/questImg/banana.png", Options = new List<string> { "N", "A", "B", "A", "N", "E", "S", "T" }, LearningTaskId = 6, Difficulty = "2 Klasse" },


                // Neue Fragen für "Wörter buchstabieren" (LearningTask 6)
                //3Klasse
                new Questions { Text = "Buchstabiere das Wort 'Schule'.", CorrectAnswer = "SCHULE", ImageUrl = "assets/questImg/elementary-school.png", Options = new List<string> { "M", "L", "H", "U", "C", "E", "R", "S" }, LearningTaskId = 6, Difficulty = "3 Klasse" },
                new Questions { Text = "Buchstabiere das Wort 'Wasser'.", CorrectAnswer = "WASSER", ImageUrl = "assets/questImg/drainage.png", Options = new List<string> { "S", "A", "S", "W", "E", "T", "R", "N" }, LearningTaskId = 6, Difficulty = "3 Klasse" },
                new Questions { Text = "Buchstabiere das Wort 'Zebra'.", CorrectAnswer = "ZEBRA", ImageUrl = "assets/questImg/zebra.png", Options = new List<string> { "Z", "L", "O", "R", "A", "B", "N", "E" }, LearningTaskId = 6, Difficulty = "3 Klasse" },
                new Questions { Text = "Buchstabiere das Wort 'Tisch'.", CorrectAnswer = "TISCH", ImageUrl = "assets/questImg/dining-table.png", Options = new List<string> { "I", "T", "H", "C", "S", "R", "E", "N" }, LearningTaskId = 6, Difficulty = "3 Klasse" },
                new Questions { Text = "Buchstabiere das Wort 'Wolke'.", CorrectAnswer = "WOLKE", ImageUrl = "assets/questImg/cloud.png", Options = new List<string> { "T", "O", "T", "K", "E", "S", "W", "L" }, LearningTaskId = 6, Difficulty = "3 Klasse" },

                // Neue Fragen für "Wörter buchstabieren" (LearningTask 6)
                //4Klasse
                new Questions { Text = "Buchstabiere das Wort 'Planet'.", CorrectAnswer = "PLANET", ImageUrl = "assets/questImg/saturn.png", Options = new List<string> { "T", "R", "A", "N", "E", "P", "S", "L" }, LearningTaskId = 6, Difficulty = "4 Klasse" },
                new Questions { Text = "Buchstabiere das Wort 'Computer'.", CorrectAnswer = "COMPUTER", ImageUrl = "assets/questImg/computers.png", Options = new List<string> { "T", "O", "M", "U", "R", "C", "E", "P" }, LearningTaskId = 6, Difficulty = "4 Klasse" },
                new Questions { Text = "Buchstabiere das Wort 'Brücke'.", CorrectAnswer = "BRÜCKE", ImageUrl = "assets/questImg/bridge.png", Options = new List<string> { "B", "Ü", "R", "K", "C", "E", "S", "T" }, LearningTaskId = 6, Difficulty = "4 Klasse" },
                new Questions { Text = "Buchstabiere das Wort 'Freund'.", CorrectAnswer = "FREUND", ImageUrl = "assets/questImg/friends.png", Options = new List<string> { "A", "S", "E", "U", "N", "D", "F", "R" }, LearningTaskId = 6, Difficulty = "4 Klasse" },
                new Questions { Text = "Buchstabiere das Wort 'Wald'.", CorrectAnswer = "WALD", ImageUrl = "assets/questImg/forest.png", Options = new List<string> { "O", "T", "L", "D", "S", "A", "R", "W" }, LearningTaskId = 6, Difficulty = "4 Klasse" },

                // Englisch Bilder (Task 7)
                //Vorschule
                new Questions { Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "Ball", ImageUrl = "assets/questImg/soccer.png", Options = new List<string> { "Ball", "Car", "Cat", "Sun" }, LearningTaskId = 7, Difficulty = "Vorschule" },
                new Questions { Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "Apple", ImageUrl = "assets/questImg/apple.png", Options = new List<string> { "Apple", "Banana", "Dog", "Hat" }, LearningTaskId = 7, Difficulty = "Vorschule" },
                new Questions { Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "Car", ImageUrl = "assets/questImg/toy-car.png", Options = new List<string> { "Car", "Bus", "Plane", "Tree" }, LearningTaskId = 7, Difficulty = "Vorschule" },
                new Questions { Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "Dog", ImageUrl = "assets/questImg/dog.png", Options = new List<string> { "Dog", "Cat", "Bird", "Fish" }, LearningTaskId = 7, Difficulty = "Vorschule" },
                new Questions { Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "Sun", ImageUrl = "assets/questImg/sun.png", Options = new List<string> { "Sun", "Moon", "Star", "Cloud" }, LearningTaskId = 7, Difficulty = "Vorschule" },

                //1Klasse
                new Questions { Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "Cat", ImageUrl = "assets/questImg/exotic-shorthair.png", Options = new List<string> { "Dog", "Cat", "Fox", "Pig" }, LearningTaskId = 7, Difficulty = "1 Klasse" },
                new Questions { Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "Fox", ImageUrl = "assets/questImg/fox.png", Options = new List<string> { "Fox", "Penguin", "Horse", "Cow" }, LearningTaskId = 7, Difficulty = "1 Klasse" },
                new Questions { Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "Bird", ImageUrl = "assets/questImg/bird.png", Options = new List<string> { "Bird", "Dog", "Cat", "Fish" }, LearningTaskId = 7, Difficulty = "1 Klasse" },
                new Questions { Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "Milk", ImageUrl = "assets/questImg/milk.png", Options = new List<string> { "Milk", "Juice", "Water", "Tea" }, LearningTaskId = 7, Difficulty = "1 Klasse" },
                new Questions { Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "Tree", ImageUrl = "assets/questImg/tree.png", Options = new List<string> { "Tree", "Flower", "Grass", "Rock" }, LearningTaskId = 7, Difficulty = "1 Klasse" },


                //2Klasse
                new Questions { Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "Horse", ImageUrl = "assets/questImg/horse.png", Options = new List<string> { "Horse", "Dog", "Cat", "Cow" }, LearningTaskId = 7, Difficulty = "2 Klasse" },
                new Questions { Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "Fish", ImageUrl = "assets/questImg/fish.png", Options = new List<string> { "Fish", "Bird", "Dog", "Fox" }, LearningTaskId = 7, Difficulty = "2 Klasse" },
                new Questions { Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "Banana", ImageUrl = "assets/questImg/banana.png", Options = new List<string> { "Banana", "Apple", "Grapes", "Orange" }, LearningTaskId = 7, Difficulty = "2 Klasse" },
                new Questions { Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "Duck", ImageUrl = "assets/questImg/ducks.png", Options = new List<string> { "Duck", "Chicken", "Goose", "Turkey" }, LearningTaskId = 7, Difficulty = "2 Klasse" },
                new Questions { Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "House", ImageUrl = "assets/questImg/house.png", Options = new List<string> { "House", "Car", "Tree", "Boat" }, LearningTaskId = 7, Difficulty = "2 Klasse" },
                
                //3Klasse
                new Questions { Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "Penguin", ImageUrl = "assets/questImg/penguin.png", Options = new List<string> { "Penguin", "Fox", "Dog", "Cat" }, LearningTaskId = 7, Difficulty = "3 Klasse" },
                new Questions { Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "Dolphin", ImageUrl = "assets/questImg/dolphin.png", Options = new List<string> { "Dolphin", "Shark", "Whale", "Fish" }, LearningTaskId = 7, Difficulty = "3 Klasse" },
                new Questions { Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "Carrot", ImageUrl = "assets/questImg/carrot.png", Options = new List<string> { "Carrot", "Potato", "Apple", "Tomato" }, LearningTaskId = 7, Difficulty = "3 Klasse" },
                new Questions { Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "Lion", ImageUrl = "assets/questImg/lion.png", Options = new List<string> { "Lion", "Tiger", "Bear", "Wolf" }, LearningTaskId = 7, Difficulty = "3 Klasse" },
                new Questions { Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "Plane", ImageUrl = "assets/questImg/airplane.png", Options = new List<string> { "Plane", "Car", "Train", "Boat" }, LearningTaskId = 7, Difficulty = "3 Klasse" },
                
                //4Klasse
                new Questions { Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "Elephant", ImageUrl = "assets/questImg/elephant.png", Options = new List<string> { "Elephant", "Lion", "Tiger", "Bear" }, LearningTaskId = 7, Difficulty = "4 Klasse" },
                new Questions { Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "Giraffe", ImageUrl = "assets/questImg/giraffe.png", Options = new List<string> { "Giraffe", "Zebra", "Horse", "Cow" }, LearningTaskId = 7, Difficulty = "4 Klasse" },
                new Questions { Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "Shark", ImageUrl = "assets/questImg/shark.png", Options = new List<string> { "Shark", "Dolphin", "Whale", "Fish" }, LearningTaskId = 7, Difficulty = "4 Klasse" },
                new Questions { Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "Tiger", ImageUrl = "assets/questImg/tiger.png", Options = new List<string> { "Tiger", "Lion", "Leopard", "Cat" }, LearningTaskId = 7, Difficulty = "4 Klasse" },
                new Questions { Text = "Wähle das korrekte Englische Wort", CorrectAnswer = "Frog", ImageUrl = "assets/questImg/frog.png", Options = new List<string> { "Frog", "Toad", "Lizard", "Snake" }, LearningTaskId = 7, Difficulty = "4 Klasse" },


                new Questions
                {
                    Text = "Wähle das korrekte Englische Wort",
                    CorrectAnswer = "Fox",
                    ImageUrl = "assets/images/fox.png",
                    Options = new List<string> { "Fox", "Dolphin", "Penguin", "Lizard" },
                    LearningTaskId = 7

                },
                
                // Vorschule – Englisch Vokabeln (LearningTask 8)

                // Category: Family
                new Questions { Text = "Onkel", CorrectAnswer = "uncle", Options = new List<string> { "uncle" }, LearningTaskId = 8, Category = "Family", Difficulty = "Vorschule" },
                new Questions { Text = "Vater", CorrectAnswer = "father", Options = new List<string> { "father" }, LearningTaskId = 8, Category = "Family", Difficulty = "Vorschule" },
                new Questions { Text = "Mutter", CorrectAnswer = "mother", Options = new List<string> { "mother" }, LearningTaskId = 8, Category = "Family", Difficulty = "Vorschule" },
                new Questions { Text = "Bruder", CorrectAnswer = "brother", Options = new List<string> { "brother" }, LearningTaskId = 8, Category = "Family", Difficulty = "Vorschule" },
                new Questions { Text = "Schwester", CorrectAnswer = "sister", Options = new List<string> { "sister" }, LearningTaskId = 8, Category = "Family", Difficulty = "Vorschule" },
                new Questions { Text = "Tante", CorrectAnswer = "aunt", Options = new List<string> { "aunt" }, LearningTaskId = 8, Category = "Family", Difficulty = "Vorschule" },
                new Questions { Text = "Großvater", CorrectAnswer = "grandpa", Options = new List<string> { "grandpa" }, LearningTaskId = 8, Category = "Family", Difficulty = "Vorschule" },
                new Questions { Text = "Großmutter", CorrectAnswer = "grandma", Options = new List<string> { "grandma" }, LearningTaskId = 8, Category = "Family", Difficulty = "Vorschule" },

                // Category: Animals
                new Questions { Text = "Hund", CorrectAnswer = "dog", Options = new List<string> { "dog" }, LearningTaskId = 8, Category = "Animal", Difficulty = "Vorschule" },
                new Questions { Text = "Katze", CorrectAnswer = "cat", Options = new List<string> { "cat" }, LearningTaskId = 8, Category = "Animal", Difficulty = "Vorschule" },
                new Questions { Text = "Hase", CorrectAnswer = "rabbit", Options = new List<string> { "rabbit" }, LearningTaskId = 8, Category = "Animal", Difficulty = "Vorschule" },
                new Questions { Text = "Maus", CorrectAnswer = "mouse", Options = new List<string> { "mouse" }, LearningTaskId = 8, Category = "Animal", Difficulty = "Vorschule" },
                new Questions { Text = "Vogel", CorrectAnswer = "bird", Options = new List<string> { "bird" }, LearningTaskId = 8, Category = "Animal", Difficulty = "Vorschule" },
                new Questions { Text = "Fisch", CorrectAnswer = "fish", Options = new List<string> { "fish" }, LearningTaskId = 8, Category = "Animal", Difficulty = "Vorschule" },
                new Questions { Text = "Schwein", CorrectAnswer = "pig", Options = new List<string> { "pig" }, LearningTaskId = 8, Category = "Animal", Difficulty = "Vorschule" },
                new Questions { Text = "Pferd", CorrectAnswer = "horse", Options = new List<string> { "horse" }, LearningTaskId = 8, Category = "Animal", Difficulty = "Vorschule" },

                // Category: Other
                new Questions { Text = "Haus", CorrectAnswer = "house", Options = new List<string> { "house" }, LearningTaskId = 8, Category = "Other", Difficulty = "Vorschule" },
                new Questions { Text = "Baum", CorrectAnswer = "tree", Options = new List<string> { "tree" }, LearningTaskId = 8, Category = "Other", Difficulty = "Vorschule" },
                new Questions { Text = "Auto", CorrectAnswer = "car", Options = new List<string> { "car" }, LearningTaskId = 8, Category = "Other", Difficulty = "Vorschule" },
                new Questions { Text = "Schiff", CorrectAnswer = "ship", Options = new List<string> { "ship" }, LearningTaskId = 8, Category = "Other", Difficulty = "Vorschule" },
                new Questions { Text = "Blume", CorrectAnswer = "flower", Options = new List<string> { "flower" }, LearningTaskId = 8, Category = "Other", Difficulty = "Vorschule" },
                new Questions { Text = "Tisch", CorrectAnswer = "table", Options = new List<string> { "table" }, LearningTaskId = 8, Category = "Other", Difficulty = "Vorschule" },
                new Questions { Text = "Stuhl", CorrectAnswer = "chair", Options = new List<string> { "chair" }, LearningTaskId = 8, Category = "Other", Difficulty = "Vorschule" },
                new Questions { Text = "Tür", CorrectAnswer = "door", Options = new List<string> { "door" }, LearningTaskId = 8, Category = "Other", Difficulty = "Vorschule" },

                // Category: Cloth
                new Questions { Text = "Hut", CorrectAnswer = "hat", Options = new List<string> { "hat" }, LearningTaskId = 8, Category = "Cloth", Difficulty = "Vorschule" },
                new Questions { Text = "Schuh", CorrectAnswer = "shoe", Options = new List<string> { "shoe" }, LearningTaskId = 8, Category = "Cloth", Difficulty = "Vorschule" },
                new Questions { Text = "Jacke", CorrectAnswer = "jacket", Options = new List<string> { "jacket" }, LearningTaskId = 8, Category = "Cloth", Difficulty = "Vorschule" },
                new Questions { Text = "Handschuhe", CorrectAnswer = "gloves", Options = new List<string> { "gloves" }, LearningTaskId = 8, Category = "Cloth", Difficulty = "Vorschule" },
                new Questions { Text = "Stiefel", CorrectAnswer = "boot", Options = new List<string> { "boot" }, LearningTaskId = 8, Category = "Cloth", Difficulty = "Vorschule" },
                new Questions { Text = "Brille", CorrectAnswer = "glasses", Options = new List<string> { "glasses" }, LearningTaskId = 8, Category = "Cloth", Difficulty = "Vorschule" },
                new Questions { Text = "Hose", CorrectAnswer = "pants", Options = new List<string> { "pants" }, LearningTaskId = 8, Category = "Cloth", Difficulty = "Vorschule" },
                new Questions { Text = "Pullover", CorrectAnswer = "sweater", Options = new List<string> { "sweater" }, LearningTaskId = 8, Category = "Cloth", Difficulty = "Vorschule" },

                // 1Klasse – Englisch Vokabeln (LearningTask 8)
                // Category: Family
                new Questions { Text = "Cousin", CorrectAnswer = "cousin", Options = new List<string> { "cousin" }, LearningTaskId = 8, Category = "Family", Difficulty = "1 Klasse" },
                new Questions { Text = "Großmutter", CorrectAnswer = "grandma", Options = new List<string> { "grandma" }, LearningTaskId = 8, Category = "Family", Difficulty = "1 Klasse" },
                new Questions { Text = "Großvater", CorrectAnswer = "grandpa", Options = new List<string> { "grandpa" }, LearningTaskId = 8, Category = "Family", Difficulty = "1 Klasse" },
                new Questions { Text = "Vater", CorrectAnswer = "father", Options = new List<string> { "father" }, LearningTaskId = 8, Category = "Family", Difficulty = "1 Klasse" },
                new Questions { Text = "Mutter", CorrectAnswer = "mother", Options = new List<string> { "mother" }, LearningTaskId = 8, Category = "Family", Difficulty = "1 Klasse" },
                new Questions { Text = "Onkel", CorrectAnswer = "uncle", Options = new List<string> { "uncle" }, LearningTaskId = 8, Category = "Family", Difficulty = "1 Klasse" },
                new Questions { Text = "Tante", CorrectAnswer = "aunt", Options = new List<string> { "aunt" }, LearningTaskId = 8, Category = "Family", Difficulty = "1 Klasse" },
                new Questions { Text = "Bruder", CorrectAnswer = "brother", Options = new List<string> { "brother" }, LearningTaskId = 8, Category = "Family", Difficulty = "1 Klasse" },

                // Category: Animals
                new Questions { Text = "Hund", CorrectAnswer = "dog", Options = new List<string> { "dog" }, LearningTaskId = 8, Category = "Animal", Difficulty = "1 Klasse" },
                new Questions { Text = "Katze", CorrectAnswer = "cat", Options = new List<string> { "cat" }, LearningTaskId = 8, Category = "Animal", Difficulty = "1 Klasse" },
                new Questions { Text = "Vogel", CorrectAnswer = "bird", Options = new List<string> { "bird" }, LearningTaskId = 8, Category = "Animal", Difficulty = "1 Klasse" },
                new Questions { Text = "Fisch", CorrectAnswer = "fish", Options = new List<string> { "fish" }, LearningTaskId = 8, Category = "Animal", Difficulty = "1 Klasse" },
                new Questions { Text = "Pferd", CorrectAnswer = "horse", Options = new List<string> { "horse" }, LearningTaskId = 8, Category = "Animal", Difficulty = "1 Klasse" },
                new Questions { Text = "Schwein", CorrectAnswer = "pig", Options = new List<string> { "pig" }, LearningTaskId = 8, Category = "Animal", Difficulty = "1 Klasse" },
                new Questions { Text = "Huhn", CorrectAnswer = "chicken", Options = new List<string> { "chicken" }, LearningTaskId = 8, Category = "Animal", Difficulty = "1 Klasse" },
                new Questions { Text = "Schaf", CorrectAnswer = "sheep", Options = new List<string> { "sheep" }, LearningTaskId = 8, Category = "Animal", Difficulty = "1 Klasse" },

                // Category: Other
                new Questions { Text = "Haus", CorrectAnswer = "house", Options = new List<string> { "house" }, LearningTaskId = 8, Category = "Other", Difficulty = "1 Klasse" },
                new Questions { Text = "Baum", CorrectAnswer = "tree", Options = new List<string> { "tree" }, LearningTaskId = 8, Category = "Other", Difficulty = "1 Klasse" },
                new Questions { Text = "Auto", CorrectAnswer = "car", Options = new List<string> { "car" }, LearningTaskId = 8, Category = "Other", Difficulty = "1 Klasse" },
                new Questions { Text = "Blume", CorrectAnswer = "flower", Options = new List<string> { "flower" }, LearningTaskId = 8, Category = "Other", Difficulty = "1 Klasse" },
                new Questions { Text = "Tisch", CorrectAnswer = "table", Options = new List<string> { "table" }, LearningTaskId = 8, Category = "Other", Difficulty = "1 Klasse" },
                new Questions { Text = "Stuhl", CorrectAnswer = "chair", Options = new List<string> { "chair" }, LearningTaskId = 8, Category = "Other", Difficulty = "1 Klasse" },
                new Questions { Text = "Tür", CorrectAnswer = "door", Options = new List<string> { "door" }, LearningTaskId = 8, Category = "Other", Difficulty = "1 Klasse" },
                new Questions { Text = "Fenster", CorrectAnswer = "window", Options = new List<string> { "window" }, LearningTaskId = 8, Category = "Other", Difficulty = "1 Klasse" },

                // Category: Cloth
                new Questions { Text = "Hut", CorrectAnswer = "hat", Options = new List<string> { "hat" }, LearningTaskId = 8, Category = "Cloth", Difficulty = "1 Klasse" },
                new Questions { Text = "Schuh", CorrectAnswer = "shoe", Options = new List<string> { "shoe" }, LearningTaskId = 8, Category = "Cloth", Difficulty = "1 Klasse" },
                new Questions { Text = "Jacke", CorrectAnswer = "jacket", Options = new List<string> { "jacket" }, LearningTaskId = 8, Category = "Cloth", Difficulty = "1 Klasse" },
                new Questions { Text = "Handschuhe", CorrectAnswer = "gloves", Options = new List<string> { "gloves" }, LearningTaskId = 8, Category = "Cloth", Difficulty = "1 Klasse" },
                new Questions { Text = "Stiefel", CorrectAnswer = "boot", Options = new List<string> { "boot" }, LearningTaskId = 8, Category = "Cloth", Difficulty = "1 Klasse" },
                new Questions { Text = "Brille", CorrectAnswer = "glasses", Options = new List<string> { "glasses" }, LearningTaskId = 8, Category = "Cloth", Difficulty = "1 Klasse" },
                new Questions { Text = "Hose", CorrectAnswer = "pants", Options = new List<string> { "pants" }, LearningTaskId = 8, Category = "Cloth", Difficulty = "1 Klasse" },
                new Questions { Text = "Pullover", CorrectAnswer = "sweater", Options = new List<string> { "sweater" }, LearningTaskId = 8, Category = "Cloth", Difficulty = "1 Klasse" },

                // 2Klasse – Englisch Vokabeln (LearningTask 8)
                // Category: Family
                new Questions { Text = "Onkel", CorrectAnswer = "uncle", Options = new List<string> { "uncle" }, LearningTaskId = 8, Category = "Family", Difficulty = "2 Klasse" },
                new Questions { Text = "Tante", CorrectAnswer = "aunt", Options = new List<string> { "aunt" }, LearningTaskId = 8, Category = "Family", Difficulty = "2 Klasse" },
                new Questions { Text = "Bruder", CorrectAnswer = "brother", Options = new List<string> { "brother" }, LearningTaskId = 8, Category = "Family", Difficulty = "2 Klasse" },
                new Questions { Text = "Schwester", CorrectAnswer = "sister", Options = new List<string> { "sister" }, LearningTaskId = 8, Category = "Family", Difficulty = "2 Klasse" },
                new Questions { Text = "Vater", CorrectAnswer = "father", Options = new List<string> { "father" }, LearningTaskId = 8, Category = "Family", Difficulty = "2 Klasse" },
                new Questions { Text = "Mutter", CorrectAnswer = "mother", Options = new List<string> { "mother" }, LearningTaskId = 8, Category = "Family", Difficulty = "2 Klasse" },
                new Questions { Text = "Großvater", CorrectAnswer = "grandpa", Options = new List<string> { "grandpa" }, LearningTaskId = 8, Category = "Family", Difficulty = "2 Klasse" },
                new Questions { Text = "Großmutter", CorrectAnswer = "grandma", Options = new List<string> { "grandma" }, LearningTaskId = 8, Category = "Family", Difficulty = "2 Klasse" },

                // Category: Animals
                new Questions { Text = "Hund", CorrectAnswer = "dog", Options = new List<string> { "dog" }, LearningTaskId = 8, Category = "Animal", Difficulty = "2 Klasse" },
                new Questions { Text = "Katze", CorrectAnswer = "cat", Options = new List<string> { "cat" }, LearningTaskId = 8, Category = "Animal", Difficulty = "2 Klasse" },
                new Questions { Text = "Hase", CorrectAnswer = "rabbit", Options = new List<string> { "rabbit" }, LearningTaskId = 8, Category = "Animal", Difficulty = "2 Klasse" },
                new Questions { Text = "Maus", CorrectAnswer = "mouse", Options = new List<string> { "mouse" }, LearningTaskId = 8, Category = "Animal", Difficulty = "2 Klasse" },
                new Questions { Text = "Vogel", CorrectAnswer = "bird", Options = new List<string> { "bird" }, LearningTaskId = 8, Category = "Animal", Difficulty = "2 Klasse" },
                new Questions { Text = "Fuchs", CorrectAnswer = "fox", Options = new List<string> { "fox" }, LearningTaskId = 8, Category = "Animal", Difficulty = "2 Klasse" },
                new Questions { Text = "Delfin", CorrectAnswer = "dolphin", Options = new List<string> { "dolphin" }, LearningTaskId = 8, Category = "Animal", Difficulty = "2 Klasse" },
                new Questions { Text = "Pinguin", CorrectAnswer = "penguin", Options = new List<string> { "penguin" }, LearningTaskId = 8, Category = "Animal", Difficulty = "2 Klasse" },

                // Category: Other
                new Questions { Text = "Haus", CorrectAnswer = "house", Options = new List<string> { "house" }, LearningTaskId = 8, Category = "Other", Difficulty = "2 Klasse" },
                new Questions { Text = "Baum", CorrectAnswer = "tree", Options = new List<string> { "tree" }, LearningTaskId = 8, Category = "Other", Difficulty = "2 Klasse" },
                new Questions { Text = "Auto", CorrectAnswer = "car", Options = new List<string> { "car" }, LearningTaskId = 8, Category = "Other", Difficulty = "2 Klasse" },
                new Questions { Text = "Tisch", CorrectAnswer = "table", Options = new List<string> { "table" }, LearningTaskId = 8, Category = "Other", Difficulty = "2 Klasse" },
                new Questions { Text = "Stuhl", CorrectAnswer = "chair", Options = new List<string> { "chair" }, LearningTaskId = 8, Category = "Other", Difficulty = "2 Klasse" },
                new Questions { Text = "Rakete", CorrectAnswer = "rocket", Options = new List<string> { "rocket" }, LearningTaskId = 8, Category = "Other", Difficulty = "2 Klasse" },
                new Questions { Text = "Vulkan", CorrectAnswer = "volcano", Options = new List<string> { "volcano" }, LearningTaskId = 8, Category = "Other" , Difficulty = "2 Klasse"},
                new Questions { Text = "Blume", CorrectAnswer = "flower", Options = new List<string> { "flower" }, LearningTaskId = 8, Category = "Other", Difficulty = "2 Klasse" },

                // Category: Cloth
                new Questions { Text = "Hut", CorrectAnswer = "hat", Options = new List<string> { "hat" }, LearningTaskId = 8, Category = "Cloth", Difficulty = "2 Klasse" },
                new Questions { Text = "Schuh", CorrectAnswer = "shoe", Options = new List<string> { "shoe" }, LearningTaskId = 8, Category = "Cloth", Difficulty = "2 Klasse" },
                new Questions { Text = "Jacke", CorrectAnswer = "jacket", Options = new List<string> { "jacket" }, LearningTaskId = 8, Category = "Cloth", Difficulty = "2 Klasse" },
                new Questions { Text = "Handschuhe", CorrectAnswer = "gloves", Options = new List<string> { "gloves" }, LearningTaskId = 8, Category = "Cloth", Difficulty = "2 Klasse" },
                new Questions { Text = "Stiefel", CorrectAnswer = "boot", Options = new List<string> { "boot" }, LearningTaskId = 8, Category = "Cloth", Difficulty = "2 Klasse" },
                new Questions { Text = "Brille", CorrectAnswer = "glasses", Options = new List<string> { "glasses" }, LearningTaskId = 8, Category = "Cloth", Difficulty = "2 Klasse" },
                new Questions { Text = "Pullover", CorrectAnswer = "sweater", Options = new List<string> { "sweater" }, LearningTaskId = 8, Category = "Cloth", Difficulty = "2 Klasse" },
                new Questions { Text = "Hose", CorrectAnswer = "pants", Options = new List<string> { "pants" }, LearningTaskId = 8, Category = "Cloth", Difficulty = "2 Klasse" },

                // 3Klasse – Englisch Vokabeln (LearningTask 8)
                // Category: Family
                new Questions { Text = "Cousin", CorrectAnswer = "cousin", Options = new List<string> { "cousin" }, LearningTaskId = 8, Category = "Family", Difficulty = "3 Klasse" },
                new Questions { Text = "Schwiegermutter", CorrectAnswer = "mother-in-law", Options = new List<string> { "mother-in-law" }, LearningTaskId = 8, Category = "Family", Difficulty = "3 Klasse" },
                new Questions { Text = "Schwiegervater", CorrectAnswer = "father-in-law", Options = new List<string> { "father-in-law" }, LearningTaskId = 8, Category = "Family", Difficulty = "3 Klasse" },
                new Questions { Text = "Neffe", CorrectAnswer = "nephew", Options = new List<string> { "nephew" }, LearningTaskId = 8, Category = "Family", Difficulty = "3 Klasse" },
                new Questions { Text = "Nichte", CorrectAnswer = "niece", Options = new List<string> { "niece" }, LearningTaskId = 8, Category = "Family", Difficulty = "3 Klasse" },
                new Questions { Text = "Geschwister", CorrectAnswer = "siblings", Options = new List<string> { "siblings" }, LearningTaskId = 8, Category = "Family", Difficulty = "3 Klasse" },
                new Questions { Text = "Ehepartner", CorrectAnswer = "spouse", Options = new List<string> { "spouse" }, LearningTaskId = 8, Category = "Family", Difficulty = "3 Klasse" },
                new Questions { Text = "Elternteil", CorrectAnswer = "parent", Options = new List<string> { "parent" }, LearningTaskId = 8, Category = "Family", Difficulty = "3 Klasse" },

                // Category: Animals
                new Questions { Text = "Fisch", CorrectAnswer = "fish", Options = new List<string> { "fish" }, LearningTaskId = 8, Category = "Animal", Difficulty = "3 Klasse" },
                new Questions { Text = "Känguru", CorrectAnswer = "kangaroo", Options = new List<string> { "kangaroo" }, LearningTaskId = 8, Category = "Animal", Difficulty = "3 Klasse" },
                new Questions { Text = "Elefant", CorrectAnswer = "elephant", Options = new List<string> { "elephant" }, LearningTaskId = 8, Category = "Animal", Difficulty = "3 Klasse" },
                new Questions { Text = "Pferd", CorrectAnswer = "horse", Options = new List<string> { "horse" }, LearningTaskId = 8, Category = "Animal", Difficulty = "3 Klasse" },
                new Questions { Text = "Frosch", CorrectAnswer = "frog", Options = new List<string> { "frog" }, LearningTaskId = 8, Category = "Animal", Difficulty = "3 Klasse" },
                new Questions { Text = "Schaf", CorrectAnswer = "sheep", Options = new List<string> { "sheep" }, LearningTaskId = 8, Category = "Animal", Difficulty = "3 Klasse" },
                new Questions { Text = "Ziege", CorrectAnswer = "goat", Options = new List<string> { "goat" }, LearningTaskId = 8, Category = "Animal", Difficulty = "3 Klasse" },
                new Questions { Text = "Schwein", CorrectAnswer = "pig", Options = new List<string> { "pig" }, LearningTaskId = 8, Category = "Animal", Difficulty = "3 Klasse" },

                // Category: Other
                new Questions { Text = "Brücke", CorrectAnswer = "bridge", Options = new List<string> { "bridge" }, LearningTaskId = 8, Category = "Other", Difficulty = "3 Klasse" },
                new Questions { Text = "Straße", CorrectAnswer = "street", Options = new List<string> { "street" }, LearningTaskId = 8, Category = "Other", Difficulty = "3 Klasse" },
                new Questions { Text = "Berg", CorrectAnswer = "mountain", Options = new List<string> { "mountain" }, LearningTaskId = 8, Category = "Other", Difficulty = "3 Klasse" },
                new Questions { Text = "Fluss", CorrectAnswer = "river", Options = new List<string> { "river" }, LearningTaskId = 8, Category = "Other", Difficulty = "3 Klasse" },
                new Questions { Text = "See", CorrectAnswer = "lake", Options = new List<string> { "lake" }, LearningTaskId = 8, Category = "Other", Difficulty = "3 Klasse" },
                new Questions { Text = "Insel", CorrectAnswer = "island", Options = new List<string> { "island" }, LearningTaskId = 8, Category = "Other", Difficulty = "3 Klasse" },
                new Questions { Text = "Burg", CorrectAnswer = "castle", Options = new List<string> { "castle" }, LearningTaskId = 8, Category = "Other", Difficulty = "3 Klasse" },
                new Questions { Text = "Leuchtturm", CorrectAnswer = "lighthouse", Options = new List<string> { "lighthouse" }, LearningTaskId = 8, Category = "Other", Difficulty = "3 Klasse" },

                // Category: Cloth
                new Questions { Text = "Schal", CorrectAnswer = "scarf", Options = new List<string> { "scarf" }, LearningTaskId = 8, Category = "Cloth", Difficulty = "3 Klasse" },
                new Questions { Text = "Kleid", CorrectAnswer = "dress", Options = new List<string> { "dress" }, LearningTaskId = 8, Category = "Cloth", Difficulty = "3 Klasse" },
                new Questions { Text = "Hemd", CorrectAnswer = "shirt", Options = new List<string> { "shirt" }, LearningTaskId = 8, Category = "Cloth", Difficulty = "3 Klasse" },
                new Questions { Text = "Socken", CorrectAnswer = "socks", Options = new List<string> { "socks" }, LearningTaskId = 8, Category = "Cloth", Difficulty = "3 Klasse" },
                new Questions { Text = "Schuhe", CorrectAnswer = "shoes", Options = new List<string> { "shoes" }, LearningTaskId = 8, Category = "Cloth", Difficulty = "3 Klasse" },
                new Questions { Text = "Mütze", CorrectAnswer = "cap", Options = new List<string> { "cap" }, LearningTaskId = 8, Category = "Cloth", Difficulty = "3 Klasse" },
                new Questions { Text = "Hose", CorrectAnswer = "pants", Options = new List<string> { "pants" }, LearningTaskId = 8, Category = "Cloth", Difficulty = "3 Klasse" },
                new Questions { Text = "Jacke", CorrectAnswer = "jacket", Options = new List<string> { "jacket" }, LearningTaskId = 8, Category = "Cloth", Difficulty = "3 Klasse" },

                // 4Klasse – Englisch Vokabeln (LearningTask 8)
                // Category: Family
                new Questions { Text = "Schwiegersohn", CorrectAnswer = "son-in-law", Options = new List<string> { "son-in-law" }, LearningTaskId = 8, Category = "Family", Difficulty = "4 Klasse" },
                new Questions { Text = "Schwiegertochter", CorrectAnswer = "daughter-in-law", Options = new List<string> { "daughter-in-law" }, LearningTaskId = 8, Category = "Family", Difficulty = "4 Klasse" },
                new Questions { Text = "Großcousin", CorrectAnswer = "first cousin once removed", Options = new List<string> { "first cousin once removed" }, LearningTaskId = 8, Category = "Family", Difficulty = "4 Klasse" },
                new Questions { Text = "Großnichte", CorrectAnswer = "grandniece", Options = new List<string> { "grandniece" }, LearningTaskId = 8, Category = "Family", Difficulty = "4 Klasse" },
                new Questions { Text = "Großneffe", CorrectAnswer = "grandnephew", Options = new List<string> { "grandnephew" }, LearningTaskId = 8, Category = "Family", Difficulty = "4 Klasse" },
                new Questions { Text = "Schwager", CorrectAnswer = "brother-in-law", Options = new List<string> { "brother-in-law" }, LearningTaskId = 8, Category = "Family", Difficulty = "4 Klasse" },
                new Questions { Text = "Schwägerin", CorrectAnswer = "sister-in-law", Options = new List<string> { "sister-in-law" }, LearningTaskId = 8, Category = "Family", Difficulty = "4 Klasse" },
                new Questions { Text = "Verlobter", CorrectAnswer = "fiancé", Options = new List<string> { "fiancé" }, LearningTaskId = 8, Category = "Family", Difficulty = "4 Klasse" },

                // Category: Animals
                new Questions { Text = "Eule", CorrectAnswer = "owl", Options = new List<string> { "owl" }, LearningTaskId = 8, Category = "Animal", Difficulty = "4 Klasse" },
                new Questions { Text = "Wal", CorrectAnswer = "whale", Options = new List<string> { "whale" }, LearningTaskId = 8, Category = "Animal", Difficulty = "4 Klasse" },
                new Questions { Text = "Pferd", CorrectAnswer = "horse", Options = new List<string> { "horse" }, LearningTaskId = 8, Category = "Animal", Difficulty = "4 Klasse" },
                new Questions { Text = "Bär", CorrectAnswer = "bear", Options = new List<string> { "bear" }, LearningTaskId = 8, Category = "Animal", Difficulty = "4 Klasse" },
                new Questions { Text = "Adler", CorrectAnswer = "eagle", Options = new List<string> { "eagle" }, LearningTaskId = 8, Category = "Animal", Difficulty = "4 Klasse" },
                new Questions { Text = "Krokodil", CorrectAnswer = "crocodile", Options = new List<string> { "crocodile" }, LearningTaskId = 8, Category = "Animal", Difficulty = "4 Klasse" },
                new Questions { Text = "Pferd", CorrectAnswer = "horse", Options = new List<string> { "horse" }, LearningTaskId = 8, Category = "Animal", Difficulty = "4 Klasse" },
                new Questions { Text = "Papagei", CorrectAnswer = "parrot", Options = new List<string> { "parrot" }, LearningTaskId = 8, Category = "Animal", Difficulty = "4 Klasse" },

                // Category: Other
                new Questions { Text = "Leiter", CorrectAnswer = "ladder", Options = new List<string> { "ladder" }, LearningTaskId = 8, Category = "Other", Difficulty = "4 Klasse" },
                new Questions { Text = "Tunnel", CorrectAnswer = "tunnel", Options = new List<string> { "tunnel" }, LearningTaskId = 8, Category = "Other", Difficulty = "4 Klasse" },
                new Questions { Text = "Brunnen", CorrectAnswer = "fountain", Options = new List<string> { "fountain" }, LearningTaskId = 8, Category = "Other", Difficulty = "4 Klasse" },
                new Questions { Text = "Wolke", CorrectAnswer = "cloud", Options = new List<string> { "cloud" }, LearningTaskId = 8, Category = "Other", Difficulty = "4 Klasse" },
                new Questions { Text = "Sonne", CorrectAnswer = "sun", Options = new List<string> { "sun" }, LearningTaskId = 8, Category = "Other", Difficulty = "4 Klasse" },
                new Questions { Text = "Mond", CorrectAnswer = "moon", Options = new List<string> { "moon" }, LearningTaskId = 8, Category = "Other", Difficulty = "4 Klasse" },
                new Questions { Text = "Stern", CorrectAnswer = "star", Options = new List<string> { "star" }, LearningTaskId = 8, Category = "Other", Difficulty = "4 Klasse" },
                new Questions { Text = "Straßenlaterne", CorrectAnswer = "streetlight", Options = new List<string> { "streetlight" }, LearningTaskId = 8, Category = "Other", Difficulty = "4 Klasse" },

                // Category: Cloth
                new Questions { Text = "Pullover", CorrectAnswer = "sweater", Options = new List<string> { "sweater" }, LearningTaskId = 8, Category = "Cloth", Difficulty = "4 Klasse" },
                new Questions { Text = "Bluse", CorrectAnswer = "blouse", Options = new List<string> { "blouse" }, LearningTaskId = 8, Category = "Cloth", Difficulty = "4 Klasse" },
                new Questions { Text = "Shorts", CorrectAnswer = "shorts", Options = new List<string> { "shorts" }, LearningTaskId = 8, Category = "Cloth", Difficulty = "4 Klasse" },
                new Questions { Text = "Schal", CorrectAnswer = "scarf", Options = new List<string> { "scarf" }, LearningTaskId = 8, Category = "Cloth", Difficulty = "4 Klasse" },
                new Questions { Text = "Handschuhe", CorrectAnswer = "gloves", Options = new List<string> { "gloves" }, LearningTaskId = 8, Category = "Cloth", Difficulty = "4 Klasse" },
                new Questions { Text = "Mantel", CorrectAnswer = "coat", Options = new List<string> { "coat" }, LearningTaskId = 8, Category = "Cloth", Difficulty = "4 Klasse" },
                new Questions { Text = "Sandalen", CorrectAnswer = "sandals", Options = new List<string> { "sandals" }, LearningTaskId = 8, Category = "Cloth", Difficulty = "4 Klasse" },
                new Questions { Text = "Hut", CorrectAnswer = "hat", Options = new List<string> { "hat" }, LearningTaskId = 8, Category = "Cloth", Difficulty = "4 Klasse" },


                // Vorschule – Finde das falsche Wort (LearningTask 9)
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "tree", Options = new List<string> { "rocket", "tree", "ship", "car" }, LearningTaskId = 9, Difficulty = "Vorschule" },
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "lava", Options = new List<string> { "lava", "green", "blue", "red" }, LearningTaskId = 9, Difficulty = "Vorschule" },
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "ship", Options = new List<string> { "hat", "boot", "ship", "shirt" }, LearningTaskId = 9, Difficulty = "Vorschule" },
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "glass", Options = new List<string> { "lion", "dog", "glass", "penguin" }, LearningTaskId = 9, Difficulty = "Vorschule" },
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "snake", Options = new List<string> { "door", "window", "wall", "snake" }, LearningTaskId = 9, Difficulty = "Vorschule" },
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "toy", Options = new List<string> { "mother", "sister", "uncle", "toy" }, LearningTaskId = 9, Difficulty = "Vorschule" },
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "food", Options = new List<string> { "golf", "football", "basketball", "food" }, LearningTaskId = 9, Difficulty = "Vorschule" },
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "cat", Options = new List<string> { "apple", "banana", "carrot", "cat" }, LearningTaskId = 9, Difficulty = "Vorschule" },
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "shoe", Options = new List<string> { "hat", "shirt", "shoe", "jacket" }, LearningTaskId = 9, Difficulty = "Vorschule" },
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "car", Options = new List<string> { "plane", "bike", "boat", "car" }, LearningTaskId = 9, Difficulty = "Vorschule" },

                // 1. Klasse – Finde das falsche Wort (LearningTask 9)
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "banana", Options = new List<string> { "apple", "orange", "banana", "grape" }, LearningTaskId = 9, Difficulty = "1 Klasse" },
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "car", Options = new List<string> { "bus", "bike", "train", "car" }, LearningTaskId = 9, Difficulty = "1 Klasse" },
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "dog", Options = new List<string> { "cat", "rabbit", "dog", "parrot" }, LearningTaskId = 9, Difficulty = "1 Klasse" },
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "hat", Options = new List<string> { "shirt", "pants", "hat", "jacket" }, LearningTaskId = 9, Difficulty = "1 Klasse" },
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "moon", Options = new List<string> { "sun", "star", "cloud", "moon" }, LearningTaskId = 9, Difficulty = "1 Klasse" },
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "shoe", Options = new List<string> { "sock", "glove", "shoe", "hat" }, LearningTaskId = 9, Difficulty = "1 Klasse" },
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "fish", Options = new List<string> { "dog", "cat", "fish", "bird" }, LearningTaskId = 9, Difficulty = "1 Klasse" },
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "tree", Options = new List<string> { "flower", "bush", "grass", "tree" }, LearningTaskId = 9, Difficulty = "1 Klasse" },
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "table", Options = new List<string> { "chair", "sofa", "table", "bed" }, LearningTaskId = 9, Difficulty = "1 Klasse" },
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "rabbit", Options = new List<string> { "lion", "tiger", "bear", "rabbit" }, LearningTaskId = 9, Difficulty = "1 Klasse" },

                // 2. Klasse – Finde das falsche Wort (LearningTask 9)
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "carrot", Options = new List<string> { "apple", "banana", "orange", "carrot" }, LearningTaskId = 9, Difficulty = "2 Klasse" },
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "train", Options = new List<string> { "dog", "cat", "rabbit", "train" }, LearningTaskId = 9, Difficulty = "2 Klasse" },
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "shirt", Options = new List<string> { "pants", "shoes", "shirt", "jacket" }, LearningTaskId = 9, Difficulty = "2 Klasse" },
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "whale", Options = new List<string> { "fish", "shark", "dolphin", "whale" }, LearningTaskId = 9, Difficulty = "2 Klasse" },
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "rose", Options = new List<string> { "oak", "pine", "rose", "maple" }, LearningTaskId = 9, Difficulty = "2 Klasse" },
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "plane", Options = new List<string> { "bus", "car", "bike", "plane" }, LearningTaskId = 9, Difficulty = "2 Klasse" },
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "milk", Options = new List<string> { "water", "juice", "milk", "tea" }, LearningTaskId = 9, Difficulty = "2 Klasse" },
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "spoon", Options = new List<string> { "knife", "fork", "plate", "spoon" }, LearningTaskId = 9, Difficulty = "2 Klasse" },
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "sun", Options = new List<string> { "Monday", "Tuesday", "Friday", "sun" }, LearningTaskId = 9, Difficulty = "2 Klasse" },
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "triangle", Options = new List<string> { "circle", "square", "triangle", "red" }, LearningTaskId = 9, Difficulty = "2 Klasse" },

                // 3. Klasse – Finde das falsche Wort (LearningTask 9)
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "computer", Options = new List<string> { "lion", "tiger", "bear", "computer" }, LearningTaskId = 9, Difficulty = "3 Klasse" },
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "giraffe", Options = new List<string> { "apple", "banana", "orange", "giraffe" }, LearningTaskId = 9, Difficulty = "3 Klasse" },
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "boots", Options = new List<string> { "shirt", "pants", "jacket", "boots" }, LearningTaskId = 9, Difficulty = "3 Klasse" },
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "rainbow", Options = new List<string> { "sun", "moon", "star", "rainbow" }, LearningTaskId = 9, Difficulty = "3 Klasse" },
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "hospital", Options = new List<string> { "school", "museum", "hospital", "library" }, LearningTaskId = 9, Difficulty = "3 Klasse" },
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "swimming", Options = new List<string> { "football", "basketball", "tennis", "swimming" }, LearningTaskId = 9, Difficulty = "3 Klasse" },
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "train", Options = new List<string> { "car", "bike", "bus", "train" }, LearningTaskId = 9, Difficulty = "3 Klasse" },
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "tree", Options = new List<string> { "dog", "cat", "rabbit", "tree" }, LearningTaskId = 9, Difficulty = "3 Klasse" },
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "chair", Options = new List<string> { "bed", "sofa", "table", "chair" }, LearningTaskId = 9, Difficulty = "3 Klasse" },
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "pencil", Options = new List<string> { "red", "blue", "green", "pencil" }, LearningTaskId = 9, Difficulty = "3 Klasse" },

                // 4. Klasse – Finde das falsche Wort (LearningTask 9)
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "cinema", Options = new List<string> { "mother", "father", "sister", "cinema" }, LearningTaskId = 9, Difficulty = "4 Klasse" },
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "parrot", Options = new List<string> { "shirt", "pants", "jacket", "parrot" }, LearningTaskId = 9, Difficulty = "4 Klasse" },
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "giraffe", Options = new List<string> { "car", "bus", "train", "giraffe" }, LearningTaskId = 9, Difficulty = "4 Klasse" },
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "electricity", Options = new List<string> { "Monday", "Tuesday", "Friday", "electricity" }, LearningTaskId = 9, Difficulty = "4 Klasse" },
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "raincoat", Options = new List<string> { "hat", "scarf", "gloves", "raincoat" }, LearningTaskId = 9, Difficulty = "4 Klasse" },
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "squirrel", Options = new List<string> { "rose", "tulip", "sunflower", "squirrel" }, LearningTaskId = 9, Difficulty = "4 Klasse" },
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "museum", Options = new List<string> { "mountain", "river", "forest", "museum" }, LearningTaskId = 9, Difficulty = "4 Klasse" },
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "football", Options = new List<string> { "math", "english", "science", "football" }, LearningTaskId = 9, Difficulty = "4 Klasse" },
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "whale", Options = new List<string> { "dog", "cat", "rabbit", "whale" }, LearningTaskId = 9, Difficulty = "4 Klasse" },
                new Questions { Text = "Wähle das Wort das nicht zu den anderen passt", CorrectAnswer = "smartphone", Options = new List<string> { "book", "newspaper", "magazine", "smartphone" }, LearningTaskId = 9, Difficulty = "4 Klasse" },

                
                

                // Fragen für "Fülle die Lücken" (LearningTask 10 - Vorschule)
                new Questions { Text = "Die Kuh macht ___.", CorrectAnswer = "Muh", Options = new List<string> { "Wuff", "Miau", "Muh" }, LearningTaskId = 10, Difficulty = "Vorschule" },
                new Questions { Text = "Der Himmel ist ___.", CorrectAnswer = "blau", Options = new List<string> { "rot", "blau", "grün" }, LearningTaskId = 10, Difficulty = "Vorschule" },
                new Questions { Text = "Die Banane ist ___.", CorrectAnswer = "gelb", Options = new List<string> { "gelb", "blau", "lila" }, LearningTaskId = 10, Difficulty = "Vorschule" },
                new Questions { Text = "Ein Auto fährt auf der ___.", CorrectAnswer = "Straße", Options = new List<string> { "Straße", "Wiese", "Wald" }, LearningTaskId = 10, Difficulty = "Vorschule" },
                new Questions { Text = "Der Ball ist ___.", CorrectAnswer = "rund", Options = new List<string> { "eckig", "rund", "flach" }, LearningTaskId = 10, Difficulty = "Vorschule" },
                new Questions { Text = "Ein Fisch schwimmt im ___.", CorrectAnswer = "Wasser", Options = new List<string> { "Sand", "Wasser", "Baum" }, LearningTaskId = 10, Difficulty = "Vorschule" },
                new Questions { Text = "Die Sonne ist ___.", CorrectAnswer = "warm", Options = new List<string> { "warm", "kalt", "nass" }, LearningTaskId = 10, Difficulty = "Vorschule" },
                new Questions { Text = "Die Katze macht ___.", CorrectAnswer = "Miau", Options = new List<string> { "Miau", "Wuff", "Muh" }, LearningTaskId = 10, Difficulty = "Vorschule" },
                new Questions { Text = "Wir essen ein ___.", CorrectAnswer = "Brot", Options = new List<string> { "Brot", "Auto", "Haus" }, LearningTaskId = 10, Difficulty = "Vorschule" },
                new Questions { Text = "Das Baby liegt im ___.", CorrectAnswer = "Bett", Options = new List<string> { "Bett", "Auto", "Baum" }, LearningTaskId = 10, Difficulty = "Vorschule" },

                // Fragen für "Fülle die Lücken" (LearningTask 10 - 1. Klasse)
                new Questions { Text = "Die Blume ist ___.", CorrectAnswer = "schön", Options = new List<string> { "schnell", "schön", "laut" }, LearningTaskId = 10, Difficulty = "1 Klasse" },
                new Questions { Text = "Der Vogel fliegt in den ___.", CorrectAnswer = "Himmel", Options = new List<string> { "Himmel", "Boden", "Baum" }, LearningTaskId = 10, Difficulty = "1 Klasse" },
                new Questions { Text = "Der Junge isst ein ___.", CorrectAnswer = "Eis", Options = new List<string> { "Auto", "Eis", "Ball" }, LearningTaskId = 10, Difficulty = "1 Klasse" },
                new Questions { Text = "Die Oma liest ein ___.", CorrectAnswer = "Buch", Options = new List<string> { "Buch", "Auto", "Apfel" }, LearningTaskId = 10, Difficulty = "1 Klasse" },
                new Questions { Text = "Das Haus hat ein ___.", CorrectAnswer = "Dach", Options = new List<string> { "Dach", "Bein", "Rad" }, LearningTaskId = 10, Difficulty = "1 Klasse" },
                new Questions { Text = "Der Hund frisst einen ___.", CorrectAnswer = "Knochen", Options = new List<string> { "Knochen", "Ball", "Schuh" }, LearningTaskId = 10, Difficulty = "1 Klasse" },
                new Questions { Text = "Die Ente schwimmt im ___.", CorrectAnswer = "Teich", Options = new List<string> { "Teich", "Haus", "Wald" }, LearningTaskId = 10, Difficulty = "1 Klasse" },
                new Questions { Text = "Das Feuer ist ___.", CorrectAnswer = "heiß", Options = new List<string> { "heiß", "kalt", "nass" }, LearningTaskId = 10, Difficulty = "1 Klasse" },
                new Questions { Text = "Der Lehrer schreibt an die ___.", CorrectAnswer = "Tafel", Options = new List<string> { "Tafel", "Wand", "Tür" }, LearningTaskId = 10, Difficulty = "1 Klasse" },
                new Questions { Text = "Die Uhr zeigt die ___.", CorrectAnswer = "Zeit", Options = new List<string> { "Zeit", "Hand", "Nase" }, LearningTaskId = 10, Difficulty = "1 Klasse" },

                // Fragen für "Fülle die Lücken" (LearningTask 10 - 2. Klasse)
                new Questions { Text = "Der Regen fällt vom ___.", CorrectAnswer = "Himmel", Options = new List<string> { "Himmel", "Boden", "Baum" }, LearningTaskId = 10, Difficulty = "2 Klasse" },
                new Questions { Text = "Die Maus läuft ins ___.", CorrectAnswer = "Loch", Options = new List<string> { "Loch", "Haus", "Glas" }, LearningTaskId = 10, Difficulty = "2 Klasse" },
                new Questions { Text = "Im Winter fällt der ___.", CorrectAnswer = "Schnee", Options = new List<string> { "Regen", "Schnee", "Wind" }, LearningTaskId = 10, Difficulty = "2 Klasse" },
                new Questions { Text = "Der Schüler schreibt mit einem ___.", CorrectAnswer = "Stift", Options = new List<string> { "Stift", "Rad", "Glas" }, LearningTaskId = 10, Difficulty = "2 Klasse" },
                new Questions { Text = "Am Abend wird es ___.", CorrectAnswer = "dunkel", Options = new List<string> { "hell", "dunkel", "laut" }, LearningTaskId = 10, Difficulty = "2 Klasse" },
                new Questions { Text = "Der Tisch hat vier ___.", CorrectAnswer = "Beine", Options = new List<string> { "Beine", "Arme", "Türen" }, LearningTaskId = 10, Difficulty = "2 Klasse" },
                new Questions { Text = "Ein Jahr hat zwölf ___.", CorrectAnswer = "Monate", Options = new List<string> { "Monate", "Tage", "Stunden" }, LearningTaskId = 10, Difficulty = "2 Klasse" },
                new Questions { Text = "Ein Apfelbaum hat viele ___.", CorrectAnswer = "Äpfel", Options = new List<string> { "Äpfel", "Autos", "Tiere" }, LearningTaskId = 10, Difficulty = "2 Klasse" },
                new Questions { Text = "Die Kerze gibt ___.", CorrectAnswer = "Licht", Options = new List<string> { "Licht", "Luft", "Lärm" }, LearningTaskId = 10, Difficulty = "2 Klasse" },
                new Questions { Text = "Der König trägt eine ___.", CorrectAnswer = "Krone", Options = new List<string> { "Krone", "Jacke", "Uhr" }, LearningTaskId = 10, Difficulty = "2 Klasse" },

                // Fragen für "Fülle die Lücken" (LearningTask 10 - 3. Klasse)
                new Questions { Text = "Die Erde dreht sich um die ___.", CorrectAnswer = "Sonne", Options = new List<string> { "Sonne", "Mond", "Sterne" }, LearningTaskId = 10, Difficulty = "3 Klasse" },
                new Questions { Text = "Ein Rechteck hat vier ___.", CorrectAnswer = "Ecken", Options = new List<string> { "Ecken", "Seiten", "Räder" }, LearningTaskId = 10, Difficulty = "3 Klasse" },
                new Questions { Text = "Der Fluss fließt ins ___.", CorrectAnswer = "Meer", Options = new List<string> { "Meer", "Gebirge", "Haus" }, LearningTaskId = 10, Difficulty = "3 Klasse" },
                new Questions { Text = "Ein Jahr hat 365 ___.", CorrectAnswer = "Tage", Options = new List<string> { "Tage", "Stunden", "Wochen" }, LearningTaskId = 10, Difficulty = "3 Klasse" },
                new Questions { Text = "Das Gegenteil von heiß ist ___.", CorrectAnswer = "kalt", Options = new List<string> { "warm", "kalt", "schön" }, LearningTaskId = 10, Difficulty = "3 Klasse" },
                new Questions { Text = "Die Hauptstadt von Deutschland ist ___.", CorrectAnswer = "Berlin", Options = new List<string> { "Berlin", "Hamburg", "Köln" }, LearningTaskId = 10, Difficulty = "3 Klasse" },
                new Questions { Text = "Die Ampel zeigt ___.", CorrectAnswer = "Farben", Options = new List<string> { "Farben", "Lichter", "Töne" }, LearningTaskId = 10, Difficulty = "3 Klasse" },
                new Questions { Text = "Das Gegenteil von laut ist ___.", CorrectAnswer = "leise", Options = new List<string> { "leise", "ruhig", "schnell" }, LearningTaskId = 10, Difficulty = "3 Klasse" },
                new Questions { Text = "Der Bäcker backt ___.", CorrectAnswer = "Brot", Options = new List<string> { "Brot", "Milch", "Tische" }, LearningTaskId = 10, Difficulty = "3 Klasse" },
                new Questions { Text = "Das Thermometer misst die ___.", CorrectAnswer = "Temperatur", Options = new List<string> { "Temperatur", "Zeit", "Höhe" }, LearningTaskId = 10, Difficulty = "3 Klasse" },

                // Fragen für "Fülle die Lücken" (LearningTask 10 - 4. Klasse)
                new Questions { Text = "Die Hauptstadt von Frankreich ist ___.", CorrectAnswer = "Paris", Options = new List<string> { "Paris", "Rom", "Berlin" }, LearningTaskId = 10, Difficulty = "4 Klasse" },
                new Questions { Text = "Wasser gefriert bei ___.", CorrectAnswer = "0 Grad", Options = new List<string> { "0 Grad", "10 Grad", "100 Grad" }, LearningTaskId = 10, Difficulty = "4 Klasse" },
                new Questions { Text = "Das größte Land der Erde ist ___.", CorrectAnswer = "Russland", Options = new List<string> { "Russland", "China", "Kanada" }, LearningTaskId = 10, Difficulty = "4 Klasse" },
                new Questions { Text = "Das Gegenteil von schwer ist ___.", CorrectAnswer = "leicht", Options = new List<string> { "leicht", "dick", "stark" }, LearningTaskId = 10, Difficulty = "4 Klasse" },
                new Questions { Text = "Die Donau ist ein ___.", CorrectAnswer = "Fluss", Options = new List<string> { "Fluss", "Gebirge", "See" }, LearningTaskId = 10, Difficulty = "4 Klasse" },
                new Questions { Text = "Die Erde ist ein ___.", CorrectAnswer = "Planet", Options = new List<string> { "Planet", "Stern", "Mond" }, LearningTaskId = 10, Difficulty = "4 Klasse" },
                new Questions { Text = "Das Gegenteil von früh ist ___.", CorrectAnswer = "spät", Options = new List<string> { "spät", "schnell", "klein" }, LearningTaskId = 10, Difficulty = "4 Klasse" },
                new Questions { Text = "Die Alpen sind ein ___.", CorrectAnswer = "Gebirge", Options = new List<string> { "Gebirge", "Fluss", "Meer" }, LearningTaskId = 10, Difficulty = "4 Klasse" },
                new Questions { Text = "Das Blut ist ___.", CorrectAnswer = "rot", Options = new List<string> { "rot", "blau", "grün" }, LearningTaskId = 10, Difficulty = "4 Klasse" },
                new Questions { Text = "Der höchste Berg der Erde ist der ___.", CorrectAnswer = "Mount Everest", Options = new List<string> { "Mount Everest", "Zugspitze", "Matterhorn" }, LearningTaskId = 10, Difficulty = "4 Klasse" },
                
                
                // Fragen für "Fülle die Form" (Learning Task 12)
                new Questions
                {
                    Text = "Wähle die passende Form für das Muster",
                    CorrectAnswer = "assets/questImg/form.heart.png",
                    ImageUrl = "assets/questImg/form.heartQuestion.png",
                    Options = new List<string> { "assets/questImg/form.label.png", "assets/questImg/form.heart.png", "assets/questImg/form.tear.png", "assets/questImg/form.new-moon.png" },
                    LearningTaskId = 12,
                    Difficulty = "Vorschule" 
                },
                new Questions
                {
                    Text = "Wähle die passende Form für das Muster",
                    CorrectAnswer = "assets/questImg/form.diamond.png",
                    ImageUrl = "assets/questImg/form.diamondQuestion.png",
                    Options = new List<string> { "assets/questImg/form.hexagon.png", "assets/questImg/form.bleach.png", "assets/questImg/form.diamond.png", "assets/questImg/form.black-square.png" },
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
                new Avatar
                {
                    Name = "Glückliche Ente",
                    ImageUrl = "assets/images/duck.png",
                    Description = "Eine fröhliche Ente, die gerne schwimmt.",
                    UnlockStarRequirement = 2
                },
                new Avatar
                {
                    Name = "Abenteuerlicher Bär",
                    ImageUrl = "assets/images/bear.png",
                    Description = "Ein mutiger Bär, der gerne im Wald spielt.",
                    UnlockStarRequirement = 4
                },
                new Avatar
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