namespace TextFx
{
    public class SyntaxError
    {
        public ITextContext Context { get; set; }

        public SyntaxError InnerError { get; set; }

        public string Message { get; set; }

        public string RuleName { get; set; }
    }
}
