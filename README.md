![.NET Core](https://github.com/stogle/WordBrain/workflows/.NET%20Core/badge.svg)
[![Coverage Status](https://coveralls.io/repos/github/stogle/WordBrain/badge.svg)](https://coveralls.io/github/stogle/WordBrain)
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE.md)

# WordBrain
Solves [WordBrain](https://www.maginteractive.com/games/wordbrain/) puzzles

## Usage
    Usage: WordBrain word_list line1 [line2 ...] length1 [length2 ...]
      word_list: The path to a file containing a list of valid words (one per line).
      line1, line2, etc.: The lines of the WordBrain grid. The grid must be square. Use '.' for blanks.
      length1, length2, etc.: The lengths of the words in the solution. They must sum to the number of letters in the grid.

## Examples

The command `WordBrain 3of6game.txt SLLY HAUE ICTN PAON 4 8 4` produces the following output:

    Read 61,442 words in 00:00:00.1030221

    S L L Y
    H A U E
    I C T N
    P A O N
    ____ ________ ____

    ACTUALLY SHIP NONE

    Found 1 solutions in 00:00:00.1116093

The command `WordBrain 3of6game.txt EZEAI UICRD LRHAD AEAEO VCTES 5 4 4 12` produces the following output:

    Read 61,442 words in 00:00:00.0933638

    E Z E A I
    U I C R D
    L R H A D
    A E A E O
    V C T E S
    _____ ____ ____ ____________

    DOES CHARACTERIZE AIDE VALUE
    DOES CHARACTERIZE IDEA VALUE
    VALUE DOSE CHARACTERIZE AIDE
    VALUE DOSE CHARACTERIZE IDEA
    VALUE ODES CHARACTERIZE AIDE
    VALUE ODES CHARACTERIZE IDEA

    Found 6 solutions in 00:00:12.7250664

The command `WordBrain 3of6game.txt G....T AF..AL PL..LI TO..EE TAM.UR ETO.MB 6 4 8 3 3` produces the following output:

    Read 61,442 words in 00:00:00.0983899

    G . . . . T
    A F . . A L
    P L . . L I
    T O . . E E
    T A M . U R
    E T O . M B
    ______ ____ ________ ___ ___

    GAP TOMATO FELT UMBRELLA TIE
    GAP TOMATO UMBRELLA TIE LEFT

    Found 2 solutions in 00:00:16.0833299
