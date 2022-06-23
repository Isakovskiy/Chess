using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Figures
{
    public class Queen : Figure
    {
        public Queen(string image, Sell sell, FigureColor color = FigureColor.Black) : base(image, sell, color)
        {
        }

        public override List<Sell> GetAvaibleSells(Sell[,] boardSells)
        {
            var list = new List<Sell>();
            list.AddRange(new Rook(image: "fake", CurrentSell).GetAvaibleSells(boardSells));
            list.AddRange(new Bishop(image: "fake", CurrentSell).GetAvaibleSells(boardSells));

            return list;
        }
    }
}
