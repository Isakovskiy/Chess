﻿using ChessWF.Models.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessWF.Models
{
    internal class Board
    {
        public const int SIZE = 8;
        public Sell[,] Sells { get; set; }

        public List<Figure> Figures { get; set; }
        public Board()
        {
            Sells = new Sell[8, 8];

            for(int i = 0; i < SIZE; i++)
            {
                for(int j = 0; j < SIZE; j++)
                {
                    Sells[i, j] = new Sell() { X = i, Y = j};
                }
            }

            Figures = new List<Figure>();
            Figures.Add(new Pawn());

            Sells[0, 0].Figure = Figures[0];
            
        }

        /// <summary>
        /// Возаращает список клеток на которые может пойти фигугра, находящаяся в выбранной координате
        /// </summary>
        /// <param name="x">Выбранная координата</param>
        /// <param name="y">Выбранная координата</param>
        /// <returns></returns>
        public List<Sell> FindAvaibleSells(int x, int y, out Figure choosedFigure)
        {
            choosedFigure = null;

            try
            {
                choosedFigure = Sells[x, y].Figure;
            }
            catch
            {
                throw new Exception("Out of board bounds");
            }

            return choosedFigure?.GetAvaibleSells(Sells[x, y], Sells);
        }

    }
}
