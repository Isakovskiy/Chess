using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Figures
{
	public class King : Figure
	{
		public King(Cell sell, IFiguresPainter figuresPainter, FigureColor color = FigureColor.Black) : base(sell, figuresPainter, color)
		{ }

		public event Action<int, int> BigCastling;
		public event Action<int, int> SmallCastling;
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

			//castling
			if (!Moved)
			{
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
							list.Add(boardSells[CurrentCell.X + direction * 2, CurrentCell.Y]);
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

		public override void Move(Cell newSell)
		{
			Cell oldCell = CurrentCell;
			base.Move(newSell);
			int direction = CurrentCell.X - oldCell.X; // -2 -> влево  2 -> вправо
			if (Math.Abs(direction) == 2)
			{
				if (direction == -2)
				{
					if (Color == FigureColor.White)
						BigCastling(3, 0);
					else
						SmallCastling(2, 7);
				}
				else if (direction == 2)
				{
					if (Color == FigureColor.White)
						SmallCastling(5, 0);
					else
						BigCastling(4, 7);
				}
				else
					throw new Exception("Error with castling, obratites' k programmistu");
			}
		}
	}
}
