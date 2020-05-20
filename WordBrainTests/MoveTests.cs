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
            var move = CreateMove();

            // Act
            int result = move.Count;

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Count_AfterPush_ReturnsOne()
        {
            // Arrange
            var move = CreateMove();
            move.Push(0, 0);

            // Act
            int result = move.Count;

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Count_AfterPushAndPop_ReturnsZero()
        {
            // Arrange
            var move = CreateMove();
            move.Push(0, 0);
            move.Pop();

            // Act
            int result = move.Count;

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Add_WhenIIsOutOfBounds_ThrowsException()
        {
            // Arrange
            var move = CreateMove();
            int i = -1;

            // Act
            var exception = Assert.ThrowsException<ArgumentException>(() => move.Push(i, 0));

            // Assert
            Assert.AreEqual("Expected value in range 0..2. (Parameter 'i')", exception.Message);
        }

        [TestMethod]
        public void Add_WhenJIsOutOfBounds_ThrowsException()
        {
            // Arrange
            var move = CreateMove();
            int j = 3;

            // Act
            var exception = Assert.ThrowsException<ArgumentException>(() => move.Push(0, j));

            // Assert
            Assert.AreEqual("Expected value in range 0..2. (Parameter 'j')", exception.Message);
        }

        [TestMethod]
        public void Add_WhenSquareIsNotAdjacent_ThrowsException()
        {
            // Arrange
            var move = CreateMove();
            move.Push(0, 0);

            // Act
            var exception = Assert.ThrowsException<InvalidOperationException>(() => move.Push(0, 2));

            // Assert
            Assert.AreEqual("Cannot push a non-adjacent square.", exception.Message);
        }

        [TestMethod]
        public void RemoveLast_WhenCountIsZero_ThrowsException()
        {
            // Arrange
            var move = CreateMove();

            // Act
            var exception = Assert.ThrowsException<InvalidOperationException>(() => move.Pop());

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
            Grid grid = CreateGrid(letters);
            var puzzle = CreatePuzzle(grid, lengths);
            var move = CreateMove(puzzle);
            var sequence = new[] { (1, 0), (1, 1), (2, 1) };
            move.Push(sequence[0].Item1, sequence[0].Item2);
            move.Push(sequence[1].Item1, sequence[1].Item2);
            move.Push(sequence[2].Item1, sequence[2].Item2);
            Grid expected = grid.Play(sequence);

            // Act
            Puzzle result = move.Play();

            // Assert
            Assert.AreEqual(puzzle.Grid.Height, result!.Grid.Height);
            Assert.AreEqual(puzzle.Grid.Width, result.Grid.Width);
            Assert.IsTrue(Enumerable.Range(0, puzzle.Grid.Height).SelectMany(i => Enumerable.Range(0, puzzle.Grid.Width).Select(j => expected[i, j] == result.Grid[i, j])).All(b => b));
            CollectionAssert.AreEqual(new[] { 2, 4 }, result.Lengths);
        }
    }
}
