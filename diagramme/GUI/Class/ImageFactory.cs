using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SmallWorld;

namespace GUI
{
    class ImageFactory
    {
        private static BitmapImage tileDesert = null;
        private static BitmapImage tileEau = null;
        private static BitmapImage tileForet = null;
        private static BitmapImage tileMontagne = null;
        private static BitmapImage tilePlaine = null;



        public static Brush getBrush(ISquare square)
        {
            ImageBrush brush = new ImageBrush();

            if (square is IDesert)
            {
                if (tileDesert == null)
                    tileDesert = new BitmapImage(new Uri(@"..\..\Ressources\terrains\desert.png", UriKind.Relative));
                brush.ImageSource = tileDesert;
            }
            else if (square is ISea)
            {
                if (tileEau == null)
                    tileEau = new BitmapImage(new Uri(@"..\..\Ressources\terrains\sea.png", UriKind.Relative));
                brush.ImageSource = tileEau;
            }
            else if (square is IForest)
            {
                if (tileForet == null)
                    tileForet = new BitmapImage(new Uri(@"..\..\Ressources\terrains\forest.png", UriKind.Relative));
                brush.ImageSource = tileForet;
            }
            else if (square is IMountain)
            {
                if (tileMontagne == null)
                    tileMontagne = new BitmapImage(new Uri(@"..\..\Ressources\terrains\mountain.png", UriKind.Relative));
                brush.ImageSource = tileMontagne;
            }
            else if (square is ILowland)
            {
                if (tilePlaine == null)
                    tilePlaine = new BitmapImage(new Uri(@"..\..\Ressources\terrains\lowland.png", UriKind.Relative));
                brush.ImageSource = tilePlaine;
            }
            return brush;
        }
    }
}
