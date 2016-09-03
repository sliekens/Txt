﻿using System;
using JetBrains.Annotations;

namespace Txt.ABNF.Core.DIGIT
{
    public class Digit : Terminal
    {
        /// <summary>
        /// </summary>
        /// <param name="terminal"></param>
        /// <exception cref="ArgumentNullException">The value of <paramref name="terminal" /> is a null reference.</exception>
        public Digit([NotNull] Terminal terminal)
            : base(terminal)
        {
        }
    }
}
