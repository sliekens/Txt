using System;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.Core
{
    public class StringTextSource2 : TextSource2
    {
        public StringTextSource2([NotNull] string data)
            : base(data == null ? null : data.ToCharArray())
        {
        }

        protected override int ReadImpl(char[] buffer, int offset, int count)
        {
            return 0;
        }
    }
}
