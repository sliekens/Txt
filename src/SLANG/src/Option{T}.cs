namespace SLANG
{
    using System.Collections.Generic;

    /// <summary>Represents an optional element.</summary>
    /// <typeparam name="T">The type of the optional element.</typeparam>
    public class Option<T> : Repetition<T>
        where T : Element
    {
        /// <summary>Initializes a new instance of the <see cref="Option{T}"/> class with no elements.</summary>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public Option(ITextContext context)
            : base(new List<T>(0), 0, 1, context)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="Option{T}"/> class with a specified element.</summary>
        /// <param name="element">The optional element.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public Option(T element, ITextContext context)
            : base(new List<T>(1) { element }, 0, 1, context)
        {
        } 
    }
}