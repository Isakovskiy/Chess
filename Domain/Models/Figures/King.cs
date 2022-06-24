using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Figures
{
	public class King : Figure
	{
		public King(string image, Cell sell, FigureColor color = FigureColor.Black) : base(image, sell, color) { }

        public override List<Cell> GetAvaibleCells(Cell[,] boardSells)
		{
			var list = new List<Cell>();
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
				int x = CurrentCell.X + tuple[i].Item1;
				int y = CurrentCell.Y + tuple[i].Item2;

				if (CanMoveTo(x, y, boardSells))
				{
					list.Add(boardSells[x, y]);
				}
			}

			if (!Moved)
			{
				//roque
				for (int direction = -1; direction <= 1; direction += 2)
                {
					int x = CurrentCell.X + direction;
					int y = CurrentCell.Y;

					while (true)
					{
						if (x < 0 || x > 7)
							break;
						if (boardSells[x, y].Figure is Rook && !boardSells[x, y].Figure.Moved)
						{
							list.Add(boardSells[CurrentCell.X += direction * 2, CurrentCell.Y]);
							break;
						}
						else if (boardSells[x, y].Figure != null)
							break;
						x += direction;
					}
				}
			}

			return list;
		}
	}
}
