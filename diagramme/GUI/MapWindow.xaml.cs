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
        Rectangle originPos;

        public MapWindow(IGame game)
        {
            InitializeComponent();

            /*IGameBuilder gameBuilder = new DemoGameBuilder();
            IGame game = gameBuilder.buildGame("Lord Breizh", new GauloisFactory(), "Paule", new DwarfFactory());*/
            this.game = game;
            this.originPos = null;

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
            displayInfoPlayer();
        }

        private void displayInfoPlayer()
        {
            IPlayer player1 = game.getPlayer1();
            playerD1.Text = player1.getName() + " - Units: " + player1.getNbUnits() + " - Points :" + player1.getPoints();

            IPlayer player2 = game.getPlayer2();
            playerD2.Text = player2.getName() + " - Units: " + player2.getNbUnits() + " - Points :" + player2.getPoints();

            roundD.Text = "Round number: " + game.getCurrentRound();
            currentD.Text = "Current PLayer: " + game.getCurrentPlayer().getName();

            lastMove.Text = game.getRound().getLastMoveInfo();
        }


        private Rectangle createRectangle(ISquare type, int c, int l)
        {
            var rectangle = new Rectangle();

            rectangle.Fill = ImageFactory.getBrushSquare(type);
            Grid.SetColumn(rectangle, c);
            Grid.SetRow(rectangle, l);
            rectangle.Tag = c * 4 + l;
            rectangle.StrokeThickness = 1;
            rectangle.Stroke = Brushes.Black;

            rectangle.MouseLeftButtonDown += new MouseButtonEventHandler(rectangleMouseLeftHandler);

            rectangle.MouseRightButtonDown += new MouseButtonEventHandler(rectangleMouseRightHandler);

            rectangle.MouseEnter += new MouseEventHandler(mouseEnterHandler);
            rectangle.MouseLeave += new MouseEventHandler(mouseLeaveHandler);
            
            return rectangle;
        }

        private void displayUnits()
        {
            Dictionary<Point, List<IUnit>> units = game.getMap().getUnits();

            foreach (Rectangle rect in unitRectangles.Values)
            {
                mapGrid.Children.Remove(rect);
            }
            unitRectangles.Clear();

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
                    rectangle.Stroke = Brushes.Black;
                    rectangle.MouseLeftButtonDown += new MouseButtonEventHandler(rectangleMouseLeftHandler);
                    rectangle.MouseRightButtonDown += new MouseButtonEventHandler(rectangleMouseRightHandler);
                    rectangle.MouseEnter += new MouseEventHandler(mouseEnterHandler);
                    rectangle.MouseLeave += new MouseEventHandler(mouseLeaveHandler);
                    mapGrid.Children.Add(rectangle);

                    unitRectangles.Add(key, rectangle);
                    Console.WriteLine(key.X + "/" + key.Y);
                }
            }
        }

        public void mouseEnterHandler(object sender, MouseEventArgs e)
        {
            var rectangle = sender as Rectangle;
            rectangle.StrokeThickness = 1;
            rectangle.Stroke = Brushes.Blue;
        }

        public void mouseLeaveHandler(object sender, MouseEventArgs e)
        {
            var rectangle = sender as Rectangle;
            if (rectangle == originPos)
            {
                rectangle.StrokeThickness = 1;
                rectangle.Stroke = Brushes.Red;
            }
            else
            {
                rectangle.StrokeThickness = 1;
                rectangle.Stroke = Brushes.Black;
            }
        }

        public void rectangleMouseLeftHandler(object sender, MouseEventArgs e)
        {
            var rectangle = sender as Rectangle;
            int column = Grid.GetColumn(rectangle);
            int row = Grid.GetRow(rectangle);
            Point position = new Point(row, column);

            IRound round = game.getRound();
            if (round.isCurrentPlayerPosition(position))
            {
                List<IUnit> units = round.getUnits(position);
                round.selectUnit(units[0]);
                rectangle.StrokeThickness = 1;
                rectangle.Stroke = Brushes.Red;

                if (originPos != null)
                {
                    originPos.Stroke = Brushes.Black;
                }
                originPos = rectangle;
            }


            e.Handled = true;
        }

        public void rectangleMouseRightHandler(object sender, MouseEventArgs e)
        {
            var rectangle = sender as Rectangle;
            int column = Grid.GetColumn(rectangle);
            int row = Grid.GetRow(rectangle);
            Point position = new Point(row, column);

            IRound round = game.getRound();
            if (round.setDestination(position))
            {
                round.executeMove();
                originPos.Stroke = Brushes.Black;

                displayUnits();
            }

            displayInfoPlayer();

            e.Handled = true;
        }

        public void onClickEndRound(object sender, RoutedEventArgs e)
        {
            game.endRound();
            
            displayUnits();
            displayInfoPlayer();

            if (game.isEndOfGame())
            {
                IPlayer player = game.getWinner();
                string messageBoxText = "Congratulation " + player.getName() + "\n You have defeated your enemy !";
                string caption = "Victory!";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Exclamation;
                // Display message box
                MessageBox.Show(messageBoxText, caption, button, icon);

                this.Close();
            }
        }
    }
}
