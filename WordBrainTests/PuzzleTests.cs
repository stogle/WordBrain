using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WordBrain.Tests
{
    [TestClass]
    public class PuzzleTests
    {
        private static Puzzle CreatePuzzle(Grid? grid = null, Solution? solution = null) => new Puzzle(grid ?? CreateGrid(), solution ?? CreateSolution());

        private static Grid CreateGrid(char?[][]? letters = null) => new Grid(letters ?? CreateLetters());

        private static char?[][] CreateLetters() => new[]
        {
            new char?[] { 'A', 'B', 'C' },
            new char?[] { 'D', 'E', 'F' },
            new char?[] { 'G', 'H', 'I' }
        };

        private static Solution CreateSolution(int[]? lengths = null) => new Solution(lengths ?? CreateLengths());

        private static int[] CreateLengths() => new[] { 2, 3, 4 };

        [TestMethod]
        public void Constructor_WhenGridIsNull_ThrowsException()
        {
            // Arrange
            Grid grid = null!;

            // Act
            var exception = Assert.ThrowsException<ArgumentNullException>(() => new Puzzle(grid, CreateSolution()));

            // Assert
            Assert.AreEqual("Value cannot be null. (Parameter 'grid')", exception.Message);
        }

        [TestMethod]
        public void Constructor_WhenSolutionIsNull_ThrowsException()
        {
            // Arrange
            Solution solution = null!;

            // Act
            var exception = Assert.ThrowsException<ArgumentNullException>(() => new Puzzle(CreateGrid(), solution));

            // Assert
            Assert.AreEqual("Value cannot be null. (Parameter 'solution')", exception.Message);
        }

        [TestMethod]
        public void Constructor_WhenRemainingLettersAreNotEqual_ThrowsException()
        {
            // Arrange
            int[] lengths = { 1, 2, 3, 4 };
            Solution solution = CreateSolution(lengths);

            // Act
            var exception = Assert.ThrowsException<ArgumentException>(() => new Puzzle(CreateGrid(), solution));

            // Assert
            Assert.AreEqual("Expected remaining letters in Solution and Grid to be equal. (Parameter 'solution')", exception.Message);
        }

        [TestMethod]
        public void Grid_AlwaysReturnsGrid()
        {
            // Arrange
            var grid = CreateGrid();
            var puzzle = CreatePuzzle(grid);

            // Act
            var result = puzzle.Grid;

            // Assert
            Assert.AreEqual(grid, result);
        }

        [TestMethod]
        public void Solution_Always_ReturnsSolution()
        {
            // Arrange
            var solution = CreateSolution();
            var puzzle = CreatePuzzle(null, solution);

            // Act
            var result = puzzle.Solution;

            // Assert
            Assert.AreEqual(solution, result);
        }

        [TestMethod]
        public void ToString_WhenTryPlayHasNotBeenCalled_ReturnsCorrectFormat()
        {
            // Arrange
            var puzzle = CreatePuzzle();

            // Act
            string result = puzzle.ToString();

            // Assert
            Assert.AreEqual($"A B C{Environment.NewLine}" +
                $"D E F{Environment.NewLine}" +
                $"G H I{Environment.NewLine}" +
                $"__ ___ ____{Environment.NewLine}", result);
        }

        [TestMethod]
        public void ToString_WhenTryPlayHasBeenCalled_ReturnsCorrectFormat()
        {
            // Arrange
            Puzzle puzzle = CreatePuzzle();
            puzzle.TryPlay(new[] { (1, 0), (1, 1), (2, 1) }, out puzzle!);

            // Act
            string result = puzzle.ToString();

            // Assert
            Assert.AreEqual($". . C{Environment.NewLine}" +
                $"A . F{Environment.NewLine}" +
                $"G B I{Environment.NewLine}" +
                $"__ DEH ____{Environment.NewLine}", result);
        }

        [TestMethod]
        public void TryPlay_WhenSequenceIsNull_ThrowsException()
        {
            // Arrange
            var puzzle = CreatePuzzle();
            IEnumerable<(int, int)> sequence = null!;

            // Act
            var exception = Assert.ThrowsException<ArgumentNullException>(() => puzzle.TryPlay(sequence, out _));

            // Assert
            Assert.AreEqual("Value cannot be null. (Parameter 'sequence')", exception.Message);
        }

        [TestMethod]
        public void TryPlay_WhenSequenceIsOutOfBounds_SetsPuzzleToNullAndReturnsFalse()
        {
            // Arrange
            var puzzle = CreatePuzzle();
            var sequence = new[] { (1, 0), (1, 1), (2, 1), (3, 1) };

            // Act
            bool result = puzzle.TryPlay(sequence, out Puzzle? outPuzzle);

            // Assert
            Assert.IsFalse(result);
            Assert.IsNull(outPuzzle);
        }

        [TestMethod]
        public void TryPlay_WhenSequenceIsValid_SetsPuzzleAndReturnsTrue()
        {
            // Arrange
            char?[][] letters = { new char?[] { 'A', 'B', 'C' }, new char?[] { 'D', 'E', 'F' }, new char?[] { 'G', 'H', 'I' } };
            int[] lengths = { 2, 3, 4 };
            Grid grid = CreateGrid(letters);
            var puzzle = CreatePuzzle(grid, CreateSolution(lengths));
            var sequence = new[] { (1, 0), (1, 1), (2, 1) };

            // Act
            bool result = puzzle.TryPlay(sequence, out Puzzle? outPuzzle);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual($". . C{Environment.NewLine}A . F{Environment.NewLine}G B I", outPuzzle!.Grid.ToString());
            Assert.AreEqual("__ DEH ____", outPuzzle.Solution.ToString());
        }
    }
}
