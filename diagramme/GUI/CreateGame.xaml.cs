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

        List<String> mapSize;
        People people1Collec;
        People people2Collec;

        public CreateGame()
        {
            InitializeComponent();
            mapSize = new List<string>();
            mapSize.Add("Small");
            mapSize.Add("Normal");
            mapSize.Add("Demo");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (mapSize == null)
            {

            }

            foreach (string st in mapSize)
            {
                ComboBoxItem cboxitem = new ComboBoxItem();
                cboxitem.Content = st;
                cbox.Items.Add(cboxitem);
            }

            people1Collec = new People();
            people1.DataContext = people1Collec;

            people2Collec = new People();
            people2.DataContext = people2Collec;

        }

        public void onChangePeople1(object sender, SelectionChangedEventArgs e)
        {
            string value = (String)people1.SelectedItem;
            people1Collec.setSelected(value);
            people2Collec.remove(value);
        }

        public void onChangePeople2(object sender, SelectionChangedEventArgs e)
        {
            string value = (String)people2.SelectedItem;
            people2Collec.setSelected(value);
            people1Collec.remove(value);
        }

    }

    class People : ObservableCollection<String>
    {
        private String selected;
        private String removed;
        public People()
        {
            Add("Gaulois");
            Add("Dwarves");
            Add("Vikings");
            selected = null;
            removed = null;
        }


        public void setSelected(String st) 
        {
            this.selected = st;
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
}
