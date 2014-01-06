using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SmallWorld;

namespace GUI {

    class ImageFactory {
        private BitmapImage tileDesert = null;
        private BitmapImage tileEau = null;
        private BitmapImage tileForet = null;
        private BitmapImage tileMontagne = null;
        private BitmapImage tilePlaine = null;
        private BitmapImage dwarf = null;
        private BitmapImage dwarfM = null;
        private BitmapImage gauloisM = null;
        private BitmapImage gaulois = null;
        private BitmapImage viking = null;
        private BitmapImage vikingM = null;
        private BitmapImage gauloisUnit = null;
        private BitmapImage vikingUnit = null;
        private BitmapImage dwarfUnit = null;
        private static ImageFactory INSTANCE = new ImageFactory();

        /**
         * Constructor
         * It is private because the class is a singleton.
         */
        private ImageFactory() {
            this.tileDesert = new BitmapImage(new Uri("../../Resources/Squares/desert.png", UriKind.Relative));
            this.tileEau = new BitmapImage(new Uri("../../Resources/Squares/sea.png", UriKind.Relative));
            this.tileForet = new BitmapImage(new Uri("../../Resources/Squares/forest.png", UriKind.Relative));
            this.tileMontagne = new BitmapImage(new Uri("../../Resources/Squares/mountain.png", UriKind.Relative));
            this.tilePlaine = new BitmapImage(new Uri("../../Resources/Squares/lowland.png", UriKind.Relative));

            this.dwarfM = new BitmapImage(new Uri("../../Resources/dwarfM.png", UriKind.Relative));
            this.dwarf = new BitmapImage(new Uri("../../Resources/dwarf.png", UriKind.Relative));
            this.gauloisM = new BitmapImage(new Uri("../../Resources/gauloisM.png", UriKind.Relative));
            this.gaulois = new BitmapImage(new Uri("../../Resources/gaulois.png", UriKind.Relative));
            this.vikingM = new BitmapImage(new Uri("../../Resources/vikingM.png", UriKind.Relative));
            this.viking = new BitmapImage(new Uri("../../Resources/viking.png", UriKind.Relative));

            this.dwarfUnit = new BitmapImage(new Uri("../../Resources/dwarfUnit.png", UriKind.Relative));
            this.gauloisUnit = new BitmapImage(new Uri("../../Resources/gauloisUnit.png", UriKind.Relative));
            this.vikingUnit = new BitmapImage(new Uri("../../Resources/vikingUnit.png", UriKind.Relative));
        }

        /**
         * @returns The instance of ImageFactory.
         */
        public static ImageFactory getInstance() {
            return INSTANCE;
        }

        /**
         * Gets the image corresponding to a certain type of square.
         * @param square The square to draw.
         * @returns The image corresponding to the square.
         */
        public Brush getBrushSquare(ISquare square) {
            ImageBrush brush = new ImageBrush();
            if(square is IDesert) {
                brush.ImageSource = this.tileDesert;
            } else if(square is ISea) {
                brush.ImageSource = this.tileEau;
            } else if(square is IForest) {
                brush.ImageSource = this.tileForet;
            } else if(square is IMountain) {
                brush.ImageSource = this.tileMontagne;
            } else if(square is ILowland) {
                brush.ImageSource = this.tilePlaine;
            }
            return brush;
        }

        /**
         * Gets the image corresponding to a certain type of unit.
         * This image in going to be placed on a square, on the map.
         * @param unit The unit to draw.
         * @param nb The number of units to draw (on the same square).
         * @returns The image corresponding to the unit.
         */
        public Brush getBrushUnit(IUnit unit, int nb) {
            ImageBrush brush = new ImageBrush();
            if(unit is IDwarf) {
                if(nb > 1) {
                    brush.ImageSource = this.dwarfM;
                } else {
                    brush.ImageSource = this.dwarf;
                }
            } else if(unit is IGaulois) {
                if(nb > 1) {
                    brush.ImageSource = this.gauloisM;
                } else {
                    brush.ImageSource = this.gaulois;
                }
            } else if(unit is IViking) {
                if(nb > 1) {
                    brush.ImageSource = this.vikingM;
                } else {
                    brush.ImageSource = this.viking;
                }
            }
            return brush;
        }

        /**
         * Gets the image corresponding to a certain type of unit.
         * This image is going to be place in the board area, as the unit's face.
         * @param unit The unit to draw.
         * @returns The image corresponding to the unit's face.
         */
        public Brush getBrushUnitFace(IUnit unit) {
            ImageBrush brush = new ImageBrush();
            if(unit is IDwarf) {
                brush.ImageSource = this.dwarfUnit;
            } else if(unit is IGaulois) {
                brush.ImageSource = this.gauloisUnit;
            } else if(unit is IViking) {
                brush.ImageSource = this.vikingUnit;
            }
            return brush;
        }
    }
}