using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Figures
{
    public class Queen : Figure
    {
        public Queen(string image, Sell sell, FigureColor color = FigureColor.Black) : base(image, sell, color)
        {
        }
		
        public override List<Sell> GetAvaibleSells(Sell[,] boardSells)
        {
            var list = new List<Sell>();

			for (int i = -1; i <= 1; i += 2)
			{
				int x = CurrentSell.X + i;
				int y = CurrentSell.Y;
				while (CanMoveTo(x, y, boardSells))
				{
					list.Add(boardSells[x, y]);
					if (boardSells[x, y].Figure != null) break;
					x += i;
				}

				x = CurrentSell.X;
				y = CurrentSell.Y + i;
				while (CanMoveTo(x, y, boardSells))
				{
					list.Add(boardSells[x, y]);
					if (boardSells[x, y].Figure != null) break;
					y += i;
				}

				for (int j = -1; j <= 1; j += 2)
				{
					x = CurrentSell.X + i;
					y = CurrentSell.Y + j;
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
