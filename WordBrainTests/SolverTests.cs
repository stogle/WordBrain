using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WordBrain.Tests
{
    [TestClass]
    public class SolverTests
    {
        private static Solver CreateSolver(WordTree? wordTree = null) => new Solver(wordTree ?? CreateWordTree());

        private static WordTree CreateWordTree(IEnumerable<string>? words = null) => new WordTree(words ?? new[] { "actually", "do", "does", "dot", "dots", "he", "hot", "none", "see", "seed", "set", "she", "ship", "test", "the" });

        private static Puzzle CreatePuzzle(char?[][]? letters = null, int[]? lengths = null) => new Puzzle(letters ?? CreateLetters(), lengths ?? CreateLengths());

        private static char?[][] CreateLetters() => new[]
        {
            new char?[] { 'D', 'O', 'T' },
            new char?[] { 'T', 'E', 'H' },
            new char?[] { 'E', 'S', 'T' }
        };

        private static int[] CreateLengths() => new[] { 2, 3, 4 };

        [TestMethod]
        public void Constructor_WhenWordTreeIsNull_ThrowsException()
        {
            // Arrange
            WordTree wordTree = null!;

            // Act
            var exception = Assert.ThrowsException<ArgumentNullException>(() => new Solver(wordTree));

            // Assert
            Assert.AreEqual("Value cannot be null. (Parameter 'wordTree')", exception.Message);

        }

        [TestMethod]
        public void Solve_WhenPuzzleIsNull_ThrowsException()
        {
            // Arrange
            var solver = CreateSolver();
            Puzzle puzzle = null!;

            // Act
            var exception = Assert.ThrowsException<ArgumentNullException>(() => solver.Solve(puzzle));

            // Assert
            Assert.AreEqual("Value cannot be null. (Parameter 'puzzle')", exception.Message);

        }

        [TestMethod]
        public void Solve_WhenPuzzleIs3x3_ReturnsSolutions()
        {
            // Arrange
            var solver = CreateSolver();
            var puzzle = CreatePuzzle();

            // Act
            string[] result = solver.Solve(puzzle).ToArray();

            // Assert
            CollectionAssert.AreEquivalent(new[] { "DO THE TEST", "HE DOT TEST" }, result);
        }

        [TestMethod]
        public void Solve_WhenPuzzleIs4x4_ReturnsSolutions()
        {
            // Arrange
            var solver = CreateSolver();
            char?[][] letters = new[] { new char?[] { 'S', 'L', 'L', 'Y' }, new char?[] { 'H', 'A', 'U', 'E' }, new char?[] { 'I', 'C', 'T', 'N' }, new char?[] { 'P', 'A', 'O', 'N' } };
            int[] lengths = { 4, 8, 4 };
            var puzzle = CreatePuzzle(letters, lengths);

            // Act
            string[] result = solver.Solve(puzzle).ToArray();

            // Assert
            CollectionAssert.AreEquivalent(new[] { "SHIP ACTUALLY NONE", "NONE ACTUALLY SHIP" }, result);
        }
    }
}
