using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net6App
{
    class Board
    {
        private readonly string[,] board = {
            { "1", "2", "3" },
            { "4", "5", "6" },
            { "7", "8", "9" }
        };

        private int moves = 0;

        private const int maxMoves = 9;

        private bool currentPlayer = true; //true == player one, false == player two

        private bool Check()
        {
            // Check horizontal
            for (int row = 0; row < board.GetLength(0); row++)
            {
                string curr = board[row, 0];
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    if (int.TryParse(board[row, col], out _)) break;
                    if (!curr.Equals(board[row, col])) break;
                    if (col == board.GetLength(1) - 1) return true;
                }
            }

            // Check vertical
            for (int col = 0; col < board.GetLength(1); col++)
            {
                string curr = board[0, col];
                for (int row = 0; row < board.GetLength(0); row++)
                {
                    if (int.TryParse(board[row, col], out _)) break;
                    if (!curr.Equals(board[row, col])) break;
                    if (row == board.GetLength(0) - 1) return true;
                }
            }

            // Check top-left diagonal
            string topLeftCurr = board[0, 0];
            for (int row = 0, col = 0; row < board.GetLength(0) && col < board.GetLength(1); row++, col++)
            {
                if (int.TryParse(board[row, col], out _)) break;
                if (!topLeftCurr.Equals(board[row, col])) break;
                if (row == board.GetLength(0) - 1 || col == board.GetLength(1) - 1) return true;
            }

            // Check top-right diagonal
            string topRightCurr = board[0, board.GetLength(1) - 1];
            for (int row = 0, col = board.GetLength(1) - 1; row < board.GetLength(0) && col >= 0; row++, col--)
            {
                if (int.TryParse(board[row, col], out _)) break;
                if (!topRightCurr.Equals(board[row, col])) break;
                if (row == board.GetLength(0) - 1 || col == 0) return true;
            }

            return false;
        }

        private int[]? MoveToCoordinate(int move)
        {
            return move switch
            {
                1 => new int[] { 0, 0 },
                2 => new int[] { 0, 1 },
                3 => new int[] { 0, 2 },
                4 => new int[] { 1, 0 },
                5 => new int[] { 1, 1 },
                6 => new int[] { 1, 2 },
                7 => new int[] { 2, 0 },
                8 => new int[] { 2, 1 },
                9 => new int[] { 2, 2 },
                _ => null,
            };
        }

        private bool CheckAndMove(int move)
        {
            int[]? cord = MoveToCoordinate(move);

            if (cord == null) return false;
            if (int.TryParse(board[cord[0], cord[1]], out _))
            {
                if (currentPlayer)
                {
                    board[cord[0], cord[1]] = "X";
                } else
                {
                    board[cord[0], cord[1]] = "O";
                }
                return true;
            }

            return false;
        }

        private void Display()
        {
            Console.Clear();
            Console.WriteLine("\t\t|\t\t|\t\t");
            Console.WriteLine($"\t{board[0, 0]}\t|\t{board[0, 1]}\t|\t{board[0, 2]}");
            Console.WriteLine("\t\t|\t\t|\t\t");
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("\t\t|\t\t|\t\t");
            Console.WriteLine($"\t{board[1, 0]}\t|\t{board[1, 1]}\t|\t{board[1, 2]}");
            Console.WriteLine("\t\t|\t\t|\t\t");
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("\t\t|\t\t|\t\t");
            Console.WriteLine($"\t{board[2, 0]}\t|\t{board[2, 1]}\t|\t{board[2, 2]}");
            Console.WriteLine("\t\t|\t\t|\t\t\n");
        }

        public void Play()
        {
            while(!Check() && moves != maxMoves)
            {
                Display();
                string curr = currentPlayer ? "Player X" : "Player O";

                bool invalidInput = false;
                do
                {   
                    if (invalidInput)
                    {
                        Console.WriteLine("\nYour last move or input was invalid.");
                    }
                    Console.Write($"{curr}, choose a move: ");
                    string? input = Console.ReadLine();

                    if (input != null && int.TryParse(input, out int move))
                    {
                        if (!CheckAndMove(move))
                        {
                            invalidInput = true; // User's input was valid, but the move itself was not
                        }
                        else
                        {
                            currentPlayer = !currentPlayer; // Next player's turn
                            invalidInput = false; // Sets to false in-case move or input was previously invalid
                        }
                    }
                    else
                    {
                        invalidInput = true; // User gave invalid input
                    }
                } while (invalidInput);

                moves++;
            }

            Display();

            if (!Check() && moves == maxMoves)
            {
                Console.WriteLine("There was a draw!");
            } else
            {
                string winner = !currentPlayer ? "Player X" : "Player O";
                Console.WriteLine($"{winner} won the game!");
            }
            Console.Read();
        }
    }
}
