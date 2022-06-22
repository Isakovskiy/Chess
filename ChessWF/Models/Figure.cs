using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessWF.Models
{
    public abstract class Figure
    {
        public readonly FigureColor Color;
        public readonly string Image;

        public bool Moved { get; private set; } = false;

        public Figure(string image, FigureColor color = FigureColor.Black)
        {
            Color = color;
            Image = image;
        }

        public void Move(Sell newSell)
        {
            //Sell.Image = null;
            //Sell.Image = Images[FigureType];

            if (newSell == null) throw new ArgumentNullException("newsell is null");

            newSell.Figure = this;
            Moved = true;
        }

        public abstract List<Sell> GetAvaibleSells(Sell figureSell, Sell[,] boardSells);

        protected bool CanMoveTo(int x, int y, Sell[,] boardSells)
        {
            try
            {
                if (boardSells[x, y].Figure?.Color == Color) return false;
            }
            catch
            {
                return false;
            }

            return true;
        }


    }
}