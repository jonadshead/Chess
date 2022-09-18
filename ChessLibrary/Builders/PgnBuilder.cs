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

namespace Chess
{
    internal static class PgnBuilder
    {
        public static (bool succeeded, ChessException? exception) TryLoad(string pgn, out ChessBoard? board)
        {
            board = new ChessBoard();

            var headersMatches = Regexes.regexHeaders.Matches(pgn);

            if (headersMatches.Count > 0)
            {
                // Extracting headers
                for (int i = 0; i < headersMatches.Count; i++)
                {
                    // [Black "Geras1mleo"]
                    // Groups[1] = Black
                    // Groups[2] = Geras1mleo
                    board.headers.Add(headersMatches[i].Groups[1].Value,
                                      headersMatches[i].Groups[2].Value);
                }
            }

            // San move can occur in header ex. in nickname of player => remove headers from string
            pgn = Regexes.regexHeaders.Replace(pgn, "");

            // Loading fen if exist
            if (board.headers.TryGetValue("FEN", out var fen))
            {
                var (succeeded, exception) = FenBoardBuilder.TryLoad(fen, out board.FenBuilder);

                if (!succeeded)
                {
                    board = null;
                    return (false, exception);
                }

                board.pieces = board.FenBuilder.Pieces;

                board.HandleKingChecked();
                board.HandleEndGame();

                if (board.IsEndGame)
                    return (true, null);
            }

            // Remove all alternatives
            pgn = Regexes.regexAlternatives.Replace(pgn, "");

            // Remove all comments
            pgn = Regexes.regexComments.Replace(pgn, "");

            // Todo Save Alternative moves(bracnhes) and Comments for moves

            var movesMatches = Regexes.regexSanMoves.Matches(pgn);

            // Execute all found moves
            for (int i = 0; i < movesMatches.Count; i++)
            {
                var (succeeded, exception) = SanBuilder.TryParse(board, movesMatches[i].Value, out var move, true);

                if (!succeeded)
                {
                    board = null;
                    return (false, exception);
                }

                // If san parsing succeeded => move is valid

                board.executedMoves.Add(move);
                board.DropPieceToNewPosition(move);
                board.moveIndex = board.executedMoves.Count - 1;
            }


            board.HandleKingChecked();
            board.HandleEndGame();

            // If not actual end game but game is in fact ended => someone resigned
            if (!board.IsEndGame)
            {
                if (pgn.Contains("1-0"))
                    board.Resign(PieceColor.Black);

                else if (pgn.Contains("0-1"))
                    board.Resign(PieceColor.White);

                else if (pgn.Contains("1/2-1/2"))
                    board.Draw();
            }

            return (true, null);
        }

        public static string BoardToPgn(ChessBoard board)
        {
            StringBuilder builder = new StringBuilder();

            foreach (var header in board.headers)
                builder.Append('[' + header.Key + @" """ + header.Value + '"' + ']' + '\n');

            if (board.headers.Count > 0)
                builder.Append('\n');

            // Needed for moves count logic
            board.moveIndex = -1;

            for (int i = 0, count = 0; i < board.executedMoves.Count; i++)
            {
                // Adding moves count when needed
                if (count != board.GetFullMovesCount())
                {
                    count = board.GetFullMovesCount();

                    // Add space before move count if not first move
                    if (i != 0) builder.Append(' ');

                    builder.Append(count + ".");
                }

                if (board.moveIndex == -1)
                {
                    // From position?
                    if (board.LoadedFromFen && board.FenBuilder.Turn == PieceColor.Black)
                        builder.Append("..");
                }

                builder.Append(' ' + board.executedMoves[i].San);

                board.moveIndex++;
            }

            if (board.IsEndGame)
            {
                if (board.EndGame.WonSide == PieceColor.White)
                    builder.Append(" 1-0");
                else if (board.EndGame.WonSide == PieceColor.Black)
                    builder.Append(" 0-1");
                else
                    builder.Append(" 1/2-1/2");
            }

            // Back to positions
            board.Last();

            return builder.ToString();
        }
    }
}