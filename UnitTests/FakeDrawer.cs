using Domain.Models;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Figures;

namespace UnitTests
{
	class FakeDrawer : IBoardPainter
	{
		public List<Cell> AvaibleSells { get; set; }
		public Cell[,] Cells { get; set; }

		public void DrawBoard(Cell[,] cells)
		{
			this.Cells = cells;
		}
		public void ResetAvaibleCells()
		{
			AvaibleSells = null;
		}
		public void DrawAvaibleCells(List<Cell> avaibleSells)
		{
			AvaibleSells = avaibleSells;
		}
	}

    class FakeFiguresDrawer : IFiguresPainter
    {
		public Figure ChoosedFigure { get; set; }
		public void ChooseFigure(Cell figureSell)
        {
			ChoosedFigure = figureSell.Figure;

		}
        public Figure DrawFigureReplaceSelection(Cell figureCell)
        {
			return new Queen(image: "fake", figureCell, new FakeFiguresDrawer(), figureCell.Figure.Color);
        }
    }
}
