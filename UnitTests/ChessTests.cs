using Domain;
using Domain.Models;
using Domain.Models.Figures;
using NUnit.Framework;
using System.Collections.Generic;

namespace UnitTests
{
	internal class ChessTests
	{
		[SetUp]
		public void Setup()
		{

		}
		[Test]
		public void ChooseFigureTest()
		{
			var painter = new FakeDrawer();
			var chess = new Chess(painter, new FakeFiguresDrawer());

			var f = new King(painter.Cells[0, 0], new FakeFiguresDrawer(), color: FigureColor.White);

			chess.ChooseFigure(0, 0);
			Assert.IsTrue(painter.AvaibleSells?.Count == 3);

			var e = new Rook(painter.Cells[0, 6], new FakeFiguresDrawer());
			chess.ChooseFigure(0, 0);
			Assert.IsTrue(painter.AvaibleSells?.Count == 2);

			var e2 = new Pawn(painter.Cells[2, 1], new FakeFiguresDrawer());
			chess.ChooseFigure(0, 0);
			Assert.IsTrue(painter.AvaibleSells?.Count == 1);
		}
		[Test]
		public void CheckTest()
		{
			var painter = new FakeDrawer();
			Chess chess = new Chess(painter, new FakeFiguresDrawer());
			var board = painter.Cells;
			var kingW = new King(board[4, 0], new FakeFiguresDrawer(), FigureColor.White);
			var pawnW = new Pawn(board[4, 1], new FakeFiguresDrawer(), FigureColor.White);
			var pawnW2 = new Pawn(board[0, 1], new FakeFiguresDrawer(), FigureColor.White);
			var rookB = new Rook(board[4, 7], new FakeFiguresDrawer());
			var queenB = new Queen(board[4, 3], new FakeFiguresDrawer());


			chess.ChooseFigure(0, 1);
			chess.Move(board[0, 2]);
			chess.ChooseFigure(4, 3);
			var g = chess.Move(board[4, 1]);
			Assert.IsTrue(g == GameResult.BlackWon);
		}
		[Test]
		public void DrawTest()
		{
			var painter = new FakeDrawer();
			var f = new FakeFiguresDrawer();
			Chess chess = new Chess(painter, new FakeFiguresDrawer());
			var board = painter.Cells;
			var bishopB1 = new Bishop(board[2, 2], f);
			var bishopB2 = new Bishop(board[3, 2], f);
			var knightB = new Knight(board[4, 4], f);

			chess.ChooseFigure(4, 0);
			chess.Move(board[6, 0]);
			
			chess.ChooseFigure(2, 2);
			chess.Move(board[0, 0]);

			chess.ChooseFigure(6, 0);
			chess.Move(board[7, 0]);

			chess.ChooseFigure(3, 2);
			chess.Move(board[5, 0]);

			chess.ChooseFigure(7, 0);
			chess.Move(board[7, 1]);

			chess.ChooseFigure(4, 4);
			chess.Move(board[5, 2]);

			chess.ChooseFigure(7, 1);
			chess.Move(board[7, 0]);

			chess.ChooseFigure(0, 0);
			var g = chess.Move(board[1, 1]);
			Assert.IsTrue(g == GameResult.Draw);
		}
	}
}
