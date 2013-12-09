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
using Point = System.Drawing.Point;
using Bitmap = System.Drawing.Bitmap;
using Image = System.Drawing.Image;
using TextureBrush = System.Drawing.TextureBrush;
using Graphics = System.Drawing.Graphics;
using SmallWorld;
using GUI;

namespace WpfMap
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            IMapBuilder builder = new MapBuilder();
            IMap map = builder.buildMap(5);

            int size = map.getSize();

            for (int c = 0; c < size; c++)
            {
                mapGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(50, GridUnitType.Pixel) });
            }

            for (int l = 0; l < size; l++)
            {
                mapGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(50, GridUnitType.Pixel) });

                for (int c = 0; c < size; c++)
                {
                    ISquare type = map.getSquare(new Point(l, c));
                    var rect = createRectangle(type, c, l);
                    mapGrid.Children.Add(rect);

                }
            }
        }

        private Rectangle createRectangle(ISquare type, int c, int l)
        {
            var rectangle = new Rectangle();



            rectangle.Fill = ImageFactory.getBrush(type);
            Grid.SetColumn(rectangle, c);
            Grid.SetRow(rectangle, l);
            rectangle.Tag = c * 4 + l;
            //rectangle.Stroke = Brushes.Red;
            rectangle.StrokeThickness = 1;
            
            return rectangle;
        }
    }
}
