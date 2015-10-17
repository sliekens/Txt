namespace TextFx
{
    using System;

    /// <summary>Used to associate an implementation of <see cref="ILexer" /> with a grammar rule.</summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class RuleNameAttribute : Attribute
    {
        private readonly string ruleName;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RuleNameAttribute" /> with a given rule name.
        /// </summary>
        /// <param name="ruleName">The name of the grammar rule.</param>
        public RuleNameAttribute(string ruleName)
        {
            if (ruleName == null)
            {
                throw new ArgumentNullException(nameof(ruleName));
            }

            this.ruleName = ruleName;
        }

        /// <summary>
        ///     Gets the name of the grammar rule.
        /// </summary>
        public string RuleName
        {
            get
            {
                return this.ruleName;
            }
        }
    }
}