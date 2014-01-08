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
using BitMap = System.Drawing.Bitmap;
using Image = System.Drawing.Image;
using TextureBrush = System.Drawing.TextureBrush;
using Graphics = System.Drawing.Graphics;
using SmallWorld;
using Point = SmallWorld.Point;
using GUI;

namespace GUI {

    /**
     * Logique d'interaction pour MainWindow.xaml
     */
    public partial class MapWindow: Window {

        private Brush selectedBrush = Brushes.Red;
        private Brush overBrush = Brushes.Black;
        private Brush unitSelecterBrush = Brushes.Black;
        private Brush advisedBrush = Brushes.Blue;
        private Brush defaultBrush = Brushes.Transparent;

        private IGame game;
        private Rectangle[,] unitRectangles;
        private Rectangle[,] squareRectangles;
        private Rectangle selectedSquare;
        private Dictionary<Border, IUnit> unitSelecterCollec;
        private List<Border> selectedUnitBorder;
        private List<Rectangle> advisedDestination;

        private bool multipleSelection;

        /**
         * Constructor
         * @param game The game represented by the window.
         */
        public MapWindow(IGame game) {
            this.WindowState = WindowState.Maximized;
            InitializeComponent();

            this.game = game;
            int size = this.game.getMap().getSize();
            this.unitRectangles = new Rectangle[size, size];
            this.selectedSquare = null;
            this.unitSelecterCollec = new Dictionary<Border, IUnit>();
            this.advisedDestination = new List<Rectangle>();
            this.selectedUnitBorder = new List<Border>();
            this.multipleSelection = false;

            this.KeyDown += new KeyEventHandler(OnButtonKeyDown);
            this.KeyUp += new KeyEventHandler(OnButtonKeyUp);
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

            squareRectangles = new Rectangle[size, size];

            for(int l=0; l<size; l++) {
                this.mapGrid.RowDefinitions.Add(new RowDefinition() {
                    Height = new GridLength(50, GridUnitType.Pixel)
                });

                for(int c = 0; c < size; c++) {
                    ISquare type = map.getSquare(new Point(l, c));
                    var rect = createSquares(type, c, l);
                    this.mapGrid.Children.Add(rect);
                    squareRectangles[l, c] = rect;
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
            rectangle.StrokeThickness = 2;
            rectangle.Stroke = defaultBrush;

            rectangle.MouseLeftButtonDown += new MouseButtonEventHandler(this.mouseLeftMapRectangle);
            rectangle.MouseRightButtonDown += new MouseButtonEventHandler(this.mouseRightMapRectangle);
            rectangle.MouseEnter += new MouseEventHandler(this.mouseEnterRectangle);
            rectangle.MouseLeave += new MouseEventHandler(this.mouseLeaveRectangle);

            return rectangle;
        }

        /**
         * Display the units selected at the left bottom of the window.
         * @param units The units selected.
         */
        private void displayUnitSelecter(List<IUnit> units, List<IUnit> selectedUnits) {
            this.unitSelecterCollec = new Dictionary<Border, IUnit>();
            foreach(IUnit unit in units) {
                Border border = new Border();
                
                border.Background = ImageFactory.getInstance().getBrushUnitFace(unit);
                TextBlock unitText = new TextBlock();
                unitText.Text = unit.getRemainingMovementPoints()+" MvPt \n"+unit.getLifePoints()+" lifePt";
                unitText.FontSize = 14;
                unitText.Foreground = unitSelecterBrush;
                unitText.FontWeight = FontWeights.Bold;
                border.Child = unitText;
                border.Width = 80;
                border.Height = 120;
                border.BorderThickness = new Thickness(2);

                if(selectedUnits.Contains(unit)) {
                    border.BorderBrush = overBrush;
                    this.selectedUnitBorder.Add(border);
                } else {
                    border.BorderBrush = defaultBrush;
                }

                this.unitSelecter.Children.Add(border);
                this.unitSelecterCollec.Add(border, unit);
                border.MouseLeftButtonDown += new MouseButtonEventHandler(this.mouseLeftUnitSelecter);
            }
        }

        /**
         * Display information about the current player at the right bottom of the window.
         */
        private void displayInfoPlayer() {
            IPlayer player1 = this.game.getPlayer1();
            this.playerD1.Text = player1.getName()+"     " + this.game.getNbUnits(player1)+ " Units     " + player1.getPoints() + " Pts";

            IPlayer player2 = this.game.getPlayer2();
            this.playerD2.Text = player2.getName() + "     " + this.game.getNbUnits(player2) + " Units     " + player2.getPoints() + " Pts";

            this.roundD.Text = "Round number: "+this.game.getCurrentRound();
            this.currentD.Text = "Current Player: "+this.game.getCurrentPlayer().getName();

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
                        rectangle.StrokeThickness = 2;
                        rectangle.Stroke = defaultBrush;
                        rectangle.MouseLeftButtonDown += new MouseButtonEventHandler(this.mouseLeftMapRectangle);
                        rectangle.MouseRightButtonDown += new MouseButtonEventHandler(this.mouseRightMapRectangle);
                        rectangle.MouseEnter += new MouseEventHandler(this.mouseEnterRectangle);
                        rectangle.MouseLeave += new MouseEventHandler(this.mouseLeaveRectangle);
                        this.mapGrid.Children.Add(rectangle);

                        this.unitRectangles[x, y] = rectangle;
                    }
                }
            }
        }

