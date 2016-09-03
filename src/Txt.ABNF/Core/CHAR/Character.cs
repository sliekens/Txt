using System;
using JetBrains.Annotations;

namespace Txt.ABNF.Core.CHAR
{
    public class Character : Terminal
    {
        /// <summary>
        /// </summary>
        /// <param name="terminal"></param>
        /// <exception cref="ArgumentNullException">The value of <paramref name="terminal" /> is a null reference.</exception>
        public Character([NotNull] Terminal terminal)
            : base(terminal)
        {
        }
    }
}
