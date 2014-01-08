﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SmallWorld {

    [Serializable()]
    public class Mountain: IMountain {

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public Mountain() {

        }

        /// <summary>
        /// Constructor for the deserialization.
        /// </summary>
        /// <param name="info">Information for the serialization.</param>
        /// <param name="context">The context for the serialization.</param>
        public Mountain(SerializationInfo info, StreamingContext context) {

        }

        /// <summary>
        /// Returns the number corresponding to this square for the C++ library.
        /// </summary>
        /// <returns>The number corresponding to this square for the C++ library.</returns>
        public int GetNumber() {
            return 5;
        }

        /// <summary>
        /// Method for the serialization.
        /// Fills info with the attributs' values.
        /// </summary>
        /// <param name="info">Information for the serialization.</param>
        /// <param name="context">The context for the serialization.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context) {
            info.AddValue("Number", 5);
        }
    }
}