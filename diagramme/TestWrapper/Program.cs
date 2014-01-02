using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mWrapper;

namespace TestWrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = Wrapper.generateMapList(4);
            foreach (int element in numbers)
            {
                System.Console.WriteLine(element);
            }
        }
    }
}
