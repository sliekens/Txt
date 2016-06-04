Txt
===========
[![Build status](https://ci.appveyor.com/api/projects/status/13pl63g6qt14boi9?svg=true)](https://ci.appveyor.com/project/StevenLiekens/txt)
[![Open Issues](https://img.shields.io/github/issues/StevenLiekens/Txt.svg)](https://github.com/StevenLiekens/Txt/issues?q=is%3Aopen)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](https://raw.githubusercontent.com/StevenLiekens/Txt/master/LICENSE)


Txt (tɛkst) is a collection of code that provides a foundation for text parsers.

This repository contains two libraries: 
* Txt.Core
* Txt.ABNF

Txt is still under active development. Pre-release versions are available on MyGet. 

# Package: Txt (previously TextFx)

[![MyGet Pre Release](https://img.shields.io/myget/ci/vpre/Txt.svg?maxAge=2592000?style=plastic)](https://www.myget.org/feed/ci/package/nuget/Txt)

All structured languages have one thing in common: each language has a formal syntax specification that describes the grammar rules for that language. Programmers use these grammar rules to create programs that parse the language.

The Txt code library assists you with creating parsers for any given language. You create your own tokens and token parsers by deriving from classes in this library.

Syntax specifications are most commonly defined in a flavor of BNF (Backus-Naur Form). Txt provides an implementation of ABNF (Augmented BNF). The ABNF specification defines a set of core grammar rules that are in common use.

```
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

### Text Source

Building a parser using Txt begins with defining a text source. A text source can be any class that implements `ITextSource` and `IDisposable`.

Txt includes `ITextSource` implementations for `System.String` or `System.IO.Stream`.

### Text Scanner

A text source object is passed to a text scanner, which provides methods for reading and matching character data. A text scanner can be any class that implements `ITextScanner` and `IDisposable`.

Txt includes a `TextScanner` class that is intended to fullfill every need.

### Lexer

A text scanner is passed to a Lexer, which reads grammar elements from the text source and converts them to `Element` objects. A Lexer can be any class that implements `ILexer<T>`. Your program should have one Lexer for every rule in a grammar.

Txt includes an abstract `Lexer` class that you should derive from.

### Element

An Element is a class representation of a grammar rule. An instance of an Element contains a substring that matches its grammar rule. Your program should have one Element for every rule in a grammar.

Txt includes an abstract `Element` class that you must derive from.

### Parser
An Element is passed to a Parser. A Parser can be any class that implements `IParser<TElement, TResult>`.

Txt includes an abstract `Parser` class that you may derive from.

### Walker
One or more parsers are passed to a Walker. A walker is passed to an element. A walker knows how to make sense out of an element tree. It walks the tree and parses individual elements along the way.

Txt includes an abstract `Walker` class that you must derive from.

## Example

This example uses a grammar that contains two rules: INTEGER and DIGIT. DIGIT is a core ABNF rule.
    
```
INTEGER = 1*10DIGIT
```

A parser implementation would have five important classes:
- An Integer element
- An Integer element lexer
- An Integer element lexer factory
- An Integer element parser
- An Integer element walker
```c#
public class Integer : Repetition
{
    public Integer(Repetition repetition)
        : base(repetition)
    {
    }
}

public class IntegerLexer : Lexer<Integer>
{
    private readonly ILexer<Repetition> innerLexer;

    public IntegerLexer(ILexer<Repetition> innerLexer)
    {
        this.innerLexer = innerLexer;
    }

    public override ReadResult<Integer> ReadImpl(ITextScanner scanner)
    {
        var result = innerLexer.Read(scanner);
        if (result.Success)
        {
            return ReadResult<Integer>.FromResult(new Integer(result.Element));
        }
        return ReadResult<Integer>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
    }
}

public class IntegerLexerFactory : ILexerFactory<Integer>
{
    private readonly IRepetitionLexerFactory repetitionLexerFactory;

    private readonly ILexer<Digit> digitLexer;

    public IntegerLexerFactory(IRepetitionLexerFactory repetitionLexerFactory, ILexer<Digit> digitLexer)
    {
        this.repetitionLexerFactory = repetitionLexerFactory;
        this.digitLexer = digitLexer;
    }

    public ILexer<Integer> Create()
    {
        return new IntegerLexer(repetitionLexerFactory.Create(digitLexer, 1, 10));
    }
}

public class IntegerParser : Parser<Integer, int>
{
    protected override int ParseImpl(Integer integer)
    {
        return int.Parse(integer.Text);
    }
}

public class IntegerWalker : Walker
{
    public void Enter(Integer integer)
    {
        Console.WriteLine("Entering the Integer");
    }

    public bool Walk(Integer integer)
    {
        var parser = new IntegerParser();
        Console.WriteLine("The integer is " + parser.Parse(integer));
        return base.Walk(integer);
    }

    public void Exit(Integer integer)
    {
        Console.WriteLine("Exiting the Integer");
    }

    public void Enter(Digit digit)
    {
        Console.WriteLine("Entering a Digit at position " + digit.Offset);
    }

    public bool Walk(Digit digit)
    {
        var parser = new DigitParser();
        Console.WriteLine("The digit is " + parser.Parse(digit));
        return base.Walk(digit);
    }

    public void Exit(Digit digit)
    {
        Console.WriteLine("Exiting the Digit");
    }
}
```


StringTextSource example:
```c#
string input = "2147483647";
var repetitionLexerFactory = new RepetitionLexerFactory();
var valueRangeLexerFactory = new ValueRangeLexerFactory();
var digitLexerFactory = new DigitLexerFactory(valueRangeLexerFactory);
var digitLexer = digitLexerFactory.Create();
var integerLexerFactory = new IntegerLexerFactory(repetitionLexerFactory, digitLexer);
var integerLexer = integerLexerFactory.Create();
var integerParser = new IntegerParser();
using (ITextSource textSource = new StringTextSource(input))
using (ITextScanner textScanner = new TextScanner(textSource))
{
    ReadResult<Integer> readResult = integerLexer.Read(textScanner);
    if (!readResult.Success) throw new InvalidOperationException();
    int value = integerParser.Parse(readResult.Element);
}
```
StreamTextSource:
```c#
File.WriteAllText("input.txt", " 2147483647");
var repetitionLexerFactory = new RepetitionLexerFactory();
var valueRangeLexerFactory = new ValueRangeLexerFactory();
var digitLexerFactory = new DigitLexerFactory(valueRangeLexerFactory);
var digitLexer = digitLexerFactory.Create();
var integerLexerFactory = new IntegerLexerFactory(repetitionLexerFactory, digitLexer);
var integerLexer = integerLexerFactory.Create();
var integerParser = new IntegerParser();
using (Stream fileStream = File.OpenRead("input.txt"))
using (PushbackInputStream inputStream = new PushbackInputStream(fileStream))
using (ITextSource textSource = new StreamTextSource(inputStream, Encoding.UTF8))
using (ITextScanner textScanner = new TextScanner(textSource))
{
    ReadResult<Integer> readResult = integerLexer.Read(textScanner);
    if (!readResult.Success) throw new InvalidOperationException();
    int value = integerParser.Parse(readResult.Element);;
}
```

Notes:

In any real application you should use a DI container to manage all the dependencies between lexer and lexer factory classes.

The `PushbackInputStream` wrapper class exists to enable support for forward-only streams like `System.Net.Sockets.NetworkStream`.

The `ReadResult<>` object contains properties that describe the read operations:
 - `Success` indicates whether the read operation succeeded
 - `Element` contains the grammar element if `Success` is `true`
 - `EndOfInput` indicates whether enough characters were available before the end of input
 - `Text` contains the matched text.
    - If `Success` is `false` then this is only a partial match.
 - `ErrorText` contains the mismatched text if `Success` is `false` and `EndOfInput` is `false`
