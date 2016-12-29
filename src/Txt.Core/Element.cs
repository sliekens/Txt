using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using JetBrains.Annotations;

namespace Txt.Core
{
    /// <summary>Provides the base class for all elements.</summary>
    public abstract class Element : IReadOnlyList<Element>
    {
        [NotNull]
        [ItemNotNull]
        private static readonly IReadOnlyList<Element> EmptyElements = new Element[0];

        [NotNull]
        [ItemNotNull]
        private readonly IReadOnlyList<Element> elements;

        [NotNull]
        private readonly string text;

        /// <summary>Initializes a new instance of the <see cref="Element" /> class with a given element to copy.</summary>
        /// <param name="element">The element to copy.</param>
        /// <exception cref="ArgumentNullException">The value of <paramref name="element" /> is a null reference.</exception>
        protected Element([NotNull] Element element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }
            text = element.text;
            elements = element.elements;
            Context = element.Context;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Element" /> class with a given string of terminal values and its
        ///     context.
        /// </summary>
        /// <param name="terminals">The terminal values.</param>
        /// <param name="context">An object that describes the current element's context.</param>
        /// <exception cref="ArgumentNullException">
        ///     The value of  <paramref name="terminals" /> or <paramref name="context" /> is a
        ///     null reference.
        /// </exception>
        protected Element([NotNull] string terminals, [NotNull] ITextContext context)
        {
            if (terminals == null)
            {
                throw new ArgumentNullException(nameof(terminals));
            }
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            text = terminals;
            elements = EmptyElements;
            Context = context;
        }

        /// <summary>Initializes a new instance of the <see cref="Element" /> class with a given sequence and its context.</summary>
        /// <param name="sequence">The text in the sequence.</param>
        /// <param name="elements">The collection of elements that represent the sequence.</param>
        /// <param name="context">An object that describes the current element's context.</param>
        /// <exception cref="ArgumentNullException">
        ///     The value of  <paramref name="sequence" /> or  <paramref name="elements" /> or
        ///     <paramref name="context" /> is a null reference.
        /// </exception>
        protected Element(
            [NotNull] string sequence,
            [NotNull] [ItemNotNull] IList<Element> elements,
            [NotNull] ITextContext context)
        {
            if (sequence == null)
            {
                throw new ArgumentNullException(nameof(sequence));
            }
            if (elements == null)
            {
                throw new ArgumentNullException(nameof(elements));
            }
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            text = sequence;
            // ReSharper disable once AssignNullToNotNullAttribute
            this.elements = elements.Count == 0 ? EmptyElements : elements.ToArray();
            Context = context;
        }

        [NotNull]
        public ITextContext Context { get; }

        /// <inheritdoc />
        public int Count => elements.Count;

        /// <summary>Gets one or more terminal values that represent the current element.</summary>
        [NotNull]
        public string Text
        {
            get
            {
                Debug.Assert(text != null);
                return text;
            }
        }

        /// <inheritdoc />
        [NotNull]
        [ItemNotNull]
        // ReSharper disable once AssignNullToNotNullAttribute
        public Element this[int index] => elements[index];

        /// <inheritdoc />
        public IEnumerator<Element> GetEnumerator()
        {
            return elements.GetEnumerator();
        }

        /// <inheritdoc />
        public sealed override string ToString()
        {
            return Text;
        }

        public void Walk([NotNull] Walker walker)
        {
            if (walker == null)
            {
                throw new ArgumentNullException(nameof(walker));
            }
            WalkImpl(walker);
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return elements.GetEnumerator();
        }

        private void WalkImpl([NotNull] dynamic walker)
        {
            walker.EnterAny(this);
            walker.Enter((dynamic)this);
            try
            {
                if (!walker.WalkAny(this))
                {
                    return;
                }
                if (!walker.Walk((dynamic)this))
                {
                    return;
                }
                foreach (dynamic element in this)
                {
                    element?.WalkImpl(walker);
                }
            }
            finally
            {
                walker.Exit((dynamic)this);
                walker.ExitAny(this);
            }
        }
    }
}
