using JetBrains.Annotations;

namespace Txt.Core
{
    public class StringTextSource2 : TextSource2
    {
        public StringTextSource2([NotNull] string data)
            : base(data == null ? null : data.ToCharArray())
        {
        }

        protected override int ReadImpl(char[] buffer, int startIndex, int maxCount)
        {
            return 0;
        }
    }
}
