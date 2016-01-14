namespace TextFx.ABNF
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using JetBrains.Annotations;

    public class AggregateSyntaxError : SyntaxError
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errors"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public AggregateSyntaxError([NotNull][ItemNotNull] params SyntaxError[] errors)
            : this((IEnumerable<SyntaxError>) errors)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errors"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public AggregateSyntaxError([NotNull][ItemNotNull] IEnumerable<SyntaxError> errors)
        {
            if (errors == null)
            {
                throw new ArgumentNullException(nameof(errors));
            }
            InnerErrors = new ReadOnlyCollection<SyntaxError>(errors.ToList());
        }

        [NotNull]
        public IReadOnlyCollection<SyntaxError> InnerErrors { get; }
    }
}
