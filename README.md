![.NET Core](https://github.com/stogle/WordBrain/workflows/.NET%20Core/badge.svg)
[![Coverage Status](https://coveralls.io/repos/github/stogle/WordBrain/badge.svg)](https://coveralls.io/github/stogle/WordBrain)
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE.md)

# WordBrain
Solves [WordBrain](https://www.maginteractive.com/games/wordbrain/) puzzles

## Usage
    Usage: WordBrain word_list line1 [line2 ...] length1 [length2 ...]
      word_list: The path to a file containing a list of valid words (one per line).
      line1, line2, etc.: The lines of the WordBrain grid. The grid must be rectangular. Use '.' for blanks.
      length1, length2, etc.: The lengths of the words in the solution. They must sum to the number of letters in the grid.

## Examples

The command `WordBrain 3of6game.txt SLLY HAUE ICTN PAON 4 8 4` produces the following output:

    Read 61,442 words in 00:00:00.1048676

    S L L Y
    H A U E
    I C T N
    P A O N
    ____ ________ ____

    SHIP ACTUALLY NONE

    Found 1 solution(s) in 00:00:00.1183183

The command `WordBrain 3of6game.txt EZEAI UICRD LRHAD AEAEO VCTES 5 4 4 12` produces the following output:

    Read 61,442 words in 00:00:00.1094828

    E Z E A I
    U I C R D
    L R H A D
    A E A E O
    V C T E S
    _____ ____ ____ ____________

    VALUE DOES AIDE CHARACTERIZE
    VALUE DOES IDEA CHARACTERIZE
    VALUE DOSE AIDE CHARACTERIZE
    VALUE DOSE IDEA CHARACTERIZE
    VALUE ODES AIDE CHARACTERIZE
    VALUE ODES IDEA CHARACTERIZE

    Found 6 solution(s) in 00:00:04.6712986

The command `WordBrain 3of6game.txt G...AT AF..LL PLMTEI TODIUE TAPNAR ETOSMB 7 6 4 8 3 3` produces the following output:

    Read 61,442 words in 00:00:00.0928488

    G . . . A T
    A F . . L L
    P L M T E I
    T O D I U E
    T A P N A R
    E T O S M B
    _______ ______ ____ ________ ___ ___

    SANDPIT TOMATO FELT UMBRELLA GAP TIE
    SANDPIT TOMATO LEFT UMBRELLA GAP TIE

    Found 2 solution(s) in 00:00:00.3883658
