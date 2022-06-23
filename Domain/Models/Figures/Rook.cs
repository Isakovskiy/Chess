

namespace Domain.Models.Figures
{
	public class Rook : Figure
	{
		public Rook(string image, Sell sell, FigureColor color = FigureColor.Black) : base(image, sell, color)
		{

		}
		public override List<Sell> GetAvaibleSells(Sell[,] boardSells)
		{
			List<Sell> list = new List<Sell>();

			for (int i = -1; i <= 1; i+=2)
			{
				int x = CurrentSell.X + i;
				int y = CurrentSell.Y;
				while (CanMoveTo(x, y, boardSells))
				{
					list.Add(boardSells[x, y]);
					x += i;
				}
			}	

			for (int i = -1; i <= 1; i+=2)
			{
				int x = CurrentSell.X;
				int y = CurrentSell.Y + i;
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
