using Domain.Models.Figures;


namespace Domain.Models
{
    public class Chess
    {
        public Chess(IBoardPainter boardPainter, IFiguresPainter figuresPainter)
        {
            BoardPainter = boardPainter;
            FiguresPainter = figuresPainter;
            var cells = Board.GetEmptyBoard();

            FigureColor color = FigureColor.White;
            for (int k = 1; k <= 6; k += 5)
            {
                for (int i = 0; i < Board.SIZE; i++)
                {
                    new Pawn(cells[i, k], figuresPainter, color);
                }
                color = color.Reverese();
            }

            var lw = new Rook(cells[0, 0], FiguresPainter, FigureColor.White);
            var rw = new Rook(cells[7, 0], FiguresPainter, FigureColor.White);
            var lb = new Rook(cells[0, 7], FiguresPainter, FigureColor.Black);
            var rb = new Rook(cells[7, 7], FiguresPainter, FigureColor.Black);

            new Knight(cells[1, 0], FiguresPainter, FigureColor.White);
            new Knight(cells[6, 0], FiguresPainter, FigureColor.White);
            new Knight(cells[1, 7], FiguresPainter, FigureColor.Black);
            new Knight(cells[6, 7], FiguresPainter, FigureColor.Black);

            new Bishop(cells[2, 0], FiguresPainter, FigureColor.White);
            new Bishop(cells[5, 0], FiguresPainter, FigureColor.White);
            new Bishop(cells[2, 7], FiguresPainter, FigureColor.Black);
            new Bishop(cells[5, 7], FiguresPainter, FigureColor.Black);

            new Queen(cells[3, 0], FiguresPainter, FigureColor.White);
            var kw = new King(cells[4, 0], FiguresPainter, FigureColor.White);
            new Queen(cells[3, 7], FiguresPainter, FigureColor.Black);
            var kb = new King(cells[4, 7], FiguresPainter, FigureColor.Black);


            kw.SmallCastling += (x, y) => rw.Move(cells[x, y]);
            kw.BigCastling += (x, y) => lw.Move(cells[x, y]);
            kb.SmallCastling += (x, y) => lb.Move(cells[x, y]);
            kb.BigCastling += (x, y) => rb.Move(cells[x, y]);


            _board = new Board(cells);
            BoardPainter.DrawBoard(_board.Cells);
        }

        private Board _board;
        private Figure? _choosedFigure = null;

        public IEnumerable<Cell> AvaibledCells => _choosedFigure?.GetAvaibleCells(_board.Cells)?.RemoveBannedMoves(_choosedFigure, _board);

        public FigureColor GoingPlayer { get; set; } = FigureColor.White;
        public IBoardPainter BoardPainter { get; set; }
        public IFiguresPainter FiguresPainter { get; set; }

        public void ChooseFigure(int x, int y)
        {
            Figure figure;

            try
            {
                figure = _board.GetFigure(x, y);
            }
            catch
            {
                return;
            }

            BoardPainter.ResetAvaibleCells();

            if(_choosedFigure != null)
            {
                FiguresPainter.CancelFigure(_choosedFigure);
                _choosedFigure = null;
            }

            if (figure != null && figure.Color == GoingPlayer)
            {
                _choosedFigure = figure;

                var avaibleSells = _choosedFigure.GetAvaibleCells(_board.Cells).RemoveBannedMoves(_choosedFigure, _board);

                FiguresPainter.ChooseFigure(_choosedFigure.CurrentCell);
                BoardPainter.DrawAvaibleCells(avaibleSells);
            }
        }

        public GameResult Move(int x, int y)
        {
            Cell toCell;
            try
            {
                toCell = _board.Cells[x, y];
            }
            catch(Exception ex)
            {
                throw ex;
            }

            if (toCell == null)
            {
                throw new ArgumentNullException();
            }
            List<Cell>? avaibleSells = _choosedFigure?.GetAvaibleCells(_board.Cells);

            if (_choosedFigure != null && avaibleSells.RemoveBannedMoves(_choosedFigure, _board).Contains(toCell))
            {
                _choosedFigure.Move(toCell);
                _choosedFigure = null;
                
                GoingPlayer = GoingPlayer.Reverese();

                BoardPainter.ResetAvaibleCells();
                BoardPainter.DrawBoard(_board.Cells);

                if (Draw())
                {
                    return GameResult.Draw;
                }
                else if (Check(GoingPlayer))
                {
                    return CheckMate(GoingPlayer);
                }

            }

            return GameResult.Going;
        }

