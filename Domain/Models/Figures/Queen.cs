using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Figures
{
    public class Queen : Figure
    {
        public Queen(string image, Cell sell, FigureColor color = FigureColor.Black) : base(image, sell, color)
        {
        }
		
        public override List<Cell> GetAvaibleCells(Cell[,] boardSells)
        {
            var list = new List<Cell>();

			for (int i = -1; i <= 1; i += 2)
			{
				int x = CurrentCell.X + i;
				int y = CurrentCell.Y;
				while (CanMoveTo(x, y, boardSells))
				{
					list.Add(boardSells[x, y]);
					if (boardSells[x, y].Figure != null) break;
					x += i;
				}

				x = CurrentCell.X;
				y = CurrentCell.Y + i;
				while (CanMoveTo(x, y, boardSells))
				{
					list.Add(boardSells[x, y]);
					if (boardSells[x, y].Figure != null) break;
					y += i;
				}

				for (int j = -1; j <= 1; j += 2)
				{
					x = CurrentCell.X + i;
					y = CurrentCell.Y + j;
					while (CanMoveTo(x, y, boardSells))
					{
						list.Add(boardSells[x, y]);
						if (boardSells[x, y].Figure != null) break;
						x += i;
						y += j;
					}
				}
			}
			return list;
        }
    }
}
