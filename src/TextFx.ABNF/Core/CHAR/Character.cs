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
    public class Character : Terminal
    {
        public Character(Terminal terminal)
            : base(terminal)
        {
        }
    }
}