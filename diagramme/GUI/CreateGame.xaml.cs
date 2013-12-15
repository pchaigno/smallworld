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

namespace GUI
{
    /// <summary>
    /// Logique d'interaction pour Creator.xaml
    /// </summary>
    public partial class CreateGame : Window
    {
        PeopleCollection people1Collec;
        PeopleCollection people2Collec;
        MapCollection mapCollection;


        public CreateGame()
        {
            InitializeComponent();


            mapCollection = new MapCollection();
            mapBox.DataContext = mapCollection;

            people1Collec = new PeopleCollection();
            people1Box.DataContext = people1Collec;

            people2Collec = new PeopleCollection();
            people2Box.DataContext = people2Collec;
        }

        public void onChangePeople1(object sender, SelectionChangedEventArgs e)
        {
            string value = (String)people1Box.SelectedItem;
            people1Collec.selected = value;
            people2Collec.remove(value);
        }

        public void onChangePeople2(object sender, SelectionChangedEventArgs e)
        {
            string value = (String)people2Box.SelectedItem;
            people2Collec.selected = value;
            people1Collec.remove(value);
        }

        public void onChangeMap(object sender, SelectionChangedEventArgs e)
        {
            mapCollection.selected = (String)mapBox.SelectedItem;
        }

        public void onClickLauncher(object sender, RoutedEventArgs e)
        {
            String name1 = name1Box.Text;
            String name2 = name2Box.Text;
            IUnitFactory factory1 = people1Collec.getFactory();
            IUnitFactory factory2 = people2Collec.getFactory();
            IGameBuilder gameBuilder = mapCollection.getBuilder();

            if (name1 != "" && name2 != "" && factory1 != null && factory2 != null && gameBuilder != null)
            {
                IGame game = gameBuilder.buildGame(name1, factory1, name2, factory2);
                MapWindow window = new MapWindow(game);
                window.Show();
                this.Close();
            }
            else
            {
                string messageBoxText = "You forgot to enter all the parameters!";
                string caption = "Oups!";
                MessageBoxButton button = MessageBoxButton.YesNoCancel;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBox.Show(messageBoxText, caption, button, icon);
            }
        }
    }

    class PeopleCollection : ObservableCollection<String>
    {
        public String selected { get; set; }
        private String removed;
        public PeopleCollection()
        {
            Add("Gaulois");
            Add("Dwarves");
            Add("Vikings");
            selected = "";
            removed = "";
        }

        public void remove(String st)
        {
            Remove(st);
            if (removed != "")
            {
                Add(removed);
            }
            removed = st;
        }

        public IUnitFactory getFactory()
        {
            IUnitFactory factory = null;
            if (selected.Equals("Dwarves"))
            {
                factory = new DwarfFactory();
            }
            else if (selected.Equals("Vikings"))
            {
                factory = new VikingFactory();
            }
            else if (selected.Equals("Gaulois"))
            {
                factory = new GauloisFactory();
            }
            return factory;
        }
    }

    class MapCollection : ObservableCollection<String>
    {
        public String selected { get; set; }
        public MapCollection()
        {
            Add("Small");
            Add("Normal");
            Add("Demo");
            selected = "";
        }

        public IGameBuilder getBuilder()
        {
            IGameBuilder builder = null;
            if (selected.Equals("Small"))
            {
                builder = new SmallGameBuilder();
            }
            else if (selected.Equals("Normal"))
            {
                builder = new NormalGameBuilder();
            }
            else if (selected.Equals("Demo"))
            {
                builder = new DemoGameBuilder();
            }
            return builder;
        }
    }
}
