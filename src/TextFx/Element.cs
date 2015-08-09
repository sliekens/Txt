using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

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
        private readonly string text;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected IList<Element> elements;

        /// <summary>Initializes a new instance of the <see cref="Element" /> class with a given element to copy.</summary>
        /// <param name="element">The element to copy.</param>
        /// <exception cref="ArgumentNullException">The value of <paramref name="element" /> is a null reference.</exception>
        protected Element(Element element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            this.text = element.text;
            this.elements = element.elements;
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

            this.text = char.ToString(value);
            this.elements = new List<Element>(0);
            this.context = context;
        }

        protected Element(IList<Element> elements, ITextContext context)
        {
            if (elements == null)
            {
                throw new ArgumentNullException("elements");
            }

            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            this.elements = elements;
            this.text = string.Concat(elements.Select(element => element.text));
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
                Debug.Assert(this.text != null);
                return this.text;
            }
        }

        public IList<Element> Elements
        {
            get
            {
                Debug.Assert(this.elements != null, "this.elements != null");
                return this.elements;
            }
        }

        /// <summary>Gets a collection of terminal elements by recursively evaluating <see cref="GetTerminals"/> for every <see cref="Element"/> in <see cref="Elements"/>.</summary>
        /// <returns>A collection of terminal elements.</returns>
        public IEnumerable<Element> GetTerminals()
        {
            if (this.Elements.Count == 0)
            {
                yield return this;
                yield break;
            }

            foreach (var terminal in this.Elements.SelectMany(t => t.GetTerminals()))
            {
                yield return terminal;
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
    return string.Concat(this.Elements.Select(element => element.GetWellFormedText()));
}

/// <inheritdoc />
public override sealed string ToString()
{
    return this.Text;
}
    }
}