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
    /// <summary>
    /// Chess end game info
    /// </summary>
    public class EndGameInfo
    {
        /// <summary>
        /// Endgame type of current chess game
        /// </summary>
        public EndgameType EndgameType { get; }

        /// <summary>
        /// Won side or null if draw/stalemate
        /// </summary>
        public PieceColor? WonSide { get; }

        /// <summary>
        /// Initializes new object of EndGameInfo with given end game parameters 
        /// </summary>
        public EndGameInfo(EndgameType endgameType, PieceColor? wonSide)
        {
            EndgameType = endgameType;
            WonSide = wonSide;
        }
    }
}