namespace Einburgerung.Model
{
    public class QuestionModel
    {
        public int Num { get; set; }
        public string? Question { get; set; }
        public string? A { get; set; }
        public string? B { get; set; }
        public string? C { get; set; }
        public string? D { get; set; }
        public string? Solution { get; set; }
        public string? Image { get; set; }
        public Translation? Translation { get; set; }
        public string? Category { get; set; }
        public string? Context { get; set; }
    }

    public class Translation
    {
        public BaseQuestion? EN { get; set; }
        public BaseQuestion? TR { get; set; }
        public BaseQuestion? RU { get; set; }
        public BaseQuestion? FR { get; set; }
        public BaseQuestion? AR { get; set; }
        public BaseQuestion? UK { get; set; }
        public BaseQuestion? HI { get; set; }
    }

    public class BaseQuestion
    {
        public string? Question { get; set; }
        public string? A { get; set; }
        public string? B { get; set; }
        public string? C { get; set; }
        public string? D { get; set; }
        public string? Context { get; set; }
    }
}

