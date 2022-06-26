using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
	[Serializable]
	public class Packet
	{
		public int X { get; set; }
		public int Y { get; set; }
		public string FigureName { get; set; }
		public string FigureColor { get; set; }
		public string CellColor { get; set; }
		public Packet(int x, int y, string figureName, string figureColor, string cellColor)
		{
			X = x;
			Y = y;
			FigureName = figureName;
			FigureColor = figureColor;
			CellColor = cellColor;
		}
	}
}
