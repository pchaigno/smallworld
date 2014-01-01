using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using mWrapper;
using System.Collections.Generic;
using System.Diagnostics;
using SmallWorld;

namespace TestWrapperMap {

    [TestClass]
    public class TestMapGenerator {

        [TestMethod]
        public void TestValuesMap() {
            this.TestValuesMap(5);
            this.TestValuesMap(10);
            this.TestValuesMap(15);
        }

        private void TestValuesMap(int size) {
            int[][] map = Wrapper.generateMapList(size);
            for(int i=0; i<size; i++) {
                for(int j=0; j<size; j++) {
                    Assert.IsTrue(map[i][j] <= 5);
                    Assert.IsTrue(map[i][j] >= 1);
                }
            }
        }

        [TestMethod]
        public void TestStartsPlayer() {
            this.TestStartPlayer(5);
            this.TestStartPlayer(10);
            this.TestStartPlayer(15);

            int sea = new Sea().getNumber();
            int forest = new Forest().getNumber();
            int[][] map = new int[][] {
                new int[5] {sea, sea, sea, sea, sea},
                new int[5] {sea, sea, sea, sea, sea},
                new int[5] {sea, sea, forest, forest, sea},
                new int[5] {sea, sea, sea, forest, sea},
                new int[5] {sea, sea, sea, sea, sea}
            };
            int[][] starts = Wrapper.getStartsPlayers(map, 5);
            Assert.AreEqual(3, starts[0][0]);
            Assert.AreEqual(3, starts[0][1]);
            Assert.AreEqual(2, starts[1][0]);
            Assert.AreEqual(2, starts[1][1]);
        }

        private void TestStartPlayer(int size) {
            int[][] map = Wrapper.generateMapList(size);
            int[][] starts = Wrapper.getStartsPlayers(map, size);
            int seaNumber = new Sea().getNumber();
            Assert.AreEqual(2, starts.Length);
            Assert.AreEqual(2, starts[0].Length);
            for(int i=0; i<2; i++) {
                for(int j=0; j<2; j++) {
                    Assert.IsTrue(starts[i][j] >= 0);
                    Assert.IsTrue(starts[i][j] < size);
                }
                Assert.IsTrue(map[starts[i][0]][starts[i][1]] != seaNumber);
            }
        }
    }
}