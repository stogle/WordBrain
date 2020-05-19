using WordBrain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace WordBrain.Tests
{
    [TestClass]
    public class MoveTests
    {
        public static Move CreateMove(Puzzle? puzzle = null) => new Move(puzzle ?? CreatePuzzle());

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
        public void Constructor_WhenPuzzleIsNull_ThrowsException()
        {
            // Arrange
            Puzzle puzzle = null!;

            // Act
            var exception = Assert.ThrowsException<ArgumentNullException>(() => new Move(puzzle));

            // Assert
            Assert.AreEqual("Value cannot be null. (Parameter 'puzzle')", exception.Message);

        }

        [TestMethod]
        public void Count_Initially_ReturnsZero()
        {
            // Arrange
            var play = CreateMove();

            // Act
            int result = play.Count;

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Count_AfterPush_ReturnsOne()
        {
            // Arrange
            var play = CreateMove();
            play.Push(0, 0);

            // Act
            int result = play.Count;

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Count_AfterPushAndPop_ReturnsZero()
        {
            // Arrange
            var play = CreateMove();
            play.Push(0, 0);
            play.Pop();

            // Act
            int result = play.Count;

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Add_WhenIIsOutOfBounds_ThrowsException()
        {
            // Arrange
            var play = CreateMove();
            int i = -1;

            // Act
            var exception = Assert.ThrowsException<ArgumentException>(() => play.Push(i, 0));

            // Assert
            Assert.AreEqual("Expected value in range 0..2. (Parameter 'i')", exception.Message);
        }

        [TestMethod]
        public void Add_WhenJIsOutOfBounds_ThrowsException()
        {
            // Arrange
            var play = CreateMove();
            int j = 3;

            // Act
            var exception = Assert.ThrowsException<ArgumentException>(() => play.Push(0, j));

            // Assert
            Assert.AreEqual("Expected value in range 0..2. (Parameter 'j')", exception.Message);
        }

        [TestMethod]
        public void Add_WhenSquareIsNotAdjacent_ThrowsException()
        {
            // Arrange
            var play = CreateMove();
            play.Push(0, 0);

            // Act
            var exception = Assert.ThrowsException<InvalidOperationException>(() => play.Push(0, 2));

            // Assert
            Assert.AreEqual("Cannot push a non-adjacent square.", exception.Message);
        }

        [TestMethod]
        public void RemoveLast_WhenCountIsZero_ThrowsException()
        {
            // Arrange
            var play = CreateMove();

            // Act
            var exception = Assert.ThrowsException<InvalidOperationException>(() => play.Pop());

            // Assert
            Assert.AreEqual("Cannot pop from an empty move.", exception.Message);
        }

        [TestMethod]
        public void Play_WhenWordIsNotValidLength_ReturnsNull()
        {
            // Arrange
            var move = CreateMove();

            // Act
            var exception = Assert.ThrowsException<InvalidOperationException>(() => move.Play());

            // Assert
            Assert.AreEqual("Cannot play a move of this length.", exception.Message);
        }

        [TestMethod]
        public void Play_WhenWordIsValid_ReturnsPuzzle()
        {
            // Arrange
            char?[][] letters = { new char?[] { 'A', 'B', 'C' }, new char?[] { 'D', 'E', 'F' }, new char?[] { 'G', 'H', 'I' } };
            int[] lengths = { 2, 3, 4 };
            var puzzle = CreatePuzzle(letters, lengths);
            var move = CreateMove(puzzle);
            move.Push(1, 0);
            move.Push(1, 1);
            move.Push(2, 1);
            char?[][] expected = { new char?[] { null, null, 'C' }, new char?[] { 'A', null, 'F' }, new char?[] { 'G', 'B', 'I' } };

            // Act
            Puzzle result = move.Play();

            // Assert
            Assert.AreEqual(puzzle.Height, result!.Height);
            Assert.AreEqual(puzzle.Width, result.Width);
            Assert.IsTrue(Enumerable.Range(0, puzzle.Height).SelectMany(i => Enumerable.Range(0, puzzle.Width).Select(j => expected[i][j] == result[i, j])).All(b => b));
            CollectionAssert.AreEqual(new[] { 2, 4 }, result.Lengths);
        }
    }
}
