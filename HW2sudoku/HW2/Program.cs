using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW2
{

    public class Cell
    {
        public int changable;
        public int val;

        public Cell(int change, int value)
        {
            changable = change;
            val = value;
        }
    }
    
    
   

    class Program
    {

        public static bool Solve(Cell[,] board, int r, int c)
        {
            if (r == 9)
            {
                int count = 0;
                for (int i = 0; i < 9; i++)                                                // check to see if board is solved
                {
                    for (int j = 0; j < 9; j++)
                    {
                        count += board[i, j].changable > 0 ? 1 : 0;
                    }
                }
                if (count == 81)
                    return true;
                else
                    return false;
            }
            if (board[r, c].changable >= 1)                                                 // check to see if value is changable
            {
                int nextR = r;
                int nextC = c + 1;
                if (nextC == 9)
                {
                    nextR = r + 1; nextC = 0;
                }
                return Solve(board, nextR, nextC);                                          // if value is not changable send the next cell through solver
            }
            else
            {
                bool[] used = new bool[9];
                for (int i = 0; i < 9; i++)                                                 // check row to see what values are used
                {
                    if (board[r, i].changable >= 1)
                        used[board[r, i].val - 1] = true;
                }
                for (int i = 0; i < 9; i++)                                                 // check column to see what values are used
                {
                    if (board[i, c].changable >= 1)
                        used[board[i, c].val - 1] = true;
                }

                for (int i = r - (r % 3); i < r - (r % 3) + 3; i++)                         // check box to see what values are used
                {
                    for (int j = c - (c % 3); j < c - (c % 3) + 3; j++)
                    {
                        if (board[i, j].changable >= 1)
                            used[board[i, j].val - 1] = true;
                    }
                }
                for (int i = 0; i < used.Length; i++)                                       // assign value to board
                {
                    if (!used[i])
                    {
                        board[r, c].changable = 1;
                        board[r, c].val = i + 1;
                        int nextR = r;
                        int nextC = c + 1;
                        if (nextC == 9)
                        {
                            nextR = r + 1; nextC = 0;
                        }
                        if (Solve(board, nextR, nextC))
                            return true;
                        for (int m = 0; m < 9; m++)
                            for (int n = 0; n < 9; n++)
                            {
                                if (m > r || (m == r && n >= c))
                                {
                                    if (board[m, n].changable == 1)
                                    {
                                        board[m, n].changable = 0;
                                        board[m, n].val = 0;
                                    }
                                }
                            }

                    }
                }

            }
            return false;
        }

        public static void Print(Cell [,] board)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write( "{0} ", board[i,j].val);
                }
                Console.WriteLine();
            }
        }
    
        static void Main(string[] args)
        {

            Cell[,] board = new Cell [,]{
            {new Cell(0, 0), new Cell(0, 0), new Cell(0, 0), new Cell(2, 1), new Cell(0, 0), new Cell(2, 8), new Cell(0, 0), new Cell(2, 3), new Cell(2, 2)},
            {new Cell(2, 5), new Cell(2, 9), new Cell(0, 0), new Cell(0, 0), new Cell(2, 4), new Cell(0, 0), new Cell(0, 0), new Cell(0, 0), new Cell(0, 0)},
            {new Cell(0, 0), new Cell(0, 0), new Cell(0, 0), new Cell(2, 7), new Cell(0, 0), new Cell(0, 0), new Cell(2, 4), new Cell(0, 0), new Cell(0, 0)},
            {new Cell(2, 9), new Cell(0, 0), new Cell(0, 0), new Cell(0, 0), new Cell(0, 0), new Cell(2, 6), new Cell(0, 0), new Cell(0, 0), new Cell(0, 0)},
            {new Cell(2, 8), new Cell(0, 0), new Cell(2, 3), new Cell(0, 0), new Cell(2, 2), new Cell(0, 0), new Cell(2, 6), new Cell(0, 0), new Cell(2, 5)},
            {new Cell(0, 0), new Cell(0, 0), new Cell(0, 0), new Cell(2, 4), new Cell(0, 0), new Cell(0, 0), new Cell(0, 0), new Cell(0, 0), new Cell(2, 9)},
            {new Cell(0, 0), new Cell(0, 0), new Cell(2, 7), new Cell(0, 0), new Cell(0, 0), new Cell(2, 4), new Cell(0, 0), new Cell(0, 0), new Cell(0, 0)},
            {new Cell(0, 0), new Cell(0, 0), new Cell(0, 0), new Cell(0, 0),  new Cell(2, 3), new Cell(0, 0), new Cell(0, 0), new Cell(2, 8), new Cell(2, 4)},
            {new Cell(2, 2), new Cell(2, 1), new Cell(0, 0), new Cell(2, 8), new Cell(0, 0), new Cell(2, 9), new Cell(0, 0), new Cell(0, 0), new Cell(0, 0)}};

            Print(board);

            Console.WriteLine();

            if (Solve(board, 0, 0))
            {
                Console.WriteLine("Found a Solution!");
                Print(board);
            }
            else
            {
                Console.WriteLine("No solution found");
            }

            
        }
    }
    
}
