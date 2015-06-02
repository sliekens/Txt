// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Character.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   Represents the CHAR rule: 1 US-ASCII character, excluding NUL. Unicode: U+0001 - U+007F.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core.CHAR
{
    using System.Diagnostics;

    public class Character : Element
    {
        public Character(Element element)
            : base(element)
        {
        }

        public static explicit operator char(Character instance)
        {
            return instance.ToChar();
        }

        public char ToChar()
        {
            Debug.Assert(this.Data.Length == 1, "this.Data.Length == 1");
            return this.Data[0];
        }
    }
}