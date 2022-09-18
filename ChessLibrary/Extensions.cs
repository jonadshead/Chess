// *****************************************************
// *                                                   *
// * O Lord, Thank you for your goodness in our lives. *
// *     Please bless this code to our compilers.      *
// *                     Amen.                         *
// *                                                   *
// *****************************************************
//                                    Made by Geras1mleo

using System.Text.RegularExpressions;
using System.Collections.Concurrent;
using System.Text;
using Ardalis.SmartEnum;
using System.Diagnostics.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Chess
{
    internal static class Extensions
    {
        /// <summary>
        /// See: https://stackoverflow.com/questions/49190830/is-it-possible-for-string-split-to-return-tuple
        /// Deconstruct into 2 vars
        /// </summary>
        internal static void Deconstruct<T>(this IList<T> list, out T first, out T second)
        {
            first = list.Count > 0 ? list[0] : default; // or throw
            second = list.Count > 1 ? list[1] : default; // or throw
        }
    }
}