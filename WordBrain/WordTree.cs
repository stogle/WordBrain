﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace WordBrain
{
    public class WordTree
    {
        private readonly IDictionary<char, WordTree> _children = new Dictionary<char, WordTree>();

        public WordTree(IEnumerable<string> words)
        {
            if (words == null)
            {
                throw new ArgumentNullException(nameof(words));
            }

            foreach (string word in words)
            {
                Add(word, 0);
            }
        }

        private WordTree()
        {
        }

        private void Add(string word, int index)
        {
            if (index == word.Length)
            {
                IsWord = true;
                return;
            }

            char c = char.ToUpperInvariant(word[index]);
            if (!_children.TryGetValue(c, out var child))
            {
                child = new WordTree();
                _children[c] = child;
            }

            child.Add(word, index + 1);
        }

        public bool IsWord { get; private set; }

        public bool TryLetter(char c, [NotNullWhen(true)]out WordTree? childTree)
        {
            if (_children.TryGetValue(char.ToUpperInvariant(c), out var value))
            {
                childTree = value;
                return true;
            }

            childTree = null;
            return false;
        }
    }
}
