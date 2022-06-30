using Domain.Models;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
	internal class BoardPainter : IBoardPainter
	{
		private Packet[] packets;
		public BoardPainter(Packet[] pack)
		{
			packets = pack;
		}
		public void DrawAvaibleCells(List<Cell> avaibleCells)
		{
			foreach (Cell cell in avaibleCells)
				packets.First(p => p.X == cell.X && p.Y == cell.Y).CellColor = "Blue";
		}

		public void DrawBoard(Cell[,] sells)
		{
			int i = 0;
			for (int x = 0; x < sells.GetLength(0); x++)
				for (int y = 0; y < sells.GetLength(1); y++)
				{
					if (x == 5 && y == 2)
					{

					}
					packets[i] = new Packet(
						x: x,
						y: y,
						figureName: sells[x, y].Figure?.Name,
						figureColor: sells[x, y].Figure?.Color.ToString(),
						cellColor: ((x + y) % 2 == 0) ? "DimGray" : "White");
					i++;
				}
		}

		public void ResetAvaibleCells()
		{
			foreach (Packet p in packets.Where(p => p.CellColor == "Blue"))
				p.CellColor = (p.X + p.Y) % 2 == 0 ? "DimGray" : "White";
		}
	}
}
