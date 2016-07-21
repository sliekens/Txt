﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using JetBrains.Annotations;

namespace Txt.Core
{
    /// <summary>Provides the base class for all elements.</summary>
    public abstract class Element : ITextContext, IReadOnlyList<Element>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private static readonly IReadOnlyList<Element> EmptyElements = new Element[0];

        [NotNull]
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ITextContext context;

        [NotNull]
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly IReadOnlyList<Element> elements;

        [NotNull]
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
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
            context = element.context;
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
            this.context = context;
        }

        /// <summary>Initializes a new instance of the <see cref="Element" /> class with a given sequence and its context.</summary>
        /// <param name="sequence">The text in the sequence.</param>
        /// <param name="elements">The collection of elements that represent the sequence.</param>
        /// <param name="context">An object that describes the current element's context.</param>
        /// <exception cref="ArgumentNullException">
        ///     The value of  <paramref name="sequence" /> or  <paramref name="elements" /> or
        ///     <paramref name="context" /> is a null reference.
        /// </exception>
        protected Element([NotNull] string sequence, [NotNull] IList<Element> elements, [NotNull] ITextContext context)
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
            this.elements = elements.Count == 0 ? EmptyElements : elements.ToArray();
            this.context = context;
        }

        /// <inheritdoc />
        public int Count => elements.Count;

        /// <summary>
        ///     Returns the sequence of elements that represent the current element. If the current element is a string of
        ///     terminal values then this collection is empty.
        /// </summary>
        [NotNull]
        public IReadOnlyList<Element> Elements
        {
            get
            {
                Debug.Assert(elements != null, "this.elements != null");
                return elements;
            }
        }

        /// <summary>Gets the current position, relative to the beginning of the data source.</summary>
        public int Offset => context.Offset;

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
        public Element this[int index] => elements[index];

        /// <inheritdoc />
        public IEnumerator<Element> GetEnumerator()
        {
            return elements.GetEnumerator();
        }

        public void Walk([NotNull] Walker walker)
        {
            if (walker == null)
            {
                throw new ArgumentNullException(nameof(walker));
            }
            WalkImpl(walker);
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
                foreach (dynamic element in Elements)
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

        /// <inheritdoc />
        public sealed override string ToString()
        {
            return Text;
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return elements.GetEnumerator();
        }
    }
}
