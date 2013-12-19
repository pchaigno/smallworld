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

namespace GUI {
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MapWindow: Window {
        IGame game;
        // TODO Use a matrix.
        Dictionary<Point, Rectangle> unitRectangles;
        Rectangle selectedSquare;
        Dictionary<Border, IUnit> unitSelecterCollec;
        Border selectedUnit;

        public MapWindow(IGame game) {
            InitializeComponent();

            /*IGameBuilder gameBuilder = new NormalGameBuilder();
            IGame game = gameBuilder.buildGame("Lord Breizh", new GauloisFactory(), "Paule", new DwarfFactory());*/
            this.game = game;


            unitRectangles = new Dictionary<Point, Rectangle>();
            this.selectedSquare = null;

            unitSelecterCollec = new Dictionary<Border, IUnit>();
            this.selectedUnit = null;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            IMap map = game.getMap();
            int size = map.getSize();

            for(int c = 0; c < size; c++) {
                mapGrid.ColumnDefinitions.Add(new ColumnDefinition() {
                    Width = new GridLength(50, GridUnitType.Pixel)
                });
            }

            for(int l = 0; l < size; l++) {
                mapGrid.RowDefinitions.Add(new RowDefinition() {
                    Height = new GridLength(50, GridUnitType.Pixel)
                });

                for(int c = 0; c < size; c++) {
                    ISquare type = map.getSquare(new Point(l, c));
                    var rect = createSquares(type, c, l);
                    mapGrid.Children.Add(rect);

                }
            }

            displayUnitsOnMap();
            displayInfoPlayer();
        }

        private Rectangle createSquares(ISquare type, int c, int l) {
            Rectangle rectangle = new Rectangle();

            rectangle.Fill = ImageFactory.getBrushSquare(type);
            Grid.SetColumn(rectangle, c);
            Grid.SetRow(rectangle, l);
            rectangle.StrokeThickness = 1;
            rectangle.Stroke = Brushes.Black;

            rectangle.MouseLeftButtonDown += new MouseButtonEventHandler(rectangleMouseLeftMapHandler);
            rectangle.MouseRightButtonDown += new MouseButtonEventHandler(rectangleMouseRightHandler);
            rectangle.MouseEnter += new MouseEventHandler(mouseEnterHandler);
            rectangle.MouseLeave += new MouseEventHandler(mouseLeaveHandler);

            return rectangle;
        }

        public void displayUnitSelecter(List<IUnit> units) {
            unitSelecterCollec = new Dictionary<Border, IUnit>();
            int i = 0;
            foreach(IUnit unit in units) {
                Border border = new Border();
                border.Background = ImageFactory.getBrushUnitFace(unit);

                TextBlock unitText = new TextBlock();
                unitText.Text = unit.getRemainingMovementPoints() + " MvPt \n" + unit.getLifePoints() + " lifePt";
                unitText.FontSize = 14;
                unitText.Foreground = Brushes.Red;
                unitText.FontWeight = FontWeights.Bold;
                border.Child = unitText;
                border.Width = 100;
                border.Height = 100;
                border.BorderThickness = new Thickness(3);
                if(i == 0) {
                    border.BorderBrush = Brushes.Red;
                    i++;
                    selectedUnit = border;
                } else {
                    border.BorderBrush = Brushes.Black;
                }

                border.MouseLeftButtonDown += new MouseButtonEventHandler(rectangleMouseLefUnitSelectertHandler);

                unitSelecter.Children.Add(border);

                unitSelecterCollec.Add(border, unit);
            }

        }

        private void displayInfoPlayer() {
            IPlayer player1 = game.getPlayer1();
            playerD1.Text = player1.getName() + " - Units: " + game.getNbUnits(player1) + " - Points :" + player1.getPoints();

            IPlayer player2 = game.getPlayer2();
            playerD2.Text = player2.getName() + " - Units: " + game.getNbUnits(player2) + " - Points :" + player2.getPoints();

            roundD.Text = "Round number: " + game.getCurrentRound();
            currentD.Text = "Current PLayer: " + game.getCurrentPlayer().getName();

            lastMove.Text = game.getRound().getLastMoveInfo();
        }

        private void displayUnitsOnMap() {
            List<IUnit>[,] units = game.getMap().getUnits();

            foreach(Rectangle rect in unitRectangles.Values) {
                mapGrid.Children.Remove(rect);
            }
            unitRectangles.Clear();

            for(int x=0; x<units.GetLength(0); x++) {
                for(int y=0; y<units.GetLength(1); y++) {
                    int nb = units[x, y].Count;
                    if(nb > 0) {
                        var rectangle = new Rectangle();

                        rectangle.Fill = ImageFactory.getBrushUnit(units[x, y][0], nb);
                        Grid.SetColumn(rectangle, y);
                        Grid.SetRow(rectangle, x);
                        rectangle.StrokeThickness = 1;
                        rectangle.Stroke = Brushes.Black;
                        rectangle.MouseLeftButtonDown += new MouseButtonEventHandler(rectangleMouseLeftMapHandler);
                        rectangle.MouseRightButtonDown += new MouseButtonEventHandler(rectangleMouseRightHandler);
                        rectangle.MouseEnter += new MouseEventHandler(mouseEnterHandler);
                        rectangle.MouseLeave += new MouseEventHandler(mouseLeaveHandler);
                        mapGrid.Children.Add(rectangle);

                        unitRectangles.Add(new Point(x, y), rectangle);
                        Console.WriteLine(x + "/" + y);
                    }
                }
            }
        }


        /********* Listeners *********/

        public void mouseEnterHandler(object sender, MouseEventArgs e) {
            var rectangle = sender as Rectangle;
            rectangle.StrokeThickness = 1;
            rectangle.Stroke = Brushes.Blue;
        }

        public void mouseLeaveHandler(object sender, MouseEventArgs e) {
            var rectangle = sender as Rectangle;
            if(rectangle == selectedSquare) {
                rectangle.StrokeThickness = 1;
                rectangle.Stroke = Brushes.Red;
            } else {
                rectangle.StrokeThickness = 1;
                rectangle.Stroke = Brushes.Black;
            }
        }

        public void rectangleMouseLeftMapHandler(object sender, MouseEventArgs e) {
            var rectangle = sender as Rectangle;
            int column = Grid.GetColumn(rectangle);
            int row = Grid.GetRow(rectangle);
            Point position = new Point(row, column);

            // Clear old selection
            unitSelecterCollec.Clear();
            unitSelecter.Children.Clear();
            if(selectedSquare != null) {
                selectedSquare.Stroke = Brushes.Black;
            }

            IRound round = game.getRound();
            if(round.isCurrentPlayerPosition(position)) {
                List<IUnit> units = round.getUnits(position);
                round.selectUnit(units[0]);
                displayUnitSelecter(units);

                rectangle.StrokeThickness = 1;
                rectangle.Stroke = Brushes.Red;


                selectedSquare = rectangle;
            } else {
                round.selectUnit(null);
            }


            e.Handled = true;
        }

        public void rectangleMouseLefUnitSelectertHandler(object sender, MouseEventArgs e) {
            selectedUnit.BorderBrush = Brushes.Black;

            Border border = sender as Border;
            border.BorderBrush = Brushes.Red;
            game.getRound().selectUnit(unitSelecterCollec[border]);
            selectedUnit = border;
            e.Handled = true;
        }

        public void rectangleMouseRightHandler(object sender, MouseEventArgs e) {
            var rectangle = sender as Rectangle;
            int column = Grid.GetColumn(rectangle);
            int row = Grid.GetRow(rectangle);
            Point position = new Point(row, column);

            IRound round = game.getRound();
            if(round.setDestination(position)) {
                round.executeMove();
                selectedSquare.Stroke = Brushes.Black;

                displayUnitsOnMap();
            }

            displayInfoPlayer();
            unitSelecterCollec.Clear();
            unitSelecter.Children.Clear();

            e.Handled = true;
        }

        public void onClickEndRound(object sender, RoutedEventArgs e) {
            game.endRound();

            unitSelecterCollec.Clear();
            unitSelecter.Children.Clear();
            displayUnitsOnMap();
            displayInfoPlayer();

            if(game.isEndOfGame()) {
                IPlayer player = game.getWinner();
                // TODO Handle draw situations.
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
