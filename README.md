TextFx
===========
[![Build status](https://ci.appveyor.com/api/projects/status/2ijyc3cck1ddlurt?svg=true)](https://ci.appveyor.com/project/StevenLiekens/textfx)
[![Open Issues](https://img.shields.io/github/issues/StevenLiekens/TextFx.svg)](https://github.com/StevenLiekens/TextFx/issues?q=is%3Aopen)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](https://raw.githubusercontent.com/StevenLiekens/TextFx/master/LICENSE)


TextFx is a collection of code that provides a foundation for text parsers.

This repository contains two libraries: 
* TextFx
* TextFx.ABNF


# TextFx

[![TextFx](https://img.shields.io/nuget/v/TextFx.svg)](https://www.nuget.org/packages/TextFx/)

All structured languages have one thing in common: each language has a formal syntax specification that describes the grammar rules for that language. Programmers use these grammar rules to create programs that parse the language.

The TextFx code library assists you with creating parsers for any given language. You create your own tokens and token parsers by deriving from classes in this library.

# TextFx.ABNF

[![TextFx.ABNF](https://img.shields.io/nuget/v/TextFx.ABNF.svg)](https://www.nuget.org/packages/TextFx.ABNF/)

Syntax specifications are most commonly defined in a flavor of BNF (Backus-Naur Form).

TextFx provides an implementation of ABNF (Augmented BNF) as a separate download.

The ABNF specification defines a set of core grammar rules that are in common use.

```abnf
ALPHA          =  %x41-5A / %x61-7A   ; A-Z / a-z

BIT            =  "0" / "1"

CHAR           =  %x01-7F
                       ; any 7-bit US-ASCII character,
                       ;  excluding NUL

CR             =  %x0D
                       ; carriage return

CRLF           =  CR LF
                       ; Internet standard newline

CTL            =  %x00-1F / %x7F
                       ; controls

DIGIT          =  %x30-39
                       ; 0-9

DQUOTE         =  %x22
                       ; " (Double Quote)

HEXDIG         =  DIGIT / "A" / "B" / "C" / "D" / "E" / "F"

HTAB           =  %x09
                       ; horizontal tab

LF             =  %x0A
                       ; linefeed

LWSP           =  *(WSP / CRLF WSP)
                       ; Use of this linear-white-space rule
                       ;  permits lines containing only white
                       ;  space that are no longer legal in
                       ;  mail headers and have caused
                       ;  interoperability problems in other
                       ;  contexts.
                       ; Do not use when defining mail
                       ;  headers and use with caution in
                       ;  other contexts.

OCTET          =  %x00-FF
                       ; 8 bits of data

SP             =  %x20

VCHAR          =  %x21-7E
                       ; visible (printing) characters

WSP            =  SP / HTAB
                       ; white space
```
Source: RFC 5234 [Augmented BNF for Syntax Specifications](https://tools.ietf.org/html/rfc5234)

Custom syntax specifications can define rules that build upon these core rules.


## How to use the code

Consider the syntax specification for an integer number:


```abnf
DIGIT = "0" / "1" / "2" / "3" / "4"  ; DIGIT is a core ABNF rule
      / "5" / "6" / "7" / "8" / "9"

SIGN  = "+" / "-"                    ; "+" or "-"

INT   = [ SIGN ] 1*DIGIT             ; An optional sign, followed by 1 or more digits
```

An `INT` has an optional `SIGN`, followed by at least one `DIGIT`. A `SIGN` can be either `"+"` or `"-"`. A `DIGIT` can be any decimal digit, and is a core rule.

An implementation of this grammar might look like this.


```C#
namespace Example
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    // SIGN = "+" / "-"
    public class Sign : Alternative
    {
        public Sign(Element element)
            : base(element)
        {
        }
    }

    // DIGIT = %x30-39
    // Because DIGIT is a core ABNF rule, the library already contains similar code
    // It is duplicated here only for demonstration
    public class Digit : Terminal
    {
        public Digit(Terminal terminal)
            : base(terminal)
        {
        }
    }

    // INT = [ SIGN ] 1*DIGIT
    public class Int : Sequence
    {
        private Element signPart;

        private Element numericPart;

        public Int(Sequence sequence)
            : base(sequence)
        {
            this.signPart = sequence.Elements[0];
            this.numericPart = sequence.Elements[1];
        }

        // Converts the current instance to System.Int32
        public int ToInt32()
        {
            var value = int.Parse(this.numericPart.Value);

            // negate the value if the sign part is a minus (otherwise do nothing)
            if (this.signPart.Value == "-")
            {
                value *= -1;
            }

            return value;
        }
    }

    public class SignLexer : Lexer<Sign>
    {
        private ILexer<TerminalString> plusLexer;

        private ILexer<TerminalString> minusLexer;

        public SignLexer()
        {
            // TextFx uses a factory pattern for configuring element lexers

            // You typically want to move this initialization code to a custom factory class
            // instead of writing it in the constructor of the ILexer
            ITerminalLexerFactory terminalLexerFactory;
            IStringLexerFactory stringLexerFactory;
            terminalLexerFactory = new TerminalLexerFactory();
            stringLexerFactory = new StringLexerFactory(terminalLexerFactory);

            // Create two inner lexers, one for each alternative
            this.plusLexer = stringLexerFactory.Create("+");
            this.minusLexer = stringLexerFactory.Create("-");
        }

        public override bool TryRead(ITextScanner scanner, out Sign element)
        {

            // Alternative 1: "+"
            TerminalString plus;
            if (this.plusLexer.TryRead(scanner, out plus))
            {
                element = new Sign(plus);
                return true;
            }

            // Alternative 2: "-"
            TerminalString minus;
            if (this.minusLexer.TryRead(scanner, out minus))
            {
                element = new Sign(minus);
                return true;
            }

            // No matches => the next input symbol is neither "+" or "-"
            element = null;
            return false;
        }
    }

    // Because DIGIT is a core ABNF rule, the library already contains similar code
    // It is duplicated here only for demonstration
    public class DigitLexer : Lexer<Digit>
    {
        private ILexer<Terminal> digitValueRangeLexer;

        public DigitLexer()
        {
            IValueRangeLexerFactory valueRangeLexerFactory;
            valueRangeLexerFactory = new ValueRangeLexerFactory();

            this.digitValueRangeLexer = valueRangeLexerFactory.Create(lowerBound: '\x30', upperBound: '\x39');
        }

        public override bool TryRead(ITextScanner scanner, out Digit element)
        {
            Terminal digit;
            if (this.digitValueRangeLexer.TryRead(scanner, out digit))
            {
                element = new Digit(digit);
                return true;
            }

            element = null;
            return false;
        }
    }

    public class IntLexer : Lexer<Int>
    {
        private ILexer<Sign> signLexer;

        private ILexer<Digit> digitLexer; 

        public override bool TryRead(ITextScanner scanner, out Int element)
        {
            ILexer<Repetition> optionalSignLexer;
            optionalSignLexer = new OptionLexer(this.signLexer);

            ILexer<Repetition> repeatingDigitsLexer;
            repeatingDigitsLexer = new RepetitionLexer(this.digitLexer, lowerBound: 1, upperBound: Int32.MaxValue);

            ILexer<Sequence> intSequenceLexer;
            intSequenceLexer = new SequenceLexer(optionalSignLexer, repeatingDigitsLexer);

            Sequence integer;
            if (intSequenceLexer.TryRead(scanner, out integer))
            {
                element = new Int(integer);
                return true;
            }

            element = null;
            return false;
        }
    }
}
```

With all this code in place, you could create a program that converts text to System.Int32.

```C#
// Convers all valid arguments to System.Int32 and prints their sum, then exits
public class Program
{
    private static Encoding encoding = Encoding.GetEncoding("us-ascii");

    private static Stream StringToMemoryStream(string s)
    {
        return new MemoryStream(encoding.GetBytes(s));
    }

    public static void Main(string[] args)
    {
        int sum = 0;
        var lexer = new IntLexer();
        foreach (string s in args)
        {
            using (var memoryStream = StringToMemoryStream(s))
            using (var pushbackStream =  new PushbackInputStream(memoryStream))
            using (var textScanner = new TextScanner(pushbackStream, encoding))
            {
                Int token;
                if (lexer.TryRead(textScanner, out token))
                {
                    sum += token.ToInt32();
                }
            }
                
        }

        Console.WriteLine(sum);
        Console.ReadLine();
    }
}
```

Important: there is currently no support for automatically generating parsers. The idea here is that if you want to do it properly, then do it yourself.