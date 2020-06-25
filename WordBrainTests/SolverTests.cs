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

        private static WordTree CreateWordTree(IEnumerable<string>? words = null) => new WordTree(words ?? new[] { "actually", "bar", "boa", "boo", "fab", "far", "fob", "for", "none", "oar", "orb", "ship", "zoo" });

        private static Puzzle CreatePuzzle(char?[][]? letters = null, int[]? lengths = null) => new Puzzle(letters ?? CreateLetters(), lengths ?? CreateLengths());

        private static char?[][] CreateLetters() => new[]
        {
            new char?[] { 'F', 'O', 'O' },
            new char?[] { 'R', 'A', 'Z' },
            new char?[] { 'B', 'A', 'B' }
        };

        private static int[] CreateLengths() => new[] { 3, 3, 3 };

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
            var exception = Assert.ThrowsException<ArgumentNullException>(() => solver.Solve(puzzle).ToList());

            // Assert
            Assert.AreEqual("Value cannot be null. (Parameter 'puzzle')", exception.Message);
        }

        [TestMethod]
        public void Solve_WhenProgressIsNotNull_ReportsProgress()
        {
            // Arrange
            var solver = CreateSolver();
            var puzzle = CreatePuzzle();
            bool isProgressReported = false;
            var progress = new Progress<Solution>(solution => isProgressReported = true);

            // Act
            solver.Solve(puzzle, progress).ToList();

            // Assert
            Assert.IsTrue(isProgressReported);
        }

        [TestMethod]
        public void Solve_WhenPuzzleIs3x3_ReturnsSolutions()
        {
            // Arrange
            var solver = CreateSolver();
            var puzzle = CreatePuzzle();

            // Act
            var result = solver.Solve(puzzle).ToList();

            // Assert
            Assert.AreEqual(1, result.Count);
            CollectionAssert.AreEquivalent(new[] { "BAR", "ZOO", "FAB" }, result[0].Words.ToList());
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
            var result = solver.Solve(puzzle).ToList();

            // Assert
            Assert.AreEqual(1, result.Count);
            CollectionAssert.AreEqual(new[] { "SHIP", "ACTUALLY", "NONE" }, result[0].Words.ToList());
        }
    }
}
