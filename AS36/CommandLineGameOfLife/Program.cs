using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameOfLife;

namespace CommandLineGameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(10);
            board.GenerateRandomBoard();
            do
            {
                Console.Clear();
                printBoard(board);
                board.NextDay();
            } while (Console.ReadKey().Key != ConsoleKey.Q);
            
        }

        static void printBoard(Board board)
        {
            for (uint row = 0; row < board.Size; row++)
            {
                for (uint col = 0; col < board.Size; col++)
                {
                    int? value = board[col, row];
                    if (value == null)
                    {
                        Console.Out.Write("|Z");
                    }
                    else if (value == 0)
                    {
                        Console.Out.Write("|D");
                    }
                    else if (value == 1)
                    {
                        Console.Out.Write("|L");
                    }
                    else
                    {
                        Console.Out.WriteLine("Error, value: " + value + " not known");
                    }
                    
                    
                }
                Console.Out.WriteLine();
            }
        }
        
    }
}
