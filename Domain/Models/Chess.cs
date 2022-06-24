using Domain.Models.Figures;

namespace Domain.Models
{
    public class Chess
    {
        public Chess(IBoardPainter painter)
        {
            Painter = painter;
            var cells = Board.GetEmptyBoard();

            //Создаем фигуры

            _board = new Board(cells);
            Painter.DrawBoard(_board.Cells);
        }

        private Board _board;
        private Figure? _choosedFigure = null;

        public FigureColor GoingPlayer { get; set; } = FigureColor.White;
        public IBoardPainter Painter { get; set; }

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

            Painter.ResetAvaibleSells();

            if(figure != null && figure.Color == GoingPlayer)
            {
                _choosedFigure = figure;
                
                var avaibleSells = _choosedFigure.GetAvaibleCells(_board.Cells).RemoveBannedMoves(_choosedFigure, _board);

                Painter.ChooseFigure(_choosedFigure.CurrentCell);
                Painter.DrawAvaibleSells(avaibleSells);
            }
        }
        
        public GameResult Move(Cell toCell)
        {
            if(toCell == null)
            {
                throw new ArgumentNullException();
            }
            List<Cell>? avaibleSells = _choosedFigure?.GetAvaibleCells(_board.Cells);
            if (_choosedFigure != null && avaibleSells.RemoveBannedMoves(_choosedFigure, _board).Contains(toCell))
            {
                _choosedFigure.Move(toCell);
                GoingPlayer = GoingPlayer.Reverese();

                Painter.ResetAvaibleSells();
                Painter.DrawBoard(_board.Cells);

                if (Draw)
                {
                    return GameResult.Draw;
                }
                else if (Check(_choosedFigure.Color.Reverese()))
                {
                    return CheckMate(_choosedFigure.Color.Reverese());
                }
                
            }

            return GameResult.Going;
        }

        private bool Check(FigureColor enemyColor) // шах
        {
            var ourFigures = _board.GetFigures(f => f.Color != enemyColor).ToList();
            var allSells = new List<Cell>();

            foreach(var f in ourFigures)
            {
                allSells.AddRange(f.GetAvaibleCells(_board.Cells));
            }
            return allSells.FirstOrDefault(s => s.Figure is King && s.Figure?.Color == enemyColor) != null;
        }
        private bool Check(Figure figure) => // шах
             figure.GetAvaibleCells(_board.Cells).FirstOrDefault(s => s.Figure is King) != null;


        private bool Draw =>
            !Check(GoingPlayer) && 
            _board.GetFigures(f => f.Color == GoingPlayer).Select(f => f.GetAvaibleCells(_board.Cells)).Count() == 0;


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
                    enemyFigures[i].ChangeSell(avaibleCell);

                    if (ourFigures.All(f => !Check(f)))
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
                f.Item1.ChangeSell(f.Item2);
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
                    figure.ChangeSell(sells[i]);
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