using System;
using JetBrains.Annotations;

namespace Txt.ABNF.Core.CR
{
    public class CarriageReturn : Terminal
    {
        /// <summary>
        /// </summary>
        /// <param name="terminal"></param>
        /// <exception cref="ArgumentNullException">The value of <paramref name="terminal" /> is a null reference.</exception>
        public CarriageReturn([NotNull] Terminal terminal)
            : base(terminal)
        {
        }
    }
}
