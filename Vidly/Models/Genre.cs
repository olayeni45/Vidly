namespace Vidly.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static readonly int Comedy = 1;
        public static readonly int Action = 2;
        public static readonly int Family = 3;
        public static readonly int Romance = 4;
    }
}