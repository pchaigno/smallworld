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
            Dictionary<Point, ISquare> squares = new Dictionary<Point, ISquare>();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    squares.Add(new Point(i, j), new Lowland());
                }
            }
            IMap map = new Map(squares);

            int width = 5;
            int height = 5;

            for (int c = 0; c < width; c++)
            {
                mapGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(50, GridUnitType.Pixel) });
            }

            for (int l = 0; l < height; l++)
            {
                mapGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(50, GridUnitType.Pixel) });

                for (int c = 0; c < width; c++)
                {
                    ISquare type = map.getSquare(new Point(l, c));
                    var rect = createRectangle(new Sea(), c, l);
                    mapGrid.Children.Add(rect);

                }
            }
        }

        private Rectangle createRectangle(ISquare type, int c, int l)
        {
            ImageBrush image = new ImageBrush();
            image.ImageSource =
                new BitmapImage(
                    new Uri(@"Ressources\terrains\eau.png", UriKind.Relative)
                );
            //Image image = new Bitmap("Ressources/terrains/eau.png");
            if (type is ISea)
            {
                //Image image = new Bitmap("Ressources/terrains/eau.png");

            } else if (type is IMountain)
            {

            } else if (type is ILowland)
            {

            } else if (type is IDesert)
            {

            } else if (type is IForest)
            {

            }

            var rectangle = new Rectangle();

            rectangle.Fill = image;
            Grid.SetColumn(rectangle, c);
            Grid.SetRow(rectangle, l);
            rectangle.Tag = c * 4 + l;
            rectangle.Stroke = Brushes.Red;
            rectangle.StrokeThickness = 1;
            
            return rectangle;
        }
    }
}
