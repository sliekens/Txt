namespace TextFx.ABNF
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class AggregateSyntaxError : SyntaxError
    {
        public AggregateSyntaxError(params SyntaxError[] errors)
            : this((IEnumerable<SyntaxError>)errors)
        {
        }

        public AggregateSyntaxError(IEnumerable<SyntaxError> errors)
        {
            this.InnerErrors = new ReadOnlyCollection<SyntaxError>(errors.ToList());
        }

        public IReadOnlyCollection<SyntaxError> InnerErrors { get; }
    }
}