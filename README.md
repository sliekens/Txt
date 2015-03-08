Structured Language (SLANG)
===========
[![Build status](https://ci.appveyor.com/api/projects/status/01jxm32c7i9769ef?svg=true)](https://ci.appveyor.com/project/StevenLiekens/text-parser)

# About
This repository contains library classes that deal with structured languages.

Examples of structured languages include...

* Programming languages
    * C#, JavaScript, SQL
* Data interchange formats
    * XML, JSON
* Text-based protocols
    * HTTP, FTP


Background and Goals
===========

All structured languages have one thing in common: each language has a formal syntax specification.
These specifications define a set of grammar rules for the language. Programmers use these grammar rules to create programs that split structured text into its core components before actually processing the text.

Today, grammars are most commonly defined in what is called Augmented BNF (ABNF) notation. The ABNF specification itself defines a set of core grammar rules, which other specifications can then use to define a formal syntax for their language.

An example that is commonly used is the formal syntax specification for integer numbers. An integer has one or more digits. A digit can be any decimal digit, and is a core ABNF rule.

```abnf
INT   = 1*DIGIT                      ; INT is a custom rule, invented for this demonstration
DIGIT = "0" / "1" / "2" / "3" / "4"  ; DIGIT is a core ABNF rule
        / "5" / "6" / "7" /"8" / "9"
```



The goal of this project is to provide a set of base classes that you can use to implement your own grammar parsers.

See also:
* RFC 5234: [Augmented BNF for Syntax Specifications](https://tools.ietf.org/html/rfc5234)

Important: there is currently no support for automatically generating parsers. The idea here is that if you want to do it properly, then do it yourself.