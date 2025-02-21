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
        public EN? EN { get; set; }
        public TR? TR { get; set; }
        public RU? RU { get; set; }
        public FR? FR { get; set; }
        public AR? AR { get; set; }
        public UK? UK { get; set; }
        public HI? HI { get; set; }
    }

    public class EN : BaseQuestion
    {
        public BaseQuestion? BaseQuestion { get; set; }
    }

    public class TR : BaseQuestion
    {
        public BaseQuestion? BaseQuestion { get; set; }
    }

    public class RU : BaseQuestion
    {
        public BaseQuestion? BaseQuestion { get; set; }
    }

    public class FR : BaseQuestion
    {
        public BaseQuestion? BaseQuestion { get; set; }
    }

    public class AR : BaseQuestion
    {
        public BaseQuestion? BaseQuestion { get; set; }
    }

    public class UK : BaseQuestion
    {
        public BaseQuestion? BaseQuestion { get; set; }
    }

    public class HI : BaseQuestion
    {
        public BaseQuestion? BaseQuestion { get; set; }
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

