using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WordBrain.Tests
{
    [TestClass]
    public class SequenceTests
    {
        public static Sequence CreateSequence(Puzzle? puzzle = null, WordTree? wordTree = null) => new Sequence(puzzle ?? CreatePuzzle(), wordTree ?? CreateWordTree());

        private static Puzzle CreatePuzzle(char?[][]? letters = null, int[]? lengths = null) => new Puzzle(letters ?? CreateLetters(), lengths ?? CreateLengths());

        private static char?[][] CreateLetters() => new[]
        {
            new char?[] { 'F', 'O', 'O' },
            new char?[] { 'R', 'A', 'Z' },
            new char?[] { 'B', 'A', 'B' }
        };

        private static int[] CreateLengths() => new[] { 3, 3, 3 };

        public static WordTree CreateWordTree(IEnumerable<string>? words = null) => new WordTree(words ?? CreateWords());

        public static IEnumerable<string> CreateWords() => new[] { "oar" };

        [TestMethod]
        public void Length_WhenCreated_ReturnsZero()
        {
            // Arrange
            var sequence = CreateSequence();

            // Act
            int result = sequence.Length;

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Length_WhenExtended_ReturnsCorrectValue()
        {
            // Arrange
            Sequence sequence = CreateSequence().Extend().First().Extend().First().Extend().First();

            // Act
            int result = sequence.Length;

            // Assert
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void GetSquares_WhenCreated_ReturnsEmpty()
        {
            // Arrange
            var sequence = CreateSequence();

            // Act
            var result = sequence.GetSquares();

            // Assert
            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void GetSquares_WhenExtended_ReturnsCorrectValue()
        {
            // Arrange
            Sequence sequence = CreateSequence().Extend().First().Extend().First().Extend().First();

            // Act
            var result = sequence.GetSquares().ToList();

            // Assert
            CollectionAssert.AreEqual(new[] { (0, 1), (1, 1), (1, 0) }, result);
        }

        [TestMethod]
        public void Extend_WhenCreated_ReturnsSequenceForEachValidSquare()
        {
            // Arrange
            var puzzle = CreatePuzzle();
            var wordTree = CreateWordTree(new[] { "far", "r", "zoo" });
            var sequence = CreateSequence(puzzle, wordTree);

            // Act
            var result = sequence.Extend().ToList();

            // Assert
            int index = 0;
            CollectionAssert.AreEqual(new[] { (0, 0) }, result[index++].GetSquares().ToList()); // F
            CollectionAssert.AreEqual(new[] { (1, 0) }, result[index++].GetSquares().ToList()); // R
            CollectionAssert.AreEqual(new[] { (1, 2) }, result[index++].GetSquares().ToList()); // Z
            Assert.AreEqual(index, result.Count);
        }

        [TestMethod]
        public void Extend_WhenExtended_ReturnsSequenceForEachUnvisitedNeighbour()
        {
            // Arrange
            var puzzle = CreatePuzzle();
            var wordTree = CreateWordTree(new[] { "far", "fob" });
            var sequence = CreateSequence(puzzle, wordTree).Extend().First();

            // Act
            var result = sequence.Extend().ToList();

            // Assert
            int index = 0;
            CollectionAssert.AreEqual(new[] { (0, 0), (0, 1) }, result[index++].GetSquares().ToList()); // FO
            CollectionAssert.AreEqual(new[] { (0, 0), (1, 1) }, result[index++].GetSquares().ToList()); // FA
            Assert.AreEqual(index, result.Count);
        }

        [TestMethod]
        public void TryPlay_WhenNotAWord_SetsPuzzleToNullAndReturnsFalse()
        {
            // Arrange
            var sequence = CreateSequence();

            // Act
            bool result = sequence.TryPlay(out Puzzle? outPuzzle);

            // Assert
            Assert.IsFalse(result);
            Assert.IsNull(outPuzzle);
        }

        [TestMethod]
        public void TryPlay_WhenNotValidLength_SetsPuzzleToNullAndReturnsFalse()
        {
            // Arrange
            WordTree wordTree = CreateWordTree(new[] { "b" });
            Sequence sequence = CreateSequence(null, wordTree).Extend().First();

            // Act
            bool result = sequence.TryPlay(out Puzzle? outPuzzle);

            // Assert
            Assert.IsFalse(result);
            Assert.IsNull(outPuzzle);
        }

        [TestMethod]
        public void TryPlay_WhenValid_SetsPuzzleAndReturnsTrue()
        {
            // Arrange
            Sequence sequence = CreateSequence().Extend().First().Extend().First().Extend().First();

            // Act
            bool result = sequence.TryPlay(out Puzzle? outPuzzle);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual($". . O{Environment.NewLine}F . Z{Environment.NewLine}B A B", outPuzzle!.Grid.ToString());
            Assert.AreEqual("OAR ___ ___", outPuzzle.Solution.ToString());
        }

        [TestMethod]
        public void ToString_WhenCreated_ReturnsEmpty()
        {
            // Arrange
            var sequence = CreateSequence();

            // Act
            string result = sequence.ToString();

            // Assert
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void ToString_WhenExtended_ReturnsCorrectValue()
        {
            // Arrange
            Sequence sequence = CreateSequence().Extend().First().Extend().First().Extend().First();

            // Act
            string result = sequence.ToString();

            // Assert
            Assert.AreEqual("OAR", result);
        }
    }
}
