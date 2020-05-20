using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WordBrain.Tests
{
    [TestClass]
    public class PuzzleTests
    {
        private static Puzzle CreatePuzzle(Grid? grid = null, int[]? lengths = null) => new Puzzle(grid ?? CreateGrid(), lengths ?? CreateLengths());

        private static Grid CreateGrid(char?[][]? letters = null) => new Grid(letters ?? CreateLetters());

        private static char?[][] CreateLetters() => new[]
        {
            new char?[] { 'A', 'B', 'C' },
            new char?[] { 'D', 'E', 'F' },
            new char?[] { 'G', 'H', 'I' }
        };

        private static int[] CreateLengths() => new[] { 2, 3, 4 };

        [TestMethod]
        public void Constructor_WhenGridIsNull_ThrowsException()
        {
            // Arrange
            Grid grid = null!;

            // Act
            var exception = Assert.ThrowsException<ArgumentNullException>(() => new Puzzle(grid, CreateLengths()));

            // Assert
            Assert.AreEqual("Value cannot be null. (Parameter 'grid')", exception.Message);
        }

        [TestMethod]
        public void Constructor_WhenLengthsIsNull_ThrowsException()
        {
            // Arrange
            int[] lengths = null!;

            // Act
            var exception = Assert.ThrowsException<ArgumentNullException>(() => new Puzzle(CreateGrid(), lengths));

            // Assert
            Assert.AreEqual("Value cannot be null. (Parameter 'lengths')", exception.Message);
        }

        [TestMethod]
        public void Constructor_WhenLengthsAreNotAllPositive_ThrowsException()
        {
            // Arrange
            int[] lengths = { 2, 3, 0 };

            // Act
            var exception = Assert.ThrowsException<ArgumentException>(() => new Puzzle(CreateGrid(), lengths));

            // Assert
            Assert.AreEqual("Expected positive integer lengths. (Parameter 'lengths')", exception.Message);
        }

        [TestMethod]
        public void Constructor_WhenLengthsDoNotSumToNumberOfLetters_ThrowsException()
        {
            // Arrange
            int[] lengths = { 1, 2, 3, 4 };

            // Act
            var exception = Assert.ThrowsException<ArgumentException>(() => new Puzzle(CreateGrid(), lengths));

            // Assert
            Assert.AreEqual("Expected lengths to sum to the number of remaining letters. (Parameter 'lengths')", exception.Message);
        }

        [TestMethod]
        public void Constructor_WhenLengthsDoNotSumToNumberOfRemainingLetters_ThrowsException()
        {
            // Arrange
            char?[][] letters = { new char?[] { 'A', 'B', 'C' }, new char?[] { 'D', 'E', null }, new char?[] { 'G', 'H', 'I' } };
            int[] lengths = { 2, 3, 4 };

            // Act
            var exception = Assert.ThrowsException<ArgumentException>(() => new Puzzle(CreateGrid(letters), lengths));

            // Assert
            Assert.AreEqual("Expected lengths to sum to the number of remaining letters. (Parameter 'lengths')", exception.Message);
        }

        [TestMethod]
        public void Grid_AlwaysReturnsGrid()
        {
            // Arrange
            var grid = CreateGrid();
            var puzzle = CreatePuzzle(grid, null);

            // Act
            var result = puzzle.Grid;

            // Assert
            Assert.AreEqual(grid, result);
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
            var puzzle = CreatePuzzle(CreateGrid(letters), lengths);

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
