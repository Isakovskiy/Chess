using Domain;
using NUnit.Framework;
using Domain.Models;
using Domain.Models.Figures;
using System.Collections.Generic;

namespace UnitTests
{
	internal class CheckMateTests
	{
		[SetUp]
		public void Setup()
		{

		}
		
		[Test]
		public void CheckTest()
		{
			var painter = new FakeDrawer();
			Chess chess = new Chess(painter, new FakeFiguresDrawer());
			var board = painter.Cells;
			var kingW = new King("fake", board[4, 0], new FakeFiguresDrawer(), FigureColor.White);
			var pawnW = new Pawn("fake", board[4, 1], new FakeFiguresDrawer(), FigureColor.White);
			var pawnW2 = new Pawn("fake", board[0, 1], new FakeFiguresDrawer(), FigureColor.White);
			var rookB = new Rook("fake", board[4, 7], new FakeFiguresDrawer());
			var queenB = new Queen("fake", board[4, 3], new FakeFiguresDrawer());


			chess.ChooseFigure(0, 1);
			chess.Move(board[0, 2]);
			chess.ChooseFigure(4, 3);
			var g = chess.Move(board[4, 1]);
			Assert.IsTrue(g == GameResult.BlackWon);
		}
	}
}
