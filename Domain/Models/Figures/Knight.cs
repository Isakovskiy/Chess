using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Figures
{
    public class Knight : Figure
    {
        public Knight(Cell sell, IFiguresPainter figuresPainter, FigureColor color = FigureColor.Black) : base(sell, figuresPainter, color)
        {
        }

        public override List<Cell> GetAvaibleCells(Cell[,] boardSells)
        {
            var list = new List<Cell>();
            Tuple<int, int>[] tuple = new Tuple<int, int>[8]
            {
                new Tuple<int, int>(-2, 1),
                new Tuple<int, int>(-2, -1),
                new Tuple<int, int>(2, 1),
                new Tuple<int, int>(2, -1),
                new Tuple<int, int>(-1, 2),
                new Tuple<int, int>(-1, -2),
                new Tuple<int, int>(1, 2),
                new Tuple<int, int>(1, -2),

            };

            for(int i = 0; i < tuple.Length; i++)
            {
                int x = CurrentCell.X + tuple[i].Item1;
                int y = CurrentCell.Y + tuple[i].Item2;

                if (CanMoveTo(x, y, boardSells))
                {
                    list.Add(boardSells[x, y]);
                }
            }
            return list;
        }
    }
}
