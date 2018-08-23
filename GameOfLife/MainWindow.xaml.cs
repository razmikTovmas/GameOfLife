using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameOfLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int ROWS_COLS = 30;

        Rectangle[,] fields = new Rectangle[ROWS_COLS, ROWS_COLS];

        public MainWindow()
        {
            InitializeComponent();

            InitializeDisplay();
        }

        private void InitializeDisplay()
        {
            displayCanvas.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            displayCanvas.Arrange(new Rect(0.0, 0.0, displayCanvas.DesiredSize.Width, displayCanvas.DesiredSize.Height));

            for (int i = 0; i < ROWS_COLS; i++)
            {
                for (int j = 0; j < ROWS_COLS; j++)
                {
                    Rectangle ractangle = new Rectangle();

                    ractangle.Height = displayCanvas.ActualHeight / ROWS_COLS - 2.0;
                    ractangle.Width = displayCanvas.ActualWidth / ROWS_COLS - 2.0;

                    ractangle.Fill = Brushes.LightGray;
                    displayCanvas.Children.Add(ractangle);
                    Canvas.SetTop(ractangle, i * displayCanvas.ActualHeight / ROWS_COLS);
                    Canvas.SetLeft(ractangle, j * displayCanvas.ActualWidth / ROWS_COLS);

                    ractangle.MouseDown += Ractangle_MouseDown;

                    fields[i, j] = ractangle;
                }
            }
        }

        private void Ractangle_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void buttonStartStop_Click(object sender, RoutedEventArgs e)
        {

        }
        
        private void buttonRandom_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
