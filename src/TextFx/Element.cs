namespace TextFx
{
    using System;
    using System.Diagnostics;

    /// <summary>Provides the base class for all elements.</summary>
    public abstract class Element : ITextContext
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ITextContext context;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string values;

        /// <summary>Initializes a new instance of the <see cref="Element" /> class with a given element to copy.</summary>
        /// <param name="element">The element to copy.</param>
        /// <exception cref="ArgumentNullException">The value of <paramref name="element" /> is a null reference.</exception>
        protected Element(Element element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            this.values = element.values;
            this.context = element.context;
        }

        /// <summary>Initializes a new instance of the <see cref="Element" /> class with a given terminal and context.</summary>
        /// <param name="value">The terminal value.</param>
        /// <param name="context">An object that describes the current element's context.</param>
        /// <exception cref="ArgumentNullException">The value of <paramref name="context" /> is a null reference.</exception>
        protected Element(char value, ITextContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            this.values = char.ToString(value);
            this.context = context;
        }

        /// <summary>Initializes a new instance of the <see cref="Element" /> class with the given terminal values and context.</summary>
        /// <param name="values">The terminal values.</param>
        /// <param name="context">An object that describes the current element's context.</param>
        /// <exception cref="ArgumentNullException">
        ///     The value of <paramref name="values" /> or <paramref name="context" /> is a
        ///     null reference.
        /// </exception>
        protected Element(string values, ITextContext context)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }

            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            this.values = values;
            this.context = context;
        }

        /// <summary>Gets the current position, relative to the beginning of the data source.</summary>
        public int Offset
        {
            get
            {
                return this.context.Offset;
            }
        }

        public Element PreviousElement { get; set; }

        public Element NextElement { get; set; }

        /// <summary>Gets one or more terminal values that represent the current element.</summary>
        public string Text
        {
            get
            {
                Debug.Assert(this.values != null);
                return this.values;
            }
        }

        /// <summary>
        ///     Gets a well-formed string that represents the current element. This is useful for elements that are
        ///     technically valid, but contain formatting errors or other inpurities. For example: mixed upper and lower case
        ///     characters where only lower case is well-formed. Unless overridden, the default return value is the value of
        ///     <see cref="Text" />.
        /// </summary>
        /// <returns>A well-formed string that represents the current element.</returns>
        public virtual string GetWellFormedText()
        {
            return this.Text;
        }

        /// <inheritdoc />
        public override sealed string ToString()
        {
            return this.Text;
        }
    }
}