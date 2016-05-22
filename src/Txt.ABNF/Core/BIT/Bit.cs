// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Bit.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using JetBrains.Annotations;

namespace Txt.ABNF.Core.BIT
{
    public class Bit : Alternation
    {
        public Bit([NotNull] Alternation element)
            : base(element)
        {
            switch (element.Ordinal)
            {
                case 1:
                    Value = false;
                    break;
                case 2:
                    Value = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public bool Value { get; }
    }
}
