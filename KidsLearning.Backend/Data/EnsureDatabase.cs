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
                DateOfBirth = DateTime.Now.AddYears(-7) // Beispielalter
            };
            context.Children.Add(child);
            context.SaveChanges();
        }

        // --- LearningTasks ---
        if (!context.LearningTasks.Any())
        {
            var tasks = new List<LearningTask>
            {
                new LearningTask
                {
                    Title = "Zahlen finden",
                    Description = "Finde die fehlenden Zahlen in der Reihe.",
                    Subject = "Mathe-Abenteuer"
                },
                new LearningTask
                {
                    Title = "Addition bis 10",
                    Description = "Addiere zwei einstellige Zahlen.",
                    Subject = "Mathe-Abenteuer"
                },
                new LearningTask
                {
                    Title = "Formen erkennen",
                    Description = "Erkenne verschiedene geometrische Formen.",
                    Subject = "Mathe-Abenteuer"
                },
                new LearningTask
                {
                    Title = "Alphabet lernen",
                    Description = "Nenne alle Buchstaben des Alphabets in der richtigen Reihenfolge.",
                    Subject = "Buchstaben-land"
                },
                new LearningTask
                {
                    Title = "Buchstaben verbinden",
                    Description = "Verbinde die Großbuchstaben mit den Kleinbuchstaben.",
                    Subject = "Buchstaben-land"
                },
                new LearningTask
                {
                    Title = "Wörter buchstabieren",
                    Description = "Buchstabiere einfache Wörter wie 'Hund' und 'Katze'.",
                    Subject = "Buchstaben-land"
                },
                new LearningTask {
                    Title = "Englische Bilder",
                    Description = "Wähle das korrekte Englische Wort zum angezeigten Bild",
                    Subject = "Englisch"

                },
                new LearningTask {
                    Title = "Deutsch/Englisch verbinden",
                    Description = "Finde zu jedem deutschen Wort das richtige Englische Wort",
                    Subject = "Englisch"

                },
                new LearningTask {
                    Title = "Finde den Imposter",
                    Description = "Wähle das Englische Wort, das nicht zu den anderen passt",
                    Subject = "Englisch"

                },
                new LearningTask
                {
                    Title = "Fülle die Lücken",
                    Description = "Schreibe die richtigen Wörter in die Lücken",
                    Subject = "Buchstaben-land"
                },
                new LearningTask
                {
                    Title = "Zahlenkombinationen",
                    Description = "Bist du bereit dir Zahlenkombinationen zu merken?",
                    Subject = "Logik-Dschungel"

                },
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
                new Questions { Text = "Welche Zahl fehlt: 1, 2, ?, 4", CorrectAnswer = "3", Options = new List<string>{ "2", "3", "4", "5" }, LearningTaskId = 1 },
                new Questions { Text = "Welche Zahl fehlt: ?, 2, 3", CorrectAnswer = "1", Options = new List<string>{ "0", "1", "2", "3" }, LearningTaskId = 1 },
                new Questions { Text = "Welche Zahl fehlt: 3, ?, 5", CorrectAnswer = "4", Options = new List<string>{ "2", "3", "4", "5" }, LearningTaskId = 1 },
                new Questions { Text = "Welche Zahl fehlt: 4, ?, 6", CorrectAnswer = "5", Options = new List<string>{ "3", "4", "5", "7" }, LearningTaskId = 1 },
                new Questions { Text = "Welche Zahl fehlt: 6, 7, ?, 9", CorrectAnswer = "8", Options = new List<string>{ "6", "7", "8", "10" }, LearningTaskId = 1 },
                new Questions { Text = "Welche Zahl fehlt: ?, 1, 2", CorrectAnswer = "0", Options = new List<string>{ "0", "1", "2", "3" }, LearningTaskId = 1 },
                new Questions { Text = "Welche Zahl fehlt: 7, 8, ?, 10", CorrectAnswer = "9", Options = new List<string>{ "7", "8", "9", "11" }, LearningTaskId = 1 },
                new Questions { Text = "Welche Zahl fehlt: 2, 3, ?, 5", CorrectAnswer = "4", Options = new List<string>{ "3", "4", "5", "6" }, LearningTaskId = 1 },
                new Questions { Text = "Welche Zahl fehlt: 9, ?, 11", CorrectAnswer = "10", Options = new List<string>{ "9", "10", "11", "12" }, LearningTaskId = 1 },
                new Questions { Text = "Welche Zahl fehlt: ?, 5, 6", CorrectAnswer = "4", Options = new List<string>{ "3", "4", "5", "6" }, LearningTaskId = 1 },

                //1Klasse
                new Questions { Text = "Welche Zahl fehlt: 2, 4, ?, 8", CorrectAnswer = "6", Options = new List<string>{ "5", "6", "7", "8" }, LearningTaskId = 2 },
                new Questions { Text = "Welche Zahl fehlt: 10, ?, 12, 13", CorrectAnswer = "11", Options = new List<string>{ "10", "11", "12", "14" }, LearningTaskId = 2 },
                new Questions { Text = "Welche Zahl fehlt: 1, 3, ?, 7", CorrectAnswer = "5", Options = new List<string>{ "4", "5", "6", "7" }, LearningTaskId = 2 },
                new Questions { Text = "Welche Zahl fehlt: ?, 15, 16", CorrectAnswer = "14", Options = new List<string>{ "13", "14", "15", "16" }, LearningTaskId = 2 },
                new Questions { Text = "Welche Zahl fehlt: 20, 21, ?, 23", CorrectAnswer = "22", Options = new List<string>{ "21", "22", "23", "24" }, LearningTaskId = 2 },
                new Questions { Text = "Welche Zahl fehlt: 5, ?, 15, 20", CorrectAnswer = "10", Options = new List<string>{ "8", "9", "10", "12" }, LearningTaskId = 2 },
                new Questions { Text = "Welche Zahl fehlt: ?, 30, 40", CorrectAnswer = "20", Options = new List<string>{ "10", "20", "25", "30" }, LearningTaskId = 2 },
                new Questions { Text = "Welche Zahl fehlt: 12, 13, ?, 15", CorrectAnswer = "14", Options = new List<string>{ "12", "13", "14", "15" }, LearningTaskId = 2 },
                new Questions { Text = "Welche Zahl fehlt: 50, ?, 52", CorrectAnswer = "51", Options = new List<string>{ "50", "51", "52", "53" }, LearningTaskId = 2 },
                new Questions { Text = "Welche Zahl fehlt: 9, 12, ?, 18", CorrectAnswer = "15", Options = new List<string>{ "14", "15", "16", "17" }, LearningTaskId = 2 },

                //2Klasse
                new Questions { Text = "Welche Zahl fehlt: 100, 200, ?, 400", CorrectAnswer = "300", Options = new List<string>{ "100", "200", "300", "400" }, LearningTaskId = 3 },
                new Questions { Text = "Welche Zahl fehlt: 2, 4, 6, ?", CorrectAnswer = "8", Options = new List<string>{ "6", "7", "8", "9" }, LearningTaskId = 3 },
                new Questions { Text = "Welche Zahl fehlt: 5, 10, ?, 20", CorrectAnswer = "15", Options = new List<string>{ "12", "15", "18", "20" }, LearningTaskId = 3 },
                new Questions { Text = "Welche Zahl fehlt: 25, 30, ?, 40", CorrectAnswer = "35", Options = new List<string>{ "32", "33", "34", "35" }, LearningTaskId = 3 },
                new Questions { Text = "Welche Zahl fehlt: ?, 70, 80", CorrectAnswer = "60", Options = new List<string>{ "50", "55", "60", "65" }, LearningTaskId = 3 },
                new Questions { Text = "Welche Zahl fehlt: 90, ?, 110", CorrectAnswer = "100", Options = new List<string>{ "95", "98", "100", "105" }, LearningTaskId = 3 },
                new Questions { Text = "Welche Zahl fehlt: 7, 14, ?, 28", CorrectAnswer = "21", Options = new List<string>{ "20", "21", "22", "23" }, LearningTaskId = 3 },
                new Questions { Text = "Welche Zahl fehlt: 11, ?, 33", CorrectAnswer = "22", Options = new List<string>{ "20", "21", "22", "23" }, LearningTaskId = 3 },
                new Questions { Text = "Welche Zahl fehlt: 40, 50, ?, 70", CorrectAnswer = "60", Options = new List<string>{ "55", "58", "60", "65" }, LearningTaskId = 3 },
                new Questions { Text = "Welche Zahl fehlt: 13, ?, 39", CorrectAnswer = "26", Options = new List<string>{ "24", "25", "26", "27" }, LearningTaskId = 3 },

                //3Klasse
                new Questions { Text = "Welche Zahl fehlt: 100, 105, ?, 115", CorrectAnswer = "110", Options = new List<string>{ "108", "109", "110", "111" }, LearningTaskId = 4 },
                new Questions { Text = "Welche Zahl fehlt: 200, 220, ?, 260", CorrectAnswer = "240", Options = new List<string>{ "230", "235", "240", "245" }, LearningTaskId = 4 },
                new Questions { Text = "Welche Zahl fehlt: 300, ?, 500", CorrectAnswer = "400", Options = new List<string>{ "350", "375", "400", "450" }, LearningTaskId = 4 },
                new Questions { Text = "Welche Zahl fehlt: 50, ?, 80", CorrectAnswer = "65", Options = new List<string>{ "60", "62", "65", "70" }, LearningTaskId = 4 },
                new Questions { Text = "Welche Zahl fehlt: 1000, 900, ?, 700", CorrectAnswer = "800", Options = new List<string>{ "750", "800", "850", "900" }, LearningTaskId = 4 },
                new Questions { Text = "Welche Zahl fehlt: ?, 3000, 4000", CorrectAnswer = "2000", Options = new List<string>{ "1000", "2000", "2500", "3000" }, LearningTaskId = 4 },
                new Questions { Text = "Welche Zahl fehlt: 15, 30, ?, 60", CorrectAnswer = "45", Options = new List<string>{ "40", "42", "45", "48" }, LearningTaskId = 4 },
                new Questions { Text = "Welche Zahl fehlt: 21, ?, 63", CorrectAnswer = "42", Options = new List<string>{ "40", "41", "42", "43" }, LearningTaskId = 4 },
                new Questions { Text = "Welche Zahl fehlt: 120, 130, ?, 150", CorrectAnswer = "140", Options = new List<string>{ "135", "138", "140", "142" }, LearningTaskId = 4 },
                new Questions { Text = "Welche Zahl fehlt: 500, ?, 700", CorrectAnswer = "600", Options = new List<string>{ "550", "580", "600", "620" }, LearningTaskId = 4 },

                //4Klasse
                new Questions { Text = "Welche Zahl fehlt: 5, 10, ?, 20, 25", CorrectAnswer = "15", Options = new List<string>{ "12", "13", "14", "15" }, LearningTaskId = 5 },
                new Questions { Text = "Welche Zahl fehlt: 100, 200, ?, 400, 500", CorrectAnswer = "300", Options = new List<string>{ "250", "275", "300", "325" }, LearningTaskId = 5 },
                new Questions { Text = "Welche Zahl fehlt: 2, 4, 8, ?, 32", CorrectAnswer = "16", Options = new List<string>{ "12", "14", "16", "18" }, LearningTaskId = 5 },
                new Questions { Text = "Welche Zahl fehlt: 3, 9, ?, 81", CorrectAnswer = "27", Options = new List<string>{ "18", "21", "27", "36" }, LearningTaskId = 5 },
                new Questions { Text = "Welche Zahl fehlt: 1, 4, 9, ?, 25", CorrectAnswer = "16", Options = new List<string>{ "12", "14", "16", "18" }, LearningTaskId = 5 },
                new Questions { Text = "Welche Zahl fehlt: ?, 64, 81", CorrectAnswer = "49", Options = new List<string>{ "36", "42", "49", "56" }, LearningTaskId = 5 },
                new Questions { Text = "Welche Zahl fehlt: 12, 24, ?, 96", CorrectAnswer = "48", Options = new List<string>{ "36", "42", "48", "54" }, LearningTaskId = 5 },
                new Questions { Text = "Welche Zahl fehlt: 144, ?, 576", CorrectAnswer = "288", Options = new List<string>{ "200", "240", "288", "320" }, LearningTaskId = 5 },
                new Questions { Text = "Welche Zahl fehlt: 7, 14, ?, 56", CorrectAnswer = "28", Options = new List<string>{ "21", "25", "28", "30" }, LearningTaskId = 5 },
                new Questions { Text = "Welche Zahl fehlt: 1000, ?, 4000", CorrectAnswer = "2000", Options = new List<string>{ "1500", "2000", "2500", "3000" }, LearningTaskId = 5 },

                // Fragen für "Addition bis 10" (LearningTask 2)
                new Questions
                {
                    Text = "Was ist 3 + 4?",
                    CorrectAnswer = "7",
                    Options = new List<string> { "5", "6", "7", "8" },
                    LearningTaskId = 2
                },
                new Questions
                {
                    Text = "Was ist 5 + 2?",
                    CorrectAnswer = "7",
                    Options = new List<string> { "6", "7", "8", "9" },
                    LearningTaskId = 2
                },

                // Fragen für "Formen erkennen" (LearningTask 3)
                new Questions
                {
                    Text = "Welche Form hat vier gleich lange Seiten?",
                    CorrectAnswer = "Quadrat",
                    Options = new List<string> { "Dreieck", "Kreis", "Quadrat", "Rechteck" },
                    LearningTaskId = 3
                },
                new Questions
                {
                    Text = "Welche Form hat drei Ecken?",
                    CorrectAnswer = "Dreieck",
                    Options = new List<string> { "Quadrat", "Dreieck", "Kreis", "Stern" },
                    LearningTaskId = 3
                },

                // Neue Fragen für "Alphabet lernen" (LearningTask 4)
                new Questions
                {
                    Text = "Welcher Buchstabe kommt nach dem B?",
                    CorrectAnswer = "C",
                    Options = new List<string> { "A", "B", "C", "D" },
                    LearningTaskId = 4
                },
                new Questions
                {
                    Text = "Welcher Buchstabe kommt vor dem G?",
                    CorrectAnswer = "F",
                    Options = new List<string> { "D", "E", "F", "H" },
                    LearningTaskId = 4
                },

                // Neue Fragen für "Buchstaben verbinden" (LearningTask 5)
                new Questions
                {
                    Text = "Welche Kombination ist richtig?",
                    CorrectAnswer = "A-a",
                    Options = new List<string> { "A-b", "A-c", "A-a", "A-d" },
                    LearningTaskId = 5
                },
                new Questions
                {
                    Text = "Welche Kombination ist richtig?",
                    CorrectAnswer = "E-e",
                    Options = new List<string> { "E-f", "E-g", "E-h", "E-e" },
                    LearningTaskId = 5
                },

                // Neue Fragen für "Wörter buchstabieren" (LearningTask 6)
                new Questions
                {
                    Text = "Buchstabiere das Wort 'Katze'.",
                    CorrectAnswer = "KATZE",
                    ImageUrl = "assets/images/bengal.png",
                    Options = new List<string> { "K", "A", "T", "Z", "E", "S", "N", "P" },
                    LearningTaskId = 6
                },
                new Questions
                {
                    Text = "Buchstabiere das Wort 'Hund'.",
                    CorrectAnswer = "HUND",
                    ImageUrl = "assets/images/golden-retriever.png",
                    Options = new List<string> { "H", "U", "N", "D", "T", "P", "B", "L" },
                    LearningTaskId = 6
                },
                
                // Englisch Bilder (Task 7)
                new Questions
                {
                    Text = "Wähle das korrekte Englische Wort",
                    CorrectAnswer = "Dog",
                    ImageUrl = "assets/images/golden-retriever.png",
                    Options = new List<string> { "Dog", "Horse", "Pig", "Whale" },
                    LearningTaskId = 7
                },
                new Questions
                {
                    Text = "Wähle das korrekte Englische Wort",
                    CorrectAnswer = "Cat",
                    ImageUrl = "assets/images/bengal.png",
                    Options = new List<string> { "Meerkat", "Guineapig", "Scarecrow", "Cat" },
                    LearningTaskId = 7
                },
                new Questions
                {
                    Text = "Wähle das korrekte Englische Wort",
                    CorrectAnswer = "Fox",
                    ImageUrl = "assets/images/fox.png",
                    Options = new List<string> { "Fox", "Dolphin", "Penguin", "Lizard" },
                    LearningTaskId = 7

                },
                
                // Englisch Vokabeln / Wörter Verbinden (Task 8)
                
                    // Category Family
                    new Questions
                    {
                        Text = "Onkel",
                        CorrectAnswer = "uncle",
                        Options = new List<string> { "uncle" },
                        LearningTaskId = 8,
                        Category = "Family"
                    },
                    new Questions
                    {
                        Text = "Vater",
                        CorrectAnswer = "father",
                        Options = new List<string> { "father" },
                        LearningTaskId = 8,
                        Category = "Family"
                    },
                    new Questions
                    {
                        Text = "Mutter",
                        CorrectAnswer = "mother",
                        Options = new List<string> { "mother" },
                        LearningTaskId = 8,
                        Category = "Family"
                    },
                    new Questions
                    {
                        Text = "Schwester",
                        CorrectAnswer = "sister",
                        Options = new List<string> { "sister" },
                        LearningTaskId = 8,
                        Category = "Family"
                    },
                    new Questions
                    {
                        Text = "Bruder",
                        CorrectAnswer = "brother",
                        Options = new List<string> { "brother" },
                        LearningTaskId = 8,
                        Category = "Family"
                    },
                    new Questions
                    {
                        Text = "Tante",
                        CorrectAnswer = "aunt",
                        Options = new List<string> { "aunt" },
                        LearningTaskId = 8,
                        Category = "Family"
                    },
                    new Questions
                    {
                        Text = "Großvater",
                        CorrectAnswer = "grandpa",
                        Options = new List<string> { "grandpa" },
                        LearningTaskId = 8,
                        Category = "Family"
                    },
                    new Questions
                    {
                        Text = "Großmutter",
                        CorrectAnswer = "grandma",
                        Options = new List<string> { "grandpa" },
                        LearningTaskId = 8,
                        Category = "Family"
                    },
                    
                    //  Category Animals
                    new Questions
                    {
                        Text = "Eichhörnchen",
                        CorrectAnswer = "squirrel",
                        Options = new List<string> { "squirrel" },
                        LearningTaskId = 8,
                        Category = "Animal"
                    },
                    new Questions
                    {
                        Text = "Eidechse",
                        CorrectAnswer = "lizard",
                        Options = new List<string> { "lizard" },
                        LearningTaskId = 8,
                        Category = "Animal"
                        
                    },
                    new Questions
                    {
                        Text = "Hund",
                        CorrectAnswer = "dog",
                        Options = new List<string> { "dog" },
                        LearningTaskId = 8,
                        Category = "Animal"
                        
                    },
                    new Questions
                    {
                        Text = "Katze",
                        CorrectAnswer = "cat",
                        Options = new List<string> { "cat" },
                        LearningTaskId = 8,
                        Category = "Animal"
                        
                    },
                    new Questions
                    {
                        Text = "Hase",
                        CorrectAnswer = "rabbit",
                        Options = new List<string> { "rabbit" },
                        LearningTaskId = 8,
                        Category = "Animal"
                        
                    },
                    new Questions
                    {
                        Text = "Maus",
                        CorrectAnswer = "mouse",
                        Options = new List<string> { "mouse" },
                        LearningTaskId = 8,
                        Category = "Animal"
                        
                    },
                    new Questions
                    {
                        Text = "Schlange",
                        CorrectAnswer = "snake",
                        Options = new List<string> { "snake" },
                        LearningTaskId = 8,
                        Category = "Animal"
                        
                    },
                    new Questions
                    {
                        Text = "Wurm",
                        CorrectAnswer = "worm",
                        Options = new List<string> { "worm" },
                        LearningTaskId = 8,
                        Category = "Animal"
                        
                    },
                    new Questions
                    {
                        Text = "Delfin",
                        CorrectAnswer = "dolphin",
                        Options = new List<string> { "dolphin" },
                        LearningTaskId = 8,
                        Category = "Animal"
                        
                    },
                    new Questions
                    {
                        Text = "Fuchs",
                        CorrectAnswer = "fox",
                        Options = new List<string> { "fox" },
                        LearningTaskId = 8,
                        Category = "Animal"
                        
                    },
                    new Questions
                    {
                        Text = "Pinguin",
                        CorrectAnswer = "penguin",
                        Options = new List<string> { "penguin" },
                        LearningTaskId = 8,
                        Category = "Animal"
                        
                    },
                    new Questions
                    {
                        Text = "Vogel",
                        CorrectAnswer = "bird",
                        Options = new List<string> { "bird" },
                        LearningTaskId = 8,
                        Category = "Animal"
                        
                    },
                    new Questions
                    {
                        Text = "Meerschweinchen",
                        CorrectAnswer = "meerkat",
                        Options = new List<string> { "meerkat" },
                        LearningTaskId = 8,
                        Category = "Animal"
                        
                    },
                    new Questions
                    {
                        Text = "Ratte",
                        CorrectAnswer = "rat",
                        Options = new List<string> { "rat" },
                        LearningTaskId = 8,
                        Category = "Animal"
                        
                    },
                    new Questions
                    {
                        Text = "Löwe",
                        CorrectAnswer = "lion",
                        Options = new List<string> { "lion" },
                        LearningTaskId = 8,
                        Category = "Animal"
                        
                    },
                    new Questions
                    {
                        Text = "Tiger",
                        CorrectAnswer = "tiger",
                        Options = new List<string> { "tiger" },
                        LearningTaskId = 8,
                        Category = "Animal"
                        
                    },
                    
                    // Category Other
                    new Questions
                    {
                        Text = "Rakete",
                        CorrectAnswer = "rocket",
                        Options = new List<string> { "rocket" },
                        LearningTaskId = 8,
                        Category = "Other"
                    },
                    new Questions
                    {
                        Text = "Haus",
                        CorrectAnswer = "house",
                        Options = new List<string> { "house" },
                        LearningTaskId = 8,
                        Category = "Other"
                    },
                    new Questions
                    {
                        Text = "Baum",
                        CorrectAnswer = "tree",
                        Options = new List<string> { "tree" },
                        LearningTaskId = 8,
                        Category = "Other"
                    },
                    new Questions
                    {
                        Text = "Auto",
                        CorrectAnswer = "car",
                        Options = new List<string> { "car" },
                        LearningTaskId = 8,
                        Category = "Other"
                    },
                    new Questions
                    {
                        Text = "Schiff",
                        CorrectAnswer = "ship",
                        Options = new List<string> { "ship" },
                        LearningTaskId = 8,
                        Category = "Other"
                    },
                    new Questions
                    {
                        Text = "Blume",
                        CorrectAnswer = "flower",
                        Options = new List<string> { "flower" },
                        LearningTaskId = 8,
                        Category = "Other"
                    },
                    new Questions
                    {
                        Text = "Tisch",
                        CorrectAnswer = "table",
                        Options = new List<string> { "table" },
                        LearningTaskId = 8,
                        Category = "Other"
                    },
                    new Questions
                    {
                        Text = "Vulkan",
                        CorrectAnswer = "volcano",
                        Options = new List<string> { "volcano" },
                        LearningTaskId = 8,
                        Category = "Other"
                    },
                    
                    // category cloth
                    new Questions
                    {
                        Text = "Hut",
                        CorrectAnswer = "hat",
                        Options = new List<string> { "hat" },
                        LearningTaskId = 8,
                        Category = "Cloth"
                    },
                    new Questions
                    {
                        Text = "Stiefel",
                        CorrectAnswer = "boot",
                        Options = new List<string> { "boot" },
                        LearningTaskId = 8,
                        Category = "Cloth"
                    },
                    new Questions
                    {
                        Text = "Handschuhe",
                        CorrectAnswer = "gloves",
                        Options = new List<string> { "gloves" },
                        LearningTaskId = 8,
                        Category = "Cloth"
                    },
                    new Questions
                    {
                        Text = "Gürtel",
                        CorrectAnswer = "waist",
                        Options = new List<string> { "waist" },
                        LearningTaskId = 8,
                        Category = "Cloth"
                    },
                    new Questions
                    {
                        Text = "Armband",
                        CorrectAnswer = "wrist",
                        Options = new List<string> { "wrist" },
                        LearningTaskId = 8,
                        Category = "Cloth"
                    },
                    new Questions
                    {
                        Text = "Jacke",
                        CorrectAnswer = "jacket",
                        Options = new List<string> { "jacket" },
                        LearningTaskId = 8,
                        Category = "Cloth"
                    },
                    new Questions
                    {
                        Text = "Schuh",
                        CorrectAnswer = "shoe",
                        Options = new List<string> { "shoe" },
                        LearningTaskId = 8,
                        Category = "Cloth"
                    },
                    new Questions
                    {
                        Text = "Brille",
                        CorrectAnswer = "glasses",
                        Options = new List<string> { "glasses" },
                        LearningTaskId = 8,
                        Category = "Cloth"
                    },
                    
                
                // Englisch finde das falsche Wort
                
                new Questions
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt",
                    CorrectAnswer = "tree",
                    Options = new List<string> { "rocket", "tree", "ship", "car" },
                    LearningTaskId = 9
                },
                new Questions
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt",
                    CorrectAnswer = "lava",
                    Options = new List<string> { "lava", "green", "blue", "red" },
                    LearningTaskId = 9
                },
                new Questions
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt",
                    CorrectAnswer = "ship",
                    Options = new List<string> { "hat", "boot", "ship", "shirt" },
                    LearningTaskId = 9
                },
                new Questions
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt",
                    CorrectAnswer = "glass",
                    Options = new List<string> { "lion", "dog", "glass", "penguin" },
                    LearningTaskId = 9
                },
                new Questions
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt",
                    CorrectAnswer = "snake",
                    Options = new List<string> { "door", "window", "wall", "snake" },
                    LearningTaskId = 9
                },
                new Questions
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt",
                    CorrectAnswer = "toy",
                    Options = new List<string> { "mother", "sister", "uncle", "toy" },
                    LearningTaskId = 9
                },
                new Questions
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt",
                    CorrectAnswer = "food",
                    Options = new List<string> { "golf", "football", "basketball", "food" },
                    LearningTaskId = 9
                },
                new Questions
                {
                    Text = "Wähle das Wort das nicht zu den anderen passt",
                    CorrectAnswer = "cat",
                    Options = new List<string> { "apple", "banana", "carrot", "cat" },
                    LearningTaskId = 9
                },
                
                
                // Fragen für "Fülle die Lücken" (LearningTask 10)
                new Questions
                {
                    Text = "Der Hund läuft durch den ___.",
                    CorrectAnswer = "Park",
                    Options = new List<string> { "Park", "Wald", "Garten" },
                    LearningTaskId = 10
                },
                new Questions
                {
                    Text = "Am Himmel scheint die ___.",
                    CorrectAnswer = "Sonne",
                    Options = new List<string> { "Mond", "Sonne", "Stern" },
                    LearningTaskId = 10
                },
                new Questions
                {
                    Text = "Die Katze sitzt auf dem ___.",
                    CorrectAnswer = "Dach",
                    Options = new List<string> { "Stuhl", "Dach", "Boden" },
                    LearningTaskId = 10
                },
                new Questions
                {
                    Text = "Ich trinke Wasser aus dem ___.",
                    CorrectAnswer = "Glas",
                    Options = new List<string> { "Glas", "Becher", "Flasche" },
                    LearningTaskId = 10
                },
                new Questions
                {
                    Text = "Der Apfel ist ___.",
                    CorrectAnswer = "rot",
                    Options = new List<string> { "blau", "rot", "gelb" },
                    LearningTaskId = 10
                },

                
                
                
                
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