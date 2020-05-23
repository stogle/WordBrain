using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WordBrain.Tests
{
    [TestClass]
    public class SolutionTests
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
        public void IsComplete_WhenIsNotComplete_ReturnsFalse()
        {
            // Arrange
            Puzzle puzzle = CreatePuzzle();

            // Act
            bool result = puzzle.Solution.IsComplete;

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsComplete_WhenIsComplete_ReturnsFalse()
        {
            // Arrange
            Puzzle puzzle = CreatePuzzle(Array.Empty<char?[]>(), Array.Empty<int>());

            // Act
            bool result = puzzle.Solution.IsComplete;

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ToString_WhenTryPlayHasNotBeenCalled_ReturnsCorrectValue()
        {
            // Arrange
            Puzzle puzzle = CreatePuzzle();

            // Act
            string result = puzzle.Solution.ToString();

            // Assert
            Assert.AreEqual("__ ___ ____", result);
        }
    }
}
