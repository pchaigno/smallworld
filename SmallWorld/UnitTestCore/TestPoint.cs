﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmallWorld;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace UnitTestCore {

    [TestClass]
    public class TestPoint {

        [TestMethod]
        public void TestConstructor() {
            IPoint pt = new Point(1, 5);
            Assert.AreEqual(1, pt.X);
            Assert.AreEqual(5, pt.Y);
            pt.X = 10;
            Assert.AreEqual(10, pt.X);
            Assert.AreEqual(5, pt.Y);
        }

        [TestMethod]
        public void TestIsNext() {
            Assert.IsTrue(new Point(0, 1).IsNext(new Point(0, 0)));
            Assert.IsFalse(new Point(1, 1).IsNext(new Point(0, 0)));
            Assert.IsTrue(new Point(14, 14).IsNext(new Point(14, 13)));
            Assert.IsFalse(new Point(14, 14).IsNext(new Point(13, 13)));
        }

        [TestMethod]
        public void TestEquals() {
            Assert.IsTrue(new Point(1, 5).Equals(new Point(1, 5)));
            Assert.IsFalse(new Point(1, 5).Equals(new Point(5, 1)));
            Assert.IsFalse(new Point(0, 5).Equals(new Point(5, 0)));
            Assert.IsFalse(new Point(1, 5).Equals(new Point(1, 6)));
            Assert.IsFalse(new Point(1, 5).Equals(new Point(0, 5)));
        }

        [TestMethod]
        public void TestSerializationPoint() {
            IPoint pts = new Point(42, 43);

            // Serializes:
            Stream stream = File.Open("Point.sav", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, pts);
            stream.Close();

            // Deserializes and checks the values:
            stream = File.Open("Point.sav", FileMode.Open);
            formatter = new BinaryFormatter();
            IPoint savedPts = (IPoint)formatter.Deserialize(stream);
            stream.Close();
            Assert.AreEqual(pts.X, savedPts.X);
            Assert.AreEqual(pts.Y, savedPts.Y);
        }
    }
}