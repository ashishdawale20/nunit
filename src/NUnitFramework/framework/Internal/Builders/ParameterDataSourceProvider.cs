// ***********************************************************************
// Copyright (c) 2008 Charlie Poole, Rob Prouse
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

#nullable enable

using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal.Builders
{
    /// <summary>
    /// ParameterDataSourceProvider supplies individual argument values for
    /// single parameters using attributes implementing IParameterDataSource.
    /// </summary>
    public class ParameterDataSourceProvider : IParameterDataProvider
    {
        #region IParameterDataProvider Members

        /// <summary>
        /// Determines whether any data is available for a parameter.
        /// </summary>
        /// <param name="parameter">The parameter of a parameterized test</param>
        public bool HasDataFor(IParameterInfo parameter)
        {
            return parameter.IsDefined<IParameterDataSource>(false);
        }

        /// <summary>
        /// Retrieves data for use with the supplied parameter.
        /// </summary>
        /// <param name="parameter">The parameter of a parameterized test</param>
        public IEnumerable GetDataFor(IParameterInfo parameter)
        {
            var data = new List<object?>();

            foreach (IParameterDataSource source in parameter.GetCustomAttributes<IParameterDataSource>(false))
            {
                foreach (object? item in source.GetData(parameter))
                    data.Add(item);
            }

            return data;
        }
        #endregion
    }
}
