using NUnit.Framework;



namespace UnitTests
{
    internal class PawnTests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void GetAvaibleSellsTest()
        {
            //var p = new Pawn("fake", FigureColor.White);
            //var sells = Board.GetEmptyBoard();

            ////sells[]
            //sells[3, 2].Figure = new Rook(image: "fake", color: FigureColor.Black);
            //var list = p.GetAvaibleSells(sells[2, 1], sells);
            //Assert.IsTrue(list.Contains(sells[2, 2]) && 
            //    list.Contains(sells[2, 3]) && 
            //    list.Count == 3);

            //p.Move(sells[2, 2]);
            //list = p.GetAvaibleSells(sells[2, 2], sells);
            //Assert.IsTrue(list.Contains(sells[2, 3]) && list.Count == 1);

            //sells[2, 3].Figure = new Rook(image: "fake", color: FigureColor.White);
            //sells[1, 3].Figure = new Rook(image: "fake", color: FigureColor.White);
            //sells[3, 3].Figure = new Rook(image: "fake", color: FigureColor.Black);

            //list = p.GetAvaibleSells(sells[2, 2], sells);
            //Assert.IsTrue(list[0] == sells[3, 3] && list.Count == 1);
        }

    }
}
