using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

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
            var wordTree = CreateWordTree();
            wordTree.TryLetter('a', ref wordTree);

            // Act
            bool result = wordTree.IsWord;

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsWord_WhenIsNotWord_ReturnsFalse()
        {
            // Arrange
            var wordTree = CreateWordTree();
            wordTree.TryLetter('b', ref wordTree);

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
            var childTree = wordTree;

            // Act
            bool result = wordTree.TryLetter('a', ref childTree);

            // Assert
            Assert.IsTrue(result);
            Assert.AreNotEqual(wordTree, childTree);
        }

        [TestMethod]
        public void TryLetter_WhenSameLetterWithDifferentCaseExists_SetsChildTreeAndReturnsTrue()
        {
            // Arrange
            var wordTree = CreateWordTree();
            var childTree = wordTree;

            // Act
            bool result = wordTree.TryLetter('A', ref childTree);

            // Assert
            Assert.IsTrue(result);
            Assert.AreNotEqual(wordTree, childTree);
        }

        [TestMethod]
        public void TryLetter_WhenLetterDoesNotExist_DoesNotSetChildTreeAndReturnsFalse()
        {
            // Arrange
            var wordTree = CreateWordTree();
            var childTree = wordTree;

            // Act
            bool result = wordTree.TryLetter('c', ref childTree);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(wordTree, childTree);
        }
    }
}
