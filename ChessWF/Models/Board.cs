using ChessWF.Models.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessWF.Models
{
    public class Board
    {
        public const int SIZE = 8;
        public Sell[,] Sells { get; set; }
        public Board()
        {
            Sells = GetEmptyBoard();
        }
        public Board(Sell[,] sells)
        {
            Sells = sells;
        }

        public Figure GetFigure(int x, int y)
        {
            if (x < 0 || y < 0 || x >= SIZE || y >= SIZE)
                throw new ArgumentException("Out of board bounds");

            return Sells[x, y].Figure;
        }

        public static Sell[,] GetEmptyBoard()
        {
            Sell[,] sells = new Sell[SIZE, SIZE];

            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    sells[i, j] = new Sell() { X = i, Y = j };
                }
            }

            return sells;
        }
    }
}
