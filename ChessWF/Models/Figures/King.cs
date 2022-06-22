using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessWF.Models.Figures
{
	public class King : Figure
	{
		public King(string image, FigureColor color = FigureColor.Black) : base(image, color)
		{
		}

		public override List<Sell> GetAvaibleSells(Sell figureSell, Sell[,] boardSells)
		{
			var list = new List<Sell>();
			Tuple<int, int>[] tuple = new Tuple<int, int>[8]
			{
				new Tuple<int, int>(0, 1),
				new Tuple<int, int>(0, -1),
				new Tuple<int, int>(1, 0),
				new Tuple<int, int>(-1, 0),
				new Tuple<int, int>(-1, -1),
				new Tuple<int, int>(1, 1),
				new Tuple<int, int>(1, -1),
				new Tuple<int, int>(-1, 1),
			};

			for (int i = 0; i < tuple.Length; i++)
			{
				int x = figureSell.X + tuple[i].Item1;
				int y = figureSell.Y + tuple[i].Item2;

				if (CanMoveTo(x, y, boardSells))
				{
					list.Add(boardSells[x, y]);
				}
			}

			//roque
			for (int direction = -1; direction <= 1; direction += 2) 
			{
				if (!Moved)
				{
					int x = figureSell.X + direction;
					int y = figureSell.Y;
					while (true)
					{
						if (x < 0 || x > 7)
							break;
						if (boardSells[x, y].Figure is Rook && !boardSells[x, y].Figure.Moved)
						{
							list.Add(boardSells[figureSell.X += direction * 2, figureSell.Y]);
							break;
						}
						else if(boardSells[x, y].Figure != null)
							break;
						x += direction;
					}
				}
			}
			return list;
		}
	}
}
