using Domain.Models.Figures;
using Domain.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
	internal class KingTests
	{
		[SetUp]
		public void Setup()
		{

		}

		[Test]
		public void GetAvaibleSellsTest()
		{
			var board = Board.GetEmptyBoard();
			var k = new King("fake", board[4, 0], new FakeFiguresDrawer(), FigureColor.White);

			var list = k.GetAvaibleCells(board);
			Assert.IsTrue(list.Count == 5);

			var q1 = new Queen("fake", board[3, 0], new FakeFiguresDrawer());
			var q2 = new Queen("fake", board[5, 0], new FakeFiguresDrawer(), FigureColor.White);

			list = k.GetAvaibleCells(board);
			Assert.IsTrue(list.Count == 4);

			var r = new Rook("fake", board[0, 0], new FakeFiguresDrawer(), FigureColor.White);
			r = new Rook("fake", board[7, 0], new FakeFiguresDrawer(), FigureColor.White);
			q1.Move(board[7, 7]);

			list = k.GetAvaibleCells(board);
			Assert.IsTrue(list.Count == 5 && list.Contains(board[2, 0]));
		}
	}
}
