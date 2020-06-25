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
            new char?[] { 'F', 'O', 'O' },
            new char?[] { 'R', 'A', 'Z' },
            new char?[] { 'B', 'A', 'B' }
        };

        private static int[] CreateLengths() => new[] { 3, 3, 3 };

        [TestMethod]
        public void NextLength_WhenLengthsIsNotEmpty_ReturnsCorrectValue()
        {
            // Arrange
            Puzzle puzzle = CreatePuzzle();

            // Act
            int? result = puzzle.Solution.NextLength;

            // Assert
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void NextLength_WhenLengthsIsEmpty_ReturnsNull()
        {
            // Arrange
            Puzzle puzzle = CreatePuzzle(Array.Empty<char?[]>(), Array.Empty<int>());

            // Act
            int? result = puzzle.Solution.NextLength;

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Words_WhenCreated_ReturnsCorrectValue()
        {
            // Arrange
            Puzzle puzzle = CreatePuzzle();

            // Act
            var result = puzzle.Solution.Words.ToList();

            // Assert
            CollectionAssert.AreEquivalent(new[] { "___", "___", "___" }, result);
        }

        [TestMethod]
        public void Words_WhenPartiallySolved_ReturnsCorrectValue()
        {
            // Arrange
            Puzzle puzzle = CreatePuzzle();
            WordTree wordTree = new WordTree(new[] { "OAR" });
            Sequence sequence = new Sequence(puzzle, wordTree).Extend().First().Extend().First().Extend().First();
            sequence.TryPlay(out puzzle!);

            // Act
            var result = puzzle.Solution.Words.ToList();

            // Assert
            CollectionAssert.AreEquivalent(new[] { "OAR", "___", "___" }, result);
        }

        [TestMethod]
        public void ToString_WhenCreated_ReturnsCorrectValue()
        {
            // Arrange
            Puzzle puzzle = CreatePuzzle();

            // Act
            string result = puzzle.Solution.ToString();

            // Assert
            Assert.AreEqual("___ ___ ___", result);
        }

        [TestMethod]
        public void ToString_WhenPartiallySolved_ReturneCorrectValue()
        {
            // Arrange
            Puzzle puzzle = CreatePuzzle();
            WordTree wordTree = new WordTree(new[] { "OAR" });
            Sequence sequence = new Sequence(puzzle, wordTree).Extend().First().Extend().First().Extend().First();
            sequence.TryPlay(out puzzle!);

            // Act
            var result = puzzle.Solution.ToString();

            // Assert
            Assert.AreEqual("OAR ___ ___", result);
        }
    }
}
