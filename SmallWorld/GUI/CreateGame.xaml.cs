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

/*
 * TODO correct bug empty combobox
 */

namespace GUI {

    /**
     * Logique d'interaction pour Creator.xaml
     */
    public partial class CreateGame: Window {
        PeopleCollection people1Collec;
        PeopleCollection people2Collec;
        MapCollection mapCollection;

        /**
         * Constructor
         * Intiliases the selectors.
         */
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

        /**
         * Listener for changes on people1.
         * @param sender The sender of the notification.
         * @param e The event.
         */
        public void OnChangePeople1(object sender, SelectionChangedEventArgs e) {
            string value = (String)people1Box.SelectedItem;
            people1Collec.selected = value;
            people2Collec.Remove(value);
        }

        /**
         * Listener for changes on people2.
         * @param sender The sender of the notification.
         * @param e The event.
         */
        public void OnChangePeople2(object sender, SelectionChangedEventArgs e) {
            string value = (String)people2Box.SelectedItem;
            people2Collec.selected = value;
            people1Collec.Remove(value);
        }

        /**
         * Listener for changes on map.
         * @param sender The sender of the notification.
         * @param e The event.
         */
        public void OnChangeMap(object sender, SelectionChangedEventArgs e) {
            mapCollection.selected = (String)mapBox.SelectedItem;
        }

        /**
         * Listener for clicks on launcher.
         * @param sender The sender of the notification.
         * @param e The event.
         */
        public void OnClickLauncher(object sender, RoutedEventArgs e) {
            String name1 = name1Box.Text;
            String name2 = name2Box.Text;
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

    /**
     * The object represented by the people selector.
     */
    class PeopleCollection: ObservableCollection<String> {
        public String selected {
            get;
            set;
        }
        private String removed;

        /**
         * Constructor
         */
        public PeopleCollection() {
            Add("Gaulois");
            Add("Dwarves");
            Add("Vikings");
        }

        /**
         * Remove an element from the collection.
         * @param st The element to remove.
         */
        public void Remove(String st) {
            base.Remove(st);
            if(removed != "") {
                Add(removed);
            }
            removed = st;
        }

        /**
         * @returns The unit factory associated to the people selected.
         */
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

    /**
     * The object represented by the map selector.
     */
    class MapCollection: ObservableCollection<String> {
        public String selected {
            get;
            set;
        }

        /**
         * Constructor
         */
        public MapCollection() {
            Add("Small");
            Add("Normal");
            Add("Demo");
        }

        /**
         * @returns The game builder associated to the map selected.
         */
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