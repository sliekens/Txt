using JetBrains.Annotations;

namespace Txt.Core
{
    public sealed class MatchResult
    {
        public static readonly MatchResult None = new MatchResult(false, null);

        public MatchResult(bool isMatch, string text)
        {
            IsMatch = isMatch;
            Text = text;
        }

        public bool IsMatch { get; }

        public string Text { get; }

        public static MatchResult Match([NotNull] string text)
        {
            return new MatchResult(true, text);
        }
    }
}
