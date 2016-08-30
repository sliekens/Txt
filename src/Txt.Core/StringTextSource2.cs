using JetBrains.Annotations;

namespace Txt.Core
{
    public class StringTextSource2 : TextSource2
    {
        public StringTextSource2([NotNull] string data)
            : base(data?.ToCharArray())
        {
        }

        public StringTextSource2([NotNull] char[] data)
            : base(data)
        {
        }

        public StringTextSource2([NotNull] char[] data, int startIndex)
            : base(data, startIndex)
        {
        }

        public StringTextSource2([NotNull] char[] data, int startIndex, int length)
            : base(data, startIndex, length)
        {
        }

        protected override int ReadImpl(char[] buffer, int startIndex, int maxCount)
        {
            // All characters were buffered upfront so return a value indicating that there are no more characters
            return 0;
        }
    }
}
