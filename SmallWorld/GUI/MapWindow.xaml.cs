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

    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">The game represented by the window.</param>
        public MapWindow(IGame game) {
            this.WindowState = WindowState.Maximized;
            InitializeComponent();

            this.game = game;
            int size = this.game.Map.Size;
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

        /// <summary>
        /// Called when the window is fully loaded.
        /// </summary>
        /// <param name="sender">The sender of the notification.</param>
        /// <param name="e">The event.</param>
        private void Window_Loaded(object sender, RoutedEventArgs e) {
            IMap map = this.game.Map;
            int size = map.Size;

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

        /// <summary>
        /// Creates a graphic square associated to an ISquare.
        /// </summary>
        /// <param name="type">The type of square.</param>
        /// <param name="col">The abscissa of the square on the map.</param>
        /// <param name="line">The ordinate of the square on the map.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Displays the units selected at the left bottom of the window.
        /// </summary>
        /// <param name="units">All the units.</param>
        /// <param name="selectedUnits">The units selected.</param>
        private void DisplayUnitSelecter(List<IUnit> units, List<IUnit> selectedUnits) {
            this.unitSelecterCollec = new Dictionary<Border, IUnit>();
            foreach(IUnit unit in units) {
                Border border = new Border();
                
                border.Background = ImageFactory.getInstance().getBrushUnitFace(unit);
                TextBlock unitText = new TextBlock();
                unitText.Text = unit.RemainingMovementPoints+" MvPt \n"+unit.LifePoints+" lifePt";
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

        /// <summary>
        /// Displays information about the current player at the right bottom of the window.
        /// </summary>
        private void DisplayInfoPlayer() {
            IPlayer player1 = this.game.Player1;
            this.playerD1.Text = player1.Name+"     " + this.game.GetNbUnits(player1)+ " Units     " + player1.Points + " Pts";

            IPlayer player2 = this.game.Player2;
            this.playerD2.Text = player2.Name + "     " + this.game.GetNbUnits(player2) + " Units     " + player2.Points + " Pts";

            this.roundD.Text = "Round number: "+this.game.CurrentRound;
            this.currentD.Text = "Current Player: "+this.game.CurrentPlayer.Name;

            this.lastMove.Text = this.game.Round.LastMoveInfo;
        }

        /// <summary>
        /// Displays all units on the map.
        /// </summary>
        private void DisplayUnitsOnMap() {
            List<IUnit>[,] units = this.game.Map.Units;

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

        /// <summary>
        /// Displays advised destination on the map.
        /// </summary>
        /// <param name="positions">The advised destination.</param>
        private void DisplayAdvisedDestination(List<IPoint> positions) {
            if(advisedDestination != null) {
                foreach(IPoint point in positions) {
                    Rectangle rect = squareRectangles[point.X, point.Y];
                    rect.Stroke = advisedBrush;
                    advisedDestination.Add(rect);
                }
            }
        }

        /// <summary>
        /// Clear advised destinations.
        /// </summary>
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

        /// <summary>
        /// Listener for when the mouse enters a rectangle.
        /// Surrounds the rectangle with a blue halo.
        /// </summary>
        /// <param name="sender">The rectangle sender of the notification.</param>
        /// <param name="e">The event.</param>
        private void MouseEnterRectangle(object sender, MouseEventArgs e) {
            var rectangle = sender as Rectangle;
            rectangle.Stroke = overBrush;
        }

        /// <summary>
        /// Listener for when the mouse leaves a rectangle.
        /// Removes the blue halo that surround the rectangle left.
        /// </summary>
        /// <param name="sender">The rectangle sender of the notification.</param>
        /// <param name="e">The event.</param>
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

        /// <summary>
        /// Listener for left clicks on a rectangle.
        /// Updates the current selected square (and units).
        /// </summary>
        /// <param name="sender">The rectangle sender of the notification.</param>
        /// <param name="e">The event.</param>
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

            IRound round = game.Round;
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

        /// <summary>
        /// Listener for left clicks on the list of units to select.
        /// Updates the current selected unit.
        /// </summary>
        /// <param name="sender">The sender of the notification, the object clicked.</param>
        /// <param name="e">The event.</param>
        private void MouseLeftUnitSelecter(object sender, MouseEventArgs e) {

            IRound round = this.game.Round;
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
        
        /// <summary>
        /// Listener for left clicks on a rectangle.
        /// Moves the currently selected unit to the rectangle clicked if possible.
        /// </summary>
        /// <param name="sender">The rectangle sender of the notification.</param>
        /// <param name="e">The event.</param>
        private void MouseRightMapRectangle(object sender, MouseEventArgs e) {
            var rectangle = sender as Rectangle;
            int column = Grid.GetColumn(rectangle);
            int row = Grid.GetRow(rectangle);
            IPoint position = new Point(row, column);

            IRound round = this.game.Round;
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

        /// <summary>
        /// Listener for clicks on the end round button.
        /// Ends the round.
        /// </summary>
        /// <param name="sender">The sender of the notification.</param>
        /// <param name="e">The event.</param>
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
                    string messageBoxText = "Congratulation " + player.Name + "\n You have defeated your enemy !";
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

        /// <summary>
        /// Listener for clicks on the start a new game menu item.
        /// Opens the CreateGame window to start a new game.
        /// </summary>
        /// <param name="sender">The sender of the notification.</param>
        /// <param name="e">The event.</param>
        private void OnClickStartNewGame(object sender, RoutedEventArgs e) {
            MessageBoxResult result = MessageBox.Show("Do you want to save the game before starting a new game?", "Open a new game", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(result == MessageBoxResult.Yes) {
                this.SaveGame();
            }
            CreateGame createWindow = new CreateGame();
            createWindow.Show();
            this.Close();
        }

        /// <summary>
        /// Listener for clicks on the open menu item.
        /// Restores a new saved game.
        /// </summary>
        /// <param name="sender">The sender of the notification.</param>
        /// <param name="e">The event.</param>
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

        /// <summary>
        /// Listener for clicks on the save menu item.
        /// Saves the current game.
        /// </summary>
        /// <param name="sender">The sender of the notification.</param>
        /// <param name="e">The event.</param>
        private void OnClickSave(object sender, RoutedEventArgs e) {
            this.SaveGame();
        }

        /// <summary>
        /// Listener for clicks on the save as menu item.
        /// Saves the current game.
        /// </summary>
        /// <param name="sender">The sender of the notification.</param>
        /// <param name="e">The event.</param>
        private void OnClickSaveAs(object sender, RoutedEventArgs e) {
            this.SaveGameAs();
        }

        /// <summary>
        /// Listener for clicks on the exit menu item.
        /// Ends the round.
        /// </summary>
        /// <param name="sender">The sender of the notification.</param>
        /// <param name="e">The event.</param>
        private void OnClickExit(object sender, RoutedEventArgs e) {
            MessageBoxResult result = MessageBox.Show("Do you want to save the game before exiting?", "Exit the game", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(result == MessageBoxResult.Yes) {
                this.SaveGame();
            }
            this.Close();
        }

        /// <summary>
        /// Saves the current game in the current save file.
        /// Checks that there is already a save file defined.
        /// If no save file is defined, calls SaveGameAs.
        /// </summary>
        private void SaveGame() {
            // TODO
        }

        /// <summary>
        /// Saves the current game after asking for the location for the save file.
        /// </summary>
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

        /// <summary>
        /// Restores the game from the current save file.
        /// </summary>
        private void RestoreGame() {
            // TODO
        }

        /// <summary>
        /// Listener for pressed shift key.
        /// Activates the multiple selection mode.
        /// </summary>
        /// <param name="sender">The sender of the notification.</param>
        /// <param name="e">The event.</param>
        private void OnButtonKeyDown(object sender, KeyEventArgs e) {
            if(e.Key == Key.LeftShift || e.Key == Key.RightShift || e.Key == Key.RightCtrl || e.Key == Key.LeftCtrl) {
                this.multipleSelection = true;
            }
            e.Handled = true;
        }

        /// <summary>
        /// Listener for pressed shift key.
        /// Desactive the multiple selection mode.
        /// </summary>
        /// <param name="sender">The sender of the notification.</param>
        /// <param name="e">The event.</param>
        private void OnButtonKeyUp(object sender, KeyEventArgs e) {
            if(e.Key == Key.LeftShift || e.Key == Key.RightShift || e.Key == Key.RightCtrl || e.Key == Key.LeftCtrl) {
                this.multipleSelection = false;
            }
            e.Handled = true;
        }
    }
}