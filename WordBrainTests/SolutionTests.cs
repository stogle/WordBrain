using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WordBrain.Tests
{
    [TestClass]
    public class SolutionTests
    {
        private static Solution CreateSolution(int[]? lengths = null) => new Solution(lengths ?? CreateLengths());

        private static int[] CreateLengths() => new[] { 2, 3, 4 };

        [TestMethod]
        public void Constructor_WhenLengthsIsNull_ThrowsException()
        {
            // Arrange
            int[] lengths = null!;

            // Act
            var exception = Assert.ThrowsException<ArgumentNullException>(() => new Solution(lengths));

            // Assert
            Assert.AreEqual("Value cannot be null. (Parameter 'lengths')", exception.Message);
        }

        [TestMethod]
        public void Constructor_WhenLengthsAreNotAllPositive_ThrowsException()
        {
            // Arrange
            int[] lengths = { 2, 3, 0 };

            // Act
            var exception = Assert.ThrowsException<ArgumentException>(() => new Solution(lengths));

            // Assert
            Assert.AreEqual("Expected positive integer lengths. (Parameter 'lengths')", exception.Message);
        }

        [TestMethod]
        public void IsComplete_WhenIsNotComplete_ReturnsFalse()
        {
            // Arrange
            Solution solution = CreateSolution();

            // Act
            bool result = solution.IsComplete;

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsComplete_WhenIsComplete_ReturnsFalse()
        {
            // Arrange
            int[] lengths = Array.Empty<int>();
            Solution solution = CreateSolution(lengths);

            // Act
            bool result = solution.IsComplete;

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ToString_WhenTryPlayHasNotBeenCalled_ReturnsCorrectValue()
        {
            // Arrange
            Solution solution = CreateSolution();

            // Act
            string result = solution.ToString();

            // Assert
            Assert.AreEqual("__ ___ ____", result);
        }
    }
}
