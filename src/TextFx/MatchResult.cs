namespace TextFx
{
    using JetBrains.Annotations;

    public sealed class MatchResult
    {
        public bool EndOfInput { get; set; }

        public bool Success { get; set; }

        public string Text { get; set; }

        [NotNull]
        public static MatchResult FromEndOfInput()
        {
            return new MatchResult
            {
                Success = false,
                EndOfInput = true
            };
        }

        [NotNull]
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
    }
}
