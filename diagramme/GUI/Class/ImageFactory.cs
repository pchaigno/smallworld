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


        private static BitmapImage dwarf = null;
        private static BitmapImage dwarfM = null;
        private static BitmapImage gauloisM = null;
        private static BitmapImage gaulois = null;
        private static BitmapImage viking = null;
        private static BitmapImage vikingM = null;

        private static BitmapImage gauloisUnit = null;
        private static BitmapImage vikingUnit = null;
        private static BitmapImage dwarfUnit = null;



        public static Brush getBrushSquare(ISquare square)
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

        public static Brush getBrushUnit(IUnit unit, int nb)
        {
            ImageBrush brush = new ImageBrush();

            if (unit is IDwarf)
            {
                Console.WriteLine("Hello");
                if (nb > 1)
                {
                    if (dwarfM == null)
                        dwarfM = new BitmapImage(new Uri(@"..\..\Ressources\dwarfM.png", UriKind.Relative));
                    brush.ImageSource = dwarfM;
                }
                else
                {
                    if (dwarf == null)
                        dwarf = new BitmapImage(new Uri(@"..\..\Ressources\dwarf.png", UriKind.Relative));
                    brush.ImageSource = dwarf;
                }
                
            }
            else if (unit is IGaulois)
            {
                if (nb > 1)
                {
                    if (gauloisM == null)
                        gauloisM = new BitmapImage(new Uri(@"..\..\Ressources\gauloisM.png", UriKind.Relative));
                    brush.ImageSource = gauloisM;
                }
                else
                {
                    if (gaulois == null)
                        gaulois = new BitmapImage(new Uri(@"..\..\Ressources\gaulois.png", UriKind.Relative));
                    brush.ImageSource = gaulois;
                }
            }
            else if (unit is IViking)
            {
                if (nb > 1)
                {
                    if (vikingM == null)
                        vikingM = new BitmapImage(new Uri(@"..\..\Ressources\vikingM.png", UriKind.Relative));
                    brush.ImageSource = vikingM;
                }
                else
                {
                    if (viking == null)
                        viking = new BitmapImage(new Uri(@"..\..\Ressources\viking.png", UriKind.Relative));
                    brush.ImageSource = viking;
                }
            }
            return brush;
        }

        public static Brush getBrushUnitFace(IUnit unit)
        {
            ImageBrush brush = new ImageBrush();

            if (unit is IDwarf)
            {
                if (dwarfUnit == null)
                    dwarfUnit = new BitmapImage(new Uri(@"..\..\Ressources\dwarfUnit.png", UriKind.Relative));
                brush.ImageSource = dwarfUnit;
            }
            else if (unit is IGaulois)
            {
                if (gauloisUnit == null)
                    gauloisUnit = new BitmapImage(new Uri(@"..\..\Ressources\gauloisUnit.png", UriKind.Relative));
                brush.ImageSource = gauloisUnit;
            }
            else if (unit is IViking)
            {
                if (vikingUnit == null)
                    vikingUnit = new BitmapImage(new Uri(@"..\..\Ressources\vikingUnit.png", UriKind.Relative));
                brush.ImageSource = vikingUnit;
            }
            return brush;
        }


    }
}
