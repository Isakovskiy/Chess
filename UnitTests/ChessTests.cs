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

            var f = new King(image: "fake", painter.Cells[0, 0], new FakeFiguresDrawer(), color: FigureColor.White);

            chess.ChooseFigure(0, 0);
            Assert.IsTrue(painter.AvaibleSells?.Count == 3);

            var e = new Rook(image: "fake", painter.Cells[0, 6], new FakeFiguresDrawer());
            chess.ChooseFigure(0, 0);
            Assert.IsTrue(painter.AvaibleSells?.Count == 2);

            var e2 = new Pawn(image: "fake", painter.Cells[2, 1], new FakeFiguresDrawer());
            chess.ChooseFigure(0, 0);
            Assert.IsTrue(painter.AvaibleSells?.Count == 1);
        }

    }
}
