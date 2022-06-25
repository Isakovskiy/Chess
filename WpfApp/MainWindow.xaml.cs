using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
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
        public ObservableCollection<CellViem> Buttons { get; set; } = new ObservableCollection<CellViem>();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;


            for (int i = 0; i < Board.SIZE; i++)
            {
                for (int j = 0; j < Board.SIZE; j++)
                {
                    Buttons.Add(new CellViem(((i + j) % 2 == 0)? Brushes.White : Brushes.Black, i, j));
                }
            }

			Chess chess = new Chess(new WpfBoardPainter(Buttons), new WpfFigurePainter());
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
        public FigureColor FigureColor { get; set; }
        public Brush Brush { get; set; }

        public CellViem(Brush brush, int row = 0, int column = 0, string name = "")
        {
            if(name != "")
            {
                ChangeImage(name);
            }
            
            Row = row;
            Column = column;
            Y = 7 - row;
            X = column;
            Brush = brush;
        }

        public void ChangeImage(string name)
        {
            //var a = new BitmapImage(new Uri("Images/PawnB.png", UriKind.Relative));

            //Path = a;
            Path = new BitmapImage(new Uri(PATH + $"{name}.png"));
            FigureColor = (name[name.Length - 1] == 'B')? FigureColor.Black : FigureColor.White;
            FigureName = name.Remove(name.Length - 1);
        }

    }
}