        /**
         * Display advised destination on the map
         * @param positions advised destination
         */
        private void displayAdvisedDestination(List<IPoint> positions) {
            if(advisedDestination != null) {
                foreach(IPoint point in positions) {
                    Rectangle rect = squareRectangles[point.X, point.Y];
                    rect.Stroke = advisedBrush;
                    advisedDestination.Add(rect);
                }
            }
        }

        /**
         * Clear advised destinations
         */
        private void clearAdvisedDestination() {
            if(advisedDestination != null) {
                foreach(Rectangle rect in advisedDestination) {
                    if(selectedSquare != rect) {
                        rect.Stroke = defaultBrush;
                    }
                }
            }
            advisedDestination.Clear();
        }

        /**
         * Listener for when the mouse enters a rectangle.
         * Surround the rectangle with a blue halo.
         * @param sender The rectangle sender of the notification.
         * @param e The event.
         */
        private void mouseEnterRectangle(object sender, MouseEventArgs e) {
            var rectangle = sender as Rectangle;
            rectangle.Stroke = overBrush;
        }

        /**
         * Listener for when the mouse leaves a rectangle.
         * Remove the blue halo that surround the rectangle left.
         * @param sender The rectangle sender of the notification.
         * @param e The event.
         */
        private void mouseLeaveRectangle(object sender, MouseEventArgs e) {
            var rectangle = sender as Rectangle;
            if(rectangle == this.selectedSquare) {
                rectangle.Stroke = selectedBrush;
            } else if (advisedDestination.Contains(rectangle)){
                rectangle.Stroke = advisedBrush;
            } else {
                rectangle.Stroke = defaultBrush;
            }
        }

        /**
         * Listener for left clicks on a rectangle.
         * Updates the current selected square (and units).
         * @param sender The rectangle sender of the notification.
         * @param e The event.
         */
        private void mouseLeftMapRectangle(object sender, MouseEventArgs e) {
            var rectangle = sender as Rectangle;
            int column = Grid.GetColumn(rectangle);
            int row = Grid.GetRow(rectangle);
            IPoint position = new Point(row, column);

            // Clear old selection
            this.unitSelecterCollec.Clear();
            this.unitSelecter.Children.Clear();
            this.selectedUnitBorder.Clear();
            clearAdvisedDestination();
            if(this.selectedSquare != null) {
                this.selectedSquare.Stroke = Brushes.Transparent;
            }

            IRound round = game.getRound();
            if(round.isCurrentPlayerPosition(position)) {
                List<IUnit> units = round.getUnits(position);
                List<IUnit> selectedUnits = new List<IUnit>();

                if(this.multipleSelection) {
                    foreach(IUnit unit in units) {
                        selectedUnits.Add(unit);
                    }
                } else {
                    selectedUnits.Add(units[0]);
                }
                round.selectUnits(selectedUnits, position);

                this.displayUnitSelecter(units, selectedUnits);
                this.displayAdvisedDestination(round.getAdvisedDestinations(units[0], position));

                rectangle.Stroke = selectedBrush;
                selectedSquare = rectangle;
            } else {
                round.unselectUnit();
            }

            e.Handled = true;
        }

