﻿// *****************************************************
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

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Chess
{
    /// <summary>
    /// Smart enum for Piece color in chess game
    /// </summary>
    public class PieceColor : SmartEnum<PieceColor>
    {
        public static readonly PieceColor White = new PieceColor("White", 1, 'w');
        public static readonly PieceColor Black = new PieceColor("Black", 2, 'b');

        /// <summary>
        /// White => 'w'<br/>
        /// Black => 'b'<br/>
        /// </summary>
        public char AsChar { get; }

        /// <summary>
        /// Opposite color <br/>
        /// White => Black<br/>
        /// Black => White<br/>
        /// </summary>
        public PieceColor OppositeColor()
        {
            return Value switch
            {
                1 => Black,
                2 => White,
                _ => throw new ChessArgumentException(null, nameof(Value), nameof(PieceColor.OppositeColor)),
            };
        }

        private PieceColor(string name, int value, char asChar) : base(name, value)
        {
            AsChar = asChar;
        }

        /// <summary>
        /// PieceColor object from char<br/>
        /// 'w' => White<br/>
        /// 'b' => Black<br/>
        /// </summary>
        public static PieceColor FromChar(char color)
        {
            return char.ToLower(color) switch
            {
                'w' => White,
                'b' => Black,
                _ => throw new ChessArgumentException(null, nameof(color), nameof(PieceColor.FromChar)),
            };
        }
    }

    /// <summary>
    /// Smart enum for Piece type in chess game
    /// </summary>
    public class PieceType : SmartEnum<PieceType>
    {
        public static readonly PieceType Pawn = new PieceType("Pawn", 1, 'p');
        public static readonly PieceType Rook = new PieceType("Rook", 2, 'r');
        public static readonly PieceType Knight = new PieceType("Knight", 3, 'n');
        public static readonly PieceType Bishop = new PieceType("Bishop", 4, 'b');
        public static readonly PieceType Queen = new PieceType("Queen", 5, 'q');
        public static readonly PieceType King = new PieceType("King", 6, 'k');

        /// <summary>
        /// Pawn => 'p'<br/>
        /// Rook => 'r'<br/>
        /// Knight => 'n'<br/>
        /// Bishop => 'b'<br/>
        /// Queen => 'q'<br/>
        /// King => 'k'<br/>
        /// </summary>
        public char AsChar { get; }

        private PieceType(string name, int value, char asChar) : base(name, value)
        {
            AsChar = asChar;
        }

        /// <summary>
        /// PieceType object from char<br/>
        /// 'p' => Pawn<br/>
        /// 'r' => Rook<br/>
        /// 'n' => Knight<br/>
        /// 'b' => Bishop<br/>
        /// 'q' => Queen<br/>
        /// 'k' => King<br/>
        /// </summary>
        public static PieceType FromChar(char type)
        {
            return char.ToLower(type) switch
            {
                'p' => Pawn,
                'r' => Rook,
                'n' => Knight,
                'b' => Bishop,
                'q' => Queen,
                'k' => King,
                _ => throw new ChessArgumentException(null, nameof(type), nameof(PieceType.FromChar)),
            };
        }
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member