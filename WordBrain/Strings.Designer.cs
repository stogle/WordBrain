﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WordBrain {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Strings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Strings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("WordBrain.Strings", typeof(Strings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Usage: {0} word_list line1 [line2 ...] length1 [length2 ...]{1}  word_list: The path to a file containing a list of valid words (one per line).{1}  line1, line2, etc.: The lines of the WordBrain grid. All lines must be the same length. The grid must be square. Use &apos;.&apos; for blanks.{1}  length1, length2, etc.: The lengths of the words in the solution. The lengths must sum to the number of letters in the grid.{1}.
        /// </summary>
        internal static string Arguments_UsageFormat {
            get {
                return ResourceManager.GetString("Arguments_UsageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expected letters to be rectangular..
        /// </summary>
        internal static string Grid_ExpectedRectangularLettersExceptionMessage {
            get {
                return ResourceManager.GetString("Grid_ExpectedRectangularLettersExceptionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Found {0} solutions in {1}.
        /// </summary>
        internal static string Program_FoundSolutionsFormat {
            get {
                return ResourceManager.GetString("Program_FoundSolutionsFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expected remaining letters in Solution and Grid to be equal..
        /// </summary>
        internal static string Puzzle_ExpectedRemainingLettersEqualExceptionMessage {
            get {
                return ResourceManager.GetString("Puzzle_ExpectedRemainingLettersEqualExceptionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expected positive integer lengths..
        /// </summary>
        internal static string Solution_ExpectedPositiveLengthsExceptionMessage {
            get {
                return ResourceManager.GetString("Solution_ExpectedPositiveLengthsExceptionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot play a word of this length..
        /// </summary>
        internal static string Solution_InvalidLengthForPlayExceptionMessage {
            get {
                return ResourceManager.GetString("Solution_InvalidLengthForPlayExceptionMessage", resourceCulture);
            }
        }
    }
}
