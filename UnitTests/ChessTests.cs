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
        class FakeDrawer : IBoardPainter
        {
            public List<Sell> AvaibleSells { get; set; }
            public Sell[,] Sells { get; set; }
            public void Draw(Sell[,] sells)
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
        }

        [Test]
        public void ChooseFigureTest()
        {
            var painter = new FakeDrawer();
            var chess = new Chess(painter);

            var f = new King(image: "fake", painter.Sells[0, 0], color: FigureColor.White);

            chess.ChooseFigure(0, 0);
            Assert.IsTrue(painter.AvaibleSells?.Count == 3);

            var e = new Rook(image: "fake", painter.Sells[0, 6]);
            chess.ChooseFigure(0, 0);
            Assert.IsTrue(painter.AvaibleSells?.Count == 2);

            var e2 = new Pawn(image: "fake", painter.Sells[2, 1]);
            chess.ChooseFigure(0, 0);
            Assert.IsTrue(painter.AvaibleSells?.Count == 1);
        }

    }
}
