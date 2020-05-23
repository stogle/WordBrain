using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WordBrain.Tests
{
    [TestClass]
    public class WordTreeTests
    {
        public static WordTree CreateWordTree(IEnumerable<string>? words = null) => new WordTree(words ?? CreateWords());

        public static IEnumerable<string> CreateWords() => new[] { "a", "alice", "bob" };

        [TestMethod]
        public void Constructor_WhenWordsIsNull_ThrowsException()
        {
            // Arrange

            // Act
            var exception = Assert.ThrowsException<ArgumentNullException>(() => new WordTree(null!));

            // Assert
            Assert.AreEqual("Value cannot be null. (Parameter 'words')", exception.Message);
        }

        [TestMethod]
        public void IsWord_WhenIsWord_ReturnsTrue()
        {
            // Arrange
            WordTree wordTree;
            CreateWordTree().TryLetter('a', out wordTree!);

            // Act
            bool result = wordTree.IsWord;

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsWord_WhenIsNotWord_ReturnsFalse()
        {
            // Arrange
            WordTree wordTree;
            CreateWordTree().TryLetter('b', out wordTree!);

            // Act
            bool result = wordTree.IsWord;

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TryLetter_WhenLetterExists_SetsChildTreeAndReturnsTrue()
        {
            // Arrange
            var wordTree = CreateWordTree();

            // Act
            bool result = wordTree.TryLetter('a', out var childTree);

            // Assert
            Assert.IsTrue(result);
            Assert.IsNotNull(childTree);
        }

        [TestMethod]
        public void TryLetter_WhenSameLetterWithDifferentCaseExists_SetsChildTreeAndReturnsTrue()
        {
            // Arrange
            var wordTree = CreateWordTree();

            // Act
            bool result = wordTree.TryLetter('A', out var childTree);

            // Assert
            Assert.IsTrue(result);
            Assert.IsNotNull(childTree);
        }

        [TestMethod]
        public void TryLetter_WhenLetterDoesNotExist_SetsChildTreeToNullAndReturnsFalse()
        {
            // Arrange
            var wordTree = CreateWordTree();

            // Act
            bool result = wordTree.TryLetter('c', out var childTree);

            // Assert
            Assert.IsFalse(result);
            Assert.IsNull(childTree);
        }
    }
}
