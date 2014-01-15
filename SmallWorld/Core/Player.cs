using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SmallWorld {

    [Serializable()]
    public class Player: IPlayer, ISerializable {
        private IUnitFactory factory;
        private string name;
        private int points;
        private int number;
        private static int count = 0;
        public string Name {
            get {
                return this.name;
            }
        }
        public int Points {
            get {
                return this.points;
            }
        }
        public int Number {
            get {
                return this.number;
            }
        }
        public int NationNumber {
            get {
                return this.factory.Number;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">The name of the player.</param>
        /// <param name="factory">The factory for this player (represent the nation).</param>
        public Player(string name, IUnitFactory factory) {
            this.name = name;
            this.factory = factory;
            count++;
            this.number = count;
        }

        /// <summary>
        /// Constructor for the deserialization.
        /// </summary>
        /// <param name="info">Information for the serialization.</param>
        /// <param name="context">The context for the serialization.</param>
        public Player(SerializationInfo info, StreamingContext context) {
            this.name = (string)info.GetValue("Name", typeof(string));
            this.points = (int)info.GetValue("Points", typeof(int));
            IUnitFactory vikingFactory = new VikingFactory();
            IUnitFactory gauloisFactory = new GauloisFactory();
            IUnitFactory dwarfFactory = new DwarfFactory();
            // Deserialises the factory by its unique number:
            int factoryNumber = (int)info.GetValue("Factory", typeof(int));
            if(factoryNumber == vikingFactory.Number) {
                this.factory = vikingFactory;
            } else if(factoryNumber == gauloisFactory.Number) {
                this.factory = gauloisFactory;
            } else if(factoryNumber == dwarfFactory.Number) {
                this.factory = dwarfFactory;
            } else {
                throw new IncorrectFactoryNumberException(factoryNumber);
            }
            this.number = (int)info.GetValue("Number", typeof(int));
            count++;
        }

        /// <summary>
        /// Method for the serialization.
        /// Fills info with the attributs' values.
        /// </summary>
        /// <param name="info">Information for the serialization.</param>
        /// <param name="context">The context for the serialization.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context) {
            info.AddValue("Name", this.name);
            info.AddValue("Points", this.points);
            info.AddValue("Number", this.number);
            info.AddValue("Factory", this.factory.Number);
        }

        /// <summary>
        /// Creates some units using the factory of the player.
        /// </summary>
        /// <param name="nbUnits">The number of units to create.</param>
        /// <returns>The list of units created.</returns>
        public List<IUnit> CreateUnits(int nbUnits) {
            List<IUnit> units = new List<IUnit>();
            for(int i=0; i<nbUnits; i++) {
                units.Add(factory.CreateUnit(this));
            }
            return units;
        }

        /// <summary>
        /// Adds some points to the player.
        /// </summary>
        /// <param name="n">The number of points to add.</param>
        /// <exception cref="ArgumentOutOfRangeException">If n is negative.</exception>
        public void AddPoints(int n) {
            if(n < 0) {
                throw new ArgumentOutOfRangeException("Only a positive number of points can be added to a player.");
            }
            this.points += n;
        }

        public override bool Equals(Object obj) {
            if(obj == null) {
                return false;
            }
            if(!(obj is Player)) {
                return false;
            }
            Player player = (Player)obj;
            return this.number == player.number;
        }

        public override int GetHashCode() {
            return this.number.GetHashCode();
        }
    }
}