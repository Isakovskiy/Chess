

namespace Domain.Models
{
    public abstract class Figure
    {
        public readonly FigureColor Color;
        public readonly string Image;

        public bool Moved { get; private set; } = false;
        public Cell CurrentCell { get; protected set; }
        public IFiguresPainter FiguresPainter { get; set; }

        public Figure(string image, Cell sell, IFiguresPainter figuresPainter, FigureColor color = FigureColor.Black)
        {
            Color = color;
            Image = image;
            CurrentCell = sell;
            CurrentCell.Figure = this;
            FiguresPainter = figuresPainter;
        }

        public virtual void Move(Cell newSell)
        {
            if (newSell == null) throw new ArgumentNullException("newsell is null");

            //Sell.Image = null;
            //Sell.Image = Images[FigureType];

            ChangeCell(newSell);

            Moved = true;
        }

        public virtual void ChangeCell(Cell newCell)
        {
            if (newCell == null) throw new ArgumentNullException("newsell is null");

            CurrentCell.Figure = null;
            newCell.Figure = this;

            CurrentCell = newCell;
        }

        public abstract List<Cell> GetAvaibleCells(Cell[,] boardCells);

        protected bool CanMoveTo(int x, int y, Cell[,] boardCells)
        {
            try
            {
                if (boardCells[x, y].Figure?.Color == Color) return false;
            }
            catch
            {
                return false;
            }

            return true;
        }


    }
}