using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Domain;
using Domain.Models;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Chess _chess;
        public ObservableCollection<CellViem> Buttons { get; set; } = new ObservableCollection<CellViem>();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            int k = 0;
            for (int i = 0; i < Board.SIZE; i++)
            {
                for (int j = 0; j < Board.SIZE; j++)
                {
                    Buttons.Add(new CellViem(((i + j) % 2 == 0)? Brushes.White : Brushes.Black, k++, i, j));
                }
            }
            
			_chess = new Chess(new WpfBoardPainter(Buttons), new WpfFigurePainter(Buttons));
		}

        public void CellClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var x = int.Parse(((Button)sender)?.Tag?.ToString());
                CellViem cell = null;
                foreach(var button in Buttons)
                {
                    if (button.Id == x) cell = button;
                }

                if (cell != null)
                {
                    if (cell.AvaibleForMove)
                    {
                        var res = _chess.Move(cell.X, cell.Y);

                        if (res != GameResult.Going)
                        {
                            MessageBox.Show(res.ToString());
                        }
                    }
                    else
                    {
                        _chess.ChooseFigure(cell.X, cell.Y);
                    }
                }
            }
            catch
            {

            }
        }
    }
    public class CellViem
    {
        #region NeOtkruvay
        #region Net
        #region Pizdec 
        private const string PATH = @"C:\Users\visua\Source\Repos\Chess2\WpfApp\Images\";
        #endregion
        #endregion
        #endregion
        public int Row { get; set; }
        public int Column { get; set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public ImageSource Path { get; set; }
        public string FigureName { get; set; }
        public FigureColor? Color { get; set; } = null;
        public Brush Brush { get; set; }
        public Brush bg { get; set; }
        public bool AvaibleForMove { get; set; } = false;
        public int Id { get; set; }
        public CellViem(Brush brush, int id, int row = 0, int column = 0, string name = null)
        {
            ChangeImage(name);
            Id = id;
            Row = row;
            Column = column;
            Y = 7 - row;
            X = column;
            Brush = brush;
            ChangeBg(Brushes.Red);
        }

        public void ChangeBg(Brush bg)
        {
            this.bg = bg;
        }
        public void ChangeImage(string name)
        {
            if (name == null || name == "")
            {
                Color = null;
                FigureName = null;
                Path = null;
                return;
            }
            //var a = new BitmapImage(new Uri("Images/PawnB.png", UriKind.Relative));

            //Path = a;
            Path = new BitmapImage(new Uri(PATH + $"{name}.png"));
            Color = (name[name.Length - 1] == 'B')? FigureColor.Black : FigureColor.White;
            FigureName = name.Remove(name.Length - 1);
        }



    }
}
