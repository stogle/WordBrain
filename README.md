![.NET Core](https://github.com/stogle/WordBrain/workflows/.NET%20Core/badge.svg)
[![Coverage Status](https://coveralls.io/repos/github/stogle/WordBrain/badge.svg)](https://coveralls.io/github/stogle/WordBrain)
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE.md)

# WordBrain
Solves [WordBrain](https://www.maginteractive.com/games/wordbrain/) puzzles

## Usage
    Usage: WordBrain.Cli word_list line1 [line2 ...] length1 [length2 ...]
      word_list: The path to a file containing a list of valid words (one per line).
      line1, line2, etc.: The lines of the WordBrain grid. The grid must be rectangular. Use '.' for blanks.
      length1, length2, etc.: The lengths of the words in the solution. They must sum to the number of letters in the grid.

## Examples

The command `WordBrain.Cli 3of6game.txt SLLY HAUE ICTN PAON 4 8 4` produces the following output:

    Read 61,442 words in 00:00:00.0863478

    S L L Y
    H A U E
    I C T N
    P A O N
    ____ ________ ____

    SHIP ACTUALLY NONE

    Found 1 solution(s) in 00:00:00.2341133

The command `WordBrain.Cli 3of6game.txt EZEAI UICRD LRHAD AEAEO VCTES 5 4 4 12` produces the following output:

    Read 61,442 words in 00:00:00.0808263

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

    Found 6 solution(s) in 00:00:04.4479744

The command `WordBrain.Cli 3of6game.txt ECSWYM.. GAENSE.. ABRSUE.. RADAELRE EATOOIRT VOCMEOIT GUERTGVA IFEODPFA 8 8 6 8 6 8 6 8` produces the following output:

    Read 61,442 words in 00:00:00.0912933

    E C S W Y M . .
    G A E N S E . .
    A B R S U E . .
    R A D A E L R E
    E A T O O I R T
    V O C M E O I T
    G U E R T G V A
    I F E O D P F A
    ________ ________ ______ ________ ______ ________ ______ ________

    FAVORITE DATABASE ASSUME COVERAGE TARGET CEREMONY FIGURE WOODPILE

    Found 1 solution(s) in 00:00:02.8165989

The command `WordBrain.Cli 3of6game.txt ECSWYMRE GAENSESE ABRSUETL RADAELEC EATOOIRT VOCMEOIT GUERTGVA IFEODPFA 8 8 6 6 8 6 8 6 8` produces the following output:

    Read 61,442 words in 00:00:00.0884889

    E C S W Y M R E
    G A E N S E S E
    A B R S U E T L
    R A D A E L E C
    E A T O O I R T
    V O C M E O I T
    G U E R T G V A
    I F E O D P F A
    ________ ________ ______ ______ ________ ______ ________ ______ ________

    DATABASE COVERAGE SELECT ASSUME CEREMONY FIGURE FAVORITE TARGET WOODPILE
    COVERAGE DATABASE ELECTS FIGURE FAVORITE ASSUME CEREMONY TARGET WOODPILE

    Found 2 solution(s) in 00:02:16.9333207
