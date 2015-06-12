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


Consider the syntax specification for an integer number:


```abnf
DIGIT = "0" / "1" / "2" / "3" / "4"  ; DIGIT is a core ABNF rule
      / "5" / "6" / "7" / "8" / "9"

SIGN  = "+" / "-"                    ; "+" or "-"

INT   = [ SIGN ] 1*DIGIT             ; An optional sign, followed by 1 or more digits
```

An `INT` has an optional `SIGN`, followed by at least one `DIGIT`. A `SIGN` can be either `"+"` or `"-"`. A `DIGIT` can be any decimal digit, and is a core rule.

Important: there is currently no support for automatically generating parsers. The idea here is that if you want to do it properly, then do it yourself.