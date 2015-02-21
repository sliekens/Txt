namespace Text.Scanning.Core
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    public class LWspLexer : Lexer<LWspElement>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ILexer<CrLfElement> crLfLexer;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ILexer<WSpElement> wSpLexer;

        public LWspLexer()
            : this(new CrLfLexer(), new WSpLexer())
        {
        }

        public LWspLexer(ILexer<CrLfElement> crLfLexer, ILexer<WSpElement> wSpLexer)
        {
            Contract.Requires(crLfLexer != null);
            Contract.Requires(wSpLexer != null);
            this.crLfLexer = crLfLexer;
            this.wSpLexer = wSpLexer;
        }

        /// <inheritdoc />
        public override LWspElement Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            var data = new List<LWspElement.CrLfWSpPair>();

            // The program should eventually exit this loop, unless the source data is an infinite stream of linear whitespace
            while (!scanner.EndOfInput)
            {
                CrLfElement crLfElement;
                WSpElement wSpElement;
                if (this.wSpLexer.TryRead(scanner, out wSpElement))
                {
                    data.Add(new LWspElement.CrLfWSpPair(wSpElement));
                }
                else if (this.crLfLexer.TryRead(scanner, out crLfElement))
                {
                    if (!scanner.EndOfInput && this.wSpLexer.TryRead(scanner, out wSpElement))
                    {
                        Contract.Assume(wSpElement.Offset == crLfElement.Offset + 2);
                        data.Add(new LWspElement.CrLfWSpPair(crLfElement, wSpElement));
                    }
                    else
                    {
                        this.crLfLexer.PutBack(scanner, crLfElement);
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            return new LWspElement(data, context);
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out LWspElement element)
        {
            var context = scanner.GetContext();
            var data = new List<LWspElement.CrLfWSpPair>();

            // The program should eventually exit this loop, unless the source data is an infinite stream of linear whitespace
            while (!scanner.EndOfInput)
            {
                CrLfElement crLfElement;
                WSpElement wSpElement;
                if (this.crLfLexer.TryRead(scanner, out crLfElement))
                {
                    if (!scanner.EndOfInput && this.wSpLexer.TryRead(scanner, out wSpElement))
                    {
                        Contract.Assume(wSpElement.Offset == crLfElement.Offset + 2);
                        data.Add(new LWspElement.CrLfWSpPair(crLfElement, wSpElement));
                    }
                    else
                    {
                        this.crLfLexer.PutBack(scanner, crLfElement);
                        break;
                    }
                }
                else if (this.wSpLexer.TryRead(scanner, out wSpElement))
                {
                    data.Add(new LWspElement.CrLfWSpPair(wSpElement));
                }
                else
                {
                    break;
                }
            }

            element = new LWspElement(data, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.wSpLexer != null);
            Contract.Invariant(this.crLfLexer != null);
        }
    }
}