        /**
         * Listener for left clicks on the list of units to select.
         * Updates the current selected unit.
         * @param sender The sender of the notification, the object clicked.
         * @param e The event.
         */
        private void mouseLeftUnitSelecter(object sender, MouseEventArgs e) {

            IRound round = this.game.getRound();
            Border border = sender as Border;

            int column = Grid.GetColumn(this.selectedSquare);
            int row = Grid.GetRow(this.selectedSquare);
            IPoint position = new Point(row, column);

            if(!multipleSelection) {
                //clear old selection
                foreach(Border borderUnit in selectedUnitBorder) {
                    borderUnit.BorderBrush = Brushes.Transparent;
                }
                selectedUnitBorder.Clear();
            }

            if(!this.selectedUnitBorder.Contains(border)) {
                border.BorderBrush = unitSelecterBrush;
                selectedUnitBorder.Add(border);
            } else {
                border.BorderBrush = defaultBrush;
                selectedUnitBorder.Remove(border);
            }

            List<IUnit> selectedUnits = new List<IUnit>();
            if(multipleSelection) {
                foreach (Border borderUnit in this.selectedUnitBorder) {
                    selectedUnits.Add(this.unitSelecterCollec[borderUnit]);
                }
            } else {
                selectedUnits.Add(this.unitSelecterCollec[border]);
            }
            round.selectUnits(selectedUnits);


            clearAdvisedDestination();
            this.displayAdvisedDestination(round.getAdvisedDestinations(this.unitSelecterCollec[border], position));

            e.Handled = true;
        }

        /**
         * Listener for left clicks on a rectangle.
         * Move the currently selected unit to the rectangle clicked if possible.
         * @param sender The rectangle sender of the notification.
         * @param e The event.
         */
        private void mouseRightMapRectangle(object sender, MouseEventArgs e) {
            var rectangle = sender as Rectangle;
            int column = Grid.GetColumn(rectangle);
            int row = Grid.GetRow(rectangle);
            IPoint position = new Point(row, column);

            IRound round = this.game.getRound();
            if(round.setDestination(position)) {
                round.executeMove();
                this.selectedSquare.Stroke = Brushes.Black;

                this.displayUnitsOnMap();
            }

            this.displayInfoPlayer();
            this.unitSelecterCollec.Clear();
            this.unitSelecter.Children.Clear();
            this.clearAdvisedDestination();
            this.selectedUnitBorder.Clear();

            e.Handled = true;
        }

        /**
         * Listener for clicks on the end round button.
         * End the round.
         * @param sender The sender of the notification.
         * @param e The event.
         */
        private void onClickEndRound(object sender, RoutedEventArgs e) {
            this.game.endRound();

            this.unitSelecterCollec.Clear();
            this.unitSelecter.Children.Clear();
            this.clearAdvisedDestination();
            this.selectedUnitBorder.Clear();

            this.displayUnitsOnMap();
            this.displayInfoPlayer();

            if(this.game.isEndOfGame()) {
                IPlayer player = this.game.getWinner();

                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Exclamation;

                if(player != null) {
                    string messageBoxText = "Congratulation " + player.getName() + "\n You have defeated your enemy !";
                    string caption = "Victory!";

                    MessageBox.Show(messageBoxText, caption, button, icon);
                } else {
                    string messageBoxText = "This fight ended in a draw...";
                    string caption = "Draw!";

                    MessageBox.Show(messageBoxText, caption, button, icon);
                }
                this.Close();
            }
        }


        /**
         * Listener for pressed shift key
         * Activate the multiple selection mode
         */
        private void OnButtonKeyDown(object sender, KeyEventArgs e) {
            if(e.Key == Key.LeftShift || e.Key == Key.RightShift || e.Key == Key.RightCtrl || e.Key == Key.LeftCtrl) {
                this.multipleSelection = true;
            }
            e.Handled = true;
        }

        /**
         * Listener for pressed shift key
         * Deactive the multiple selection mode
         */
        private void OnButtonKeyUp(object sender, KeyEventArgs e) {
            if(e.Key == Key.LeftShift || e.Key == Key.RightShift || e.Key == Key.RightCtrl || e.Key == Key.LeftCtrl) {
                this.multipleSelection = false;
            }
            e.Handled = true;
        }
    }
}