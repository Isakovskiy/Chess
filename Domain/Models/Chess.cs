using Domain.Models.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Chess
    {
        private Board _board;
        private Figure? _choosedFigure = null;

        public FigureColor GoingPlayer { get; set; } = FigureColor.White;
        public IBoardPainter Painter { get; set; }

        public Chess(IBoardPainter painter)
        {
            Painter = painter;
            var sells = Board.GetEmptyBoard();

            //Создаем фигуры

            _board = new Board(sells);

        }

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

            if(figure != null && figure.Color == GoingPlayer)
            {
                _choosedFigure = figure;

                var avaibleSells = _choosedFigure.GetAvaibleSells(_board.Sells).RemoveBannedMoves(_choosedFigure, _board);
                Painter.DrawAvaibleSells(avaibleSells);

            }

        }

        public void Move(Sell sell)
        {
            if(sell == null)
            {
                throw new ArgumentNullException();
            }
                    
            if(_choosedFigure != null && _choosedFigure.GetAvaibleSells(_board.Sells).RemoveBannedMoves(_choosedFigure, _board).Contains(sell))
            {
                _choosedFigure.Move(sell);
                Painter.Draw(_board.Sells);
            }
        }


    }

    static class Hepler
    {
        public static List<Sell> RemoveBannedMoves(this List<Sell> sells, Figure figure, Board board)
        {
            if(figure == null || board == null)
            {
                throw new ArgumentNullException();
            }

            var startSell = figure.CurrentSell;

            try
            {
                
                for (int i = 0; i < sells.Count; i++)
                {
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

                }
            }
            catch
            {
                figure.Move(startSell);
            }

            return sells;
        }
    }
}