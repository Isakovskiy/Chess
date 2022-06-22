using ChessWF.Models.Figures;
using ChessWF.Models;
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
			var k = new King("fake", FigureColor.White);
			var board = Board.GetEmptyBoard();

			var list = k.GetAvaibleSells(board[4, 0], board);
			Assert.IsTrue(list.Count == 5);

			board[3, 0].Figure = new Queen("fake");
			board[5, 0].Figure = new Queen("fake", FigureColor.White);

			list = k.GetAvaibleSells(board[4, 0], board);
			Assert.IsTrue(list.Count == 4);

			board[0, 0].Figure = new Rook("fake", FigureColor.White);
			board[7, 0].Figure = new Rook("fake", FigureColor.White);
			board[3, 0].Figure = null;

			list = k.GetAvaibleSells(board[4, 0], board);
			Assert.IsTrue(list.Count == 5 && list.Contains(board[2, 0]));
		}
	}
}
