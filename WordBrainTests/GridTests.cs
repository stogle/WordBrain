using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WordBrain.Tests
{
    [TestClass]
    public class GridTests
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
        public void Height_Always_ReturnsHeight()
        {
            // Arrange
            var letters = CreateLetters();
            var puzzle = CreatePuzzle(letters);

            // Act
            int result = puzzle.Grid.Height;

            // Assert
            Assert.AreEqual(letters.Length, result);
        }

        [TestMethod]
        public void Width_Always_ReturnsWidth()
        {
            // Arrange
            var letters = CreateLetters();
            var puzzle = CreatePuzzle(letters);

            // Act
            int result = puzzle.Grid.Width;

            // Assert
            Assert.AreEqual(letters[0].Length, result);
        }

        [TestMethod]
        public void Indexer_Always_ReturnsCharactersFromLetters()
        {
            // Arrange
            var letters = CreateLetters();
            var puzzle = CreatePuzzle(letters);

            // Act
            var result = Enumerable.Range(0, puzzle.Grid.Height).SelectMany(i => Enumerable.Range(0, puzzle.Grid.Width).Select(j => letters[i][j] == puzzle.Grid[i, j]));

            // Assert
            Assert.IsTrue(result.All(b => b));
        }

        [TestMethod]
        public void ToString_WhenNoBlankLetters_ReturnsCorrectFormat()
        {
            // Arrange
            var puzzle = CreatePuzzle();

            // Act
            string result = puzzle.Grid.ToString();

            // Assert
            Assert.AreEqual($"A B C{Environment.NewLine}D E F{Environment.NewLine}G H I", result);
        }

        [TestMethod]
        public void ToString_WhenBlankLetters_ReturnsCorrectFormat()
        {
            // Arrange
            char?[][] letters = { new char?[] { null, null, 'C' }, new char?[] { 'A', null, 'F' }, new char?[] { 'G', 'B', 'I' } };
            int[] lengths = { 2, 4 };
            var puzzle = CreatePuzzle(letters, lengths);

            // Act
            string result = puzzle.Grid.ToString();

            // Assert
            Assert.AreEqual($". . C{Environment.NewLine}A . F{Environment.NewLine}G B I", result);
        }
    }
}
