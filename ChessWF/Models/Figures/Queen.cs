using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessWF.Models.Figures
{
    internal class Queen : Figure
    {
        public Queen(string image, bool isBlack = false) : base(image, isBlack)
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
