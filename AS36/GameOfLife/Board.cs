using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BDSA12;

namespace GameOfLife
{
    public class Board : IGameOfLife
    {
        private int?[,] board;
        private uint size;

        public Board(uint size)
        {
            board = new int?[size, size];
            this.size = size;
        }


        public uint Size
        {
            get { return size; }
        }

        public int? this[uint col, uint row]
        {
            get { return board[col, row]; }
        }

        public void NextDay()
        {
            List<BoardEdit> changes = new List<BoardEdit>();
            Random random = new Random();
            for (uint col = 0; col < size; col++)
            {
                for (uint row = 0; row < size; row++)
                {
                    if (board[col, row] != null)
                    {
                        bool zombie;
                        uint neighbors = getNeighbors(col, row,out zombie);

                        if (board[col, row] == 0)//Dead cell
                        {
                            if(neighbors == 3){
                                changes.Add(new BoardEdit(col,row,1));
                            }
                        }else if(board[col,row] == 1)//Living cell
                        {
                            if (neighbors < 2 || neighbors > 3)
                            {
                                changes.Add(new BoardEdit(col,row,0));
                            }
                            else if (zombie && random.Next(2) == 0)
                            {
                                changes.Add(new BoardEdit(col,row,null));
                            }
                        }
                    }
                }
            }

            ChangeStatus(changes.ToArray());
        }

        public void ChangeStatus(params BoardEdit[] edits)
        {
            foreach (BoardEdit edit in edits)
            {
                if (withInBoard(edit.col, edit.row) && (edit.status == 0 || edit.status == 1 || edit.status == null))
                {
                    board[edit.col, edit.row] = edit.status;
                }
            }
        }

        public void GenerateRandomBoard()
        {
            GenerateRandom(new Random());    
        }

        public void GenerateRandomBoard(int Seed)
        {
            GenerateRandom(new Random(Seed));
        }

        private void GenerateRandom(Random random)
        {
            for (uint col = 0; col < size; col++)
            {
                for (uint row = 0; row < size; row++)
                {
                    int option = random.Next(3);
                    if (option == 0) { board[col, row] = 0; }
                    if (option == 1) { board[col,row] = 1; }
                    if (option == 2) { board[col, row] = null; };
                }
            }
        }


        public struct BoardEdit
        {
            public uint col, row;
            public int? status;

            public BoardEdit(uint col, uint row, int? status)
            {
                this.col = col;
                this.row = row;
                this.status = status;
            }
        }

        private uint getNeighbors(uint col, uint row, out bool zombie)
        {
            if (withInBoard(col, row))
            {
                zombie = false;
                uint neighbors = 0;

                //Left col
                if (col >= 1)
                {
                    if (board[col - 1, row] == null) { zombie = true; }
                    else if (board[col - 1, row] == 1) { neighbors++; }

                    if (row >= 1)
                    {
                        if (board[col - 1, row - 1] == null) { zombie = true; }
                        else if (board[col - 1, row - 1] == 1) { neighbors++; }
                    }

                    if (row < size - 1)
                    {
                        if (board[col - 1, row + 1] == null) { zombie = true; }
                        else if (board[col - 1, row + 1] == 1) { neighbors++; }
                    }
                }
                
                //Right col
                if (col < size - 1)
                {
                    if (board[col + 1, row] == null) { zombie = true; }
                    else if (board[col + 1, row] == 1) { neighbors++; }

                    if (row > 1)
                    {
                        if (board[col + 1, row - 1] == null) { zombie = true; }
                        else if (board[col + 1, row - 1] == 1) { neighbors++; }
                    }

                    if (row < size - 1)
                    {
                        if (board[col + 1, row + 1] == null) { zombie = true; }
                        else if (board[col + 1, row + 1] == 1) { neighbors++; }
                    }
                }
                
                //Up/Down
                if (row > 1)
                {
                    if (board[col, row - 1] == null) { zombie = true; }
                    else if (board[col, row - 1] == 1) { neighbors++; }
                }

                if (row < size - 1)
                {
                    if (board[col, row + 1] == null) { zombie = true; }
                    else if (board[col, row + 1] == 1) {neighbors++; }
                }

                return neighbors;
            }
            else
            {
                throw new IndexOutOfRangeException(col + "," + row + " not in board");
            }
        }

        private bool withInBoard(uint col, uint row)
        {
            return col >= 0 && col < size && row >= 0 && row < size;
        }

    }
}
