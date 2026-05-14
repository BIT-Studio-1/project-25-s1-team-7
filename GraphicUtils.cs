using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class GraphicUtils
    {
        public static void HelloWorld()
        {
            Console.WriteLine("Hello World!");
        }

        public static void RenderFrame(string filePath)
        {
            using StreamReader reader = new(filePath);
            string fileContent = reader.ReadToEnd();

            Console.WriteLine(fileContent);
        }
    }
}
