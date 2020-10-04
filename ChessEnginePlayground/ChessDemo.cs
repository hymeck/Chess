﻿using static System.Console;
using ChessEngine;
using ChessEngine.Domain;
using ChessEngine.Console;

namespace ChessEnginePlayground
{
    class ChessDemo
    {
        static void Main(string[] args)
        {
            var game = new ChessGame();

            while (
                !game.IsCheckmate && 
                !game.IsStalemate)
            {
                PrintGameState(game);
            moveInput:
                Write("\n > ");
                string move = ReadLine();
                if (move == "" || move == "q" || move == "exit")
                    break;
                try
                {
                    MakeMove(ref game, move);
                    Clear();
                }

                catch
                {
                    WriteLine(" Incorrect input");
                    goto moveInput;
                }
            }

            if (game.IsCheckmate)
            {
                PrintBoard(game);
                WriteLine($" {game.ActiveColor.GetReversedColor()} won");
            }

            else if (game.IsStalemate)
            {
                PrintBoard(game);
                WriteLine($" Stalemate");
            }
            ReadKey();
        }

        private static void PrintGameState(ChessGame game)
        {
            PrintBoard(game);

            WriteLine($" Turn     : {game.ActiveColor.ToString()}");
            WriteLine($" Move     : {game.MoveCount}");
            WriteLine($" Check    : {game.IsCheck}");
            WriteLine($" Checkmate: {game.IsCheckmate}");
            WriteLine($" Stalemate: {game.IsStalemate}");
        }

        private static void PrintBoard(ChessGame game)
        {
            WriteLine();

            if (game.ActiveColor.IsWhite())
                PrintWhiteBoard(game.Board);
            else
                PrintBlackBoard(game.Board);
        }

        private static void MakeMove(ref ChessGame game, string move)
        {
            var (from, to) = ConsoleMoveInput.ParseMove(move);
            game = game.Move(from, to);
        }

        private static void PrintWhiteBoard(Board board)
        {
            for (int y = 7; y > -1; y--)
            {
                for (int x = 0; x < 8; x++)
                {
                    var piece = board[x, y].IsNone() ? "." : board[x, y].ToString();
                    var output = " " + piece;
                    Write(output);
                }
                WriteLine($" {y + 1}");
            }
            WriteLine(" a b c d e f g h\n");
        }

        private static void PrintBlackBoard(Board board)
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    var piece = board[x, y].IsNone() ? "." : board[x, y].ToString();
                    var output = " " + piece;
                    Write(output);
                }
                WriteLine($" {y + 1}");
            }

            WriteLine(" a b c d e f g h\n");
        }
    }
}