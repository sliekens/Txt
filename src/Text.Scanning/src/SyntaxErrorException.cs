// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SyntaxErrorException.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Text.Scanning
{
    using System;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// The exception that is thrown when a parsing engine encounters an error. Errors include invalid or unexpected
    /// symbols, or unexpectedly reaching the end of input.
    /// </summary>
    public class SyntaxErrorException : Exception, ITextContext
    {
        /// <summary>TODO </summary>
        private readonly int offset;

        /// <summary>Initializes a new instance of the <see cref="T:Text.Scanning.SyntaxErrorException"/> class with a specified
        /// text context.</summary>
        /// <param name="context">The <see cref="ITextContext"/> that describes the location of the error.</param>
        public SyntaxErrorException(ITextContext context)
        {
            Contract.Requires(context != null);
            this.offset = context.Offset;
        }

        /// <summary>Initializes a new instance of the <see cref="T:Text.Scanning.SyntaxErrorException"/> class with a specified
        /// error message and text context.</summary>
        /// <param name="context">The <see cref="ITextContext"/> that describes the location of the error.</param>
        /// <param name="message">The message that describes the error.</param>
        public SyntaxErrorException(ITextContext context, string message)
            : base(message)
        {
            Contract.Requires(context != null);
            this.offset = context.Offset;
        }

        /// <summary>Initializes a new instance of the <see cref="T:Text.Scanning.SyntaxErrorException"/> class with a specified
        /// error message and text context, and a reference to the inner exception that is the cause of this exception.</summary>
        /// <param name="context">The <see cref="ITextContext"/> that describes the location of the error.</param>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="inner">The exception that is the cause of the current exception, or a null reference (Nothing in Visual
        /// Basic) if no inner exception is specified.</param>
        public SyntaxErrorException(ITextContext context, string message, Exception inner)
            : base(message, inner)
        {
            Contract.Requires(context != null);
            this.offset = context.Offset;
        }

        /// <summary>Gets the position where the error occurred, relative to the beginning of the data source.</summary>
        public int Offset
        {
            get
            {
                return this.offset;
            }
        }
    }
}