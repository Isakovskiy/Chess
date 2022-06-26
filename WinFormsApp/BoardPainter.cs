using Domain;
using Domain.Models;
using Domain.Models.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp
{ 

    class s : IFiguresPainter
    {
        //private  Color defaultColor = Color.White;
        public s(CellView[,] cellsView)
        {
            _cellsView = cellsView;
        }

        private CellView[,] _cellsView;

        public void ChooseFigure(Cell figureCell)
        {
            foreach(var c in _cellsView)
            {
                if(c.X == figureCell.X && c.Y == figureCell.Y)
                {
                    c.BackColor = Color.FromArgb(10, Color.Yellow);
                }
            }
        }

        public TransformFigures DrawFigureReplaceSelectionAndGet()
        {
            return TransformFigures.Queen;
        }

        public void MoveFigure(Cell from, Cell to)
        {
            
        }

        public void CancelFigure(Figure figure)
        {
            foreach (var c in _cellsView)
            {
                if (c.X == figure.CurrentCell.X && c.Y == figure.CurrentCell.Y)
                {
                    c.BackColor = Color.FromArgb(255, c.defaultColor);
                }
            }
        }
    }
    internal class BoardPainter : IBoardPainter
    {
        public BoardPainter(CellView[,] cellsView)
        {
            _cellsView = cellsView;
        }

        private CellView[,] _cellsView;

        public void DrawAvaibleCells(List<Cell> avaibleCells)
        {
            foreach(var cell in _cellsView)
            {
                if (avaibleCells.FirstOrDefault(c => c.X == cell.X && c.Y == cell.Y) != null)
                {
                    cell.BackColor = Color.FromArgb(50, Color.Blue);
                }
            }
        }

        public void DrawBoard(Cell[,] sells)
        {
            for(int i = 0; i < sells.GetLength(0); i++)
            {
                for(int j = 0; j < sells.GetLength(1); j++)
                {
                    if (sells[i, j].Figure != null) 
                    {
                        _cellsView[i, j].Image = new Bitmap(Image.FromFile($@"..\..\..\Figures\{sells[i, j].Figure.Name}{sells[i, j].Figure.Color.ToString()[0]}.png"), new Size(75, 75));
                    }
                    else
                    {
                        _cellsView[i, j].Image = null;
                    }
                }
            }
        }

        public void ResetAvaibleCells()
        {
            foreach (var cell in _cellsView)
            {
                cell.BackColor = Color.FromArgb(255, cell.defaultColor);
            }
        }
    }
}
