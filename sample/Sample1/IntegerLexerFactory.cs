namespace Sample1
{
    using System;
    using TextFx;
    using TextFx.ABNF;
    using TextFx.ABNF.Core;

    public sealed class IntegerLexerFactory : ILexerFactory<Integer>
    {
        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ILexerFactory<Digit> digitLexerFactory;

        private readonly IOptionLexerFactory optionLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ILexerFactory<Sign> signLexerFactory;

        public IntegerLexerFactory(
            IConcatenationLexerFactory concatenationLexerFactory,
            IOptionLexerFactory optionLexerFactory,
            IRepetitionLexerFactory repetitionLexerFactory,
            ILexerFactory<Sign> signLexerFactory,
            ILexerFactory<Digit> digitLexerFactory)
        {
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }
            if (optionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(optionLexerFactory));
            }
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }
            if (signLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(signLexerFactory));
            }
            if (digitLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(digitLexerFactory));
            }
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.optionLexerFactory = optionLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.signLexerFactory = signLexerFactory;
            this.digitLexerFactory = digitLexerFactory;
        }

        public ILexer<Integer> Create()
        {
            var signLexer = signLexerFactory.Create();
            var optionalSignLexer = optionLexerFactory.Create(signLexer);
            var digitLexer = digitLexerFactory.Create();
            var manyDigitsLexer = repetitionLexerFactory.Create(digitLexer, 1, int.MaxValue);
            var innerLexer = concatenationLexerFactory.Create(optionalSignLexer, manyDigitsLexer);
            return new IntegerLexer(innerLexer);
        }
    }
}
