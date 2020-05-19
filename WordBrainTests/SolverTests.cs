using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace WordBrain.Tests
{
    [TestClass()]
    public class SolverTests
    {
        private static Solver CreateSolver() => new Solver(CreateWordTree());

        private static WordTree CreateWordTree() => new WordTree(new[] { "do", "does", "dot", "dots", "he", "hot", "see", "seed", "set", "she", "test", "the" });

        private static Puzzle CreatePuzzle(char?[][]? letters = null, int[]? lengths = null) =>
            new Puzzle(letters ?? CreateLetters(), lengths ?? CreateLengths());

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
        public void Solve_WhenSolutionExists_ReturnsSolutions()
        {
            // Arrange
            var solver = CreateSolver();
            var puzzle = CreatePuzzle();

            // Act
            string[] result = solver.Solve(puzzle).ToArray();

            // Assert
            CollectionAssert.AreEqual(new[] { "DO THE TEST", "HE DOT TEST" }, result);
        }
    }
}
