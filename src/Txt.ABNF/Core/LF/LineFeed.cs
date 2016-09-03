using System;
using JetBrains.Annotations;

namespace Txt.ABNF.Core.LF
{
    public class LineFeed : Terminal
    {
        /// <summary>
        /// </summary>
        /// <param name="terminal"></param>
        /// <exception cref="ArgumentNullException">The value of <paramref name="terminal" /> is a null reference.</exception>
        public LineFeed([NotNull] Terminal terminal)
            : base(terminal)
        {
        }
    }
}
