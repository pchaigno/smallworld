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

namespace GUI
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MapWindow : Window
    {
        IGame game;
        Dictionary<Point, Rectangle> unitRectangles;

        public MapWindow(/*IGame game*/)
        {
            InitializeComponent();

            IGameBuilder gameBuilder = new DemoGameBuilder(); ;
            IGame game = gameBuilder.buildGame("Lord", new GauloisFactory(), "Pierre", new DwarfFactory());
            this.game = game;

            unitRectangles = new Dictionary<Point,Rectangle>();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            IMap map = game.getMap();

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

            displayUnits();
            

        }

        private Rectangle createRectangle(ISquare type, int c, int l)
        {
            var rectangle = new Rectangle();



            rectangle.Fill = ImageFactory.getBrushSquare(type);
            Grid.SetColumn(rectangle, c);
            Grid.SetRow(rectangle, l);
            rectangle.Tag = c * 4 + l;
            //rectangle.Stroke = Brushes.Red;
            rectangle.StrokeThickness = 1;
            
            return rectangle;
        }

        private void displayUnits()
        {
            Dictionary<Point, List<IUnit>> units = game.getMap().getUnits();
            foreach (Point key in units.Keys)
            {
                int nb = units[key].Count;
                if (nb > 0)
                {
                    var rectangle = new Rectangle();


                    rectangle.Fill = ImageFactory.getBrushUnit(units[key][0], nb);
                    Grid.SetColumn(rectangle, key.Y);
                    Grid.SetRow(rectangle, key.X);
                    rectangle.StrokeThickness = 1;
                    mapGrid.Children.Add(rectangle);

                    unitRectangles.Add(key, rectangle);
                    Console.WriteLine(key.X + "/" + key.Y);

                }
            }
        }

        private void updateUnitDisplay(Point p)
        {
            //TODO Improve
            Rectangle rectangleR = unitRectangles[p];
            if (rectangleR != null)
            {
                mapGrid.Children.Remove(rectangleR);
            }
            Dictionary<Point, List<IUnit>> units = game.getMap().getUnits();
            int nb = units[p].Count;
            if (nb > 0)
            {
                var rectangle = new Rectangle();


                rectangle.Fill = ImageFactory.getBrushUnit(units[p][0], nb);
                Grid.SetColumn(rectangle, p.Y);
                Grid.SetRow(rectangle, p.X);
                rectangle.StrokeThickness = 1;
                mapGrid.Children.Add(rectangle);

                unitRectangles.Add(p, rectangle);
                Console.WriteLine(p.X + "/" + p.Y);

            }
        }
    }
}
