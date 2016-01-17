TextFx
===========
[![Build status](https://ci.appveyor.com/api/projects/status/2ijyc3cck1ddlurt?svg=true)](https://ci.appveyor.com/project/StevenLiekens/textfx)
[![Open Issues](https://img.shields.io/github/issues/StevenLiekens/TextFx.svg)](https://github.com/StevenLiekens/TextFx/issues?q=is%3Aopen)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](https://raw.githubusercontent.com/StevenLiekens/TextFx/master/LICENSE)


TextFx is a collection of code that provides a foundation for text parsers.

This repository contains two libraries: 
* TextFx
* TextFx.ABNF

Both are available as separate packages on NuGet (stable builds &  pre-release builds) and MyGet (continuous integration builds).


# Package: TextFx

[![TextFx Stable](https://img.shields.io/nuget/v/TextFx.svg)](https://www.nuget.org/packages/TextFx/)
[![TextFx CI](https://img.shields.io/myget/textfx/vpre/TextFx.svg)](https://www.myget.org/gallery/textfx)

All structured languages have one thing in common: each language has a formal syntax specification that describes the grammar rules for that language. Programmers use these grammar rules to create programs that parse the language.

The TextFx code library assists you with creating parsers for any given language. You create your own tokens and token parsers by deriving from classes in this library.

# Package: TextFx.ABNF

[![TextFx.ABNF Stable](https://img.shields.io/nuget/v/TextFx.ABNF.svg)](https://www.nuget.org/packages/TextFx.ABNF/)
[![TextFx.ABNF CI](https://img.shields.io/myget/textfx/vpre/TextFx.ABNF.svg)](https://www.myget.org/gallery/textfx)

Syntax specifications are most commonly defined in a flavor of BNF (Backus-Naur Form).

TextFx provides an implementation of ABNF (Augmented BNF) as a separate download.

The ABNF specification defines a set of core grammar rules that are in common use.

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

The solution contains a sample program that reads numbers from console input and calculates their sum.
The program uses the following grammar:

```
DIGIT     = "0" / "1" / "2" / "3" / "4"  ; DIGIT is a core ABNF rule
          / "5" / "6" / "7" / "8" / "9"

SIGN      = "+" / "-"                    ; "+" or "-"

INTEGER   = [ SIGN ] 1*DIGIT             ; An optional sign, followed by 1 or more digits
```

In English: an `INTEGER` has an optional `SIGN`, followed by at least one `DIGIT`.
A `SIGN` can be either `"+"` or `"-"`.
A `DIGIT` can be any decimal digit, and is a core rule.

The `DIGIT` rule is a core rule in namespace `TextFx.ABNF.Core`.
The sample program adds two custom rules:
 - the `SIGN` rule is represented by the `Sign` class
 - the `INTEGER` rule is represented by the `Integer` class

The program uses AutoFac to wire up all dependencies that are required to build a reader object for the `INTEGER` rule.

Feel free to use a different IoC container for your program, or no IoC container at all.

```c#
private static IContainer BuildContainer()
{
    var builder = new ContainerBuilder();
    builder.RegisterType<TerminalLexerFactory>().As<ITerminalLexerFactory>().SingleInstance();
    builder.RegisterType<ValueRangeLexerFactory>().As<IValueRangeLexerFactory>().SingleInstance();
    builder.RegisterType<ConcatenationLexerFactory>().As<IConcatenationLexerFactory>().SingleInstance();
    builder.RegisterType<RepetitionLexerFactory>().As<IRepetitionLexerFactory>().SingleInstance();
    builder.RegisterType<AlternativeLexerFactory>().As<IAlternativeLexerFactory>().SingleInstance();
    builder.RegisterType<OptionLexerFactory>().As<IOptionLexerFactory>().SingleInstance();
    builder.RegisterType<SignLexerFactory>().As<ILexerFactory<Sign>>().SingleInstance();
    builder.RegisterType<DigitLexerFactory>().As<ILexerFactory<Digit>>().SingleInstance();
    builder.RegisterType<IntegerLexerFactory>().As<ILexerFactory<Integer>>().SingleInstance();

    // With all dependencies wired up, register a delegate that creates a new IntegerLexer
    builder.Register(
        ctx =>
        {
            var integerLexerFactory = ctx.Resolve<ILexerFactory<Integer>>();
            return integerLexerFactory.Create();
        })
        .As<ILexer<Integer>>()
        .SingleInstance();
    return builder.Build();
}
```

The final reader object is instantiated by AutoFac.

```c#
ILexer<Integer> integerLexer;
using (var container = BuildContainer())
{
    integerLexer = container.Resolve<ILexer<Integer>>();
}
```

This `integerLexer` object has a `Read()` method that reads integers from a text source. A text source can be any class that implements `ITextSource`.

TextFx includes `ITextSource` implementations for `System.String` or `System.IO.Stream`.

StringTextSource:
```c#
string input = "123";
using (ITextSource textSource = new StringTextSource(input))
using (ITextScanner textScanner = new TextScanner(textSource))
{
    ReadResult<Integer> readResult = integerLexer.Read(textScanner);
}
```
StreamTextSource:
```c#
using (Stream fileStream = File.OpenRead("input.txt"))
using (PushbackInputStream inputStream = new PushbackInputStream(fileStream))
using (ITextSource textSource = new StreamTextSource(inputStream, Encoding.UTF8))
using (ITextScanner textScanner = new TextScanner(textSource))
{
    ReadResult<Integer> readResult = integerLexer.Read(textScanner);
}
```

The `PushbackInputStream` wrapper class exists to enable support for forward-only streams such as `System.Net.Sockets.NetworkStream`. When seeking is not supported, the `Write(...)` method can be used to write bytes to a pushback buffer. The next time the `Read(...)` method is called, bytes are read from the buffer instead of the underlying stream.


```c#
ReadResult<Integer> readResult = integerLexer.Read(textScanner);
```

The `ReadResult<>` object contains properties that describe the read operations:
 - `Success` indicates whether the read operation succeeded
 - `Element` contains the `Integer` object if `Success` is `true`
 - `EndOfInput` indicates whether enough characters were available before the end of input
 - `Text` contains the matched text.
    - If `Success` is `false` then this is only a partial match.
 - `ErrorText` contains the mismatched text if `Success` is `false` and `EndOfInput` is `false`
