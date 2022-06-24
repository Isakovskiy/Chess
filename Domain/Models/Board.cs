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
        public Cell[,] Cells { get; set; }
        public Board()
        {
            Cells = GetEmptyBoard();
        }
        public Board(Cell[,] sells)
        {
            Cells = sells;
        }

        public Figure GetFigure(int x, int y)
        {
            if (x < 0 || y < 0 || x >= SIZE || y >= SIZE)
                throw new ArgumentException("Out of board bounds");

            return Cells[x, y].Figure;
        }

        public IEnumerable<Tuple<Figure, Cell>> GetFigures()
        {
            return GetFigures(f => true).Select(f => new Tuple<Figure, Cell>(f, f.CurrentCell));
        }
            

        public IEnumerable<Figure> GetFigures(Func<Figure, bool> predicate)
        {
            for(int x = 0; x < SIZE; x++)
            {
                for(var y = 0; y < SIZE; y++)
                {
                    if(Cells[x, y].Figure != null && predicate(Cells[x, y].Figure))
                    {
                        yield return Cells[x, y].Figure;
                    }
                }
            }
        }

        public static Cell[,] GetEmptyBoard()
        {
            Cell[,] cells = new Cell[SIZE, SIZE];

            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    cells[i, j] = new Cell() { X = i, Y = j };
                }
            }

            return cells;
        }
    }
}
