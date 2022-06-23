using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Figures
{
    public class Pawn : Figure
    {
        public Pawn(string image, Sell sell, FigureColor color = FigureColor.Black) : base(image, sell, color)
        {
        }
        public override List<Sell> GetAvaibleSells(Sell[,] boardSells)
        {
            var list = new List<Sell>();
            var dir = Color == FigureColor.Black ? -1 : 1;

            var x = CurrentSell.X;
            var y = CurrentSell.Y;

            if (CanMoveTo(x, y + dir, boardSells))
            {
                list.Add(boardSells[x, y + dir]);

                if(!Moved && CanMoveTo(x, y + dir * 2, boardSells))
                {
                    list.Add(boardSells[x, y + dir * 2]);
                }
            }
            for(int i = -1; i <= 1; i += 2)
            {
                if (CanMoveTo(x + i, y + dir, boardSells) && boardSells[x + i, y + dir].Figure != null)
                {
                    list.Add(boardSells[x + i, y + dir]);
                }
            }

            return list;
        }

    }


}
