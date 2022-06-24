using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Figures
{
    public class Bishop : Figure
    {
        public Bishop(string image, Cell sell, FigureColor color = FigureColor.Black) : base(image, sell, color)
        {
        }

        public override List<Cell> GetAvaibleCells(Cell[,] boardCells)
        {
            var list = new List<Cell>();

            for(int i = -1; i <= 1; i += 2)
            {
                for(int j = -1; j <= 1; j += 2)
                {
                    var x = CurrentCell.X + i;
                    var y = CurrentCell.Y + j;
                    while (CanMoveTo(x, y, boardCells))
                    {
                        list.Add(boardCells[x, y]);
						if (boardCells[x, y].Figure != null) break;
						x += i;
                        y += j;
                    }
                }
            }

            return list;
        }
    }
}
