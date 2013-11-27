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
            int valeur = 5;
            List<int> numbers = Wrapper.generateMapList(valeur);
            Assert.AreEqual(valeur * valeur, numbers.Count);
        }
    }
}
