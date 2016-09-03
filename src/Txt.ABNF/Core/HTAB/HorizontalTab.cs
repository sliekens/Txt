using System;
using JetBrains.Annotations;

namespace Txt.ABNF.Core.HTAB
{
    public class HorizontalTab : Terminal
    {
        /// <summary>
        /// </summary>
        /// <param name="terminal"></param>
        /// <exception cref="ArgumentNullException">The value of <paramref name="terminal" /> is a null reference.</exception>
        public HorizontalTab([NotNull] Terminal terminal)
            : base(terminal)
        {
        }
    }
}
