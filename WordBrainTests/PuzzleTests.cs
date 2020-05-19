using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WordBrain.Tests
{
    [TestClass]
    public class PuzzleTests
    {
        private static Puzzle CreatePuzzle(char?[][]? letters = null, int[]? lengths = null) =>
            new Puzzle(letters ?? CreateLetters(), lengths ?? CreateLengths());

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
        public void Constructor_WhenLengthsDoNotSumToNumberOfLetters_ThrowsException()
        {
            // Arrange
            int[] lengths = { 1, 2, 3, 4 };

            // Act
            var exception = Assert.ThrowsException<ArgumentException>(() => new Puzzle(CreateLetters(), lengths));

            // Assert
            Assert.AreEqual("Expected lengths to sum to the number of letters. (Parameter 'lengths')", exception.Message);
        }

        [TestMethod]
        public void Constructor_WhenLengthsDoNotSumToNumberOfNonBlankLetters_ThrowsException()
        {
            // Arrange
            char?[][] letters = { new char?[] { 'A', 'B', 'C' }, new char?[] { 'D', 'E', null }, new char?[] { 'G', 'H', 'I' } };
            int[] lengths = { 2, 3, 4 };

            // Act
            var exception = Assert.ThrowsException<ArgumentException>(() => new Puzzle(letters, lengths));

            // Assert
            Assert.AreEqual("Expected lengths to sum to the number of letters. (Parameter 'lengths')", exception.Message);
        }

        [TestMethod]
        public void Height_Always_ReturnsHeight()
        {
            // Arrange
            var letters = CreateLetters();
            var puzzle = CreatePuzzle(letters);

            // Act
            int result = puzzle.Height;

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
            int result = puzzle.Width;

            // Assert
            Assert.AreEqual(letters[0].Length, result);
        }

        [TestMethod]
        public void Indexer_Always_ReturnsCharactersFromLetters()
        {
            // Arrange
            var letters = CreateLetters();
            var puzzle = CreatePuzzle(letters);
            string[] expected = letters.Select(row => string.Concat(row)).ToArray(); // Combine each row into a string

            // Act
            var result = Enumerable.Range(0, puzzle.Height).SelectMany(i => Enumerable.Range(0, puzzle.Width).Select(j => letters[i][j] == puzzle[i, j]));

            // Assert
            Assert.IsTrue(result.All(b => b));
        }

        [TestMethod]
        public void Lengths_Always_ReturnsLengths()
        {
            // Arrange
            var lengths = CreateLengths();
            var puzzle = CreatePuzzle(null, lengths);

            // Act
            var result = puzzle.Lengths;

            // Assert
            CollectionAssert.AreEqual(lengths, result);
        }

        [TestMethod]
        public void ToString_WhenNoBlankLetters_ReturnsCorrectFormat()
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
        public void ToString_WhenBlankLetters_ReturnsCorrectFormat()
        {
            // Arrange
            char?[][] letters = { new char?[] { null, null, 'C' }, new char?[] { 'A', null, 'F' }, new char?[] { 'G', 'B', 'I' } };
            int[] lengths = { 2, 4 };
            var puzzle = CreatePuzzle(letters, lengths);

            // Act
            string result = puzzle.ToString();

            // Assert
            Assert.AreEqual($". . C{Environment.NewLine}" +
                $"A . F{Environment.NewLine}" +
                $"G B I{Environment.NewLine}" +
                $"__ ____{Environment.NewLine}", result);
        }
    }
}
