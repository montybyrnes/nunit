// ***********************************************************************
// Copyright (c) 2015 Charlie Poole
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************

using System;
using NUnit.Framework.Interfaces;
using System.Text.RegularExpressions;

namespace NUnit.Framework.Internal.Filters
{
    /// <summary>
    /// SplitFilter filter selects tests based on their split requirements
    /// </summary>
    [Serializable]
    public class SplitFilter : ValueMatchFilter
    {

        private int numOfSplits;
        private int currentSplit;
        /// <summary>
        /// Construct a SplitFilter for a single name
        /// </summary>
        /// <param name="expectedValue">The name the filter will recognize.</param>
        public SplitFilter(string expectedValue) : base(expectedValue) {
            string splitPattern = "(-)";
            string[]splitInfo = Regex.Split(expectedValue, splitPattern);

            numOfSplits = int.Parse(splitInfo[0]);
            currentSplit = int.Parse(splitInfo[2]);
        }

        /// <summary>
        /// Match a test against a single value.
        /// </summary>
        public override bool Match(ITest test)
        {
            if (!test.HasChildren)
            {
                int result = test.ClassName.GetHashCode();
                if (result < 0) result *= -1;
                if (result % numOfSplits == (currentSplit-1))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        /// <summary>
        /// Gets the element name
        /// </summary>
        /// <value>Element name</value>
        protected override string ElementName
        {
            get { return "split"; }
        }
    }
}
