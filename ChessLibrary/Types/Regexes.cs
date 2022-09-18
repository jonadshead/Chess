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
    internal class Regexes
    {
        internal const string SanOneMovePattern = @"(^([PNBRQK])?([a-h])?([1-8])?(x|X|-)?([a-h][1-8])(=[NBRQ]| ?e\.p\.)?|^O-O(-O)?)(\+|\#|\$)?$";

        internal const string SanMovesPattern = @"([PNBRQK]?[a-h]?[1-8]?[xX-]?[a-h][1-8](=[NBRQ]| ?e\.p\.)?|O-O(?:-O)?)[+#$]?";

        internal const string HeadersPattern = @"\[(.*?) ""(.*?)""\]";

        internal const string AlternativesPattern = @"\(.*?\)";

        internal const string CommentsPattern = @"\{.*?\}";

        internal const string FenPattern = @"^(((?:[rnbqkpRNBQKP1-8]+\/){7})[rnbqkpRNBQKP1-8]+) ([b|w]) (-|[K|Q|k|q]{1,4}) (-|[a-h][36]) (\d+ \d+)$";

        internal const string PiecePattern = "^[wb][bknpqr]$";

        internal const string FenPiecePattern = "^([bknpqr]|[BKNPQR])$";

        internal const string PositionPattern = "^[a-h][1-8]$";

        internal const string MovePattern = @"^{(([wb][bknpqr]) - )?([a-h][1-8]) - ([a-h][1-8])( - ([wb][bknpqr]))?( - (o-o|o-o-o|e\.p\.|=|=q|=r|=b|=n))?( - ([+#$]))?}$";

        internal readonly static Regex regexSanOneMove = new Regex(SanOneMovePattern, RegexOptions.Compiled, TimeSpan.FromMilliseconds(250));

        internal readonly static Regex regexSanMoves = new Regex(SanMovesPattern, RegexOptions.Compiled, TimeSpan.FromMilliseconds(250));

        internal readonly static Regex regexHeaders = new Regex(HeadersPattern, RegexOptions.Compiled, TimeSpan.FromMilliseconds(250));

        internal readonly static Regex regexAlternatives = new Regex(AlternativesPattern, RegexOptions.Compiled | RegexOptions.Singleline, TimeSpan.FromMilliseconds(250));

        internal readonly static Regex regexComments = new Regex(CommentsPattern, RegexOptions.Compiled | RegexOptions.Singleline, TimeSpan.FromMilliseconds(250));

        internal readonly static Regex regexFen = new Regex(FenPattern, RegexOptions.Compiled, TimeSpan.FromMilliseconds(250));

        internal readonly static Regex regexPiece = new Regex(PiecePattern, RegexOptions.Compiled, TimeSpan.FromMilliseconds(250));

        internal readonly static Regex regexFenPiece = new Regex(FenPiecePattern, RegexOptions.Compiled, TimeSpan.FromMilliseconds(250));

        internal readonly static Regex regexPosition = new Regex(PositionPattern, RegexOptions.Compiled, TimeSpan.FromMilliseconds(250));

        internal readonly static Regex regexMove = new Regex(MovePattern, RegexOptions.Compiled, TimeSpan.FromMilliseconds(250));
    }
}