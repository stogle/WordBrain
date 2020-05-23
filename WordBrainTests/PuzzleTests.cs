using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WordBrain.Tests
{
    [TestClass]
    public class PuzzleTests
    {
        private static Puzzle CreatePuzzle(char?[][]? letters = null, int[]? lengths = null) => new Puzzle(letters ?? CreateLetters(), lengths ?? CreateLengths());

        private static char?[][] CreateLetters() => new[]
        {
            new char?[] { 'A', 'B', 'C' },
            new char?[] { 'D', 'E', 'F' },
            new char?[] { 'G', 'H', 'I' }
        };

        private static int[] CreateLengths() => new[] { 2, 3, 4 };

        [TestMethod]
        public void Constructor_WhenLettersIsNull_ThrowsException()
        {
            // Arrange
            char?[][] letters = null!;

            // Act
            var exception = Assert.ThrowsException<ArgumentNullException>(() => new Puzzle(letters, CreateLengths()));

            // Assert
            Assert.AreEqual("Value cannot be null. (Parameter 'letters')", exception.Message);
        }

        [TestMethod]
        public void Constructor_WhenLettersAreNotRectangular_ThrowsException()
        {
            // Arrange
            char?[][] letters = { new char?[] { 'A', 'B', 'C' }, new char?[] { 'D', 'E' }, new char?[] { 'G', 'H', 'I' } };

            // Act
            var exception = Assert.ThrowsException<ArgumentException>(() => new Puzzle(letters, CreateLengths()));

            // Assert
            Assert.AreEqual("Expected letters to be rectangular. (Parameter 'letters')", exception.Message);
        }

        [TestMethod]
        public void Constructor_WhenLengthsIsNull_ThrowsException()
        {
            // Arrange
            int[] lengths = null!;

            // Act
            var exception = Assert.ThrowsException<ArgumentNullException>(() => new Puzzle(CreateLetters(), lengths));

            // Assert
            Assert.AreEqual("Value cannot be null. (Parameter 'lengths')", exception.Message);
        }

        [TestMethod]
        public void Constructor_WhenLengthsAreNotAllPositive_ThrowsException()
        {
            // Arrange
            int[] lengths = { 2, 3, 0 };

            // Act
            var exception = Assert.ThrowsException<ArgumentException>(() => new Puzzle(CreateLetters(), lengths));

            // Assert
            Assert.AreEqual("Expected positive integer lengths. (Parameter 'lengths')", exception.Message);
        }

        [TestMethod]
        public void Constructor_WhenRemainingLettersAreNotEqual_ThrowsException()
        {
            // Arrange
            int[] lengths = { 1, 2, 3, 4 };

            // Act
            var exception = Assert.ThrowsException<ArgumentException>(() => new Puzzle(CreateLetters(), lengths));

            // Assert
            Assert.AreEqual("Expected remaining letters in grid and solution to be equal. (Parameter 'lengths')", exception.Message);
        }

        [TestMethod]
        public void Grid_AlwaysReturnsGrid()
        {
            // Arrange
            var letters = CreateLetters();
            var puzzle = CreatePuzzle(letters);

            // Act
            var result = puzzle.Grid;

            // Assert
            Assert.AreEqual(letters.Length, result.Height);
            Assert.AreEqual(letters[0].Length, result.Width);
            for (int i = 0; i < letters.Length; i++)
            {
                for (int j = 0; j < letters[i].Length; j++)
                {
                    Assert.AreEqual(letters[i][j], result[i, j]);
                }
            }
        }

        [TestMethod]
        public void Solution_Always_ReturnsSolution()
        {
            // Arrange
            var lengths = CreateLengths();
            var puzzle = CreatePuzzle(null, lengths);

            // Act
            var result = puzzle.Solution;

            // Assert
            Assert.AreEqual(string.Join(' ', lengths.Select(n => new string('_', n))), result.ToString());
        }

        [TestMethod]
        public void ToString_WhenUnsolved_ReturnsCorrectFormat()
        {
            // Arrange
            var puzzle = CreatePuzzle();

            // Act
            string result = puzzle.ToString();

            // Assert
            Assert.AreEqual($"A B C{Environment.NewLine}D E F{Environment.NewLine}G H I{Environment.NewLine}" +
                $"__ ___ ____{Environment.NewLine}", result);
        }

        [TestMethod]
        public void ToString_WhenPartiallySolved_ReturnsCorrectFormat()
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
            Puzzle puzzle = CreatePuzzle();
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
