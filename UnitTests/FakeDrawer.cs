using Domain.Models;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
	class FakeDrawer : IBoardPainter
	{
		public List<Sell> AvaibleSells { get; set; }
		public Sell[,] Sells { get; set; }
		public Figure ChoosedFigure { get; set; }

		public void DrawBoard(Sell[,] sells)
		{
			Sells = sells;
		}
		public void ResetAvaibleSells()
		{
			AvaibleSells = null;
		}
		public void DrawAvaibleSells(List<Sell> avaibleSells)
		{
			AvaibleSells = avaibleSells;
		}
		public void ChooseFigure(Sell figureSell)
		{
			ChoosedFigure = figureSell.Figure;
		}
	}
}
