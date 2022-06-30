using Domain.Models.Figures;
using Domain.Models;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
	internal class FigurePainter : IFiguresPainter
	{
		private Packet[] packets;
		public FigurePainter(Packet[] pack)
		{
			packets = pack;
		}
		public void CancelFigure(Figure figure)
		{
			Packet p = packets.First(p => p.Y == figure.CurrentCell.Y && p.X == figure.CurrentCell.X);
			p.CellColor = (p.X + p.Y) % 2 == 0 ? "DimGray" : "White";
		}

		public void ChooseFigure(Cell figureCell)
		{
			packets.First(p => p.X == figureCell.X && p.Y == figureCell.Y).CellColor = "Yellow";
		}

		public TransformFigures DrawFigureReplaceSelectionAndGet()
		{
			return TransformFigures.Queen;
		}

		public void MoveFigure(Cell from, Cell to)
		{

		}
	}
}