        private bool Check(FigureColor enemyColor) // шах
        {
            var ourFigures = _board.GetFigures(f => f.Color != enemyColor).ToList();
            var allSells = new List<Cell>();

            foreach (var f in ourFigures)
            {
                allSells.AddRange(f.GetAvaibleCells(_board.Cells).RemoveBannedMoves(f, _board));
            }
            return allSells.FirstOrDefault(s => s.Figure is King && s.Figure?.Color == enemyColor) != null;
        }
        private bool Check(Figure figure) => // шах
             figure.GetAvaibleCells(_board.Cells).FirstOrDefault(s => s.Figure is King) != null;


        private bool Draw()
        {
            //black
            return !Check(GoingPlayer) && !Check(GoingPlayer.Reverese()) && 
             _board.GetFigures(f => f.Color == GoingPlayer).Where(f => f.GetAvaibleCells(_board.Cells).RemoveBannedMoves(f, _board).Count != 0).Count() == 0;
        }


        private GameResult CheckMate(FigureColor enemyColor) // шах и мат
        {
            var enemyFigures = _board.GetFigures(f => f.Color == enemyColor).ToList();
            var ourFigures = _board.GetFigures(f => f.Color != enemyColor).ToList();

            var realBoard = new List<Tuple<Figure, Cell>>();
            realBoard.AddRange(_board.GetFigures()); // сохраняем все начальные позиции

            for (int i = 0; i < enemyFigures.Count; i++)
            {
                foreach(var avaibleCell in enemyFigures[i].GetAvaibleCells(_board.Cells))
                {
                    enemyFigures[i].ChangeCell(avaibleCell);

                    if (!Check(enemyColor))
                    {
                        realBoard.ReturnFigures();
                        return GameResult.Going;
                    }

                    realBoard.ReturnFigures();
                }
            }

            return enemyColor == FigureColor.Black ? GameResult.WhiteWon : GameResult.BlackWon;
        }


    }

    static class Hepler
    {
        public static FigureColor Reverese(this FigureColor figureColor)
        {
            return figureColor == FigureColor.White ? FigureColor.Black : FigureColor.White;
        }
        public static bool IsEveryone<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            foreach(var item in collection)
            {
                if (!predicate(item)) return false;
            }

            return true;
        }

        public static void ReturnFigures(this List<Tuple<Figure, Cell>> figures)
        {
            foreach (var f in figures)
            {
                f.Item1.ChangeCell(f.Item2);
            }
        }

        public static List<Cell> RemoveBannedMoves(this List<Cell> sells, Figure figure, Board board)
        {
            if(figure == null || board == null)
            {
                throw new ArgumentNullException();
            }

            var realBoard = new List<Tuple<Figure, Cell>>();
            realBoard.AddRange(board.GetFigures()); // сохраняем все начальные позиции

            try
            {
                for (int i = 0; i < sells.Count; i++)
                {
                    figure.ChangeCell(sells[i]);
                    var enemyFigures = board.GetFigures(predicate: f => f.Color != figure.Color).ToList();

                    for(int j = 0; j < enemyFigures.Count(); j++)
                    {
                        var avaibleMoves = enemyFigures[j].GetAvaibleCells(board.Cells);
                        if(avaibleMoves.FirstOrDefault(s => s.Figure is King && s.Figure.Color == figure.Color) != null)
                        {
                            sells.RemoveAt(i);
                            i--;
                            break;
                        }
                    }

                    realBoard.ReturnFigures();
                }
            }
            finally
            {
                realBoard.ReturnFigures();
            }
            return sells;
        }
    }
}