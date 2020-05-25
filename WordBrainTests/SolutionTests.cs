using System;
using System.Linq;
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
        public void IsComplete_WhenLengthsIsNotEmpty_ReturnsFalse()
        {
            // Arrange
            Puzzle puzzle = CreatePuzzle();

            // Act
            bool result = puzzle.Solution.IsComplete;

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsComplete_WhenLengthsIsEmpty_ReturnsTrue()
        {
            // Arrange
            Puzzle puzzle = CreatePuzzle(Array.Empty<char?[]>(), Array.Empty<int>());

            // Act
            bool result = puzzle.Solution.IsComplete;

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Words_WhenCreated_ReturneCorrectValue()
        {
            // Arrange
            Puzzle puzzle = CreatePuzzle();

            // Act
            var result = puzzle.Solution.Words.ToList();

            // Assert
            CollectionAssert.AreEquivalent(new[] { "__", "___", "____" }, result);
        }

        [TestMethod]
        public void Words_WhenPartiallySolved_ReturneCorrectValue()
        {
            // Arrange
            Puzzle puzzle = CreatePuzzle();
            WordTree wordTree = new WordTree(new[] { "BED" });
            Sequence sequence = new Sequence(puzzle, wordTree).Extend().First().Extend().First().Extend().First();
            sequence.TryPlay(out puzzle!);

            // Act
            var result = puzzle.Solution.Words.ToList();

            // Assert
            CollectionAssert.AreEquivalent(new[] { "__", "BED", "____" }, result);
        }

        [TestMethod]
        public void ToString_WhenCreated_ReturnsCorrectValue()
        {
            // Arrange
            Puzzle puzzle = CreatePuzzle();

            // Act
            string result = puzzle.Solution.ToString();

            // Assert
            Assert.AreEqual("__ ___ ____", result);
        }

        [TestMethod]
        public void ToString_WhenPartiallySolved_ReturneCorrectValue()
        {
            // Arrange
            Puzzle puzzle = CreatePuzzle();
            WordTree wordTree = new WordTree(new[] { "BED" });
            Sequence sequence = new Sequence(puzzle, wordTree).Extend().First().Extend().First().Extend().First();
            sequence.TryPlay(out puzzle!);

            // Act
            var result = puzzle.Solution.ToString();

            // Assert
            Assert.AreEqual("BED __ ____", result);
        }
    }
}
