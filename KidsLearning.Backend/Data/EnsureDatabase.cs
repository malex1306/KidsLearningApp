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
                AvatarUrl = "https://via.placeholder.com/40",
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
                new Questions
                {
                    Text = "Welche Zahl fehlt in der Reihe: 1, 2, ?, 4, 5",
                    CorrectAnswer = "3",
                    Options = new List<string> { "1", "2", "3", "4" },
                    LearningTaskId = 1
                },
                new Questions
                {
                    Text = "Welche Zahl fehlt in der Reihe: 5, ?, 7, 8, 9",
                    CorrectAnswer = "6",
                    Options = new List<string> { "5", "6", "7", "8" },
                    LearningTaskId = 1
                },
                 new Questions
                {
                    Text = "Welche Zahl fehlt in der Reihe: ?, 9, 10, 11, 12",
                    CorrectAnswer = "8",
                    Options = new List<string> { "5", "6", "7", "8" },
                    LearningTaskId = 1
                },

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
                
                
                // Buchstabenland Fill in the Blank
                
                new Questions
                {
                    Text = "DEINS PLACEHOLDER",
                    CorrectAnswer = "tree",
                    Options = new List<string> { "rocket, tree, ship, car" },
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