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
		public List<Cell> AvaibleSells { get; set; }
		public Cell[,] Sells { get; set; }
		public Figure ChoosedFigure { get; set; }

		public void DrawBoard(Cell[,] sells)
		{
			Sells = sells;
		}
		public void ResetAvaibleSells()
		{
			AvaibleSells = null;
		}
		public void DrawAvaibleSells(List<Cell> avaibleSells)
		{
			AvaibleSells = avaibleSells;
		}
		public void ChooseFigure(Cell figureSell)
		{
			ChoosedFigure = figureSell.Figure;
		}
	}
}
