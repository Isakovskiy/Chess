using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessWF.Models
{
    abstract class Figure
    {
        public readonly bool IsBlack;
        public readonly string Image;

        public event Action FigureMoved;

        public Figure(string image, bool isBlack = false)
        {
            IsBlack = isBlack;
            Image = image;
        }

        public void Move(Sell newSell)
        {
            //Sell.Image = null;
            //Sell.Image = Images[FigureType];
            FigureMoved?.Invoke();
        }

        public abstract List<Sell> GetAvaibleSells(Sell figureSell, Sell[,] boardSells);

        protected bool CanMoveTo(int x, int y, Sell[,] boardSells)
        {
            try
            {
                if (boardSells[x, y].Figure?.IsBlack == IsBlack) return false;
            }
            catch
            {
                return false;
            }

            return true;
        }


    }
}