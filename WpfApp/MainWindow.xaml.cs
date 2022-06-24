using System.Windows;
using Domain;
using Domain.Models;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Chess chess = new Chess(new WpfBoardPainter(), new WpfFigurePainter());

            

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
