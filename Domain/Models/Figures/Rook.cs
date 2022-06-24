

namespace Domain.Models.Figures
{
	public class Rook : Figure
	{
		public Rook(string image, Cell sell, FigureColor color = FigureColor.Black) : base(image, sell, color)
		{

		}
		public override List<Cell> GetAvaibleCells(Cell[,] boardSells)
		{
			List<Cell> list = new List<Cell>();

			for (int i = -1; i <= 1; i+=2)
			{
				int x = CurrentCell.X + i;
				int y = CurrentCell.Y;
				while (CanMoveTo(x, y, boardSells))
				{
					list.Add(boardSells[x, y]);
					if (boardSells[x, y].Figure != null) break;
					x += i;
				}
			}	

			for (int i = -1; i <= 1; i+=2)
			{
				int x = CurrentCell.X;
				int y = CurrentCell.Y + i;
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
