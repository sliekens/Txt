namespace TextFx
{
    public class SyntaxError
    {
        public SyntaxError Details { get; set; }

        public string RuleName { get; set; }

        public string Message { get; set; }

        public ITextContext Context { get; set; }
    }
}