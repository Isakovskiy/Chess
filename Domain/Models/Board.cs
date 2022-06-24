using Domain.Models.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
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

        public IEnumerable<Tuple<Figure, Sell>> GetFigures()
        {
            return GetFigures(f => true).Select(f => new Tuple<Figure, Sell>(f, f.CurrentSell));
        }
            

        public IEnumerable<Figure> GetFigures(Func<Figure, bool> predicate)
        {
            for(int x = 0; x < SIZE; x++)
            {
                for(var y = 0; y < SIZE; y++)
                {
                    if(Sells[x, y].Figure != null && predicate(Sells[x, y].Figure))
                    {
                        yield return Sells[x, y].Figure;
                    }
                }
            }
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
