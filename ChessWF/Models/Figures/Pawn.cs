using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessWF.Models.Figures
{
    public class Pawn : Figure
    {
        public Pawn(string image, FigureColor color = FigureColor.Black) : base(image, color)
        {
        }
        public override List<Sell> GetAvaibleSells(Sell figureSell, Sell[,] boardSells)
        {
            var list = new List<Sell>();
            var dir = Color == FigureColor.Black ? -1 : 1;

            if (CanMoveTo(figureSell.X, figureSell.Y + dir, boardSells))
            {
                list.Add(boardSells[figureSell.X, figureSell.Y + dir]);

                if(!Moved && CanMoveTo(figureSell.X, figureSell.Y + dir * 2, boardSells))
                {
                    list.Add(boardSells[figureSell.X, figureSell.Y + dir * 2]);
                }
            }
            for(int i = -1; i <= 1; i += 2)
            {
                if (CanMoveTo(figureSell.X + i, figureSell.Y + dir, boardSells) && boardSells[figureSell.X + i, figureSell.Y + dir].Figure != null)
                {
                    list.Add(boardSells[figureSell.X + i, figureSell.Y + dir]);
                }
            }


            return list;
        }

    }


}
