using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using mWrapper;

namespace TestWrapperMap {

    [TestClass]
    public class TestAdviceGenerator {

        [TestMethod]
        public void TestValues() {
            for(int size=5; size<20; size+=5) {
                this.TestValues(size, 1, 2);
                this.TestValues(size, 2, 3);
                this.TestValues(size, 3, 1);
            }
        }

        private void TestValues(int size, int nationPlayer1, int nationPlayer2) {
            int[][] map = Wrapper.generateMapList(size);
            int[][] units = new int[size][];
            for(int i=0; i<size; i++) {
                units[i] = new int[size];
            }
            units[0][0] = 1;
            units[size-1][size-1] = 2;
            units[0][1] = 1;
            units[1][1] = 1;

            int[][] advices = Wrapper.getAdvice(map, size, nationPlayer1, nationPlayer2, 0, 0, units, 1);
            for(int i=0; i<3; i++) {
                Assert.IsTrue(advices[i][0] <= 1);
                Assert.IsTrue(advices[i][1] <= 1);
            }
        }
    }
}