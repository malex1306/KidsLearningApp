namespace KidsLearning.Backend.Models
{
    public class Badge
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string IconUrl { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        
        public int ChildId { get; set; }
        public Child? Child { get; set; }
    }
}