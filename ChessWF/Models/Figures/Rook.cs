using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessWF.Models.Figures
{
	public class Rook : Figure
	{
		public Rook(string image, FigureColor color = FigureColor.Black) : base(image, color)
		{

		}
		public override List<Sell> GetAvaibleSells(Sell figureSell, Sell[,] boardSells)
		{
			List<Sell> list = new List<Sell>();

			for (int i = -1; i <= 1; i+=2)
			{
				int x = figureSell.X + i;
				int y = figureSell.Y;
				while (CanMoveTo(x, y, boardSells))
				{
					list.Add(boardSells[x, y]);
					x += i;
				}
			}	

			for (int i = -1; i <= 1; i+=2)
			{
				int x = figureSell.X;
				int y = figureSell.Y + i;
				while (CanMoveTo(x, y, boardSells))
				{
					list.Add(boardSells[x, y]);
					y += i;
				}
			}

			return list;
		}
	}
}
