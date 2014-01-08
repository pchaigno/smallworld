using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SmallWorld {

    [Serializable()]
    public class Sea: ISea {

        /**
         * @returns The number corresponding to this square for the C++ library.
         */
        public int getNumber() {
            return 1;
        }

        /**
         * Empty constructor.
         */
        public Sea() {

        }

        /**
         * Constructor for the deserialization.
         * @param info Information for the serialization.
         * @param context The context for the serialization.
         */
        public Sea(SerializationInfo info, StreamingContext context) {

        }
        
        /**
         * Method for the serialization.
         * Fills info with the attributs' values.
         * @param info Information for the serialization.
         * @param context The context for the serialization.
         */
        public void GetObjectData(SerializationInfo info, StreamingContext context) {
            info.AddValue("Number", 1);
        }
    }
}