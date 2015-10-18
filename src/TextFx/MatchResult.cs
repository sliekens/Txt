namespace TextFx
{
    public sealed class MatchResult
    {
        public bool Success { get; set; }

        public string Text { get; set; }

        public bool EndOfInput { get; set; }

        public static MatchResult FromMatch(string text)
        {
            return new MatchResult
            {
                Success = true,
                Text = text
            };
        }

        public static MatchResult FromMismatch(string text)
        {
            return new MatchResult
            {
                Success = false,
                Text = text
            };
        }

        public static MatchResult FromEndOfInput()
        {
            return new MatchResult
            {
                Success = false,
                EndOfInput = true
            };
        }
    }
}