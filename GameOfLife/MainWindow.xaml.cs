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
using System.Windows.Threading;

namespace GameOfLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int ROWS_COLS = 30;

        Rectangle[,] fields = new Rectangle[ROWS_COLS, ROWS_COLS];

        DispatcherTimer timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();

            InitializeDisplay();

            /* Initialize timer */
            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Tick += Update;
        }

        private void Update(object sender, EventArgs e)
        {
            int[,] temp = new int[ROWS_COLS, ROWS_COLS];

            for (int i = 0; i < ROWS_COLS; i++)
            {
                for (int j = 0; j < ROWS_COLS; j++)
                {
                    int iP1 = (i + 1 == ROWS_COLS) ? 0 : i + 1;
                    int jP1 = (j + 1 == ROWS_COLS) ? 0 : j + 1;
                    int iM1 = (i == 0) ? ROWS_COLS - 1 : i - 1;
                    int jM1 = (j == 0) ? ROWS_COLS - 1 : j - 1;

                    temp[i, j] = Sum(fields[iM1, jM1], fields[i, jM1], fields[iP1, jM1],
                                    fields[iM1, j], fields[iP1, j],
                                    fields[iM1, jP1], fields[i, jP1], fields[iP1, jP1]);
                }
            }

            for (int i = 0; i < ROWS_COLS; i++)
            {
                for (int j = 0; j < ROWS_COLS; j++)
                {
                    if (temp[i, j] < 2 || temp[i, j] > 3)
                    {
                        fields[i, j].Fill = Brushes.LightGray;
                    }
                    else if (temp[i, j] == 3 && fields[i, j].Fill == Brushes.LightGray)
                    {
                        fields[i, j].Fill = GetRandomColor();
                    }
                }
            }
        }

        private int Sum(params Rectangle[] arr)
        {
            int res = 0;

            foreach (Rectangle r in arr)
            {
                if (r.Fill != Brushes.LightGray) res++;
            }

            return res;
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
            if (((Rectangle)sender).Fill == Brushes.LightGray)
            {
                ((Rectangle)sender).Fill = GetRandomColor();
            }
            else
            {
                ((Rectangle)sender).Fill = Brushes.LightGray;
            }
        }

        private Brush GetRandomColor()
        {
            return Brushes.Black;
            //Random r = new Random();
            //switch (r.Next(5))
            //{
            //    case 0: return Brushes.DarkRed;
            //    case 1: return Brushes.DarkGreen;
            //    case 2: return Brushes.DarkBlue;
            //    case 3: return Brushes.DarkOrange;
            //    default: return Brushes.DarkKhaki;
            //}
        }

        private void buttonStartStop_Click(object sender, RoutedEventArgs e)
        {
            if (timer.IsEnabled)
            {
                timer.Stop();
                ((Button)sender).Content = "Start Simulaton";
            }
            else
            {
                timer.Start();
                ((Button)sender).Content = "Stop Simulaton";
            }
        }

        private void buttonRandom_Click(object sender, RoutedEventArgs e)
        {
            Random r = new Random();

            for (int i = 0; i < ROWS_COLS; i++)
            {
                for (int j = 0; j < ROWS_COLS; j++)
                {
                    if (r.Next(3) == 0)
                    {
                        fields[i, j].Fill = GetRandomColor();
                    }
                    else
                    {
                        fields[i, j].Fill = Brushes.LightGray;
                    }
                }
            }
        }

        private void buttonClean_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < ROWS_COLS; i++)
            {
                for (int j = 0; j < ROWS_COLS; j++)
                {
                    fields[i, j].Fill = Brushes.LightGray;
                }
            }
        }
    }
}
