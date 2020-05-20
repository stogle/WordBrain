using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WordBrain.Tests
{
    [TestClass]
    public class ArgumentsTests
    {
        public static Arguments CreateArguments(string[]? args = null, string? args0 = null) => new Arguments(args ?? CreateArgs(), args0 ?? CreateArgs0());

        public static string[] CreateArgs() => Array.Empty<string>();

        public static string CreateArgs0() => "WordBrain";

        [TestMethod]
        public void Constructor_WhenArgsIsNull_ThrowsException()
        {
            // Arrange

            // Act
            var exception = Assert.ThrowsException<ArgumentNullException>(() => new Arguments(null!, CreateArgs0()));

            // Assert
            Assert.AreEqual("Value cannot be null. (Parameter 'args')", exception.Message);
        }

        [TestMethod]
        public void Constructor_WhenArgs0IsNull_ThrowsException()
        {
            // Arrange

            // Act
            var exception = Assert.ThrowsException<ArgumentNullException>(() => new Arguments(CreateArgs(), null!));

            // Assert
            Assert.AreEqual("Value cannot be null. (Parameter 'args0')", exception.Message);
        }

        [TestMethod]
        public void Path_WhenArgsDoesNotContainPath_ReturnsNull()
        {
            // Arrange
            string[] args = Array.Empty<string>();
            var arguments = CreateArguments(args);

            // Act
            string? result = arguments.Path;

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Path_WhenArgsContainsPath_ReturnsPath()
        {
            // Arrange
            string[] args = { "words.txt" };
            var arguments = CreateArguments(args);

            // Act
            string? result = arguments.Path;

            // Assert
            Assert.AreEqual("words.txt", result);
        }

        [TestMethod]
        public void Puzzle_WhenArgsDoesNotContainValidLetters_ReturnsNull()
        {
            // Arrange
            string[] args = { "words.txt" };
            var arguments = CreateArguments(args);

            // Act
            var result = arguments.Puzzle;

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Puzzle_WhenArgsDoesNotContainValidLengths_ReturnsNull()
        {
            // Arrange
            string[] args = { "words.txt", "ABC", "DEF", "GHI" };
            var arguments = CreateArguments(args);

            // Act
            var result = arguments.Puzzle;

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Puzzle_WhenArgsContainsValidLettersAndLengths_ReturnsPuzzle()
        {
            // Arrange
            string[] args = { "words.txt", "ABC", "DEF", "GHI", "2", "3", "4" };
            var arguments = CreateArguments(args);

            // Act
            var result = arguments.Puzzle!;

            // Assert
            Assert.AreEqual(3, result.Grid.Height);
            Assert.AreEqual(3, result.Grid.Width);
            Assert.IsTrue(Enumerable.Range(0, 3).All(i => Enumerable.Range(0, 3).All(j => args[1 + i][j] == result.Grid[i, j])));
            Assert.AreEqual("__ ___ ____", result.Solution.ToString());
        }

        [TestMethod]
        public void IsValid_WhenArgsDoesNotContainPath_ReturnsFalse()
        {
            // Arrange
            string[] args = Array.Empty<string>();
            var arguments = CreateArguments(args);

            // Act
            bool result = arguments.IsValid;

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValid_WhenArgsDoesNotContainLetters_ReturnsFalse()
        {
            // Arrange
            string[] args = { "words.txt" };
            var arguments = CreateArguments(args);

            // Act
            bool result = arguments.IsValid;

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValid_WhenArgsDoesNotContainsEqualLineLengths_ReturnsFalse()
        {
            // Arrange
            string[] args = { "words.txt", "ABC", "DE", "FGH" };
            var arguments = CreateArguments(args);

            // Act
            bool result = arguments.IsValid;

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValid_WhenArgsDoesNotContainsSufficientLines_ReturnsFalse()
        {
            // Arrange
            string[] args = { "words.txt", "ABC", "DEF" };
            var arguments = CreateArguments(args);

            // Act
            bool result = arguments.IsValid;

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValid_WhenArgsDoesNotContainLengths_ReturnsFalse()
        {
            // Arrange
            string[] args = { "words.txt", "ABC", "DEF", "GHI" };
            var arguments = CreateArguments(args);

            // Act
            bool result = arguments.IsValid;

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValid_WhenArgsDoesNotContainsValidLengths_ReturnsFalse()
        {
            // Arrange
            string[] args = { "words.txt", "ABC", "DEF", "GHI", "2", "3x", "4" };
            var arguments = CreateArguments(args);

            // Act
            bool result = arguments.IsValid;

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValid_WhenArgsContainsPathValidLettersAndValidLengths_ReturnsTrue()
        {
            // Arrange
            string[] args = { "words.txt", "ABC", "DEF", "GHI", "2", "3", "4" };
            var arguments = CreateArguments(args);

            // Act
            bool result = arguments.IsValid;

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Usage_Always_ReturnsCorrectFormat()
        {
            // Arrange
            var arguments = CreateArguments();

            // Act
            string result = arguments.Usage;

            // Assert
            Assert.AreEqual($"Usage: WordBrain word_list line1 [line2 ...] length1 [length2 ...]{Environment.NewLine}" +
                $"  word_list: The path to a file containing a list of valid words (one per line).{Environment.NewLine}" +
                $"  line1, line2, etc.: The lines of the WordBrain grid. All lines must be the same length. The grid must be square. Use '.' for blanks.{Environment.NewLine}" +
                $"  length1, length2, etc.: The lengths of the words in the solution. The lengths must sum to the number of letters in the grid.{Environment.NewLine}", result);
        }
    }
}
