using Domain.Models.Figures;

namespace Domain.Models
{
    public class Chess
    {
        public Chess(IBoardPainter painter)
        {
            Painter = painter;
            var sells = Board.GetEmptyBoard();

            //Создаем фигуры

            _board = new Board(sells);
            Painter.DrawBoard(_board.Sells);
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
                
                var avaibleSells = _choosedFigure.GetAvaibleSells(_board.Sells).RemoveBannedMoves(_choosedFigure, _board);

                Painter.ChooseFigure(_choosedFigure.CurrentSell);
                Painter.DrawAvaibleSells(avaibleSells);
            }
        }

        public GameResult Move(Sell toSell)
        {
            if(toSell == null)
            {
                throw new ArgumentNullException();
            }
            List<Sell>? avaibleSells = _choosedFigure?.GetAvaibleSells(_board.Sells);
            if (_choosedFigure != null && avaibleSells.RemoveBannedMoves(_choosedFigure, _board).Contains(toSell))
            {
                _choosedFigure.Move(toSell);

                Painter.ResetAvaibleSells();
                Painter.DrawBoard(_board.Sells);

                if (Check(avaibleSells)) return CheckForCheckMate(_choosedFigure.Color);
            }

            return GameResult.Going;
        }

        private bool Check(List<Sell> sells) // шах
            => sells.FirstOrDefault(s => s.Figure is King) != null;

        private GameResult CheckForCheckMate(FigureColor enemyColor) // шах и мат
        {
            var enemyFigures = _board.GetFigures(f => f.Color == enemyColor).ToList();
            var ourFigures = _board.GetFigures(f => f.Color != enemyColor).ToList();

            for (int i = 0; i < enemyFigures.Count; i++)
            {
                var startPos = enemyFigures[i].CurrentSell;
                var eatedFigures = new List<Tuple<Figure, Sell>>();

                foreach(var avaibleSell in enemyFigures[i].GetAvaibleSells(_board.Sells))
                {
                    if (avaibleSell.Figure != null) eatedFigures.Add(new Tuple<Figure, Sell>(avaibleSell.Figure, avaibleSell));
                    enemyFigures[i].Move(avaibleSell);

                    if (ourFigures.IsEveryone(f => !Check(f.GetAvaibleSells(_board.Sells))))
                    {
                        eatedFigures.ReturnEatedFigures();
                        enemyFigures[i].Move(startPos);

                        return GameResult.Going;
                    }

                    eatedFigures.ReturnEatedFigures();
                }

                enemyFigures[i].Move(startPos);
            }

            return enemyColor == FigureColor.Black ? GameResult.WhiteWon : GameResult.BlackWon;
        }


    }

    static class Hepler
    {
        public static bool IsEveryone<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            foreach(var item in collection)
            {
                if (!predicate(item)) return false;
            }

            return true;
        }

        public static void ReturnEatedFigures(this List<Tuple<Figure, Sell>> eatedFigures)
        {
            foreach (var f in eatedFigures)
            {
                f.Item1.Move(f.Item2);
            }
            eatedFigures = null;
        }

        public static List<Sell> RemoveBannedMoves(this List<Sell> sells, Figure figure, Board board)
        {
            if(figure == null || board == null)
            {
                throw new ArgumentNullException();
            }

            var startSell = figure.CurrentSell;
            List<Tuple<Figure, Sell>>? eatedFigures = null;

            try
            {
                for (int i = 0; i < sells.Count; i++)
                {
                    eatedFigures = new List<Tuple<Figure, Sell>>();
                    if (sells[i].Figure != null) eatedFigures.Add(new Tuple<Figure, Sell>(sells[i].Figure, sells[i]));

                    figure.Move(sells[i]);
                    var enemyFigures = board.GetFigures(predicate: f => f.Color != figure.Color).ToList();

                    for(int j = 0; j < enemyFigures.Count(); j++)
                    {
                        var avaibleMoves = enemyFigures[j].GetAvaibleSells(board.Sells);
                        if(avaibleMoves.FirstOrDefault(s => s.Figure is King && s.Figure.Color == figure.Color) != null)
                        {
                            sells.RemoveAt(i);
                            i--;
                            break;
                        }
                    }

                    figure.Move(startSell);
                    eatedFigures.ReturnEatedFigures();
                }
            }
            finally
            {
                figure.Move(startSell);
                eatedFigures?.ReturnEatedFigures();
            }
            return sells;
        }
    }
}