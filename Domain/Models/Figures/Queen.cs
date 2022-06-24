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
			//list.AddRange(new Rook(CurrentSell).GetAvaibleSells(boardSells));
			//list.AddRange(new Bishop(CurrentSell).GetAvaibleSells(boardSells));

			for (int i = -1; i <= 1; i += 2)
			{
				for (int j = -1; j <= 1; j += 2)
				{
					var x = CurrentSell.X + i;
					var y = CurrentSell.Y + j;
					while (CanMoveTo(x, y, boardSells))
					{
						list.Add(boardSells[x, y]);
						if (boardSells[x, y].Figure != null) break;
						x += i;
						y += j;
					}
				}
			}
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
			}

			for (int i = -1; i <= 1; i += 2)
			{
				int x = CurrentSell.X;
				int y = CurrentSell.Y + i;
				while (CanMoveTo(x, y, boardSells))
				{
					list.Add(boardSells[x, y]);
					if (boardSells[x, y].Figure != null) break;
					y += i;
				}
			}
			return list;
        }
    }
}
