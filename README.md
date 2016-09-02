Txt 
===========
[![license](https://img.shields.io/github/license/StevenLiekens/Txt.svg?style=flat-square)]()
[![AppVeyor](https://img.shields.io/appveyor/ci/StevenLiekens/txt.svg?style=flat-square)](https://ci.appveyor.com/project/StevenLiekens/txt)
[![MyGet](https://img.shields.io/myget/ci/v/Txt.svg?style=flat-square&label=prerelease)](https://www.myget.org/feed/ci/package/nuget/Txt)

Txt (tɛkst) is a collection of code that provides a foundation for text parsers.

This repository contains two libraries: 
* Txt.Core
* Txt.ABNF

Txt is still under active development. Pre-release versions are available on MyGet. 

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
