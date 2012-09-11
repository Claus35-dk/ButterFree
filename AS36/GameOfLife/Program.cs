using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BDSA12;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(20);
            //board.ChangeStatus(new Board.BoardEdit(3, 2, 1), new Board.BoardEdit(4, 2, 1), new Board.BoardEdit(5, 2, 1));
            board.GenerateRandomBoard();
            GOFRunner.Run(board);
        }
    }
}
