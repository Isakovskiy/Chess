

using Domain.Models.Figures;

namespace Domain.Models
{
    public abstract class Figure
    {
        public readonly FigureColor Color;
        public bool Moved { get; private set; } = false;
        public Cell CurrentCell { get; protected set; }
        public IFiguresPainter FiguresPainter { get; set; }
        abstract public string Name { get; }

        private List<MoveRecord> _movesLog;
        public IEnumerable<MoveRecord> MovesLog => _movesLog;
        public Figure(Cell sell, IFiguresPainter figuresPainter, FigureColor color = FigureColor.Black)
        {
            Color = color;
            CurrentCell = sell;
            CurrentCell.Figure = this;
            FiguresPainter = figuresPainter;

            _movesLog = new List<MoveRecord>();
        }

        public virtual MoveRecord Move(Cell newCell, MoveRecord lastMove)
        {
            if (newCell == null) throw new ArgumentNullException("newsell is null");

            // Взятие на проходе
            if (lastMove != null)
            {
                var passantCell = TryGetPassantCell(lastMove);
                if(passantCell?.Item1 == newCell.X && passantCell?.Item2 == newCell.Y)
                {
                    lastMove.Figure.FreeCell();
                }
            } 

            var moveRecord = new MoveRecord(this, CurrentCell, newCell);
            _movesLog.Add(moveRecord);

            ChangeCell(newCell);
            FiguresPainter.MoveFigure(CurrentCell, newCell);
            
            Moved = true;
            return moveRecord;
        }

        public virtual void ChangeCell(Cell newCell)
        {
            if (newCell == null) throw new ArgumentNullException("Newsell is null");

            FreeCell();
            newCell.Figure = this;

            CurrentCell = newCell;
        }

        public abstract List<Cell> GetAvaibleCells(Cell[,] boardCells, MoveRecord lastMove);

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
        /// <summary>
        /// Ищет клетку для взятия на проходе
        /// </summary>
        /// <param name="lastMove"></param>
        /// <returns>Клетку для взятия на проходе, если она есть</returns>
        protected Tuple<int, int> TryGetPassantCell(MoveRecord? lastMove)
        {
            if(lastMove?.Figure is Pawn && lastMove.Figure.Color != this.Color &&
                Math.Abs(lastMove.To.Y - lastMove.From.Y) > 1)
            {
                var dir = (lastMove.To.Y - lastMove.From.Y) / 2;
                return new Tuple<int, int>(lastMove.To.X, lastMove.To.Y - dir);
            }
            return null;
        }

        public void FreeCell()
        {
            if (CurrentCell.Figure == this)
            {
                CurrentCell.Figure = null;
                CurrentCell = null;
            }
        }
    }
}