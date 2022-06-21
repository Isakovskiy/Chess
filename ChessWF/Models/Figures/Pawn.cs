using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessWF.Models.Figures
{
    class Pawn : Figure
    {
        private bool _moved = false;

        public Pawn()
        {
            FigureType = FigureType.Pawn;
            FigureMoved += () => _moved = true;
        }
        public override List<Sell> GetAvaibleSells(Sell figureSell, Sell[,] boardSells)
        {
            var list = new List<Sell>();
            var dir = IsBlack ? -1 : 1;


            if (!_moved)
            {
                list.Add(boardSells[figureSell.X, figureSell.Y + dir]);
                list.Add(boardSells[figureSell.X, figureSell.Y + dir * 2]);
            }
            else
            {
                if(CanMoveTo(figureSell.X, figureSell.Y + dir, boardSells))
                {
                    list.Add(boardSells[figureSell.X, figureSell.Y + dir]);
                }

                for(int i = -1; i <= 1; i += 2)
                {
                    if (CanMoveTo(figureSell.X + i, figureSell.Y + dir, boardSells) && boardSells[figureSell.X + i, figureSell.Y + dir].Figure != null)
                    {
                        list.Add(boardSells[figureSell.X + i, figureSell.Y + dir]);
                    }
                }

            }

            return list;
        }

    }


}
