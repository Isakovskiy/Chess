

namespace Domain.Models
{
    public abstract class Figure
    {
        public readonly FigureColor Color;
        public readonly string Image;

        public bool Moved { get; private set; } = false;
        public Sell CurrentSell { get; protected set; }

        public Figure(string image, Sell sell, FigureColor color = FigureColor.Black)
        {
            Color = color;
            Image = image;
            CurrentSell = sell;
            CurrentSell.Figure = this;
        }

        public virtual void Move(Sell newSell)
        {
            //Sell.Image = null;
            //Sell.Image = Images[FigureType];

            if (newSell == null) throw new ArgumentNullException("newsell is null");

            CurrentSell.Figure = null;
            newSell.Figure = this;
            
            CurrentSell = newSell;

            Moved = true;
        }

        public abstract List<Sell> GetAvaibleSells(Sell[,] boardSells);

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