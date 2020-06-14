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

    Read 61,442 words in 00:00:00.0944764

    S L L Y
    H A U E
    I C T N
    P A O N
    ____ ________ ____

    ACTUALLY SHIP NONE

    Found 1 solutions in 00:00:00.1002846

The command `WordBrain 3of6game.txt EZEAI UICRD LRHAD AEAEO VCTES 5 4 4 12` produces the following output:

    Read 61,442 words in 00:00:00.0963334

    E Z E A I
    U I C R D
    L R H A D
    A E A E O
    V C T E S
    _____ ____ ____ ____________

    ODES CHARACTERIZE AIDE VALUE
    ODES CHARACTERIZE IDEA VALUE
    DOES CHARACTERIZE AIDE VALUE
    DOES CHARACTERIZE IDEA VALUE
    VALUE DOSE CHARACTERIZE AIDE
    VALUE DOSE CHARACTERIZE IDEA

    Found 6 solutions in 00:00:11.8792028

The command `WordBrain 3of6game.txt ....AT .F..LL .LMTLI TODIUE TAPNAR ETOSMB 7 6 4 8 3` produces the following output:

    Read 61,442 words in 00:00:00.0935246

    . . . . A T
    . F . . L L
    . L M T L I
    T O D I U E
    T A P N A R
    E T O S M B
    _______ ______ ____ ________ ___

    SANDPIT TOMATO FELT UMBRELLA TIL
    SANDPIT TOMATO FELT UMBRELLA LIT
    SANDPIT TOMATO UMBRELLA TIL LEFT
    SANDPIT TOMATO UMBRELLA LEFT LIT

    Found 4 solutions in 00:00:49.0017859
