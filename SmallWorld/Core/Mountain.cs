using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SmallWorld {

    [Serializable()]
    public class Mountain: IMountain {

        /**
         * @returns The number corresponding to this square for the C++ library.
         */
        public int getNumber() {
            return 5;
        }

        /**
         * Empty constructor.
         */
        public Mountain() {

        }

        /**
         * Constructor for the deserialization.
         * @param info Information for the serialization.
         * @param context The context for the serialization.
         */
        public Mountain(SerializationInfo info, StreamingContext context) {

        }
        
        /**
         * Method for the serialization.
         * Fills info with the attributs' values.
         * @param info Information for the serialization.
         * @param context The context for the serialization.
         */
        public void GetObjectData(SerializationInfo info, StreamingContext context) {
            info.AddValue("Number", 5);
        }
    }
}