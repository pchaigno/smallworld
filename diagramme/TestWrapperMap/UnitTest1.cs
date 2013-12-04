using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using mWrapper;
using System.Collections.Generic;


namespace TestWrapperMap
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethodGenerate()
        {
            int size = 5;
            int[][] map = Wrapper.generateMapList(size);
            for(int i=0; i<size; i++) {
                for(int j=0; j<size; j++) {
                    Console.WriteLine("{0} ", map[i][j]);
                }
                Console.WriteLine("\n");
            }
        }
    }
}
