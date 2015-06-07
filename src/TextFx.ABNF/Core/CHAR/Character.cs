// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Character.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   Represents the CHAR rule: 1 US-ASCII character, excluding NUL. Unicode: U+0001 - U+007F.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextFx.ABNF.Core
{
    using System.Diagnostics;

    public class Character : Terminal
    {
        public Character(Terminal terminal)
            : base(terminal)
        {
        }

        public static explicit operator char(Character instance)
        {
            return instance.ToChar();
        }

        public char ToChar()
        {
            Debug.Assert(this.Values.Length == 1, "this.Values.Length == 1");
            return this.Values[0];
        }
    }
}