using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessWF.Models.Figures
{
    internal class Bishop : Figure
    {
        public Bishop(string image, bool isBlack = false) : base(image, isBlack)
        {
        }

        public override List<Sell> GetAvaibleSells(Sell figureSell, Sell[,] boardSells)
        {
            var list = new List<Sell>();

            for(int i = -1; i <= 1; i += 2)
            {
                for(int j = -1; j <= 1; j += 2)
                {
                    var x = figureSell.X + i;
                    var y = figureSell.Y + j;
                    while (CanMoveTo(x, y, boardSells))
                    {
                        list.Add(boardSells[x, y]);

                        x += i;
                        y += j;
                    }
                }
            }

            return list;
        }
    }
}
