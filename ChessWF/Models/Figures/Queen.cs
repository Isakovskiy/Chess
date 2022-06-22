using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessWF.Models.Figures
{
    public class Queen : Figure
    {
        public Queen(string image, FigureColor color = FigureColor.Black) : base(image, color)
        {
        }

        public override List<Sell> GetAvaibleSells(Sell figureSell, Sell[,] boardSells)
        {
            var list = new List<Sell>();
            list.AddRange(new Rook(image: "fake").GetAvaibleSells(figureSell, boardSells));
            list.AddRange(new Bishop(image: "fake").GetAvaibleSells(figureSell, boardSells));

            return list;
        }
    }
}
