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
using SmallWorld;

namespace WpfMap
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<int> map;
        Dictionary<ICoordinates, ISquare> squares;
        IMap map2 = new Map();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            map = new List<int>();
            int width = 5;
            int height = 5;
            for (int i = 0; i < 25; i++)
            {
                map.Add(i % 6);
            }

            for (int c = 0; c < width; c++)
            {
                mapGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(50, GridUnitType.Pixel) });
            }

            for (int l = 0; l < height; l++)
            {
                mapGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(50, GridUnitType.Pixel) });

                for (int c = 0; c < width; c++)
                {
                    int type = map[l * width + c];
                    var rect = createRectangle(type, c, l);
                    mapGrid.Children.Add(rect);

                }
            }
        }

        private Rectangle createRectangle(int type, int c, int l)
        {
            var rectangle = new Rectangle();
            rectangle.Fill = Brushes.Beige;
            Grid.SetColumn(rectangle, c);
            Grid.SetRow(rectangle, l);
            rectangle.Tag = c * 4 + l;
            rectangle.Stroke = Brushes.Red;
            rectangle.StrokeThickness = 1;




            return rectangle;
        }
    }
}
