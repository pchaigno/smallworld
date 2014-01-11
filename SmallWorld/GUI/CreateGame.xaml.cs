using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using SmallWorld;
using MainWindow;

/**
 * TODO correct bug empty combobox
 */
namespace GUI {

    /// <summary>
    /// Logique d'interaction pour Creator.xaml
    /// </summary>
    public partial class CreateGame: Window {
        PeopleCollection people1Collec;
        PeopleCollection people2Collec;
        MapCollection mapCollection;

        /// <summary>
        /// Constructor
        /// Intiliases the selectors.
        /// </summary>
        public CreateGame() {
            this.WindowState = WindowState.Maximized;
            InitializeComponent();

            mapCollection = new MapCollection();
            mapBox.DataContext = mapCollection;
            mapBox.SelectedItem = "Small";

            people1Collec = new PeopleCollection();
            people1Box.DataContext = people1Collec;
            people1Box.SelectedItem = "Gaulois";

            people2Collec = new PeopleCollection();
            people2Box.DataContext = people2Collec;
            people2Box.SelectedItem = "Vikings";
        }

        /// <summary>
        /// Listener for changes on people1.
        /// </summary>
        /// <param name="sender">The sender of the notification.</param>
        /// <param name="e">The event.</param>
        public void OnChangePeople1(object sender, SelectionChangedEventArgs e) {
            string value = (string)people1Box.SelectedItem;
            people1Collec.selected = value;
            people2Collec.Remove(value);
        }

        /// <summary>
        /// Listener for changes on people2.
        /// </summary>
        /// <param name="sender">The sender of the notification.</param>
        /// <param name="e">The event.</param>
        public void OnChangePeople2(object sender, SelectionChangedEventArgs e) {
            string value = (string)people2Box.SelectedItem;
            people2Collec.selected = value;
            people1Collec.Remove(value);
        }

        /// <summary>
        /// Listener for changes on map.
        /// </summary>
        /// <param name="sender">The sender of the notification.</param>
        /// <param name="e">The event.</param>
        public void OnChangeMap(object sender, SelectionChangedEventArgs e) {
            mapCollection.selected = (string)mapBox.SelectedItem;
        }

        /// <summary>
        /// Listener for clicks on launcher.
        /// </summary>
        /// <param name="sender">The sender of the notification.</param>
        /// <param name="e">The event.</param>
        public void OnClickLauncher(object sender, RoutedEventArgs e) {
            string name1 = name1Box.Text;
            string name2 = name2Box.Text;
            IUnitFactory factory1 = people1Collec.GetFactory();
            IUnitFactory factory2 = people2Collec.GetFactory();
            IGameBuilder gameBuilder = mapCollection.GetBuilder();

            if(name1!="" && name2!="" && factory1!=null && factory2!=null && gameBuilder!=null) {
                IGame game = gameBuilder.BuildGame(name1, factory1, name2, factory2);
                MapWindow window = new MapWindow(game);
                window.Show();
                this.Close();
            } else {
                string messageBoxText = "You forgot to enter all the parameters!";
                string caption = "Oups!";
                MessageBoxButton button = MessageBoxButton.YesNoCancel;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBox.Show(messageBoxText, caption, button, icon);
            }
        }
    }

    /// <summary>
    /// The object represented by the people selector.
    /// </summary>
    class PeopleCollection: ObservableCollection<string> {
        public string selected {
            get;
            set;
        }
        private string removed;

        /// <summary>
        /// Constructor
        /// </summary>
        public PeopleCollection() {
            Add("Gaulois");
            Add("Dwarves");
            Add("Vikings");
        }

        /// <summary>
        /// Removes an element from the collection.
        /// </summary>
        /// <param name="st">The element to remove.</param>
        public new void Remove(string st) {
            base.Remove(st);
            if(removed != "") {
                Add(removed);
            }
            removed = st;
        }

        /// <summary>
        /// Returns the unit factory associated to the people selected.
        /// </summary>
        /// <returns>The unit factory associated to the people selected.</returns>
        public IUnitFactory GetFactory() {
            IUnitFactory factory = null;
            if(selected.Equals("Dwarves")) {
                factory = new DwarfFactory();
            } else if(selected.Equals("Vikings")) {
                factory = new VikingFactory();
            } else if(selected.Equals("Gaulois")) {
                factory = new GauloisFactory();
            }
            return factory;
        }
    }

    /// <summary>
    /// The object represented by the map selector.
    /// </summary>
    class MapCollection: ObservableCollection<string> {
        public string selected {
            get;
            set;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public MapCollection() {
            Add("Small");
            Add("Normal");
            Add("Demo");
        }

        /// <summary>
        /// Returns the game builder associated to the map selected.
        /// </summary>
        /// <returns>The game builder associated to the map selected.</returns>
        public IGameBuilder GetBuilder() {
            IGameBuilder builder = null;
            if(selected.Equals("Small")) {
                builder = new SmallGameBuilder();
            } else if(selected.Equals("Normal")) {
                builder = new NormalGameBuilder();
            } else if(selected.Equals("Demo")) {
                builder = new DemoGameBuilder();
            }
            return builder;
        }
    }
}