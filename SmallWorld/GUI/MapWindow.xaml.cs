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
using Microsoft.Win32;
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
        private String saveFile;

        /**
         * Constructor
         * @param game The game represented by the window.
         */
        public MapWindow(IGame game) {
            this.WindowState = WindowState.Maximized;
            InitializeComponent();

            this.game = game;
            int size = this.game.GetMap().GetSize();
            this.unitRectangles = new Rectangle[size, size];
            this.selectedSquare = null;
            this.unitSelecterCollec = new Dictionary<Border, IUnit>();
            this.advisedDestination = new List<Rectangle>();
            this.selectedUnitBorder = new List<Border>();
            this.multipleSelection = false;
            this.saveFile = null;

            this.KeyDown += new KeyEventHandler(OnButtonKeyDown);
            this.KeyUp += new KeyEventHandler(OnButtonKeyUp);
        }

        /**
         * Called when the window is fully loaded.
         * @param sender The sender of the notification.
         * @param e The event.
         */
        private void Window_Loaded(object sender, RoutedEventArgs e) {
            IMap map = this.game.GetMap();
            int size = map.GetSize();

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
                    ISquare type = map.GetSquare(new Point(l, c));
                    var rect = CreateSquares(type, c, l);
                    this.mapGrid.Children.Add(rect);
                    squareRectangles[l, c] = rect;
                }
            }

            this.DisplayUnitsOnMap();
            this.DisplayInfoPlayer();
        }

        /**
         * Creates a graphic square associated to an ISquare.
         * @param type The type of square.
         * @param col The abscissa of the square on the map.
         * @param line The ordinate of the square on the map.
         */
        private Rectangle CreateSquares(ISquare type, int col, int line) {
            Rectangle rectangle = new Rectangle();

            rectangle.Fill = ImageFactory.getInstance().getBrushSquare(type);
            Grid.SetColumn(rectangle, col);
            Grid.SetRow(rectangle, line);
            rectangle.StrokeThickness = 2;
            rectangle.Stroke = defaultBrush;

            rectangle.MouseLeftButtonDown += new MouseButtonEventHandler(this.MouseLeftMapRectangle);
            rectangle.MouseRightButtonDown += new MouseButtonEventHandler(this.MouseRightMapRectangle);
            rectangle.MouseEnter += new MouseEventHandler(this.MouseEnterRectangle);
            rectangle.MouseLeave += new MouseEventHandler(this.MouseLeaveRectangle);

            return rectangle;
        }

        /**
         * Display the units selected at the left bottom of the window.
         * @param units The units selected.
         */
        private void DisplayUnitSelecter(List<IUnit> units, List<IUnit> selectedUnits) {
            this.unitSelecterCollec = new Dictionary<Border, IUnit>();
            foreach(IUnit unit in units) {
                Border border = new Border();
                
                border.Background = ImageFactory.getInstance().getBrushUnitFace(unit);
                TextBlock unitText = new TextBlock();
                unitText.Text = unit.GetRemainingMovementPoints()+" MvPt \n"+unit.GetLifePoints()+" lifePt";
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
                border.MouseLeftButtonDown += new MouseButtonEventHandler(this.MouseLeftUnitSelecter);
            }
        }

        /**
         * Display information about the current player at the right bottom of the window.
         */
        private void DisplayInfoPlayer() {
            IPlayer player1 = this.game.GetPlayer1();
            this.playerD1.Text = player1.GetName()+"     " + this.game.GetNbUnits(player1)+ " Units     " + player1.GetPoints() + " Pts";

            IPlayer player2 = this.game.GetPlayer2();
            this.playerD2.Text = player2.GetName() + "     " + this.game.GetNbUnits(player2) + " Units     " + player2.GetPoints() + " Pts";

            this.roundD.Text = "Round number: "+this.game.GetCurrentRound();
            this.currentD.Text = "Current Player: "+this.game.GetCurrentPlayer().GetName();

            this.lastMove.Text = this.game.GetRound().GetLastMoveInfo();
        }

        /**
         * Display all units on the map.
         */
        private void DisplayUnitsOnMap() {
            List<IUnit>[,] units = this.game.GetMap().GetUnits();

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
                        rectangle.MouseLeftButtonDown += new MouseButtonEventHandler(this.MouseLeftMapRectangle);
                        rectangle.MouseRightButtonDown += new MouseButtonEventHandler(this.MouseRightMapRectangle);
                        rectangle.MouseEnter += new MouseEventHandler(this.MouseEnterRectangle);
                        rectangle.MouseLeave += new MouseEventHandler(this.MouseLeaveRectangle);
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
        private void DisplayAdvisedDestination(List<IPoint> positions) {
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
        private void ClearAdvisedDestination() {
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
        private void MouseEnterRectangle(object sender, MouseEventArgs e) {
            var rectangle = sender as Rectangle;
            rectangle.Stroke = overBrush;
        }

        /**
         * Listener for when the mouse leaves a rectangle.
         * Remove the blue halo that surround the rectangle left.
         * @param sender The rectangle sender of the notification.
         * @param e The event.
         */
        private void MouseLeaveRectangle(object sender, MouseEventArgs e) {
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
        private void MouseLeftMapRectangle(object sender, MouseEventArgs e) {
            var rectangle = sender as Rectangle;
            int column = Grid.GetColumn(rectangle);
            int row = Grid.GetRow(rectangle);
            IPoint position = new Point(row, column);

            // Clear old selection
            this.unitSelecterCollec.Clear();
            this.unitSelecter.Children.Clear();
            this.selectedUnitBorder.Clear();
            ClearAdvisedDestination();
            if(this.selectedSquare != null) {
                this.selectedSquare.Stroke = Brushes.Transparent;
            }

            IRound round = game.GetRound();
            if(round.IsCurrentPlayerPosition(position)) {
                List<IUnit> units = round.GetUnits(position);
                List<IUnit> selectedUnits = new List<IUnit>();

                if(this.multipleSelection) {
                    foreach(IUnit unit in units) {
                        selectedUnits.Add(unit);
                    }
                } else {
                    selectedUnits.Add(units[0]);
                }
                round.SelectUnits(selectedUnits, position);

                this.DisplayUnitSelecter(units, selectedUnits);
                this.DisplayAdvisedDestination(round.GetAdvisedDestinations(units[0], position));

                rectangle.Stroke = selectedBrush;
                selectedSquare = rectangle;
            } else {
                round.UnselectUnit();
            }

            e.Handled = true;
        }

        /**
         * Listener for left clicks on the list of units to select.
         * Updates the current selected unit.
         * @param sender The sender of the notification, the object clicked.
         * @param e The event.
         */
        private void MouseLeftUnitSelecter(object sender, MouseEventArgs e) {

            IRound round = this.game.GetRound();
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
            round.SelectUnits(selectedUnits);


            ClearAdvisedDestination();
            this.DisplayAdvisedDestination(round.GetAdvisedDestinations(this.unitSelecterCollec[border], position));

            e.Handled = true;
        }

        /**
         * Listener for left clicks on a rectangle.
         * Move the currently selected unit to the rectangle clicked if possible.
         * @param sender The rectangle sender of the notification.
         * @param e The event.
         */
        private void MouseRightMapRectangle(object sender, MouseEventArgs e) {
            var rectangle = sender as Rectangle;
            int column = Grid.GetColumn(rectangle);
            int row = Grid.GetRow(rectangle);
            IPoint position = new Point(row, column);

            IRound round = this.game.GetRound();
            if(round.SetDestination(position)) {
                round.ExecuteMove();
                this.selectedSquare.Stroke = Brushes.Black;

                this.DisplayUnitsOnMap();
            }

            this.DisplayInfoPlayer();
            this.unitSelecterCollec.Clear();
            this.unitSelecter.Children.Clear();
            this.ClearAdvisedDestination();
            this.selectedUnitBorder.Clear();

            e.Handled = true;
        }

        /**
         * Listener for clicks on the end round button.
         * End the round.
         * @param sender The sender of the notification.
         * @param e The event.
         */
        private void OnClickEndRound(object sender, RoutedEventArgs e) {
            this.game.EndRound();

            this.unitSelecterCollec.Clear();
            this.unitSelecter.Children.Clear();
            this.ClearAdvisedDestination();
            this.selectedUnitBorder.Clear();

            this.DisplayUnitsOnMap();
            this.DisplayInfoPlayer();

            if(this.game.IsEndOfGame()) {
                IPlayer player = this.game.GetWinner();

                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Exclamation;

                if(player != null) {
                    string messageBoxText = "Congratulation " + player.GetName() + "\n You have defeated your enemy !";
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
         * Listener for clicks on the start a new game menu item.
         * Open the CreateGame window to start a new game.
         * @param sender The sender of the notification.
         * @param e The event.
         */
        private void OnClickStartNewGame(object sender, RoutedEventArgs e) {
            MessageBoxResult result = MessageBox.Show("Do you want to save the game before starting a new game?", "Open a new game", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(result == MessageBoxResult.Yes) {
                this.SaveGame();
            }
            CreateGame createWindow = new CreateGame();
            createWindow.Show();
            this.Close();
        }

        /**
         * Listener for clicks on the open menu item.
         * Restore a new saved game.
         * @param sender The sender of the notification.
         * @param e The event.
         */
        private void OnClickOpen(object sender, RoutedEventArgs e) {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".sav";
            dlg.Filter = "Saved game (*.sav) | *.sav | All files (*.*) | *.*";
            Nullable<bool> result = dlg.ShowDialog();
            if(result == true) {
                this.saveFile = dlg.FileName;

                this.RestoreGame();
            }
        }

        /**
         * Listener for clicks on the save menu item.
         * Save the current game.
         * @param sender The sender of the notification.
         * @param e The event.
         */
        private void OnClickSave(object sender, RoutedEventArgs e) {
            this.SaveGame();
        }

        /**
         * Listener for clicks on the save as menu item.
         * Save the current game.
         * @param sender The sender of the notification.
         * @param e The event.
         */
        private void OnClickSaveAs(object sender, RoutedEventArgs e) {
            this.SaveGameAs();
        }

        /**
         * Listener for clicks on the exit menu item.
         * End the round.
         * @param sender The sender of the notification.
         * @param e The event.
         */
        private void OnClickExit(object sender, RoutedEventArgs e) {
            MessageBoxResult result = MessageBox.Show("Do you want to save the game before exiting?", "Exit the game", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(result == MessageBoxResult.Yes) {
                this.SaveGame();
            }
            this.Close();
        }

        /**
         * Saves the current game in the current save file.
         * Checks that there is already a save file defined.
         * If no save file is defined, calls SaveGameAs.
         */
        private void SaveGame() {
            // TODO
        }

        /**
         * Saves the current game after asking for the location for the save file.
         */
        private void SaveGameAs() {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".sav";
            dlg.Filter = "Saved game (*.sav) | *.sav | All files (*.*) | *.*";
            Nullable<bool> result = dlg.ShowDialog();
            if(result == true) {
                this.saveFile = dlg.FileName;

                this.SaveGame();
            }
        }

        /**
         * Restores the game from the current save file.
         */
        private void RestoreGame() {
            // TODO
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