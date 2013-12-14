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

namespace MainWindow
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
            selected = null;
            removed = null;
        }

        public void remove(String st)
        {
            Remove(st);
            if (removed != null)
            {
                Add(removed);
            }
            removed = st;
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
            selected = null;
        }
    }
}
