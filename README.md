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

The command `WordBrain nwl2018.txt SLLY HAUE ICTN PAON 4 8 4` produces the following output:

    Read 192,111 words in 00:00:00.4762284

    S L L Y
    H A U E
    I C T N
    P A O N
    ____ ________ ____

    SHIP ACTUALLY NONE

    Found 1 solutions in 00:00:00.1665678

The command `WordBrain nwl2018.txt EZEAI UICRD LRHAD AEAEO VCTES 5 4 4 12` produces the following output:

    Read 192,111 words in 00:00:00.4885625
    
    E Z E A I
    U I C R D
    L R H A D
    A E A E O
    V C T E S
    _____ ____ ____ ____________
    
    VALUE ADOS CHARACTERIZE EIDE
    VALUE DOES CHARACTERIZE AIDE
    VALUE DOES CHARACTERIZE IDEA
    VALUE DOSE CHARACTERIZE AIDE
    VALUE DOSE CHARACTERIZE IDEA
    VALUE ODEA CHARACTERIZE IDES
    VALUE ODES CHARACTERIZE AIDE
    VALUE ODES CHARACTERIZE IDEA
    VALUE SODA CHARACTERIZE EIDE
    
    Found 9 solutions in 00:01:42.0179057

The command `WordBrain nwl2018.txt G....T AF..AL PL..LI TO..EE TAM.UR ETO.MB 6 4 8 3 3` produces the following output:

    Read 192,111 words in 00:00:00.4621304
    
    G . . . . T
    A F . . A L
    P L . . L I
    T O . . E E
    T A M . U R
    E T O . M B
    ______ ____ ________ ___ ___
    
    GAP TOMATO FELT UMBRELLA TIE
    GAP TOMATO UMBRELLA TIE LEFT
    FAG ATOP MOTTLE UMBRELLA TIE
    
    Found 3 solutions in 00:01:32.9075473
