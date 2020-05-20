using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WordBrain.Tests
{
    [TestClass]
    public class GridTests
    {
        private static Grid CreateGrid(char?[][]? letters = null) => new Grid(letters ?? CreateLetters());

        private static char?[][] CreateLetters() => new[]
        {
            new char?[] { 'A', 'B', 'C' },
            new char?[] { 'D', 'E', 'F' },
            new char?[] { 'G', 'H', 'I' }
        };

        [TestMethod]
        public void Constructor_WhenLettersIsNull_ThrowsException()
        {
            // Arrange
            char?[][] letters = null!;

            // Act
            var exception = Assert.ThrowsException<ArgumentNullException>(() => new Grid(letters));

            // Assert
            Assert.AreEqual("Value cannot be null. (Parameter 'letters')", exception.Message);
        }

        [TestMethod]
        public void Constructor_WhenLettersAreNotRectangular_ThrowsException()
        {
            // Arrange
            char?[][] letters = { new char?[] { 'A', 'B', 'C' }, new char?[] { 'D', 'E' }, new char?[] { 'G', 'H', 'I' } };

            // Act
            var exception = Assert.ThrowsException<ArgumentException>(() => new Grid(letters));

            // Assert
            Assert.AreEqual("Expected letters to be rectangular. (Parameter 'letters')", exception.Message);
        }

        [TestMethod]
        public void Height_Always_ReturnsHeight()
        {
            // Arrange
            var letters = CreateLetters();
            var grid = CreateGrid(letters);

            // Act
            int result = grid.Height;

            // Assert
            Assert.AreEqual(letters.Length, result);
        }

        [TestMethod]
        public void Width_Always_ReturnsWidth()
        {
            // Arrange
            var letters = CreateLetters();
            var grid = CreateGrid(letters);

            // Act
            int result = grid.Width;

            // Assert
            Assert.AreEqual(letters[0].Length, result);
        }

        [TestMethod]
        public void Indexer_Always_ReturnsCharactersFromLetters()
        {
            // Arrange
            var letters = CreateLetters();
            var grid = CreateGrid(letters);

            // Act
            var result = Enumerable.Range(0, grid.Height).SelectMany(i => Enumerable.Range(0, grid.Width).Select(j => letters[i][j] == grid[i, j]));

            // Assert
            Assert.IsTrue(result.All(b => b));
        }

        [TestMethod]
        public void RemainingLetters_WhenNoBlankLetters_ReturnsCorrectValue()
        {
            // Arrange
            var grid = CreateGrid();

            // Act
            int result = grid.RemainingLetters;

            // Assert
            Assert.AreEqual(9, result);
        }

        [TestMethod]
        public void RemainingLetters_WhenBlankLetters_ReturnsCorrectValue()
        {
            // Arrange
            char?[][] letters = { new char?[] { null, null, 'C' }, new char?[] { 'A', null, 'F' }, new char?[] { 'G', 'B', 'I' } };
            var grid = CreateGrid(letters);

            // Act
            int result = grid.RemainingLetters;

            // Assert
            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void ToString_WhenNoBlankLetters_ReturnsCorrectFormat()
        {
            // Arrange
            var grid = CreateGrid();

            // Act
            string result = grid.ToString();

            // Assert
            Assert.AreEqual($"A B C{Environment.NewLine}D E F{Environment.NewLine}G H I", result);
        }

        [TestMethod]
        public void ToString_WhenBlankLetters_ReturnsCorrectFormat()
        {
            // Arrange
            char?[][] letters = { new char?[] { null, null, 'C' }, new char?[] { 'A', null, 'F' }, new char?[] { 'G', 'B', 'I' } };
            var grid = CreateGrid(letters);

            // Act
            string result = grid.ToString();

            // Assert
            Assert.AreEqual($". . C{Environment.NewLine}A . F{Environment.NewLine}G B I", result);
        }
    }
}
