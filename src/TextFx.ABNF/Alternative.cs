namespace TextFx.ABNF
{
    using System;
    using System.Diagnostics;
    using JetBrains.Annotations;

    public class Alternative : Element
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly int ordinal;

        /// <summary>
        /// </summary>
        /// <param name="alternative"></param>
        /// <exception cref="ArgumentNullException">The value of <paramref name="alternative" /> is a null reference.</exception>
        public Alternative([NotNull] Alternative alternative)
            : base(alternative)
        {
            ordinal = alternative.ordinal;
        }

        /// <summary>
        /// </summary>
        /// <param name="alternative">The text in the alternative.</param>
        /// <param name="ordinal"></param>
        /// <exception cref="ArgumentNullException">The value of <paramref name="alternative" /> is a null reference.</exception>
        public Alternative([NotNull] Element alternative, int ordinal)
            : base(alternative)
        {
            this.ordinal = ordinal;
        }

        public int Ordinal
        {
            get
            {
                Debug.Assert(ordinal > 0, "this.ordinal > 0");
                return ordinal;
            }
        }
    }
}
