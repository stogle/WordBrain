using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WordBrain.Tests
{
    [TestClass]
    public class ProgramTests
    {
        private static Program CreateProgram(Func<DateTime>? now = null, Func<string, string[]>? fileReader = null, TextWriter? stdout = null, string[]? args = null) =>
            new Program(now ?? CreateNow(), fileReader ?? CreateFileReader(), stdout ?? CreateStdout(), args ?? CreateArgs());

        private static Func<DateTime> CreateNow()
        {
            long ticks = 0;
            return () => new DateTime(ticks += 10000000L);
        }

        private static Func<string, string[]> CreateFileReader(string[]? output = null) => _ => output ?? new[] { "ACTUALLY", "NONE", "SHIP" };

        private static TextWriter CreateStdout(StringBuilder? sb = null) => new StringWriter(sb ?? new StringBuilder());

        private static string[] CreateArgs() => new[] { "3of6game.txt", "SLLY", "HAUE", "ICTN", "PAON", "4", "8", "4" };

        [TestMethod]
        public void Constructor_WhenNowIsNull_ThrowsException()
        {
            // Arrange
            Func<DateTime> now = null!;
            Func<string, string[]> fileReader = null!;
            TextWriter stdout = null!;
            string[] args = null!;

            // Act
            var exception = Assert.ThrowsException<ArgumentNullException>(() => new Program(now, fileReader, stdout, args));

            // Assert
            Assert.AreEqual("Value cannot be null. (Parameter 'now')", exception.Message);
        }

        [TestMethod]
        public void Constructor_WhenFileReaderIsNull_ThrowsException()
        {
            // Arrange
            Func<string, string[]> fileReader = null!;
            TextWriter stdout = null!;
            string[] args = null!;

            // Act
            var exception = Assert.ThrowsException<ArgumentNullException>(() => new Program(CreateNow(), fileReader, stdout, args));

            // Assert
            Assert.AreEqual("Value cannot be null. (Parameter 'fileReader')", exception.Message);
        }

        [TestMethod]
        public void Constructor_WhenStdoutIsNull_ThrowsException()
        {
            // Arrange
            TextWriter stdout = null!;
            string[] args = null!;

            // Act
            var exception = Assert.ThrowsException<ArgumentNullException>(() => new Program(CreateNow(), CreateFileReader(), stdout, args));

            // Assert
            Assert.AreEqual("Value cannot be null. (Parameter 'stdout')", exception.Message);
        }

        [TestMethod]
        public void Constructor_WhenArgsAreInvalid_ThrowsException()
        {
            // Arrange
            var args = Array.Empty<string>();

            // Act
            var exception = Assert.ThrowsException<ArgumentException>(() => new Program(CreateNow(), CreateFileReader(), CreateStdout(), args));

            // Assert
            Assert.AreEqual("Expected valid arguments. (Parameter 'args')", exception.Message);
        }

        [TestMethod]
        public void Start_Always_OutputsResult()
        {
            // Arrange
            var sb = new StringBuilder();
            var program = CreateProgram(stdout: CreateStdout(sb));

            // Act
            program.Start();

            // Assert
            Assert.IsTrue(Regex.IsMatch(sb.ToString(), $"Read 3 words in 00:00:01{Environment.NewLine}" +
                $"{Environment.NewLine}" +
                $"S L L Y{Environment.NewLine}" +
                $"H A U E{Environment.NewLine}" +
                $"I C T N{Environment.NewLine}" +
                $"P A O N{Environment.NewLine}" +
                $"____ ________ ____{Environment.NewLine}" +
                $"{Environment.NewLine}" +
                $"(SHIP ACTUALLY|ACTUALLY SHIP) NONE{Environment.NewLine}" +
                $"{Environment.NewLine}" +
                $"Found 1 solution\\(s\\) in 00:00:01{Environment.NewLine}"));
        }
    }
}
