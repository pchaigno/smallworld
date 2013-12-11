using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using mWrapper;
using System.Collections.Generic;
using System.Diagnostics;


namespace TestWrapperMap {
    [TestClass]
    public class UnitTest1 {
        [TestMethod]
        public void TestMethodGenerate() {
            int size = 5;
            String log = "";
            int[][] map = Wrapper.generateMapList(size);
            for(int i=0; i<size; i++) {
                for(int j=0; j<size; j++) {
                    Assert.IsTrue(map[i][j]<=5 && map[i][j]>=1);
                    log += map[i][j]+" ";
                }
                log += "\n";
            }
            Debug.WriteLine(log);
            Assert.AreEqual(0,0);
        }
    }
}
