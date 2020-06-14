using System;
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
        public void TryParse_WhenArgsDoesNotContainPath_SetsNothingAndReturnsFalse()
        {
            // Arrange
            string[] args = Array.Empty<string>();
            var arguments = CreateArguments(args);

            // Act
            bool result = arguments.TryParse(out string? path, out char?[][]? letters, out int[]? lengths);

            // Assert
            Assert.IsFalse(result);
            Assert.IsNull(path);
            Assert.IsNull(letters);
            Assert.IsNull(lengths);
        }

        [TestMethod]
        public void TryParse_WhenArgsDoesNotContainLetters_SetsPathAndReturnsFalse()
        {
            // Arrange
            string[] args = { "words.txt" };
            var arguments = CreateArguments(args);

            // Act
            bool result = arguments.TryParse(out string? path, out char?[][]? letters, out int[]? lengths);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual("words.txt", path);
            Assert.IsNull(letters);
            Assert.IsNull(lengths);
        }

        [TestMethod]
        public void TryParse_WhenArgsDoesNotContainLengths_SetsPathAndLettersAndReturnsFalse()
        {
            // Arrange
            string[] args = { "words.txt", "ABC", "DEF", "GHI" };
            var arguments = CreateArguments(args);

            // Act
            bool result = arguments.TryParse(out string? path, out char?[][]? letters, out int[]? lengths);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual("words.txt", path);
            Assert.AreEqual(3, letters!.Length);
            CollectionAssert.AreEqual("ABC".ToCharArray(), letters![0]);
            CollectionAssert.AreEqual("DEF".ToCharArray(), letters![1]);
            CollectionAssert.AreEqual("GHI".ToCharArray(), letters![2]);
            Assert.IsNull(lengths);
        }

        [TestMethod]
        public void TryParse_WhenArgsContainsPathValidLettersAndValidLengths_SetsPathLettersAndLengthAndReturnsTrue()
        {
            // Arrange
            string[] args = { "words.txt", "ABC", "DEF", "GHI", "2", "3", "4" };
            var arguments = CreateArguments(args);

            // Act
            bool result = arguments.TryParse(out string? path, out char?[][]? letters, out int[]? lengths);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual("words.txt", path);
            Assert.AreEqual(3, letters!.Length);
            CollectionAssert.AreEqual("ABC".ToCharArray(), letters![0]);
            CollectionAssert.AreEqual("DEF".ToCharArray(), letters![1]);
            CollectionAssert.AreEqual("GHI".ToCharArray(), letters![2]);
            CollectionAssert.AreEqual(new[] { 2, 3, 4 }, lengths);
        }

        [TestMethod]
        public void TryParse_WhenArgsContainsInvalidLetters_SetsPathAndReturnsFalse()
        {
            // Arrange
            string[] args = { "words.txt", "ABC", "DE", "FGH", "2", "2", "4" };
            var arguments = CreateArguments(args);

            // Act
            bool result = arguments.TryParse(out string? path, out char?[][]? letters, out int[]? lengths);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual("words.txt", path);
            Assert.IsNull(letters);
            Assert.IsNull(lengths);
        }

        [TestMethod]
        public void TryParse_WhenArgsContainsInvalidLengths_SetsPathAndLettersAndReturnsFalse()
        {
            // Arrange
            string[] args = { "words.txt", "ABC", "DEF", "GHI", "2", "3x", "4" };
            var arguments = CreateArguments(args);

            // Act
            bool result = arguments.TryParse(out string? path, out char?[][]? letters, out int[]? lengths);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual("words.txt", path);
            Assert.AreEqual(3, letters!.Length);
            CollectionAssert.AreEqual("ABC".ToCharArray(), letters![0]);
            CollectionAssert.AreEqual("DEF".ToCharArray(), letters![1]);
            CollectionAssert.AreEqual("GHI".ToCharArray(), letters![2]);
            Assert.IsNull(lengths);
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
                $"  line1, line2, etc.: The lines of the WordBrain grid. The grid must be rectangular. Use '.' for blanks.{Environment.NewLine}" +
                $"  length1, length2, etc.: The lengths of the words in the solution. They must sum to the number of letters in the grid.{Environment.NewLine}", result);
        }
    }
}
