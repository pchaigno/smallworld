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

    /**
     * Logique d'interaction pour MainWindow.xaml
     */
    public partial class MapWindow: Window {
        IGame game;
        Rectangle[,] unitRectangles;
        Rectangle selectedSquare;
        Dictionary<Border, IUnit> unitSelecterCollec;
        Border selectedUnit;

        /**
         * Constructor
         * @param game The game represented by the window.
         */
        public MapWindow(IGame game) {
            InitializeComponent();

            this.game = game;
            
            int size = this.game.getMap().getSize();
            this.unitRectangles = new Rectangle[size, size];
            this.selectedSquare = null;

            this.unitSelecterCollec = new Dictionary<Border, IUnit>();
            this.selectedUnit = null;
        }

        /**
         * Called when the window is fully loaded.
         * @param sender The sender of the notification.
         * @param e The event.
         */
        private void Window_Loaded(object sender, RoutedEventArgs e) {
            IMap map = this.game.getMap();
            int size = map.getSize();

            for(int c=0; c<size; c++) {
                this.mapGrid.ColumnDefinitions.Add(new ColumnDefinition() {
                    Width = new GridLength(50, GridUnitType.Pixel)
                });
            }

            for(int l=0; l<size; l++) {
                this.mapGrid.RowDefinitions.Add(new RowDefinition() {
                    Height = new GridLength(50, GridUnitType.Pixel)
                });

                for(int c = 0; c < size; c++) {
                    ISquare type = map.getSquare(new Point(l, c));
                    var rect = createSquares(type, c, l);
                    this.mapGrid.Children.Add(rect);
                }
            }

            this.displayUnitsOnMap();
            this.displayInfoPlayer();
        }

        /**
         * Creates a graphic square associated to an ISquare.
         * @param type The type of square.
         * @param col The abscissa of the square on the map.
         * @param line The ordinate of the square on the map.
         */
        private Rectangle createSquares(ISquare type, int col, int line) {
            Rectangle rectangle = new Rectangle();

            rectangle.Fill = ImageFactory.getInstance().getBrushSquare(type);
            Grid.SetColumn(rectangle, col);
            Grid.SetRow(rectangle, line);
            rectangle.StrokeThickness = 1;
            rectangle.Stroke = Brushes.Black;

            rectangle.MouseLeftButtonDown += new MouseButtonEventHandler(this.rectangleMouseLeftMapHandler);
            rectangle.MouseRightButtonDown += new MouseButtonEventHandler(this.rectangleMouseRightHandler);
            rectangle.MouseEnter += new MouseEventHandler(this.mouseEnterHandler);
            rectangle.MouseLeave += new MouseEventHandler(this.mouseLeaveHandler);

            return rectangle;
        }

        /**
         * Display the units selected at the left bottom of the window.
         * @param units The units selected.
         */
        public void displayUnitSelecter(List<IUnit> units) {
            this.unitSelecterCollec = new Dictionary<Border, IUnit>();
            int i = 0;
            foreach(IUnit unit in units) {
                Border border = new Border();
                border.Background = ImageFactory.getInstance().getBrushUnitFace(unit);

                TextBlock unitText = new TextBlock();
                unitText.Text = unit.getRemainingMovementPoints()+" MvPt \n"+unit.getLifePoints()+" lifePt";
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

                border.MouseLeftButtonDown += new MouseButtonEventHandler(this.rectangleMouseLefUnitSelectertHandler);

                this.unitSelecter.Children.Add(border);

                this.unitSelecterCollec.Add(border, unit);
            }

        }

        /**
         * Display information about the current player at the right bottom of the window.
         */
        private void displayInfoPlayer() {
            IPlayer player1 = this.game.getPlayer1();
            this.playerD1.Text = player1.getName()+" - Units: "+this.game.getNbUnits(player1)+" - Points :"+player1.getPoints();

            IPlayer player2 = this.game.getPlayer2();
            this.playerD2.Text = player2.getName()+" - Units: "+this.game.getNbUnits(player2)+" - Points :"+player2.getPoints();

            this.roundD.Text = "Round number: "+this.game.getCurrentRound();
            this.currentD.Text = "Current PLayer: "+this.game.getCurrentPlayer().getName();

            this.lastMove.Text = this.game.getRound().getLastMoveInfo();
        }

        /**
         * Display all units on the map.
         */
        private void displayUnitsOnMap() {
            List<IUnit>[,] units = this.game.getMap().getUnits();

            for(int x=0; x<this.unitRectangles.GetLength(0); x++) {
                for(int y=0; y<this.unitRectangles.GetLength(1); y++) {
                    this.mapGrid.Children.Remove(this.unitRectangles[x, y]);
                }
            }

            for(int x=0; x<units.GetLength(0); x++) {
                for(int y=0; y<units.GetLength(1); y++) {
                    int nb = units[x, y].Count;
                    if(nb > 0) {
                        var rectangle = new Rectangle();

                        rectangle.Fill = ImageFactory.getInstance().getBrushUnit(units[x, y][0], nb);
                        Grid.SetColumn(rectangle, y);
                        Grid.SetRow(rectangle, x);
                        rectangle.StrokeThickness = 1;
                        rectangle.Stroke = Brushes.Black;
                        rectangle.MouseLeftButtonDown += new MouseButtonEventHandler(this.rectangleMouseLeftMapHandler);
                        rectangle.MouseRightButtonDown += new MouseButtonEventHandler(this.rectangleMouseRightHandler);
                        rectangle.MouseEnter += new MouseEventHandler(this.mouseEnterHandler);
                        rectangle.MouseLeave += new MouseEventHandler(this.mouseLeaveHandler);
                        this.mapGrid.Children.Add(rectangle);

                        this.unitRectangles[x, y] = rectangle;
                        Console.WriteLine(x+"/"+y);
                    }
                }
            }
        }

        /**
         * Listener for when the mouse enters a rectangle.
         * Surround the rectangle with a blue halo.
         * @param sender The rectangle sender of the notification.
         * @param e The event.
         */
        public void mouseEnterHandler(object sender, MouseEventArgs e) {
            var rectangle = sender as Rectangle;
            rectangle.StrokeThickness = 1;
            rectangle.Stroke = Brushes.Blue;
        }

        /**
         * Listener for when the mouse leaves a rectangle.
         * Remove the blue halo that surround the rectangle left.
         * @param sender The rectangle sender of the notification.
         * @param e The event.
         */
        public void mouseLeaveHandler(object sender, MouseEventArgs e) {
            var rectangle = sender as Rectangle;
            if(rectangle == this.selectedSquare) {
                rectangle.StrokeThickness = 1;
                rectangle.Stroke = Brushes.Red;
            } else {
                rectangle.StrokeThickness = 1;
                rectangle.Stroke = Brushes.Black;
            }
        }

        /**
         * Listener for left clicks on a rectangle.
         * Updates the current selected square (and units).
         * @param sender The rectangle sender of the notification.
         * @param e The event.
         */
        public void rectangleMouseLeftMapHandler(object sender, MouseEventArgs e) {
            var rectangle = sender as Rectangle;
            int column = Grid.GetColumn(rectangle);
            int row = Grid.GetRow(rectangle);
            Point position = new Point(row, column);

            // Clear old selection
            this.unitSelecterCollec.Clear();
            this.unitSelecter.Children.Clear();
            if(this.selectedSquare != null) {
                this.selectedSquare.Stroke = Brushes.Black;
            }

            IRound round = game.getRound();
            if(round.isCurrentPlayerPosition(position)) {
                List<IUnit> units = round.getUnits(position);
                round.selectUnit(units[0]);
                this.displayUnitSelecter(units);

                rectangle.StrokeThickness = 1;
                rectangle.Stroke = Brushes.Red;

                selectedSquare = rectangle;
            } else {
                round.selectUnit(null);
            }

            e.Handled = true;
        }

        /**
         * Listener for left clicks on the list of units to select.
         * Updates the current selected unit.
         * @param sender The sender of the notification, the object clicked.
         * @param e The event.
         */
        public void rectangleMouseLefUnitSelectertHandler(object sender, MouseEventArgs e) {
            this.selectedUnit.BorderBrush = Brushes.Black;

            Border border = sender as Border;
            border.BorderBrush = Brushes.Red;
            this.game.getRound().selectUnit(this.unitSelecterCollec[border]);
            selectedUnit = border;
            e.Handled = true;
        }

        /**
         * Listener for left clicks on a rectangle.
         * Move the currently selected unit to the rectangle clicked if possible.
         * @param sender The rectangle sender of the notification.
         * @param e The event.
         */
        public void rectangleMouseRightHandler(object sender, MouseEventArgs e) {
            var rectangle = sender as Rectangle;
            int column = Grid.GetColumn(rectangle);
            int row = Grid.GetRow(rectangle);
            Point position = new Point(row, column);

            IRound round = this.game.getRound();
            if(round.setDestination(position)) {
                round.executeMove();
                this.selectedSquare.Stroke = Brushes.Black;

                this.displayUnitsOnMap();
            }

            this.displayInfoPlayer();
            this.unitSelecterCollec.Clear();
            this.unitSelecter.Children.Clear();

            e.Handled = true;
        }

        /**
         * Listener for clicks on the end round button.
         * End the round.
         * @param sender The sender of the notification.
         * @param e The event.
         */
        public void onClickEndRound(object sender, RoutedEventArgs e) {
            this.game.endRound();

            this.unitSelecterCollec.Clear();
            this.unitSelecter.Children.Clear();
            this.displayUnitsOnMap();
            this.displayInfoPlayer();

            if(this.game.isEndOfGame()) {
                IPlayer player = this.game.getWinner();
                // TODO Handle draw situations.
                string messageBoxText = "Congratulation "+player.getName()+"\n You have defeated your enemy !";
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